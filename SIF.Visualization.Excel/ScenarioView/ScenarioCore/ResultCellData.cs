using SIF.Visualization.Excel.ScenarioCore.Visitor;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public class ResultCellData : CellData, IAcceptVisitor
    {
        #region Fields

        private double differenceUp = Properties.Settings.Default.StandartDifference;
        private double differenceDown = Properties.Settings.Default.StandartDifference;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the up difference of this scenario.
        /// </summary>
        public double DifferenceUp
        {
            get { return differenceUp; }
            set { SetProperty(ref differenceUp, value); }
        }

        /// <summary>
        /// Gets or sets the down difference of this scenario.
        /// </summary>
        public double DifferenceDown
        {
            get { return differenceDown; }
            set { SetProperty(ref differenceDown, value); }
        }

        #endregion

        public ResultCellData()
        {
        }

        #region Accept Visitor
        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
        #endregion
    }
}
