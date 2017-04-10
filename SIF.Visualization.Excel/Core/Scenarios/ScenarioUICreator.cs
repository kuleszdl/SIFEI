using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.View;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SIF.Visualization.Excel.Core.Scenarios {

    public class ScenarioUICreator {

        #region Singleton

        private static volatile ScenarioUICreator instance;
        private static object syncRoot = new Object();

        private ScenarioUICreator() {}

        /// <summary>
        /// Gets the current ScenarioUICreator instance.
        /// </summary>
        public static ScenarioUICreator Instance {
            get {
                if (instance == null) {
                    lock (syncRoot) {
                        if (instance == null)
                            instance = new ScenarioUICreator();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Fields

        private List<ScenarioDataFieldContainer> containers = new List<ScenarioDataFieldContainer>();
        private Workbook workbook;
        private Scenario newScenario;
        private static object syncScenario = new Object();

        #endregion

        #region Methods
        public void Start(WorkbookModel wb, string scenarioTitle) {
            if (newScenario != null) return;
            lock (syncScenario) {
                newScenario = new Scenario {
                    Title = scenarioTitle,
                    CreationDate = DateTime.Now,
                };

            }
            workbook = wb.Workbook;

            var workingList = wb.ScenarioCells.ToList();

            //sort working list column first
            #region sort

            workingList.Sort(delegate(Cell x, Cell y) {
                //sort by worksheet
                var xSheet = workbook.Sheets[x.WorksheetKey] as Worksheet;
                var ySheet = workbook.Sheets[y.WorksheetKey] as Worksheet;

                if (xSheet.Index < ySheet.Index) {
                    return -1;
                }
                if (xSheet.Index > ySheet.Index) {
                    return 1;
                }
                //sort by column
                var xRange = xSheet.Range[x.ShortLocation];
                var yRange = ySheet.Range[y.ShortLocation];

                if (xRange.Column < yRange.Column) {
                    return -1;
                }
                if (xRange.Column > yRange.Column) {
                    return 1;
                }
                //sort by row
                if (xRange.Row < yRange.Row) {
                    return -1;
                }
                return xRange.Row > yRange.Row ? 1 : 0;
            });

            #endregion

            foreach (var c in DataModel.Instance.CurrentWorkbook.ScenarioCells) {
                switch (c.ScenarioCellType) {
                    case ScenarioCellType.INPUT:
                        InputData inputData = new InputData(c.Location);
                        createContainer(c, inputData);
                        newScenario.Inputs.Add(inputData);
                        break;
                    case ScenarioCellType.INVARIANT:
                        InvariantData invariantData = new InvariantData(c.Location);
                        newScenario.Invariants.Add(invariantData);
                        break;
                    case ScenarioCellType.CONDITION:
                        ConditionData conditionData = new ConditionData(c.Location);
                        createContainer(c, conditionData);
                        newScenario.Conditions.Add(conditionData);
                        break;
                }
            }

            //set focus to first control
            if (containers.Count > 0) {
                foreach (var c in containers) {
                    c.ScenarioDataField.RegisterNextFocusField(c.ScenarioDataField);
                }
                containers.First().ScenarioDataField.SetFocus();
            }

        }

        private void createContainer(Cell c, object cellData) {
            //create container
            var container = new ScenarioDataFieldContainer();
            container.ScenarioDataField.DataContext = cellData;
            containers.Add(container);
            //get worksheet
            var currentWorksheet = workbook.Sheets[c.WorksheetKey] as Worksheet;
            var vsto = Globals.Factory.GetVstoObject(currentWorksheet);
            //create control
            var control = vsto.Controls.AddControl(container, currentWorksheet.Range[c.ShortLocation], Guid.NewGuid().ToString());
        }

        /// <summary>
        /// Calculates the number of empty controls of a cell type while the scenario creation process.
        /// </summary>
        /// <param name="cellType">Class type of Cells.InputCell, Cells.IntermediateCell or Cells.OutputCell</param>
        /// <returns>Number of empty controls</returns>
        public int GetEmptyInputsCount() {
            if (newScenario == null)
                return 0;
            else 
                return (from q in newScenario.Inputs where q.Value.Equals("") select q).ToList().Count;
        }

        public int GetEmptyConditionsCount() {
            if (newScenario == null)
                return 0;
            else
                return (from q in newScenario.Conditions where q.Value.Equals("") select q).ToList().Count;
        }

        public Scenario End() {
            if (newScenario == null) return null;

            //delete data contexts of the containers
            foreach (var c in containers) {
                c.ScenarioDataField.DataContext = null;
            }

            //destroy controls
            foreach (Worksheet ws in workbook.Worksheets) {
                var vsto = Globals.Factory.GetVstoObject(ws);
                for (int i = vsto.Controls.Count - 1; i >= 0; i--) {
                    var control = vsto.Controls[i];
                    if (control.GetType() == typeof(ScenarioDataFieldContainer))
                        vsto.Controls.Remove(control);
                }
            }

            // end up and clear
            var resultScenario = newScenario;

            lock (syncScenario) {
                //clear this object
                containers.Clear();
                workbook = null;
                newScenario = null;

                if (resultScenario.Inputs.Count == 0 &&
                    resultScenario.Invariants.Count == 0 &&
                    resultScenario.Conditions.Count == 0) {
                    return null;
                }
                return resultScenario;
            }
        }
        #endregion

    }  
}