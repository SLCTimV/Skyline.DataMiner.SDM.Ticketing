namespace Skyline.DataMiner.SDM.Ticketing.Storage
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.Net;

    using SLDataGateway.API.Types.Querying;
    using Skyline.DataMiner.SDM.Ticketing.Models;
    using Newtonsoft.Json;

    public class TicketFileStorageProvider : IStorageProvider<Ticket>
    {
        private readonly IConnection connection;
        private readonly string folderLocation;

        public TicketFileStorageProvider(IConnection connection) : this(connection, @"C:\Skyline_Data\SDM")
        {
        }

        public TicketFileStorageProvider(IConnection connection, string folderLocation)
        {
            this.connection = connection;
            this.folderLocation = folderLocation;

            if (!Directory.Exists(folderLocation))
            {
                Directory.CreateDirectory(folderLocation);
            }
        }

        public Ticket Create(Ticket createObject)
        {
            createObject.Guid = Guid.NewGuid();
            File.WriteAllText($"{Path.Combine(folderLocation, Convert.ToString(createObject.Guid))}.json", JsonConvert.SerializeObject(createObject));
            return createObject;
        }

        public IEnumerable<Ticket> Read(FilterElement<Ticket> filter)
        {
            if (filter is null)
            {
                yield break;
            }

            var selector = filter.getLambda();
            foreach (var filePath in Directory.GetFiles(folderLocation, "*.json"))
            {
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                using (StreamReader reader = new StreamReader(fs))
                {
                    string content = reader.ReadToEnd();
                    var workflow = JsonConvert.DeserializeObject<Ticket>(content);
                    workflow.Guid = Guid.Parse(Path.GetFileNameWithoutExtension(filePath));
                    if (selector.Invoke(workflow))
                    {
                        yield return workflow;
                    }
                }
            }
        }

        public IEnumerable<Ticket> Read(IQuery<Ticket> query)
        {
            var items = new List<Ticket>();
            foreach (var model in Read(query.Filter))
            {
                items.Add(model);
                if (items.Count >= query.Limit.Limit)
                {
                    break;
                }
            }

            return query.Order.ExecuteInMemory(items);
        }

        public Ticket Update(Ticket updateObject)
        {
            File.WriteAllText($"{folderLocation}\\{Convert.ToString(updateObject.Guid)}.json", JsonConvert.SerializeObject(updateObject));
            return updateObject;
        }

        public Ticket Delete(Ticket deleteObject)
        {
            File.Delete($"{Path.Combine(folderLocation, Convert.ToString(deleteObject.Guid))}.json");
            return deleteObject;
        }
    }
}
