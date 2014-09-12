using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIF.Visualization.Excel.ScenarioCore.Visitor;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public class SanityValueCellData : CellData, IAcceptVisitor
    {
        public SanityValueCellData()
        {
        }

        #region Accept Visitor
        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
        #endregion
    }
    public class SanityConstraintCellData : CellData, IAcceptVisitor
    {
               
        public SanityConstraintCellData()
        {
        }

        #region Accept Visitor
        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
        #endregion
    }
    public class SanityExplanationCellData : CellData, IAcceptVisitor
    {        
        public SanityExplanationCellData()
        {
        }

        #region Accept Visitor
        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
        #endregion
    }
    public class SanityCheckingCellData : CellData, IAcceptVisitor
    {        
        public SanityCheckingCellData()
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
