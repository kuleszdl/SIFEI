using System;
using System.Xml.Linq;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ScenarioCore.Visitor
{
    class Sprudel1_5XMLVisitor : IVisitor
    {
        private InspectionType type;

        public Sprudel1_5XMLVisitor(InspectionType scanningType)
        {
            type = scanningType;
        }
        /// <summary>
        /// Create the sprudel xml document
        /// </summary>
        /// <param name="n">WorkbookModel</param>
        /// <returns>complete sprudel xml as XElement</returns>
        public object Visit(WorkbookModel n)
        {
            PolicyConfigurationModel settings = n.PolicySettings;
            var root = new XElement("policyList");
            var dynamicPolicy = new XElement("dynamicPolicy");
            //attributes
            dynamicPolicy.Add(new XAttribute("name", NullCheck(n.Title) + " Inspection"));
            dynamicPolicy.Add(new XAttribute("description", GetDocumentProperty(n, "Comments")));
            dynamicPolicy.Add(new XAttribute("author", GetDocumentProperty(n, "Author")));

            //rules 
            var rules = new XElement("rules");
            foreach (var scenario in n.Scenarios)
            {
                rules.Add(scenario.Accept(this) as XElement);
            }
            dynamicPolicy.Add(rules);

            //spreadsheet file path
            dynamicPolicy.Add(new XElement("spreadsheetFilePath", NullCheck(n.Spreadsheet)));

            //input cells
            dynamicPolicy.Add(CreateInputCells(n));

            //output cells
            dynamicPolicy.Add(CreateOutputCells(n));
            // TODO: don't add when no scenario is present
            root.Add(dynamicPolicy);
            XElement sanityRules = CreateSanityRules(n);
            if (sanityRules != null)
            {
                root.Add(sanityRules);
            }
            if ((settings.ReadingDirection && type == InspectionType.MANUAL) || 
                (settings.ReadingDirection && settings.ReadingDirectionAutomatic))
            {
                XElement readingDirection = createReadingDirection();
                root.Add(readingDirection);
            }
            if ((settings.NoConstantsInFormulas && type == InspectionType.MANUAL) ||
                (settings.NoConstantsInFormulas && settings.NoConstantsInFormulasAutomatic))
            {
                XElement constants = createNoConstants();
                root.Add(constants);
            }
            if ((settings.FormulaComplexity && type == InspectionType.MANUAL) ||
                (settings.FormulaComplexity && settings.FormulaComplexityAutomatic))
            {
                XElement formulaComplexity = createFormulaComplexity();
                root.Add(formulaComplexity);
            }

            if ((settings.NonConsideredConstants && type == InspectionType.MANUAL) ||
                (settings.NonConsideredConstants && settings.NonConsideredConstantsAutomatic))
            {
                XElement nonConsidered = createNonConsideredValues();
                root.Add(nonConsidered);
            }
            if ((settings.OneAmongOthers && type == InspectionType.MANUAL) ||
                (settings.OneAmongOthers && settings.OneAmongOthersAutomatic))
            {
                XElement oneAmongOthers = createOneAmongOthers();
                root.Add(oneAmongOthers);
            }
            if ((settings.RefToNull && type == InspectionType.MANUAL) ||
                (settings.RefToNull && settings.RefToNullAutomatic))
            {
                XElement refToNull = createRefToNull();
                root.Add(refToNull);
            }
            if ((settings.StringDistance && type == InspectionType.MANUAL) ||
                (settings.StringDistance && settings.StringDistanceAutomatic))
            {
                XElement stringDistance = createStringDistance(settings);
                root.Add(stringDistance);
            }
            if ((settings.MultipleSameRef && type == InspectionType.MANUAL) ||
                (settings.MultipleSameRef && settings.MultipleSameRefAutomatic))
            {
                XElement msr = createMultipleSameRef();
                root.Add(msr);
            }
            if ((settings.ErrorInCells && type == InspectionType.MANUAL) ||
                (settings.ErrorInCells && settings.ErrorInCellsAutomatic))
            {
                XElement eic = createErrorInCells();
                root.Add(eic);
            }
                
            return root;
        }

        private XElement createErrorInCells()
        {
            XElement eic = new XElement("errorContainingCellPolicyRule");
            return eic;
        }

        private XElement createMultipleSameRef()
        {
            XElement msr = new XElement("multipleSameRefPolicyRule");
            return msr;
        }

        private XElement createStringDistance(PolicyConfigurationModel settings)
        {
            XElement stringDst = new XElement("stringDistancePolicyRule");
            XElement dist = new XElement("stringDistanceDifference");
            dist.Value = settings.StringDistanceMaxDist.ToString();
            stringDst.Add(dist);

            return stringDst;
        }

        private XElement createRefToNull()
        {
            XElement refToNull = new XElement("refToNullPolicyRule");
            return refToNull;
        }

        private XElement createOneAmongOthers()
        {
            XElement oneAmong = new XElement("oneAmongOthersPolicyRule");
            // 1 = horizontal, 2 = vertical, 3 = cross
            XElement style = new XElement("oneAmongOthersStyle");
            style.Value = "3";
            XElement length = new XElement("oneAmongOthersLength");
            length.Value = "2";
            oneAmong.Add(style);
            oneAmong.Add(length);

            return oneAmong;
        }

        private XElement createNonConsideredValues()
        {
            XElement nonConsidered = new XElement("nonConsideredValuesPolicyRule");
            XElement ignoredWs = new XElement("ignoredWorksheets");
            XElement erlass = new XElement("ignoredWorksheetName");
            erlass.Value = "Erlass_Anlage"; // for the usual testcase of the bafoeg calculator, until it can be set properly
            ignoredWs.Add(erlass);
            nonConsidered.Add(ignoredWs);

            return nonConsidered;
        }

        private XElement createReadingDirection()
        {
            XElement reading = new XElement("readingDirectionPolicyRule");
            XElement left = new XElement("leftToRight");
            left.Value = "true";
            XElement top = new XElement("topToBottom");
            top.Value = "true";

            reading.Add(left);
            reading.Add(top);
            return reading;
        }

        private XElement createNoConstants()
        {
            XElement noConstant = new XElement("noConstantsPolicyRule");
            //XElement ignoredConstants = new XElement("ignoredConstants");
            //XElement ignoreOne = new XElement("ignoredConstant");
            //ignoreOne.Value = "1";
            //ignoredConstants.Add(ignoreOne);
            //noConstant.Add(ignoredConstants);
            return noConstant;
        }

        private XElement createFormulaComplexity()
        {
            XElement formulaRule = new XElement("formulaComplexityPolicyRule");
            XElement maxNesting = new XElement("formulaComplexityMaxNesting");
            maxNesting.Value = "3";
            formulaRule.Add(maxNesting);

            return formulaRule;
        }

        #region private workbook model visitor methods

        private XElement CreateSanityRules(WorkbookModel n)
        {
            var root = new XElement("sanityRules");

            XElement checking = CreateSanityCheckingCells(n);
            if (!checking.HasElements) return null;

            root.Add(checking);
            root.Add(CreateSanityValueCells(n));

            root.Add(CreateSanityConstraintCells(n));

            root.Add(CreateSanityExplanationCells(n));



            root.Add(new XElement("sanityWarnings", n.SanityWarnings));
            return root;
        }

        private XElement CreateInputCells(WorkbookModel n)
        {
            var root = new XElement("inputCells");

            foreach (var cell in n.OutputCells)
            {
                var cellElement = new XElement("inputCell");
                cellElement.Add(new XElement("name", NullCheck(new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, cell.Location).ShortLocation)));
            }

            return root;
        }

        private XElement CreateSanityValueCells(WorkbookModel n)
        {
            var root = new XElement("sanityValueCells");

            foreach (var cell in n.SanityValueCells)
            {
                root.Add(new XElement("location", NullCheck(new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, cell.Location).Location)));
            }

            return root;
        }

        private XElement CreateSanityConstraintCells(WorkbookModel n)
        {
            var root = new XElement("sanityConstraintCells");

            foreach (var cell in n.SanityConstraintCells)
            {
                root.Add(new XElement("location", NullCheck(new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, cell.Location).Location)));
            }

            return root;
        }

        private XElement CreateSanityExplanationCells(WorkbookModel n)
        {
            var root = new XElement("sanityExplanationCells");

            foreach (var cell in n.SanityExplanationCells)
            {
                root.Add(new XElement("location", NullCheck(new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, cell.Location).Location)));
            }

            return root;
        }

        private XElement CreateSanityCheckingCells(WorkbookModel n)
        {
            var root = new XElement("sanityCheckingCells");

            foreach (var cell in n.SanityCheckingCells)
            {
                root.Add(new XElement("location", NullCheck(new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, cell.Location).Location)));
            }

            return root;
        }


        private XElement CreateOutputCells(WorkbookModel n)
        {
            var root = new XElement("outputCells");

            foreach (var cell in n.OutputCells)
            {
                var cellElement = new XElement("outputCell");
                cellElement.Add(new XElement("name", NullCheck(new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, cell.Location).ShortLocation)));
            }

            return root;
        }

        /// <summary>
        /// Find a document property
        /// </summary>
        /// <param name="n">Workbook model with the excel workbook</param>
        /// <param name="propertyName">name of the requested property</param>
        /// <returns></returns>
        private string GetDocumentProperty(WorkbookModel n, string propertyName)
        {
            var properties = (Microsoft.Office.Core.DocumentProperties)n.Workbook.BuiltinDocumentProperties;
            string value;
            try
            {
                value = (properties[propertyName].Value != null) ? properties[propertyName].Value.ToString() : String.Empty;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                value = String.Empty;
            }

            return value;
        }

        #endregion

        /// <summary>
        /// Creats a part of sprudel with the data fo a scenario
        /// </summary>
        /// <param name="n">Scenario</param>
        /// <returns>XElement</returns>
        public object Visit(Scenario n)
        {
            var rule = new XElement("rule");
            //attributes
            rule.Add(new XAttribute("name", NullCheck(n.Title)));
            rule.Add(new XAttribute("author", NullCheck(n.Author)));
            rule.Add(new XAttribute("severityWeight", n.Rating));

            //invariants
            rule.Add(CreateRule(n, "invariants", true, true));

            //test inputs
            rule.Add(CreateTestInputs(n));

            //post conditions
            //rule.Add(CreateRule(n, "postconditions", true, true));

            return rule;
        }

        #region private scenario visitor methods
        /// <summary>
        /// Creates the sprudel test inputs
        /// </summary>
        /// <param name="n">Scenario</param>
        /// <returns>XElement of sprudel testInputs</returns>
        private XElement CreateTestInputs(Scenario n)
        {
            var testInputs = new XElement("testInputs");

            foreach (var test in n.Inputs)
            {
                if (test.Location != null && test.Content != null)
                {
                    var testInputElement = new XElement("testInput");
                    testInputElement.Add(new XElement("target", NullCheck(new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, test.Location).ShortLocation)));
                    testInputElement.Add(new XElement("type", test.CellType.ToString()));
                    testInputElement.Add(new XElement("value", NullCheck(test.Content)));

                    testInputs.Add(testInputElement);
                }
            }

            return testInputs;
        }

        /// <summary>
        /// Creates a rule
        /// </summary>
        /// <param name="n">Scenario</param>
        /// <param name="type">Sprudel rule type: 'invariants' or 'postconditions'</param>
        /// <param name="takeIntermediates">If true, add the intermediates of the scenario to the rule</param>
        /// <param name="takeResults">If true, add the results of the scenario to the rule</param>
        /// <returns></returns>
        private XElement CreateRule(Scenario n, string type, bool takeIntermediates, bool takeResults)
        {
            if (String.IsNullOrEmpty(type)) return null;
            if (type != "invariants" && type != "postconditions") return null;

            var rule = new XElement(type);

            if (takeIntermediates)
            {
                foreach (var intermediate in n.Intermediates)
                {
                    if (IsNotNull(intermediate) && intermediate.Location != null && intermediate.Content != null)
                    {
                        double conentDouble;

                        if (Double.TryParse(intermediate.Content, out conentDouble))
                        {
                            //create intervall
                            var intervalElement = new XElement("interval");
                            intervalElement.Add(new XElement("target", new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, intermediate.Location).ShortLocation));
                            intervalElement.Add(new XElement("value", conentDouble - intermediate.DifferenceDown));
                            //relation (equal, greaterThan, lessThan, lessOrEqual, greaterOrEqual) see DIP-3388 page 35
                            intervalElement.Add(new XElement("relation", "open"));
                            intervalElement.Add(new XElement("value2", conentDouble + intermediate.DifferenceUp));

                            rule.Add(intervalElement);
                        }
                        else
                        {
                            //create compare
                            var compare = new XElement("compare");
                            compare.Add(new XElement("target", new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, intermediate.Location).ShortLocation));
                            compare.Add(new XElement("value", intermediate.Content));
                            //relation (equal, greaterThan, lessThan, lessOrEqual, greaterOrEqual) see DIP-3388 page 35
                            compare.Add(new XElement("relation", "equal"));

                            rule.Add(compare);
                        }
                    }
                }
            }
            if (takeResults)
            {
                foreach (var result in n.Results)
                {
                    double conentDouble;

                    if (Double.TryParse(result.Content, out conentDouble))
                    {
                        //create intervall
                        var intervalElement = new XElement("interval");
                        intervalElement.Add(new XElement("target", new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, result.Location).ShortLocation));
                        intervalElement.Add(new XElement("value", conentDouble - result.DifferenceDown));
                        //relation (equal, greaterThan, lessThan, lessOrEqual, greaterOrEqual) see DIP-3388 page 35
                        intervalElement.Add(new XElement("relation", "open"));
                        intervalElement.Add(new XElement("value2", conentDouble + result.DifferenceUp));

                        rule.Add(intervalElement);
                    }
                    else
                    {
                        //create compare
                        var compare = new XElement("compare");
                        compare.Add(new XElement("target", new CellLocation(DataModel.Instance.CurrentWorkbook.Workbook, result.Location).ShortLocation));
                        compare.Add(new XElement("value", result.Content));
                        //relation (equal, greaterThan, lessThan, lessOrEqual, greaterOrEqual) see DIP-3388 page 35
                        compare.Add(new XElement("relation", "equal"));

                        rule.Add(compare);
                    }
                }
            }

            return rule;
        }

        #endregion

        #region not implemented
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

        public object Visit(Cell n)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region private class methods
        private string NullCheck(string content)
        {
            return (content != null) ? content : String.Empty;
        }

        private bool IsNotNull(IntermediateCellData n)
        {
            var result = true;

            if (n.Location == null) result = false;
            if (n.Content == null) result = false;

            return result;
        }

        private bool IsNotNull(ResultCellData n)
        {
            var result = true;

            if (n.Location == null) result = false;
            if (n.Content == null) result = false;

            return result;
        }

        #endregion


        public object Visit(StaticScenarios.StaticScenario n)
        {
            throw new NotImplementedException();
        }
    }
}
