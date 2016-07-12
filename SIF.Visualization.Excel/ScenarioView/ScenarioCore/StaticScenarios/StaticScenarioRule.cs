using SIF.Visualization.Excel.Core;

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
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// Gets or sets the author of the current rule.
        /// </summary>
        public string Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }

        /// <summary>
        /// Gets or sets the severtyWight of the current rule.
        /// </summary>
        public double SevertyWight
        {
            get { return severtyWight; }
            set { SetProperty(ref severtyWight, value); }
        }

        #endregion

    }
}
