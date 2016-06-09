using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;

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
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// Gets or sets the author of the current scenario.
        /// </summary>
        public string Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }

        /// <summary>
        /// Gets or sets the date of creation of the current scenario.
        /// </summary>
        public DateTime CrationDate
        {
            get { return creationDate; }
            set { SetProperty(ref creationDate, value); }
        }

        /// <summary>
        /// Gets the static scenario rules of creation of the current scenario.
        /// </summary>
        public SpecializedCollection<StaticScenarioRule> StaticScenarioRules
        {
            get 
            {
                if (staticScenarioRules == null) staticScenarioRules = new SpecializedCollection<StaticScenarioRule>();
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
