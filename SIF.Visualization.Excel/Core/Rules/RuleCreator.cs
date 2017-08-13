using System.Collections.Generic;
using System.Linq;

namespace SIF.Visualization.Excel.Core.Rules
{
    internal class RuleCreator
    {
        #region Singelton

        private static volatile RuleCreator instance;
        private static readonly object syncRoot = new object();
        public bool edited = false;

        private RuleCreator()
        {
        }

        public static RuleCreator Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RuleCreator();
                    }
                return instance;
            }
        }

        #endregion

        #region Fields

        private Rule currentRule;
        private static readonly object syncRule = new object();

        #endregion

        #region Methods

        public void BlankStart()
        {
            if (currentRule != null)
                return;
            lock (syncRule)
            {
                currentRule = new Rule
                {
                    Title = "",
                    Conditions = null,
                    Description = "",
                    RuleCells = null
                };
            }
        }

        public void OpenRule(Rule rule)
        {
            lock (syncRule)
            {
                currentRule = rule;
                currentRule.Conditions = rule.Conditions;
                currentRule.Description = rule.Description;
                currentRule.RuleCells = rule.RuleCells;
                currentRule.Title = rule.Title;
            }
        }

        public Rule GetRule()
        {
            if (currentRule != null)
                return currentRule;
            return null;
        }

        public List<Condition> GetCondition()
        {
            var condition = currentRule.Conditions.ToList();
            return condition;
        }

        public void SetProperties(string ruleTitle, string description)
        {
            if (currentRule == null)
                return;
            lock (syncRule)
            {
                currentRule.Title = ruleTitle;
                currentRule.Description = description;
                //Date?
            }
        }

        public void SetRuleCells(WorkbookModel wb)
        {
            currentRule.RuleCells.Clear();

            foreach (var c in DataModel.Instance.CurrentWorkbook.RuleCells)
                try
                {
                    var ruleCells = new RuleCells(c.Location);
                    currentRule.RuleCells.Add(ruleCells);
                }
                catch
                {
                }
            DataModel.Instance.CurrentWorkbook.RuleCells.Clear();
        }

        public Rule AddRegexCondition(string name, string value)
        {
            Condition newCondition;
            newCondition = new Condition
            {
                Type = Condition.ConditionType.Regex,
                Value = value,
                Name = name
            };
            currentRule.Conditions.Add(newCondition);
            return currentRule;
        }

        public Rule AddEmptyCondition(string name)
        {
            Condition newCondition;
            newCondition = new Condition
            {
                Type = Condition.ConditionType.Empty,
                Value = "^$",
                Name = name
            };
            currentRule.Conditions.Add(newCondition);
            return currentRule;
        }

        public Rule AddCharacterCondition(string name, string value)
        {
            Condition newCondition;
            newCondition = new Condition
            {
                Type = Condition.ConditionType.CharacterCount,
                Value = value,
                Name = name
            };
            currentRule.Conditions.Add(newCondition);
            return currentRule;
        }

        public Rule AddOnlyNumbersCondition(string name)
        {
            Condition newCondition;
            newCondition = new Condition
            {
                Type = Condition.ConditionType.Regex,
                Value = "([0-9])*",
                Name = name
            };
            currentRule.Conditions.Add(newCondition);
            return currentRule;
        }

        public int GetEmptyRuleDataCount()
        {
            if (currentRule == null)
                return 0;
            return (from q in currentRule.RuleCells where q.Value.Equals("") select q).ToList().Count;
        }

        public Rule End()
        {
            if (currentRule == null)
                return null;

            var resultRule = currentRule;

            lock (syncRule)
            {
                if (edited == true)
                    DataModel.Instance.CurrentWorkbook.Rules.Remove(currentRule);
                currentRule = null;

                if (resultRule.RuleCells.Count == 0)
                    return null;
                return resultRule;
            }
        }

        #endregion
    }
}