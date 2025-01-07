// Ignore Spelling: SDM Json Seriliazer

namespace Skyline.DataMiner.SDM.Ticketing.Unit_Tests.DOM
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Compression;
    using System.Linq;
    using System.Text;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
    using Skyline.DataMiner.Net.Apps.Modules;
    using Skyline.DataMiner.Net.ManagerStore;
    using Skyline.DataMiner.Net.Messages;
    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.Net.Sections;

    public class DomSerializer
    {
        private static readonly JsonSerializer JsonSerializer = JsonSerializer.Create(new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Auto,
            ContractResolver = new DefaultContractResolver { IgnoreSerializableInterface = true }
        });

        private readonly ModuleSettingsHelper moduleSettingsHelper;
        private readonly Func<DMSMessage[], DMSMessage[]> sendSLNetMessages;
        private DomHelper domHelper;
        private JsonTextReader jsonTextReader;

        public DomSerializer()
        {
        }

        public List<DomModule> Deserialize(string path)
        {
            var modules = new List<DomModule>();

            try
            {
                using (var reader = new Reader(path))
                {
                    jsonTextReader = reader.JsonTextReader;
                    jsonTextReader.Read(); // start array
                    while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
                    {
                        modules.Add(ReadModule());
                        jsonTextReader.Read(); // end object
                    }
                }
            }
            catch (IOException e)
            {
                throw;
            }
            catch (JsonException e)
            {
                throw;
            }

            return modules;
        }

        private IEnumerable<CustomSectionDefinition> ReadSectionDefinitions()
        {
            jsonTextReader.Read();
            jsonTextReader.Read();
            while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
            {
                yield return JsonSerializer.Deserialize<CustomSectionDefinition>(jsonTextReader);
            }
        }

        private IEnumerable<DomBehaviorDefinition> ReadDomBehaviorDefinitions()
        {
            jsonTextReader.Read();
            jsonTextReader.Read();
            while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
            {
                yield return JsonSerializer.Deserialize<DomBehaviorDefinition>(jsonTextReader);
            }
        }

        private IEnumerable<DomDefinition> ReadDomDefinitions()
        {
            jsonTextReader.Read();
            jsonTextReader.Read();
            while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
            {
                yield return JsonSerializer.Deserialize<DomDefinition>(jsonTextReader);
            }
        }

        private IEnumerable<DomTemplate> ReadDomTemplates()
        {
            jsonTextReader.Read();
            jsonTextReader.Read();
            while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
            {
                yield return JsonSerializer.Deserialize<DomTemplate>(jsonTextReader);
            }
        }

        private IEnumerable<DomInstance> ReadDomInstances()
        {
            jsonTextReader.Read();
            jsonTextReader.Read();
            while (jsonTextReader.Read() && jsonTextReader.TokenType == JsonToken.StartObject)
            {
                yield return JsonSerializer.Deserialize<DomInstance>(jsonTextReader);
            }
        }

        private DomModule ReadModule()
        {
            jsonTextReader.Read(); // property name
            jsonTextReader.Read(); // start object
            var moduleSettings = JsonSerializer.Deserialize<ModuleSettings>(jsonTextReader);
            var sections = ReadSectionDefinitions();
            var behaviors = ReadDomBehaviorDefinitions();
            var definitions = ReadDomDefinitions();
            var templates = ReadDomTemplates();
            var instances = ReadDomInstances();

            return new DomModule(
                moduleSettings,
                sections,
                behaviors,
                definitions,
                templates,
                instances);
        }

        private sealed class Reader : IDisposable
        {
            private readonly FileStream fileStream;
            private readonly StreamReader streamReader;

            public Reader(string path)
            {
                try
                {
                    fileStream = new FileStream(path, FileMode.Open);
                    streamReader = new StreamReader(fileStream, Encoding.UTF8);
                    JsonTextReader = new JsonTextReader(streamReader);
                    JsonTextReader.SupportMultipleContent = true;
                }
                catch
                {
                    Dispose();
                    throw;
                }
            }

            public JsonTextReader JsonTextReader { get; }

            public void Dispose()
            {
                ((IDisposable)JsonTextReader)?.Dispose();
                streamReader?.Dispose();
                fileStream?.Dispose();
            }
        }
    }
}
