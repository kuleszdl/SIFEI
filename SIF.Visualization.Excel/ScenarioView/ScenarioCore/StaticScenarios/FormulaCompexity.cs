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
            get { return maxNestingLevel; }
            set { SetProperty(ref maxNestingLevel, value); }
        }

        /// <summary>
        /// Gets or sets the maximum numbers of operations of a formula of the current rule.
        /// </summary>
        public int MaxNumbersOfOperations
        {
            get { return maxNumbersOfOperations; }
            set { SetProperty(ref maxNumbersOfOperations, value); }
        }

        #endregion
    }
}
