// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Models
{
    using System;
    using System.ComponentModel;
    using Skyline.DataMiner.SDM.Abstractions.Attributes;

    [DataMinerObject]
    public class Ticket
    {
        public Guid Guid { get; internal set; }

        public string ID { get; set; }

       

        [DataMinerLink(typeof(TicketType))]
        public Guid Type { get; set; }

        public TicketStatus Status { get; set; } = TicketStatus.Acknowledged;

        public TicketPriority Priority { get; set; } = TicketPriority.Low;

        public TicketSeverity Severity { get; set; } = TicketSeverity.Minor;

        public DateTime RequestedResolutionDate { get; set; }

        public DateTime ExpectedResolutionDate { get; set; }

        public DateTime LastModified { get; set; }

        public string LastModifiedBy { get; set; }

        public DateTime CreatedAt { get; set; }

        public string CreatedBy { get; set; }

        [Mandatory]
        public string Name { get; set; }

        [Mandatory((int)TicketState.inProgress)]
        public string Description { get; set; }

        public TicketState State { get;  set; }
       
    }

    public enum TicketState
    {
        [Description("Acknowledged")]
        acknowledged,
        [Description("In Progress")]
        inProgress,
        [Description("Rejected")]
        rejected,
        [Description("Resolved")]
        resolved,
        [Description("Cancelled")]
        cancelled,
        [Description("Closed")]
        closed,
        [Description("Pending")]
        pending,
        [Description("Held")]
        held
    }
}
