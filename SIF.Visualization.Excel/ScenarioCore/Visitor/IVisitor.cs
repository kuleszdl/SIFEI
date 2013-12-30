using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore.StaticScenarios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace SIF.Visualization.Excel.ScenarioCore.Visitor
{
    public interface IVisitor
    {
        object Visit(Scenario n);

        object Visit(StaticScenario n);

        object Visit(WorkbookModel n);

        object Visit(InputCellData n);

        object Visit(IntermediateCellData n);

        object Visit(ResultCellData n);

        object Visit(Cell n);

    }
}
