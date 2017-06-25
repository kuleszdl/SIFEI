using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Rule newRule;
        private static Object syncRule = new Object();

        #endregion

        #region Methods

        public void BlankStart()
        {
            if (newRule != null)
                return;
            lock(syncRule)
            {
                newRule = new Rule { };
            }
        }

        public Rule GetRule()
        {
            if (newRule != null)
                return newRule;
            return null;
        }

        public List<Condition> GetCondition()
        {
            List<Condition> condition = newRule.Conditions.ToList();
            return condition;
        }

        public void SetProperties(string ruleTitle, string description)
        {
            if (newRule != null)
                return;
            lock (syncRule)
            {
                newRule.Title = ruleTitle;
                newRule.Description = description;
                //Date?
            }            
        }

        public void SetRuleCells(WorkbookModel wb)
        {
            foreach (var c in DataModel.Instance.CurrentWorkbook.RuleCells)
            {
                try
                {
                    RuleData ruleData = new RuleData(c.Location);
                    newRule.RuleData.Add(ruleData);
                    DataModel.Instance.CurrentWorkbook.RuleCells.Clear();
                }
                catch
                {
                    
                }
            }
        }

        public Rule AddRegexCondition(string name, string value)
        {
            Condition newCondition;
            newCondition = new Condition {
                Type = Condition.ConditionType.Regex,
                Value = value,
                Name = name
            };
            newRule.Conditions.Add(newCondition);
            return newRule;
        }

        public Rule AddCharacterCondition(string name, string value)
        {
            Condition newCondition;
            newCondition = new Condition {
                Type = Condition.ConditionType.CharacterCount,
                Value = value,
                Name = name
            };
            newRule.Conditions.Add(newCondition);
            return newRule;
        }

        

        public int GetEmptyRuleDataCount()
        {
            if (newRule == null)
                return 0;
            else
                return (from q in newRule.RuleData where q.Value.Equals("") select q).ToList().Count;
        }

        public Rule End()
        {
            if (newRule == null)
                return null;
           
            var resultRule = newRule;

            lock (syncRule)
            {
                // workbook = null;
                newRule = null;

                if (resultRule.RuleData.Count == 0)
                {
                    return null;
                }
                return resultRule;
            }
        }

        
        #endregion

        
    }
}
