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
        private List<RuleDataFieldContainer> containers = new List<RuleDataFieldContainer>();
        private Workbook workbook;
        private Rule newRule;
        private static Object syncRule = new Object();

        #endregion

        #region Methods
        public void Start(WorkbookModel wb, string ruleTitle, int rows)
        {
            if (newRule != null)
                return;
            lock (syncRule)
            {
                newRule = new Rule
                {
                    Title = ruleTitle
                    //Date?
                };
            }
            
            
                     
            for (int i = rows; i > 0; i --)
            {
                // condition type check 
                // transform into regex
                 newRule.Conditions.Add(condition);
            }
                
                
                    
                    
            

            foreach (var c in DataModel.Instance.CurrentWorkbook.RuleCells)
            {
                RuleData ruleData = new RuleData(c.Location);
            //  registerContainer(c, ruleData);
                newRule.RuleData.Add(ruleData);
            }

            

            

        }

        private void registerContainer(Cell c, RuleData ruleData)
        {
            var currentWorksheet = workbook.Sheets[c.WorksheetKey] as Worksheet;
            var vsto = Globals.Factory.GetVstoObject(currentWorksheet);

            
        }

        #endregion

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
                workbook = null;
                newRule = null;

                if (resultRule.RuleData.Count == 0)
                {
                    return null;
                }
                return resultRule;
            }
        }

        public Condition condition { get; set; }
    }
}
