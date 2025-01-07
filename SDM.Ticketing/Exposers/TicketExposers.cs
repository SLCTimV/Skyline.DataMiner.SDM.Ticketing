// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Exposers
{
    using System;
    using System.Collections.Generic;

    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Models;

    public static class TicketExposers
    {
        public static readonly Exposer<Ticket, Guid> Guid                           = new Exposer<Ticket, Guid>((ticket) => ticket.Guid, nameof(Ticket.Guid));
        public static readonly Exposer<Ticket, string> ID                           = new Exposer<Ticket, string>((ticket) => ticket.ID, nameof(Ticket.ID));
        public static readonly Exposer<Ticket, string> Description                  = new Exposer<Ticket, string>((ticket) => ticket.Description, nameof(Ticket.Description));
        public static readonly Exposer<Ticket, TicketType> Type                     = new Exposer<Ticket, TicketType>((ticket) => ticket.Type, nameof(Ticket.Type));
        public static readonly Exposer<Ticket, TicketPriority> Priority             = new Exposer<Ticket, TicketPriority>((ticket) => ticket.Priority, nameof(Ticket.Priority));
        public static readonly Exposer<Ticket, TicketSeverity> Severity             = new Exposer<Ticket, TicketSeverity>((ticket) => ticket.Severity, nameof(Ticket.Severity));
        public static readonly Exposer<Ticket, DateTime> RequestedResolutionDate    = new Exposer<Ticket, DateTime>((ticket) => ticket.RequestedResolutionDate, nameof(Ticket.RequestedResolutionDate));
        public static readonly Exposer<Ticket, DateTime> ExpectdeResolutionDate     = new Exposer<Ticket, DateTime>((ticket) => ticket.ExpectdeResolutionDate, nameof(Ticket.ExpectdeResolutionDate));

        public static readonly IReadOnlyList<FieldExposer> FieldExposers = new List<FieldExposer>
        {
            Guid,
            ID,
            Description,
            Type,
            Priority,
            Severity,
            RequestedResolutionDate,
            ExpectdeResolutionDate,
        };
    }
}
