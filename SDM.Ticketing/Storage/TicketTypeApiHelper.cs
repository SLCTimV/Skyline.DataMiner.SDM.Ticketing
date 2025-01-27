namespace Skyline.DataMiner.SDM.Ticketing.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Skyline.DataMiner.SDM.Ticketing.Storage;

    using SLDataGateway.API.Types.Querying;

    public class TicketTypeApiHelper : IStorageProvider<TicketType>
    {
        private readonly IConnection connection;
        private readonly IStorageProvider<TicketType> provider;

        public TicketTypeApiHelper(IConnection connection)
        {
            this.connection = connection;
            this.provider = new TicketTypeDomStorageProvider(connection);
        }

        public TicketType Create(TicketType createObject)
        {
            return provider.Create(createObject);
        }

        public IEnumerable<TicketType> Read(FilterElement<TicketType> filter)
        {
            return provider.Read(filter);
        }

        public IEnumerable<TicketType> Read(IQuery<TicketType> filter)
        {
            return Enumerable.Empty<TicketType>();
        }

        public TicketType Update(TicketType updateObject)
        {
            return provider.Update(updateObject);
        }

        public TicketType Delete(TicketType deleteObject)
        {
            return provider.Delete(deleteObject);
        }
    }
}