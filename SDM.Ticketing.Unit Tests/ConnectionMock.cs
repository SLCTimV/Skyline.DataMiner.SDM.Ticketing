// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Unit_Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using Moq;

    using Skyline.DataMiner.Net;
    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
    using Skyline.DataMiner.Net.Messages;
    using Skyline.DataMiner.SDM.Ticketing.Unit_Tests.DOM;
    using Skyline.DataMiner.Utils.DOM.UnitTesting;

    internal class ConnectionMock : Mock<IConnection>
    {
        private readonly DomSLNetMessageHandler messageHandler;

        private readonly List<DomInstance> instances = new List<DomInstance>();

        public ConnectionMock(string modulePath)
        {
            messageHandler = new DomSLNetMessageHandler();
            var serializer = new DomSerializer();
            var modules = serializer.Deserialize(modulePath).ToList();

            instances = modules.SelectMany((module) => module.Instances).ToList();

            messageHandler.SetSectionDefinitions(modules.SelectMany((module) => module.Sections));
            messageHandler.SetBehaviorDefinitions(modules.SelectMany((module) => module.Behaviors));
            messageHandler.SetDefinitions(modules.SelectMany((module) => module.Definitions));
            messageHandler.SetInstances(modules.SelectMany((module) => module.Instances));

            Setup();
        }

        private void Setup()
        {
            this.Setup(mock => mock.HandleMessages(It.IsAny<DMSMessage[]>())).Returns<DMSMessage[]>((messages) =>
            {
                //var temp = ((ManagerStoreReadRequest<DomInstance>)messages[0]).Query.ExecuteInMemory(instances).ToList();
                return messageHandler.HandleMessages(messages);
            });
        }
    }
}
