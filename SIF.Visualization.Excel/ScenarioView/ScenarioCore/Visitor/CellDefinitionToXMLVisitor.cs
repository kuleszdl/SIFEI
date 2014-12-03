using System;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.ScenarioCore.Visitor
{
    public class CellDefinitionToXMLVisitor : IVisitor
    {
        public object Visit(Core.WorkbookModel n)
        {
            var root = new XElement("cellDefinitions");

            //input cells
            var inputElement = new XElement("inputCells");
            foreach (var c in n.InputCells)
            {
                inputElement.Add(c.Accept(this));
            }
            root.Add(inputElement);

            //intermediate cells
            var intermediateElement = new XElement("intermediateCells");
            foreach (var c in n.IntermediateCells)
            {
                intermediateElement.Add(c.Accept(this));
            }
            root.Add(intermediateElement);


            //result cells
            var resultElement = new XElement("resultCells");
            foreach (var c in n.OutputCells)
            {
                resultElement.Add(c.Accept(this));
            }
            root.Add(resultElement);

            var sanityValueElement = new XElement("sanityValueCells");
            foreach (var c in n.SanityValueCells)
            {
                sanityValueElement.Add(c.Accept(this));
            }
            root.Add(sanityValueElement);

            var sanityConstraintElement = new XElement("sanityConstraintCells");
            foreach (var c in n.SanityConstraintCells)
            {
                sanityConstraintElement.Add(c.Accept(this));
            }
            root.Add(sanityConstraintElement);

            var sanityExplanationElement = new XElement("sanityExplanationCells");
            foreach (var c in n.SanityExplanationCells)
            {
                sanityExplanationElement.Add(c.Accept(this));
            }
            root.Add(sanityExplanationElement);

            var sanityCheckingElement = new XElement("sanityCheckingCells");
            foreach (var c in n.SanityCheckingCells)
            {
                sanityCheckingElement.Add(c.Accept(this));
            }
            root.Add(sanityCheckingElement);

            
            root.Add(resultElement);
            return root;
        }

        public object Visit(Core.Cell n)
        {
            var root = new XElement("cell");

            root.Add(new XElement("sifLocation", n.SifLocation));
            root.Add(new XElement("content", n.Content));

            return root;
        }

        #region not implemented
        public object Visit(Scenario n)
        {
            throw new NotImplementedException();
        }

        public object Visit(StaticScenarios.StaticScenario n)
        {
            throw new NotImplementedException();
        }

        public object Visit(InputCellData n)
        {
            throw new NotImplementedException();
        }

        public object Visit(IntermediateCellData n)
        {
            throw new NotImplementedException();
        }

        public object Visit(ResultCellData n)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
