using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore.StaticScenarios;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.ScenarioCore.StaticScenarios
{
    public class StaticScenario : BindableBase, IAcceptVisitor
    {
        private string description;
        private string author;
        private DateTime creationDate;
        private SpecializedCollection<StaticScenarioRule> staticScenarioRules;


        #region Properties

        /// <summary>
        /// Gets or sets the title of the current scenario.
        /// </summary>
        public string Title
        {
            get { return Properties.Settings.Default.StaticScenarioName;}
        }

        /// <summary>
        /// Gets or sets the static description base of the current scenario.
        /// </summary>
        public string StaticDescription
        {
            get { return Properties.Settings.Default.StaticScenarioDescriptionBase; }
        }

        /// <summary>
        /// Gets or sets the description of the current scenario.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets the author of the current scenario.
        /// </summary>
        public string Author
        {
            get { return this.author; }
            set { this.SetProperty(ref this.author, value); }
        }

        /// <summary>
        /// Gets or sets the date of creation of the current scenario.
        /// </summary>
        public DateTime CrationDate
        {
            get { return this.creationDate; }
            set { this.SetProperty(ref this.creationDate, value); }
        }

        /// <summary>
        /// Gets the static scenario rules of creation of the current scenario.
        /// </summary>
        public SpecializedCollection<StaticScenarioRule> StaticScenarioRules
        {
            get 
            {
                if (this.staticScenarioRules == null) this.staticScenarioRules = new SpecializedCollection<StaticScenarioRule>();
                return staticScenarioRules;
            }
        }

        #endregion

        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
    }
}
