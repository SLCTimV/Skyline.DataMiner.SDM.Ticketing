// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Unit_Tests.Storage
{
    using System;
    using System.Linq;

    using DomHelpers.SlcTicketing;

    using FluentAssertions;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.Net.Sections;
    using Skyline.DataMiner.SDM.Ticketing.Exposers;
    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Skyline.DataMiner.SDM.Ticketing.Unit_Tests;

    [TestClass]
    public class DomEquatableTests
    {
        [TestMethod]
        public void Equate_Raw_DomInstances()
        {
            // Arrange
            var connection = new ConnectionMock("Files/module.json");
            var helper = new DomHelper(connection.Object.HandleMessages, SlcTicketingIds.ModuleId);

            // Act
            var ticket = helper.DomInstances.Read(DomInstanceExposers.DomDefinitionId.Equal(SlcTicketingIds.Definitions.Ticket.Id)).First();
            var updatedTicket = helper.DomInstances.Read(DomInstanceExposers.DomDefinitionId.Equal(SlcTicketingIds.Definitions.Ticket.Id)).First();

            Console.WriteLine(ticket.GetFieldValue<string>(SlcTicketingIds.Sections.TicketGeneral.Id, SlcTicketingIds.Sections.TicketGeneral.TicketDescription));
            Console.WriteLine(updatedTicket.GetFieldValue<string>(SlcTicketingIds.Sections.TicketGeneral.Id, SlcTicketingIds.Sections.TicketGeneral.TicketDescription));

            updatedTicket.AddOrUpdateFieldValue<string>(SlcTicketingIds.Sections.TicketGeneral.Id, SlcTicketingIds.Sections.TicketGeneral.TicketDescription, "My Awesome Ticket Description");
            updatedTicket = helper.DomInstances.Update(updatedTicket);

            Console.WriteLine(ticket.GetFieldValue<string>(SlcTicketingIds.Sections.TicketGeneral.Id, SlcTicketingIds.Sections.TicketGeneral.TicketDescription));
            Console.WriteLine(updatedTicket.GetFieldValue<string>(SlcTicketingIds.Sections.TicketGeneral.Id, SlcTicketingIds.Sections.TicketGeneral.TicketDescription));

            // Assert
            ticket.Should().BeEquivalentTo(updatedTicket);
        }
    }
}
