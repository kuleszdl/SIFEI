using System.Collections.ObjectModel;

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
                if (ingnoredConstants == null) ingnoredConstants = new SpecializedCollection<TestInputType>();
                return ingnoredConstants;
            }
            set { SetProperty(ref ingnoredConstants, value); }
        }

        /// <summary>
        /// Gets or sets the ignored functions list of the current rule.
        /// </summary>
        public ObservableCollection<string> IgnoredFunction
        {
            get
            {
                if (ignoredFunction == null) ignoredFunction = new ObservableCollection<string>();
                return ignoredFunction;
            }
            set { SetProperty(ref ignoredFunction, value); }
        }

        #endregion
    }
}
