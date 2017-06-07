using SIF.Visualization.Excel.Core.Scenarios;
using SIF.Visualization.Excel.Core.Rules;

namespace SIF.Visualization.Excel.Core
{
    public interface IVisitor
    {
        object Visit(Scenario n);
        
        object Visit(WorkbookModel n);

        object Visit(InputData n);

        object Visit(ScenarioData n);

        object Visit(ConditionData n);

        object Visit(Cell n);

        //object Visit(Condition n);

        object Visit(Rule n);

    }
}
