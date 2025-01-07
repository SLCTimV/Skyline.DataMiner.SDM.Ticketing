namespace Skyline.DataMiner.SDM.Ticketing.Unit_Tests.DOM
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Skyline.DataMiner.Net.Apps.DataMinerObjectModel;
    using Skyline.DataMiner.Net.Apps.Modules;
    using Skyline.DataMiner.Net.Sections;

    public class DomModule
    {
        private readonly ModuleSettings settings;
        private readonly List<CustomSectionDefinition> domSections = new List<CustomSectionDefinition>();
        private readonly List<DomBehaviorDefinition> domBehaviors = new List<DomBehaviorDefinition>();
        private readonly List<DomDefinition> domDefinitions = new List<DomDefinition>();
        private readonly List<DomTemplate> domTemplates = new List<DomTemplate>();
        private readonly List<DomInstance> domInstances = new List<DomInstance>();

        public DomModule(
            ModuleSettings settings,
            IEnumerable<CustomSectionDefinition> domSections,
            IEnumerable<DomBehaviorDefinition> domBehaviors,
            IEnumerable<DomDefinition> domDefinitions,
            IEnumerable<DomTemplate> domTemplates,
            IEnumerable<DomInstance> domInstances)
        {
            this.settings = settings;
            this.domSections = domSections.ToList();
            this.domBehaviors = domBehaviors.ToList();
            this.domDefinitions = domDefinitions.ToList();
            this.domTemplates = domTemplates.ToList();
            this.domInstances = domInstances.ToList();
        }

        public ModuleSettings Settings => settings;

        public IReadOnlyList<CustomSectionDefinition> Sections => domSections;

        public IReadOnlyList<DomBehaviorDefinition> Behaviors => domBehaviors;

        public IReadOnlyList<DomDefinition> Definitions => domDefinitions;

        public IReadOnlyList<DomTemplate> Templates => domTemplates;

        public IReadOnlyList<DomInstance> Instances => domInstances;
    }
}
