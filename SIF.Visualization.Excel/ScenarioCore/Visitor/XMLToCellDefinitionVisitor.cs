using SIF.Visualization.Excel.Cells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.ScenarioCore.Visitor
{
    public class XMLToCellDefinitionVisitor : IVisitor
    {

        private XElement root;
        private Core.WorkbookModel wb;

        public XMLToCellDefinitionVisitor(XElement root)
        {
            this.root = root;
        }

        public XMLToCellDefinitionVisitor(XElement root, Core.WorkbookModel wb)
        {
            this.root = root;
            this.wb = wb;
        }

        public object Visit(Core.WorkbookModel n)
        {
            if (root == null) return false;

            //get input cells
            var inputCellsElement = root.Element(XName.Get("inputCells"));
            if (inputCellsElement != null)
            {
                foreach (var c in inputCellsElement.Elements())
                {
                    var inputCell = new Core.Cell();
                    inputCell.Accept(new XMLToCellDefinitionVisitor(c, n));
                    n.InputCells.Add(inputCell.ToInputCell());
                }
            }

            //get intermediate cells
            var intermediateCellsElement = root.Element(XName.Get("intermediateCells"));
            if (intermediateCellsElement != null)
            {
                foreach (var c in intermediateCellsElement.Elements())
                {
                    var intermediateCell = new Core.Cell();
                    intermediateCell.Accept(new XMLToCellDefinitionVisitor(c, n));
                    n.IntermediateCells.Add(intermediateCell.ToIntermediateCell());
                }
            }

            //get result cells
            var resultCellsElement = root.Element(XName.Get("resultCells"));
            if (resultCellsElement != null)
            {
                foreach (var c in resultCellsElement.Elements())
                {
                    var resultCell = new Core.Cell();
                    resultCell.Accept(new XMLToCellDefinitionVisitor(c, n));
                    n.OutputCells.Add(resultCell.ToOutputCell());
                }
            }

            return true;
        }

        public object Visit(Core.Cell n)
        {
            if (root == null || wb == null) return false;

            var sifLocationElement = root.Element(XName.Get("sifLocation"));
            n.SifLocation = (sifLocationElement != null) ? sifLocationElement.Value : String.Empty;

            var contentElement = root.Element(XName.Get("content"));
            n.Content = (contentElement != null) ? contentElement.Value : String.Empty;

            //get the user cell name
            if (n.SifLocation != null && n.SifLocation != String.Empty)
            {
                n.Location = CellManager.Instance.GetUserCellNameWithSIFName(wb, n.SifLocation);
            }

            //future work: update content

            return true;
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
