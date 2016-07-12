using System;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.ScenarioCore.Visitor
{
    class XMLToScenarioVisitor : IVisitor
    {
        private XElement root;

        public XMLToScenarioVisitor(XElement root)
        {
            this.root = root;
        }

        public object Visit(Core.WorkbookModel n)
        {
            if (root == null) return false;
            foreach (var scenarioElement in root.Elements())
            {
                if (scenarioElement.Name != "Scenario") continue;
                var newScenario = new Scenario();
                newScenario.Accept(new XMLToScenarioVisitor(scenarioElement));

                n.Scenarios.Add(newScenario);
            } 

            return true;
        }

        public object Visit(Scenario n)
        {
            if (root == null) return false;

            var titleAttribute = root.Attribute(XName.Get("Title"));
            n.Title = (titleAttribute != null) ? titleAttribute.Value : String.Empty;

            var descriptionAttribute = root.Attribute(XName.Get("Description"));
            n.Description = (descriptionAttribute != null) ? descriptionAttribute.Value : String.Empty;

            var authorAttribute = root.Attribute(XName.Get("Author"));
            n.Author = (authorAttribute != null) ? authorAttribute.Value : String.Empty;

            var creationDateAttribute = root.Attribute(XName.Get("CreationDate"));
            n.CrationDate = (creationDateAttribute != null) ? Convert.ToDateTime(creationDateAttribute.Value) : DateTime.Now;

            var ratingAttribute = root.Attribute(XName.Get("Rating"));
            n.Rating = (ratingAttribute != null) ? Convert.ToDouble(ratingAttribute.Value) : 0.0;


            //inputs
            var inputElements = root.Element(XName.Get("Inputs"));
            if (inputElements != null)
            {
                foreach (var inputElement in inputElements.Elements())
                {
                    var newInput = new InputCellData();
                    newInput.Accept(new XMLToScenarioVisitor(inputElement));
                    n.Inputs.Add(newInput);
                }
            }

            //intermediates
            var intermediateElements = root.Element(XName.Get("Intermediates"));
            if (intermediateElements != null)
            {
                foreach (var intermediateElement in intermediateElements.Elements())
                {
                    var newIntermediate = new IntermediateCellData();
                    newIntermediate.Accept(new XMLToScenarioVisitor(intermediateElement));
                    n.Intermediates.Add(newIntermediate);
                }
            }

            //results
            var resultElements = root.Element(XName.Get("Results"));
            if (resultElements != null)
            {
                foreach (var resultElement in resultElements.Elements())
                {
                    var newResult = new ResultCellData();
                    newResult.Accept(new XMLToScenarioVisitor(resultElement));
                    n.Results.Add(newResult);
                }
            }
            return true;
        }

        public object Visit(InputCellData n)
        {
            if (root == null) return false;

            var locationElement = root.Element(XName.Get("Location"));
            n.Location = (locationElement != null) ? locationElement.Value : String.Empty;

            var contentElement = root.Element(XName.Get("Content"));
            n.Content = (contentElement != null) ? contentElement.Value : String.Empty;
            
            var cellTypeElement = root.Element(XName.Get("CellType"));
            if (cellTypeElement != null)
            {
                n.CellType = (TestInputType) Enum.Parse(typeof(TestInputType), cellTypeElement.Value.ToUpper());
            }

            return true;
        }

        public object Visit(IntermediateCellData n)
        {
            if (root == null) return false;

            var locationElement = root.Element(XName.Get("Location"));
            n.Location = (locationElement != null) ? locationElement.Value : String.Empty;

            var contentElement = root.Element(XName.Get("Content"));
            n.Content = (contentElement != null) ? contentElement.Value : String.Empty;

            var cellTypeElement = root.Element(XName.Get("CellType"));
            if (cellTypeElement != null)
            {
                n.CellType = (TestInputType)Enum.Parse(typeof(TestInputType), cellTypeElement.Value.ToUpper());
            }

            var differenceUpElement = root.Element(XName.Get("differenceUp"));
            n.DifferenceUp = (differenceUpElement != null) ? Double.Parse(differenceUpElement.Value) : Properties.Settings.Default.StandartDifference;

            var differenceDownElement = root.Element(XName.Get("differenceDown"));
            n.DifferenceDown = (differenceDownElement != null) ? Double.Parse(differenceDownElement.Value) : Properties.Settings.Default.StandartDifference;

            return true;
        }

        public object Visit(ResultCellData n)
        {
            if (root == null) return false;

            var locationElement = root.Element(XName.Get("Location"));
            n.Location = (locationElement != null) ? locationElement.Value : String.Empty;

            var contentElement = root.Element(XName.Get("Content"));
            n.Content = (contentElement != null) ? contentElement.Value : String.Empty;

            var cellTypeElement = root.Element(XName.Get("CellType"));
            if (cellTypeElement != null)
            {
                n.CellType = (TestInputType)Enum.Parse(typeof(TestInputType), cellTypeElement.Value.ToUpper());
            }

            var differenceUpElement = root.Element(XName.Get("differenceUp"));
            n.DifferenceUp = (differenceUpElement != null) ? Double.Parse(differenceUpElement.Value) : Properties.Settings.Default.StandartDifference;

            var differenceDownElement = root.Element(XName.Get("differenceDown"));
            n.DifferenceDown = (differenceDownElement != null) ? Double.Parse(differenceDownElement.Value) : Properties.Settings.Default.StandartDifference;

            return true;
        }

        public object Visit(Core.Cell n)
        {
            if (root == null) return false;

            var idElement = root.Element(XName.Get("ID"));
            n.Id = (idElement != null) ? Convert.ToInt32(idElement.Value) : 1;

            var contentElement = root.Element(XName.Get("Content"));
            n.Content = (contentElement != null) ? contentElement.Value : String.Empty;

            var locationElement = root.Element(XName.Get("Location"));
            n.Location = (locationElement != null) ? contentElement.Value : String.Empty;

            return true;
        }


        public object Visit(StaticScenarios.StaticScenario n)
        {
            throw new NotImplementedException();
        }
    }
}
