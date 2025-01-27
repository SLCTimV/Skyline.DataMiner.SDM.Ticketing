// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Unit_Tests.Storage
{
    using System;
    using System.Linq;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Exposers;
    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Skyline.DataMiner.SDM.Ticketing.Unit_Tests;

    using SLDataGateway.API.Querying;

    [TestClass]
    public class DomStorageProviderTests
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
            var filter = new ANDFilterElement<Ticket>(severityFilter, priorityFilter);
            var tickets = storageModel.Read(filter).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(18);
        }

        [TestMethod]
        public void ReadTest_OrderBy()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketDomStorageProvider(connection.Object);

            // Act
            var tickets = storageModel.Read(new TRUEFilterElement<Ticket>().OrderBy(TicketExposers.CreatedAt)).ToList();

            tickets.ForEach(x => Console.WriteLine(x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(32);
        }

        [TestMethod]
        public void ReadTest_OrderBy_2()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketDomStorageProvider(connection.Object);

            // Act
            var tickets = storageModel.Read(
                new TRUEFilterElement<Ticket>()
                .OrderBy(TicketExposers.Priority)
                .ThenByDescending(TicketExposers.CreatedAt))
                .ToList();

            tickets.ForEach(x => Console.WriteLine($"{x.Priority}: {x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")}"));

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(32);
        }

        [TestMethod]
        public void ReadTest_OrderBy_WithFilters()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketDomStorageProvider(connection.Object);

            // Act
            var severityFilter = new ORFilterElement<Ticket>(
                TicketExposers.Severity.UncheckedEqual(TicketSeverity.Minor),
                TicketExposers.Severity.UncheckedEqual(TicketSeverity.Major));
            var priorityFilter = TicketExposers.Priority.UncheckedEqual(TicketPriority.Medium);
            var filter = new ANDFilterElement<Ticket>(severityFilter, priorityFilter);
            var tickets = storageModel.Read(filter.OrderBy(TicketExposers.CreatedAt)).ToList();

            tickets.ForEach(x => Console.WriteLine(x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(18);
        }

        [TestMethod]
        public void ReadTest_All()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketDomStorageProvider(connection.Object);

            // Act
            var tickets = storageModel.Read(new TRUEFilterElement<Ticket>()).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCountGreaterThan(0);
        }

        [TestMethod]
        public void ReadTest_DomInstanceID_Exists()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketApiHelper(connection.Object);
            var guid = new Guid("bc8ce57c-c662-4abe-b30e-30f7c3d2c1d7");

            // Act
            var tickets = storageModel.Read(TicketExposers.Guid.Equal(guid)).ToList();

            var myTicket = new Ticket
            {
                ID = "Ticket 1",
                Description = "My Awsome Ticket",
            };

            storageModel.Create(myTicket);

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(1);
        }

        [TestMethod]
        public void ReadTest_DomInstanceID_NotExists()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketApiHelper(connection.Object);
            var guid = new Guid("bc85e57c-c662-4abe-b30e-30f7c3d2c1d7");

            // Act
            var tickets = storageModel.Read(TicketExposers.Guid.Equal(guid)).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(0);
        }

        [TestMethod]
        public void ReadTest_TicketType_Name()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var ticketModel = new TicketApiHelper(connection.Object);
            var ticketTypeModel = new TicketTypeDomStorageProvider(connection.Object);
            var ticketTypeName = "Service Order";

            // Act
            var type = ticketTypeModel.Read(TicketTypeExposers.Name.UncheckedEqual(ticketTypeName)).First();
            var tickets = ticketModel.Read(TicketExposers.Type.UncheckedEqual(type.Guid)).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(1);
        }

        [TestMethod]
        public void ReadTest_Limit_5()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var ticketProvider = new TicketApiHelper(connection.Object);
            var timeFilter = DateTime.ParseExact("2024-12-20 10:50:11.499", "yyyy-MM-dd HH:mm:ss.fff", System.Globalization.CultureInfo.InvariantCulture);

            // Act
            var filter = TicketExposers.Priority.UncheckedEqual(TicketPriority.Low).Limit(5);
            var tickets = ticketProvider.Read(filter).ToList();

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(5);
        }

        [TestMethod]
        public void ReadTest_Latest_5()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var ticketProvider = new TicketApiHelper(connection.Object);

            // Act
            var tickets = ticketProvider.Read(
                new TRUEFilterElement<Ticket>()
                .OrderByDescending(TicketExposers.CreatedAt)
                .Limit(5))
                .ToList();

            tickets.ForEach(x => Console.WriteLine(x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(5);
        }

        [TestMethod]
        public void ReadTest_Earliest_5()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var ticketProvider = new TicketApiHelper(connection.Object);

            // Act
            var tickets = ticketProvider.Read(
                new TRUEFilterElement<Ticket>()
                .OrderBy(TicketExposers.CreatedAt)
                .Limit(5))
                .ToList();

            tickets.ForEach(x => Console.WriteLine(x.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss.fff")));

            // Assert
            tickets.Should().NotBeNull();
            tickets.Should().HaveCount(5);
        }

        [TestMethod]
        public void UpdateTest_Priority()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var storageModel = new TicketApiHelper(connection.Object);
            var guid = new Guid("bc8ce57c-c662-4abe-b30e-30f7c3d2c1d7");
            var description = "My Awesome new description.";

            // Act
            var ticket = storageModel.Read(TicketExposers.Guid.UncheckedEqual(guid)).FirstOrDefault();
            ticket.Description = description;
            storageModel.Update(ticket);

            var updatedTicket = storageModel.Read(TicketExposers.Guid.UncheckedEqual(guid)).FirstOrDefault();

            // Assert
            updatedTicket.Should().NotBeNull();
            updatedTicket.Description.Should().BeEquivalentTo(description);
        }
    }
}