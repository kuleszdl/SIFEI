using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore.StaticScenarios;


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
