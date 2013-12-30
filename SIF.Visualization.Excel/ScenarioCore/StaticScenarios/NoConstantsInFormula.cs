using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.ScenarioCore.StaticScenarios
{
    public class NoConstantsInFormula : StaticScenarioRule
    {
        SpecializedCollection<TestInputType> ingnoredConstants;
        ObservableCollection<string> ignoredFunction;

        //Future work: ignored cells

        #region Properties

        /// <summary>
        /// Gets or sets the ignored constants list of the current rule.
        /// </summary>
        public SpecializedCollection<TestInputType> IngnoredConstants
        {
            get
            {
                if (ingnoredConstants == null) this.ingnoredConstants = new SpecializedCollection<TestInputType>();
                return this.ingnoredConstants;
            }
            set { this.SetProperty(ref this.ingnoredConstants, value); }
        }

        /// <summary>
        /// Gets or sets the ignored functions list of the current rule.
        /// </summary>
        public ObservableCollection<string> IgnoredFunction
        {
            get
            {
                if (ignoredFunction == null) this.ignoredFunction = new ObservableCollection<string>();
                return this.ignoredFunction;
            }
            set { this.SetProperty(ref this.ignoredFunction, value); }
        }

        #endregion
    }
}
