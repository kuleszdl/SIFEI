using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
