namespace Skyline.DataMiner.Automation
{
    using System;
    using System.Linq;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    public class ScriptContext
    {
        public ScriptContext(IEngine engine)
        {
            Engine = engine;

            TicketGuid = GetScriptParam("Ticket Guid").SingleOrDefault();
        }

        public IEngine Engine { get; }

        public string TicketGuid { get; }

        private string[] GetScriptParam(string name)
        {
            var rawValue = Engine.GetScriptParam(name).Value;
            if (String.IsNullOrEmpty(rawValue))
            {
                return new string[0];
            }

            if (IsJsonArray(rawValue))
            {
                return JsonConvert.DeserializeObject<string[]>(rawValue);
            }
            else
            {
                return new[] { rawValue };
            }
        }

        private static bool IsJsonArray(string json)
        {
            try
            {
                JArray.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
