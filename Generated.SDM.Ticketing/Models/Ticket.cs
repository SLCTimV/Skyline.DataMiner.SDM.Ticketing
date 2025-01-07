// Ignore Spelling: SDM

namespace Generated.SDM.Ticketing.Models
{
    using System;
    using System.Net.Sockets;

    using Skyline.DataMiner.SDM;

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

        public DateTime ExpectedResolutionDate { get; set; }
    }
}
