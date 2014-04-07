using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.ScenarioView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        private Scenario newScenario = null;
        private static object syncScenario = new Object();

        #endregion

        #region Methods
        public void Start(Core.WorkbookModel wb)
        {
            if (this.newScenario != null) return;
            lock (syncScenario)
            {
                if (this.newScenario != null) return;
                this.newScenario = new Scenario()
                    {
                        Title = "Untiteled Scenario - " + DateTime.Now.ToString(),
                        CrationDate = DateTime.Now,
                        Author = this.GetDocumentProperty(wb, "Last Author")
                    };

            }
            this.workbook = wb.Workbook;

            var workingList = wb.InputCells.Union(wb.IntermediateCells).Union(wb.OutputCells).ToList();

            //sort working list column first
            #region sort

            workingList.Sort(delegate(Core.Cell x, Core.Cell y)
            {
                //sort by worksheet
                var xSheet = workbook.Sheets[Cells.CellManager.Instance.ParseWorksheetName(x.Location)] as Worksheet;
                var ySheet = workbook.Sheets[Cells.CellManager.Instance.ParseWorksheetName(x.Location)] as Worksheet;

                if (xSheet.Index < ySheet.Index)
                {
                    return -1;
                }
                else if (xSheet.Index > ySheet.Index)
                {
                    return 1;
                }
                else
                {
                    //sort by column
                    var xRange = xSheet.Range[Cells.CellManager.Instance.ParseCellLocation(x.Location)];
                    var yRange = ySheet.Range[Cells.CellManager.Instance.ParseCellLocation(y.Location)];

                    if (xRange.Column < yRange.Column)
                    {
                        return -1;
                    }
                    else if (xRange.Column > yRange.Column)
                    {
                        return 1;
                    }
                    else
                    {
                        //sort by row
                        if (xRange.Row < yRange.Row)
                        {
                            return -1;
                        }
                        else if (xRange.Row > yRange.Row)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                }
                
            });

            #endregion

            CreateScenarioDataFieldContainer containerFirst = null;
            CreateScenarioDataFieldContainer containerBefore = null;
            foreach (var c in workingList)
            {
                //create cell data
                CellData cellData;

                #region create cell data
                if (c is Cells.InputCell)
                {
                    cellData = new InputCellData();
                    cellData.Location = c.Location;
                    cellData.SifLocation = c.SifLocation;
                    newScenario.Inputs.Add(cellData as InputCellData);
                }
                else if (c is Cells.IntermediateCell)
                {
                    cellData = new IntermediateCellData();
                    cellData.Location = c.Location;
                    cellData.SifLocation = c.SifLocation;
                    newScenario.Intermediates.Add(cellData as IntermediateCellData);
                }
                else if (c is Cells.OutputCell)
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
                        this.containers.Clear();
                        this.workbook = null;
                        this.newScenario = null;

                        return;
                    }
                }

                #endregion

                //get worksheet
                var currentWorksheet = workbook.Sheets[Cells.CellManager.Instance.ParseWorksheetName(c.Location)] as Worksheet;
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
                    currentWorksheet.Range[Cells.CellManager.Instance.ParseCellLocation(c.Location)],
                    Guid.NewGuid().ToString());
                control.Placement = Microsoft.Office.Interop.Excel.XlPlacement.xlMove;
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

            if (cellType == typeof(Cells.InputCell))
            {
                var emptyInputs = (from q in newScenario.Inputs
                                   where q.Content == null
                                   select q).ToList().Count;
                return emptyInputs;
            }
            else if (cellType == typeof(Cells.IntermediateCell))
            {
                var emptyIntermediates = (from q in newScenario.Intermediates
                                          where q.Content == null
                                          select q).ToList().Count;
                return emptyIntermediates;
            }
            else if (cellType == typeof(Cells.OutputCell))
            {
                var emptyResults = (from q in newScenario.Results
                                    where q.Content == null
                                    select q).ToList().Count;
                return emptyResults;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Checks if a filed of a result cell is filled while the scenario creation process.
        /// </summary>
        /// <param name="cellType">Class type of Cells.InputCell, Cells.IntermediateCell or Cells.OutputCell</param>
        /// <returns>True, if one field of a result cell is filled. Else false.</returns>
        public bool NoValue(Type cellType)
        {
            if (newScenario == null) return true;

            if (cellType == typeof(Cells.InputCell))
            {
                var noFilledInputs = (from q in newScenario.Inputs
                                       where q.Content != null
                                       select q).ToList().Count <= 0;

                return noFilledInputs;
            }
            else if (cellType == typeof(Cells.IntermediateCell))
            {
                var noFilledIntermediates = (from q in newScenario.Intermediates
                                             where q.Content != null
                                             select q).ToList().Count <= 0;

                return noFilledIntermediates;
            }
            else if (cellType == typeof(Cells.OutputCell))
            {
                var noFilledResults = (from q in newScenario.Results
                                       where q.Content != null
                                       select q).ToList().Count <= 0;

                return noFilledResults;
            }
            else
            {
                return true;
            }
        }

        public Scenario End()
        {
            if (this.newScenario == null) return null;

            //delete data contexts of the containers
            foreach (var c in this.containers)
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
            var resultScenario = this.newScenario;

            lock (syncScenario)
            {
                //clear this object
                this.containers.Clear();
                this.workbook = null;
                this.newScenario = null;

                if (resultScenario.Inputs.Count == 0 &&
                    resultScenario.Intermediates.Count == 0 &&
                    resultScenario.Results.Count == 0)
                {
                    return null;
                }
                else
                {
                    return resultScenario;
                }
            }
        }

        private string GetDocumentProperty(Core.WorkbookModel n, string propertyName)
        {
            var properties = (Microsoft.Office.Core.DocumentProperties)n.Workbook.BuiltinDocumentProperties;
            string value;
            try
            {
                value = properties[propertyName].Value.ToString();
            }
            catch (Exception e)
            {
                value = String.Empty;
            }

            return value;
        }


        #endregion
    }

}
