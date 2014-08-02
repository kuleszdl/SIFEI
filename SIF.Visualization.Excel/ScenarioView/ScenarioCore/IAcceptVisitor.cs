using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SIF.Visualization.Excel.ScenarioCore.Visitor;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public interface IAcceptVisitor
    {
        object Accept(IVisitor v);
    }
}
