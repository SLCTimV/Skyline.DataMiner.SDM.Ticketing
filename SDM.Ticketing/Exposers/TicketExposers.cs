// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Exposers
{
    using System;
    using System.Collections.Generic;

    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Models;

    public static class TicketTypeExposers
    {
        public static readonly Exposer<TicketType, Guid> Guid = new Exposer<TicketType, Guid>((ticket) => ticket.Guid, nameof(TicketType.Guid));
        public static readonly Exposer<TicketType, string> ID = new Exposer<TicketType, string>((ticket) => ticket.ID, nameof(TicketType.ID));
        public static readonly Exposer<TicketType, string> Name = new Exposer<TicketType, string>((ticket) => ticket.Name, nameof(TicketType.Name));
        public static readonly IReadOnlyList<FieldExposer> FieldExposers = new List<FieldExposer> { Guid, ID, Name };
    }

    public static class TicketExposers
    {
        public static readonly Exposer<Ticket, Guid> Guid = new Exposer<Ticket, Guid>((ticket) => ticket.Guid, nameof(Ticket.Guid));
        public static readonly Exposer<Ticket, string> ID = new Exposer<Ticket, string>((ticket) => ticket.ID, nameof(Ticket.ID));
        public static readonly Exposer<Ticket, string> Description = new Exposer<Ticket, string>((ticket) => ticket.Description, nameof(Ticket.Description));
        public static readonly Exposer<Ticket, Guid> Type = new Exposer<Ticket, Guid>((ticket) => ticket.Type, nameof(Ticket.Type));
        public static readonly Exposer<Ticket, TicketPriority> Priority = new Exposer<Ticket, TicketPriority>((ticket) => ticket.Priority, nameof(Ticket.Priority));
        public static readonly Exposer<Ticket, TicketSeverity> Severity = new Exposer<Ticket, TicketSeverity>((ticket) => ticket.Severity, nameof(Ticket.Severity));
        public static readonly Exposer<Ticket, DateTime> RequestedResolutionDate = new Exposer<Ticket, DateTime>((ticket) => ticket.RequestedResolutionDate, nameof(Ticket.RequestedResolutionDate));
        public static readonly Exposer<Ticket, DateTime> ExpectedResolutionDate = new Exposer<Ticket, DateTime>((ticket) => ticket.ExpectedResolutionDate, nameof(Ticket.ExpectedResolutionDate));
        public static readonly Exposer<Ticket, DateTime> LastModified = new Exposer<Ticket, DateTime>((ticket) => ticket.LastModified, nameof(Ticket.LastModified));
        public static readonly Exposer<Ticket, string> LastModifiedBy = new Exposer<Ticket, string>((ticket) => ticket.LastModifiedBy, nameof(Ticket.LastModifiedBy));
        public static readonly Exposer<Ticket, DateTime> CreatedAt = new Exposer<Ticket, DateTime>((ticket) => ticket.CreatedAt, nameof(Ticket.CreatedAt));
        public static readonly Exposer<Ticket, string> CreatedBy = new Exposer<Ticket, string>((ticket) => ticket.CreatedBy, nameof(Ticket.CreatedBy));
        public static readonly IReadOnlyList<FieldExposer> FieldExposers = new List<FieldExposer>
        {
            Guid,
            ID,
            Description,
            Type,
            Priority,
            Severity,
            RequestedResolutionDate,
            ExpectedResolutionDate,
            LastModified,
            LastModifiedBy,
            CreatedAt,
            CreatedBy
        };
    }
}
