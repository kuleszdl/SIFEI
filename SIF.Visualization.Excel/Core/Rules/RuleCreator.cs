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
                if (Instance == null)
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
        public void Start(WorkbookModel wb, string ruleTitle)
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
            workbook = wb.Workbook;
            var workingList = wb.RuleCells.ToList();

            foreach (var c in DataModel.Instance.CurrentWorkbook.RuleCells)
            {
                RuleData ruleData = new RuleData(c.Location);
                createContainer(c, ruleData);
                newRule.RuleData.Add(ruleData);
            }

            //set focus 
            //if (containers.Count > 0)
            //{
            //    foreach (var c in containers)
            //    {
            //        c.RuleDataField.RegisternextFocusField(c.RuleDataField);
            //    }
            //    containers.First().RuleDataField.SetFocus();
            //}
        }

        private void createContainer(Cell c, object cellData)
        {
            //var container = new RuleDataFieldContainer();
            //container.RuleDataField.DataContext = cellData;
            //containers.Add(container);

            //var currentWorksheet = workbook.Sheets[c.WorksheetKey] as Worksheet;
            //var vsto = Globals.Factory.GetVstoObject(currentWorksheet);

            //var control = vsto.Controls.AddControl(container, currentWorksheet.Range[c.ShortLocation], Guid.NewGuid().ToString());
        }
        #endregion
    }
}
