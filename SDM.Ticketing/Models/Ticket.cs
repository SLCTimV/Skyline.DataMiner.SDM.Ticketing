// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Models
{
    using System;

    using Skyline.DataMiner.Net.Jobs;

    [DataMinerObject]
    public class Ticket
    {
        public Guid Guid { get; internal set; }

        public string ID { get; set; }

        public string Description { get; set; }

        public TicketType Type { get; set; }

        public TicketPriority Priority { get; set; } = TicketPriority.Low;

        public TicketSeverity Severity { get; set; } = TicketSeverity.Minor;

        public DateTime RequestedResolutionDate { get; set; }

        public DateTime ExpectdeResolutionDate { get; set; }
    }
}
