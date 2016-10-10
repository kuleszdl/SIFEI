using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioView;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public class ScenarioUICreator
    {

        #region Singleton

        private static volatile ScenarioUICreator instance;
        private static object syncRoot = new Object();

        private ScenarioUICreator()
        {
        }

        /// <summary>
        /// Gets the current ScenarioUICreator instance.
        /// </summary>
        public static ScenarioUICreator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new ScenarioUICreator();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Fields

        private List<CreateScenarioDataFieldContainer> containers = new List<CreateScenarioDataFieldContainer>();
        private Workbook workbook;
        private Scenario newScenario;
        private static object syncScenario = new Object();

        #endregion

        #region Methods
        public void Start(WorkbookModel wb, string scenarioTitle)
        {
            if (newScenario != null) return;
            lock (syncScenario)
            {
                if (newScenario != null) return;

                newScenario = new Scenario
                {
                        Title = scenarioTitle,
                        CrationDate = DateTime.Now,
                        Author = GetDocumentProperty(wb, "Last Author")
                    };

            }
            workbook = wb.Workbook;

            var workingList = wb.InputCells.Union(wb.IntermediateCells).Union(wb.OutputCells).ToList();

            //sort working list column first
            #region sort

            workingList.Sort(delegate(Cell x, Cell y)
            {
                //sort by worksheet
                var xSheet = workbook.Sheets[CellManager.Instance.ParseWorksheetName(x.Location)] as Worksheet;
                var ySheet = workbook.Sheets[CellManager.Instance.ParseWorksheetName(x.Location)] as Worksheet;

                if (xSheet.Index < ySheet.Index)
                {
                    return -1;
                }
                if (xSheet.Index > ySheet.Index)
                {
                    return 1;
                }
                //sort by column
                var xRange = xSheet.Range[CellManager.Instance.ParseCellLocation(x.Location)];
                var yRange = ySheet.Range[CellManager.Instance.ParseCellLocation(y.Location)];

                if (xRange.Column < yRange.Column)
                {
                    return -1;
                }
                if (xRange.Column > yRange.Column)
                {
                    return 1;
                }
                //sort by row
                if (xRange.Row < yRange.Row)
                {
                    return -1;
                }
                return xRange.Row > yRange.Row ? 1 : 0;
            });

            #endregion

            CreateScenarioDataFieldContainer containerFirst = null;
            CreateScenarioDataFieldContainer containerBefore = null;
            foreach (var c in workingList)
            {
                //create cell data
                CellData cellData;

                #region create cell data
                if (c is InputCell)
                {
                    cellData = new InputCellData();
                    cellData.Location = c.Location;
                    cellData.SifLocation = c.SifLocation;
                    newScenario.Inputs.Add(cellData as InputCellData);
                }
                else if (c is IntermediateCell)
                {
                    cellData = new IntermediateCellData();
                    cellData.Location = c.Location;
                    cellData.SifLocation = c.SifLocation;
                    newScenario.Intermediates.Add(cellData as IntermediateCellData);
                }
                else if (c is OutputCell)
                {
                    cellData = new ResultCellData();
                    cellData.Location = c.Location;
                    cellData.SifLocation = c.SifLocation;
                    newScenario.Results.Add(cellData as ResultCellData);
                }
                else
                {
                    //abort
                    lock (syncScenario)
                    {
                        //clear this object
                        containers.Clear();
                        workbook = null;
                        newScenario = null;

                        return;
                    }
                }

                #endregion

                //get worksheet
                var currentWorksheet = workbook.Sheets[CellManager.Instance.ParseWorksheetName(c.Location)] as Worksheet;
                var vsto = Globals.Factory.GetVstoObject(currentWorksheet);

                //create container
                var container = new CreateScenarioDataFieldContainer();
                container.createScenarioDataField.DataContext = cellData;
                containers.Add(container);

                //register for focus handling
                #region focus handling

                if (c == workingList.First())
                {
                    containerFirst = container;
                }
                else if (containerBefore != null)
                {
                    containerBefore.createScenarioDataField.RegisterNextFocusField(container.createScenarioDataField);
                }
                containerBefore = container;

                #endregion

                //create control
                var control = vsto.Controls.AddControl(
                    container,
                    currentWorksheet.Range[CellManager.Instance.ParseCellLocation(c.Location)],
                    Guid.NewGuid().ToString());
                control.Placement = XlPlacement.xlMove;
            }

            //set focus to first control
            if (containerFirst != null)
            {
                containerFirst.createScenarioDataField.SetFocus();
            }

        }

        /// <summary>
        /// Calculates the number of empty controls of a cell type while the scenario creation process.
        /// </summary>
        /// <param name="cellType">Class type of Cells.InputCell, Cells.IntermediateCell or Cells.OutputCell</param>
        /// <returns>Number of empty controls</returns>
        public int GetEmptyEntrysCount(Type cellType)
        {
            if (newScenario == null) return 0;

            if (cellType == typeof(InputCell))
            {
                var emptyInputs = (from q in newScenario.Inputs
                                   where q.Content == null
                                   select q).ToList().Count;
                return emptyInputs;
            }
            if (cellType == typeof(IntermediateCell))
            {
                var emptyIntermediates = (from q in newScenario.Intermediates
                    where q.Content == null
                    select q).ToList().Count;
                return emptyIntermediates;
            }
            if (cellType == typeof(OutputCell))
            {
                var emptyResults = (from q in newScenario.Results
                    where q.Content == null
                    select q).ToList().Count;
                return emptyResults;
            }
            return 0;
        }

        /// <summary>
        /// Checks if a filed of a result cell is filled while the scenario creation process.
        /// </summary>
        /// <param name="cellType">Class type of Cells.InputCell, Cells.IntermediateCell or Cells.OutputCell</param>
        /// <returns>True, if one field of a result cell is filled. Else false.</returns>
        public bool NoValue(Type cellType)
        {
            if (newScenario == null) return true;

            if (cellType == typeof(InputCell))
            {
                var noFilledInputs = (from q in newScenario.Inputs
                                      where q.Content != null
                                      select q).ToList().Count <= 0;

                return noFilledInputs;
            }
            if (cellType == typeof(IntermediateCell))
            {
                var noFilledIntermediates = (from q in newScenario.Intermediates
                    where q.Content != null
                    select q).ToList().Count <= 0;

                return noFilledIntermediates;
            }
            if (cellType == typeof(OutputCell))
            {
                var noFilledResults = (from q in newScenario.Results
                    where q.Content != null
                    select q).ToList().Count <= 0;

                return noFilledResults;
            }
            return true;
        }

        public Scenario End()
        {
            if (newScenario == null) return null;

            //delete data contexts of the containers
            foreach (var c in containers)
            {
                c.createScenarioDataField.DataContext = null;
            }

            //destroy controls
            foreach (Worksheet ws in workbook.Worksheets)
            {
                var vsto = Globals.Factory.GetVstoObject(ws);
                for (int i = vsto.Controls.Count - 1; i >= 0; i--)
                {
                    var control = vsto.Controls[i];
                    if (control.GetType() == typeof(CreateScenarioDataFieldContainer))
                        vsto.Controls.Remove(control);
                }
            }

            // delete cell datas with out values
            #region delete cell datas
            //inputs
            var removeInputs = (from q in newScenario.Inputs
                                where q.Content == null
                                select q).ToList();
            foreach (var input in removeInputs)
            {
                newScenario.Inputs.Remove(input);
            }

            //intermediates
            var removeIntermediates = (from q in newScenario.Intermediates
                                       where q.Content == null
                                       select q).ToList();
            foreach (var intermediate in removeIntermediates)
            {
                newScenario.Intermediates.Remove(intermediate);
            }

            //results
            var removeResults = (from q in newScenario.Results
                                 where q.Content == null
                                 select q).ToList();
            foreach (var result in removeResults)
            {
                newScenario.Results.Remove(result);
            }

            #endregion

            // end up and clear
            var resultScenario = newScenario;

            lock (syncScenario)
            {
                //clear this object
                containers.Clear();
                workbook = null;
                newScenario = null;

                if (resultScenario.Inputs.Count == 0 &&
                    resultScenario.Intermediates.Count == 0 &&
                    resultScenario.Results.Count == 0)
                {
                    return null;
                }
                return resultScenario;
            }
        }

        private string GetDocumentProperty(WorkbookModel n, string propertyName)
        {
            var properties = (DocumentProperties)n.Workbook.BuiltinDocumentProperties;
            string value;
            try
            {
                value = properties[propertyName].Value.ToString();
            }
            catch (Exception e)
            {
                value = String.Empty;
                Console.WriteLine(e.Message);
            }

            return value;
        }


        #endregion
    }

}
