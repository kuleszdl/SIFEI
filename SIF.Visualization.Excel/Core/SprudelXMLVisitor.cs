using System;
using System.Xml.Linq;
using SIF.Visualization.Excel.Core.Rules;
using SIF.Visualization.Excel.Core.Scenarios;

namespace SIF.Visualization.Excel.Core
{
    internal class SprudelXMLVisitor : IVisitor
    {
        /// <summary>
        ///     Create the sprudel xml document
        /// </summary>
        /// <param name="n">WorkbookModel</param>
        /// <returns>complete sprudel xml as XElement</returns>
        public object Visit(WorkbookModel n)
        {
            var settings = n.PolicySettings;
            var wrapper = new XElement("inspectionRequest");
            var root = new XElement("policies");

            var dynamicPolicy = CreateScenarioElements(n);
            if (dynamicPolicy != null) root.Add(dynamicPolicy);

            var sanityChecks = CreateSanityElements(n);
            if (sanityChecks != null) root.Add(sanityChecks);

            var rulePolicy = CreateRuleElements(n);
            if (rulePolicy != null)
                root.Add(rulePolicy);

            if (settings.ReadingDirection)
            {
                var readingDirection = createReadingDirection(settings);
                root.Add(readingDirection);
            }

            if (settings.NoConstantsInFormulas)
            {
                var constants = createNoConstants(settings);
                root.Add(constants);
            }

            if (settings.FormulaComplexity)
            {
                var formulaComplexity = createFormulaComplexity(settings);
                root.Add(formulaComplexity);
            }

            if (settings.NonConsideredConstants)
            {
                var nonConsidered = createNonConsideredValues(settings);
                root.Add(nonConsidered);
            }

            if (settings.OneAmongOthers)
            {
                var oneAmongOthers = createOneAmongOthers(settings);
                root.Add(oneAmongOthers);
            }

            if (settings.RefToNull)
            {
                var refToNull = createRefToNull(settings);
                root.Add(refToNull);
            }

            if (settings.StringDistance)
            {
                var stringDistance = createStringDistance(settings);
                root.Add(stringDistance);
            }

            if (settings.MultipleSameRef)
            {
                var msr = createMultipleSameRef(settings);
                root.Add(msr);
            }

            if (settings.ErrorInCells)
            {
                var eic = createErrorInCells(settings);
                root.Add(eic);
            }

            wrapper.Add(root);
            return wrapper;
        }

        private XElement CreateScenarioElements(WorkbookModel n)
        {
            if (n.Scenarios.Count > 0)
            {
                var dynamicPolicy = new XElement("dynamicTestingPolicy");
                // scenarios 
                var scenarios = new XElement("scenarios");
                foreach (var scenario in n.Scenarios) scenarios.Add(Visit(scenario));
                dynamicPolicy.Add(scenarios);
                return dynamicPolicy;
            }
            return null;
        }

        private XElement CreateRuleElements(WorkbookModel n)
        {
            if (n.Rules.Count > 0)
            {
                var rulesPolicy = new XElement("customRulesPolicy");
                var rules = new XElement("rules");
                foreach (var rule in n.Rules)
                    rules.Add(Visit(rule));
                rulesPolicy.Add(rules);
                return rulesPolicy;
            }
            return null;
        }


        private XElement createErrorInCells(PolicyConfigurationModel settings)
        {
            var eic = new XElement("errorContainingCellPolicy");
            return eic;
        }

        private XElement createMultipleSameRef(PolicyConfigurationModel settings)
        {
            var msr = new XElement("multipleSameRefPolicy");
            return msr;
        }

        private XElement createStringDistance(PolicyConfigurationModel settings)
        {
            var stringDst = new XElement("stringDistancePolicy");
            var dist = new XElement("minDistance");
            dist.Value = settings.StringDistanceMinDist.ToString();
            stringDst.Add(dist);

            return stringDst;
        }

        private XElement createRefToNull(PolicyConfigurationModel settings)
        {
            var refToNull = new XElement("refToNullPolicy");
            return refToNull;
        }

        private XElement createOneAmongOthers(PolicyConfigurationModel settings)
        {
            var oneAmong = new XElement("oneAmongOthersPolicy");
            var style = new XElement("environmentStyle");
            style.Value = settings.OneAmongOthersStyle;
            var length = new XElement("environmentLength");
            length.Value = settings.OneAmongOthersLength.ToString();
            oneAmong.Add(style);
            oneAmong.Add(length);

            return oneAmong;
        }

        private XElement createNonConsideredValues(PolicyConfigurationModel settings)
        {
            var nonConsidered = new XElement("nonConsideredValuesPolicy");
            return nonConsidered;
        }

        private XElement createReadingDirection(PolicyConfigurationModel settings)
        {
            var reading = new XElement("readingDirectionPolicy");
            var left = new XElement("leftToRight");
            left.Value = settings.ReadingDirectionLeftRight.ToString().ToLower();
            var top = new XElement("topToBottom");
            top.Value = settings.ReadingDirectionTopBottom.ToString().ToLower();
            reading.Add(left);
            reading.Add(top);
            return reading;
        }

        private XElement createNoConstants(PolicyConfigurationModel settings)
        {
            var noConstant = new XElement("noConstantsInFormulasPolicy");
            return noConstant;
        }

        private XElement createFormulaComplexity(PolicyConfigurationModel settings)
        {
            var formulaRule = new XElement("formulaComplexityPolicy");
            var maxNesting = new XElement("maxNesting");
            maxNesting.Value = settings.FormulaComplexityMaxDepth.ToString();
            var maxOperations = new XElement("maxOperations");
            maxOperations.Value = settings.FormulaComplexityMaxOperations.ToString();
            formulaRule.Add(maxNesting);
            formulaRule.Add(maxOperations);

            return formulaRule;
        }

        #region private class methods

        private string NullCheck(string content)
        {
            return content != null ? content : string.Empty;
        }

        #endregion

        #region private workbook model visitor methods

        private XElement CreateSanityElements(WorkbookModel n)
        {
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
        ///     Creats a part of sprudel with the data fo a scenario
        /// </summary>
        /// <param name="n">Scenario</param>
        /// <returns>XElement</returns>
        public object Visit(Scenario n)
        {
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

        public object Visit(Rule n)
        {
            var rule = new XElement("rule");

            rule.Add(new XElement("name", n.Title));
            rule.Add(new XElement("description", n.Conditions));

            rule.Add(CreateRuleData(n));
            rule.Add(CreateRuleCondition(n));

            return rule;
        }

        private XElement CreateRuleData(Rule n)
        {
            var inputs = new XElement("ruleCells");
            foreach (var test in n.RuleCells)
                if (test.Value.Equals(""))
                {
                    var inputElement = new XElement("ruleCell");
                    inputElement.Add(new XElement("target", test.Target));
                    inputElement.Add(new XElement("type", test.Type.ToString()));
                    inputElement.Add(new XElement("value", test.Value));
                    inputs.Add(inputElement);
                }
            return inputs;
        }

        private XElement CreateRuleCondition(Rule n)
        {
            var inputs = new XElement("ruleConditions");
            foreach (var test in n.Conditions)
            {
                var inputElement = new XElement("ruleCondition");
                inputElement.Add(new XElement("conditionName", test.Name));
                inputElement.Add(new XElement("conditionType", test.Type.ToString()));
                inputElement.Add(new XElement("conditionValue", test.Value));
                inputs.Add(inputElement);
            }
            return inputs;
        }


        private XElement CreateInputs(Scenario n)
        {
            var inputs = new XElement("inputs");
            foreach (var test in n.Inputs)
                if (!test.Value.Equals(""))
                {
                    var inputElement = new XElement("input");
                    inputElement.Add(new XElement("target", test.Target));
                    inputElement.Add(new XElement("type", test.Type.ToString()));
                    inputElement.Add(new XElement("value", test.Value));
                    inputs.Add(inputElement);
                }
            return inputs;
        }

        private XElement CreateInvariants(Scenario n)
        {
            var invariants = new XElement("invariants");
            foreach (var invariant in n.Invariants)
            {
                var invariantElement = new XElement("invariant");
                invariantElement.Add(new XElement("target", invariant.Target));
                invariants.Add(invariantElement);
            }
            return invariants;
        }

        private XElement CreateConditions(Scenario n)
        {
            var conditions = new XElement("conditions");
            foreach (var condition in n.Conditions)
                if (!condition.Value.Equals(""))
                {
                    var conditionElement = new XElement("condition");
                    conditionElement.Add(new XElement("operator", condition.Operator.ToString()));
                    conditionElement.Add(new XElement("target", condition.Target));
                    conditionElement.Add(new XElement("type", condition.Type.ToString()));
                    conditionElement.Add(new XElement("value", condition.Value));
                    conditions.Add(conditionElement);
                }
            return conditions;
            ;
        }

        #endregion

        #region not implemented

        public object Visit(InputData n)
        {
            throw new NotImplementedException();
        }

        public object Visit(ScenarioData n)
        {
            throw new NotImplementedException();
        }

        public object Visit(ConditionData n)
        {
            throw new NotImplementedException();
        }

        public object Visit(Cell n)
        {
            throw new NotImplementedException();
        }

        public object Visit(Condition n)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}