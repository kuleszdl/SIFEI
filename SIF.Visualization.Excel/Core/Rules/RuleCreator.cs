using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel.Core.Rules
{
    class RuleCreator
    {
        #region Singelton
        private static volatile RuleCreator instance;
        private static object syncRoot = new Object();

        private RuleCreator()
        {

        }

        public static RuleCreator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RuleCreator();
                    }
                }
                return instance;
            }
        }

        #endregion

        #region Fields
        private Rule currentRule;
        private static Object syncRule = new Object();

        #endregion

        #region Methods

        public void BlankStart()
        {
            if (currentRule != null)
                return;
            lock(syncRule)
            {
                currentRule = new Rule {
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
            List<Condition> condition = currentRule.Conditions.ToList();
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
            {
                try
                {
                    RuleCells ruleCells = new RuleCells(c.Location);
                    currentRule.RuleCells.Add(ruleCells);
                }
                catch
                {
                    
                }
            }
            DataModel.Instance.CurrentWorkbook.RuleCells.Clear();
        }

        public Rule AddRegexCondition(string name, string value)
        {
            Condition newCondition;
            newCondition = new Condition {
                Type = Condition.ConditionType.Regex,
                Value = value,
                Name = name
            };
            currentRule.Conditions.Add(newCondition);
            return currentRule;
        }

        public Rule AddCharacterCondition(string name, string value)
        {
            Condition newCondition;
            newCondition = new Condition {
                Type = Condition.ConditionType.CharacterCount,
                Value = value,
                Name = name
            };
            currentRule.Conditions.Add(newCondition);
            return currentRule;
        }

        public int GetEmptyRuleDataCount()
        {
            if (currentRule == null)
                return 0;
            else
                return (from q in currentRule.RuleCells where q.Value.Equals("") select q).ToList().Count;
        }

        public Rule End()
        {
            if (currentRule == null)
                return null;
           
            var resultRule = this.currentRule as Rule;
            
            lock (syncRule)
            {
                this.currentRule = null;

                if (resultRule.RuleCells.Count == 0)
                {
                    return null;
                }
                return resultRule;
                
            }
        }

        
        #endregion

        
    }
}
