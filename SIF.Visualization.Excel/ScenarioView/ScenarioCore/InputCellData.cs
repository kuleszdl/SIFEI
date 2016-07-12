using SIF.Visualization.Excel.ScenarioCore.Visitor;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public class InputCellData : CellData, IAcceptVisitor
    {
        public InputCellData()
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
