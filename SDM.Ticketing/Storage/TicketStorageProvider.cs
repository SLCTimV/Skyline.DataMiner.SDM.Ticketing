namespace Skyline.DataMiner.SDM.Ticketing.Models
{
    using System;
    using System.Collections.Generic;

    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Skyline.DataMiner.SDM.Ticketing.Storage;

    public class TicketStorageProvider : IStorageProvider<Ticket>
    {
        private readonly IConnection connection;
        private readonly IStorageProvider<Ticket> provider;

        public TicketStorageProvider(IConnection connection)
        {
            this.connection = connection;
            this.provider = new TicketDomStorageProvider(connection);
        }

        public Ticket Create(Ticket createObject)
        {
            return provider.Create(createObject);
        }

        public IEnumerable<Ticket> Read(FilterElement<Ticket> filter)
        {
            return provider.Read(filter);
        }

        public Ticket Update(Ticket updateObject)
        {
            return provider.Update(updateObject);
        }

        public Ticket Delete(Ticket deleteObject)
        {
            return provider.Delete(deleteObject);
        }
    }
}