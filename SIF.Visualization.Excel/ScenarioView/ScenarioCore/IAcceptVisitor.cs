using SIF.Visualization.Excel.ScenarioCore.Visitor;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public interface IAcceptVisitor
    {
        object Accept(IVisitor v);
    }
}
