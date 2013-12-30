using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.ScenarioCore.StaticScenarios
{
    public abstract class StaticScenarioRule : BindableBase
    {
        private string name;
        private string author;
        private double severtyWight;

        #region Properties

        /// <summary>
        /// Gets or sets the name of the current rule.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets the author of the current rule.
        /// </summary>
        public string Author
        {
            get { return this.author; }
            set { this.SetProperty(ref this.author, value); }
        }

        /// <summary>
        /// Gets or sets the severtyWight of the current rule.
        /// </summary>
        public double SevertyWight
        {
            get { return this.severtyWight; }
            set { this.SetProperty(ref this.severtyWight, value); }
        }

        #endregion

    }
}
