using SIF.Visualization.Excel.Core.Scenarios;
using System;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core {
    class SprudelXMLVisitor : IVisitor {
        public SprudelXMLVisitor() { }

        /// <summary>
        /// Create the sprudel xml document
        /// </summary>
        /// <param name="n">WorkbookModel</param>
        /// <returns>complete sprudel xml as XElement</returns>
        public object Visit(WorkbookModel n) {
            PolicyConfigurationModel settings = n.PolicySettings;
            var wrapper = new XElement("inspectionRequest");
            var root = new XElement("policies");

            XElement dynamicPolicy = CreateScenarioElements(n);
            if (dynamicPolicy != null) {
                root.Add(dynamicPolicy);
            }

            XElement sanityChecks = CreateSanityElements(n);
            if (sanityChecks != null) {
                root.Add(sanityChecks);
            }

            if (settings.ReadingDirection) {
                XElement readingDirection = createReadingDirection(settings);
                root.Add(readingDirection);
            }

            if (settings.NoConstantsInFormulas) {
                XElement constants = createNoConstants(settings);
                root.Add(constants);
            }

            if (settings.FormulaComplexity) {
                XElement formulaComplexity = createFormulaComplexity(settings);
                root.Add(formulaComplexity);
            }

            if (settings.NonConsideredConstants) {
                XElement nonConsidered = createNonConsideredValues(settings);
                root.Add(nonConsidered);
            }

            if (settings.OneAmongOthers) {
                XElement oneAmongOthers = createOneAmongOthers(settings);
                root.Add(oneAmongOthers);
            }

            if (settings.RefToNull) {
                XElement refToNull = createRefToNull(settings);
                root.Add(refToNull);
            }

            if (settings.StringDistance) {
                XElement stringDistance = createStringDistance(settings);
                root.Add(stringDistance);
            }

            if (settings.MultipleSameRef) {
                XElement msr = createMultipleSameRef(settings);
                root.Add(msr);
            }

            if (settings.ErrorInCells) {
                XElement eic = createErrorInCells(settings);
                root.Add(eic);
            }

            wrapper.Add(root);
            return wrapper;
        }

        private XElement CreateScenarioElements(WorkbookModel n) {
            if (n.Scenarios.Count > 0) {

                XElement dynamicPolicy = new XElement("dynamicTestingPolicy");
                // scenarios 
                var scenarios = new XElement("scenarios");
                foreach (var scenario in n.Scenarios) {
                    scenarios.Add(Visit(scenario));
                }
                dynamicPolicy.Add(scenarios);
                return dynamicPolicy;
            }
            return null;
        }

        private XElement createErrorInCells(PolicyConfigurationModel settings) {
            XElement eic = new XElement("errorContainingCellPolicy");
            return eic;
        }

        private XElement createMultipleSameRef(PolicyConfigurationModel settings) {
            XElement msr = new XElement("multipleSameRefPolicy");
            return msr;
        }

        private XElement createStringDistance(PolicyConfigurationModel settings) {
            XElement stringDst = new XElement("stringDistancePolicy");
            XElement dist = new XElement("minDistance");
            dist.Value = settings.StringDistanceMinDist.ToString();
            stringDst.Add(dist);

            return stringDst;
        }

        private XElement createRefToNull(PolicyConfigurationModel settings) {
            XElement refToNull = new XElement("refToNullPolicy");
            return refToNull;
        }

        private XElement createOneAmongOthers(PolicyConfigurationModel settings) {
            XElement oneAmong = new XElement("oneAmongOthersPolicy");
            XElement style = new XElement("environmentStyle");
            style.Value = settings.OneAmongOthersStyle;
            XElement length = new XElement("environmentLength");
            length.Value = settings.OneAmongOthersLength.ToString();
            oneAmong.Add(style);
            oneAmong.Add(length);

            return oneAmong;
        }

        private XElement createNonConsideredValues(PolicyConfigurationModel settings) {
            XElement nonConsidered = new XElement("nonConsideredValuesPolicy");
            return nonConsidered;
        }

        private XElement createReadingDirection(PolicyConfigurationModel settings) {
            XElement reading = new XElement("readingDirectionPolicy");
            XElement left = new XElement("leftToRight");
            left.Value = settings.ReadingDirectionLeftRight.ToString().ToLower();
            XElement top = new XElement("topToBottom");
            top.Value = settings.ReadingDirectionTopBottom.ToString().ToLower();
            reading.Add(left);
            reading.Add(top);
            return reading;
        }

        private XElement createNoConstants(PolicyConfigurationModel settings) {
            XElement noConstant = new XElement("noConstantsInFormulasPolicy");
            return noConstant;
        }

        private XElement createFormulaComplexity(PolicyConfigurationModel settings) {
            XElement formulaRule = new XElement("formulaComplexityPolicy");
            XElement maxNesting = new XElement("maxNesting");
            maxNesting.Value = settings.FormulaComplexityMaxDepth.ToString();
            XElement maxOperations = new XElement("maxOperations");
            maxOperations.Value = settings.FormulaComplexityMaxOperations.ToString();
            formulaRule.Add(maxNesting);
            formulaRule.Add(maxOperations);

            return formulaRule;
        }

        #region private workbook model visitor methods

        private XElement CreateSanityElements(WorkbookModel n) {
            var root = new XElement("sanityChecks");

            // @TODO
            /*
            XElement checking = CreateSanityCheckingCells(n);
            if (!checking.HasElements) return null;

            root.Add(checking);
            root.Add(CreateSanityValueCells(n));
            root.Add(CreateSanityConstraintCells(n));
            root.Add(CreateSanityExplanationCells(n));

            root.Add(new XElement("sanityWarnings", n.SanityWarnings));
            */
            return root;
        }

        /// <summary>
        /// Creats a part of sprudel with the data fo a scenario
        /// </summary>
        /// <param name="n">Scenario</param>
        /// <returns>XElement</returns>
        public object Visit(Scenario n) {
            var scenario = new XElement("scenario");

            //attributes
            scenario.Add(new XElement("name", n.Title));

            //test inputs
            scenario.Add(CreateInputs(n));

            //invariants
            scenario.Add(CreateInvariants(n));

            //post conditions
            scenario.Add(CreateConditions(n));

            return scenario;
        }

        private XElement CreateInputs(Scenario n) {
            var inputs = new XElement("inputs");
            foreach (var test in n.Inputs) {
                if (!test.Value.Equals("")) {
                    var inputElement = new XElement("input");
                    inputElement.Add(new XElement("target", test.Target));
                    inputElement.Add(new XElement("type", test.Type.ToString()));
                    inputElement.Add(new XElement("value", test.Value));
                    inputs.Add(inputElement);
                }
            }
            return inputs;
        }

        private XElement CreateInvariants(Scenario n) {
            var invariants = new XElement("invariants");
            foreach (var invariant in n.Invariants) {
                var invariantElement = new XElement("invariant");
                invariantElement.Add(new XElement("target", invariant.Target));
                invariants.Add(invariantElement);
            }
            return invariants;
        }

        private XElement CreateConditions(Scenario n) {
            var conditions = new XElement("conditions");
            foreach (var condition in n.Conditions) {
                if (!condition.Value.Equals("")) {
                    var conditionElement = new XElement("condition");
                    conditionElement.Add(new XElement("operator", condition.Operator.ToString()));
                    conditionElement.Add(new XElement("target", condition.Target));
                    conditionElement.Add(new XElement("type", condition.Type.ToString()));
                    conditionElement.Add(new XElement("value", condition.Value));
                    conditions.Add(conditionElement);
                }
            }
            return conditions; ;
        }

        #endregion

        #region not implemented

        public object Visit(InputData n) {
            throw new NotImplementedException();
        }

        public object Visit(ScenarioData n) {
            throw new NotImplementedException();
        }

        public object Visit(ConditionData n) {
            throw new NotImplementedException();
        }

        public object Visit(Cell n) {
            throw new NotImplementedException();
        }

        #endregion

        #region private class methods
        private string NullCheck(string content) {
            return (content != null) ? content : String.Empty;
        }
        #endregion
    }
}
