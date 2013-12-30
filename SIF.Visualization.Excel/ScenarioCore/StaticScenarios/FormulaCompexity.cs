using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.ScenarioCore.StaticScenarios
{
    public class FormulaCompexity : StaticScenarioRule
    {
        int maxNestingLevel;
        int maxNumbersOfOperations;

        #region Properties

        /// <summary>
        /// Gets or sets the maximum numbers of nesting levels of a formula of the current rule.
        /// </summary>
        public int MaxNestingLevel
        {
            get { return this.maxNestingLevel; }
            set { this.SetProperty(ref this.maxNestingLevel, value); }
        }

        /// <summary>
        /// Gets or sets the maximum numbers of operations of a formula of the current rule.
        /// </summary>
        public int MaxNumbersOfOperations
        {
            get { return this.maxNumbersOfOperations; }
            set { this.SetProperty(ref this.maxNumbersOfOperations, value); }
        }

        #endregion
    }
}
