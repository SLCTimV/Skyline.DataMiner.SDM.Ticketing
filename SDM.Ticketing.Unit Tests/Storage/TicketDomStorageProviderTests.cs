// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Unit_Tests.Storage
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Exposers;
    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Skyline.DataMiner.SDM.Ticketing.Unit_Tests;

    [TestClass]
    public class TicketDomStorageProviderTests
    {
        [TestMethod]
        public void ReadTest()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketDomStorageProvider(connection.Object);

            // Act
            var severityFilter = new ORFilterElement<Ticket>(
                TicketExposers.Severity.UncheckedEqual(TicketSeverity.Minor),
                TicketExposers.Severity.UncheckedEqual(TicketSeverity.Major));
            var priorityFilter = TicketExposers.Priority.UncheckedEqual(TicketPriority.Medium);
            var tickets = storageModel.Read(new ANDFilterElement<Ticket>(severityFilter, priorityFilter)).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public void ReadTest_DomInstanceID_Exists()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketDomStorageProvider(connection.Object);
            var guid = new Guid("bc8ce57c-c662-4abe-b30e-30f7c3d2c1d7");

            // Act
            var temp = storageModel.Read(null).Select(x => x.Guid).ToList();
            var tickets = storageModel.Read(TicketExposers.Guid.Equal(guid)).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(1);
        }

        [TestMethod]
        public void ReadTest_DomInstanceID_NotExists()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketDomStorageProvider(connection.Object);
            var guid = new Guid("bc85e57c-c662-4abe-b30e-30f7c3d2c1d7");

            // Act
            var tickets = storageModel.Read(TicketExposers.Guid.Equal(guid)).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(0);
        }
    }
}