using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Tools.Ribbon;
using SIF.Visualization.Excel.Core;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using SIF.Visualization.Excel.Properties;
using System.Diagnostics;
using SIF.Visualization.Excel.Cells;
using System.Windows.Controls.Primitives;

namespace SIF.Visualization.Excel
{
    public partial class Ribbon
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            DataModel.Instance.WorkbookSelectionChangedEventHandler += Ribbon_WorkbookSelectionChanged;
            DataModel.Instance.CellDefinitionChangedEventHandler += Ribbon_WorkbookSelectionChanged;
        }

        private void testButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Inspect the current workbook
            DataModel.Instance.CurrentWorkbook.Inspect();

            // close the scenario creation panes
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Define Cells")].Visible = false;
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenarios")].Visible = false;
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenario Details")].Visible = false;

            // Open the findings pane
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Findings")].Visible = true;
        }

        private void StaticScan_Click(object sender, RibbonControlEventArgs e)
        {
            // Inspect the current workbook
            DataModel.Instance.CurrentWorkbook.Inspect(WorkbookModel.InspectionMode.Static);

            // close the scenario creation panes
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Define Cells")].Visible = false;
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenarios")].Visible = false;
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenario Details")].Visible = false;

            // Open the findings pane
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Findings")].Visible = true;
        }

        private void DynamicScan_Click(object sender, RibbonControlEventArgs e)
        {
            // Inspect the current workbook
            DataModel.Instance.CurrentWorkbook.Inspect(WorkbookModel.InspectionMode.Dynamic);

            // close the scenario creation panes
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Define Cells")].Visible = false;
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenarios")].Visible = false;
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenario Details")].Visible = false;

            // Open the findings pane
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Findings")].Visible = true;
        }


        private void findingsPaneButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Find the correct task pane for the currently active workbook
            Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Findings")].Visible = true;
        }

        private void scenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Find the correct task pane for the currently active workbook
            var pane = Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenarios")];

            pane.Visible = !pane.Visible;

        }

        private void clearButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Remove all controls from this workbook
            var worksheets = DataModel.Instance.CurrentWorkbook.Workbook.Worksheets;
            foreach (Worksheet worksheet in worksheets)
            {
                var vsto = Globals.Factory.GetVstoObject(worksheet);
                for (int i = vsto.Controls.Count - 1; i >= 0; i--)
                {
                    var control = vsto.Controls[i];
                    if (control.GetType() == typeof(CellErrorInfoContainer))
                        vsto.Controls.Remove(control);
                }
            }

            DataModel.Instance.CurrentWorkbook.Findings.Clear();
            //DataModel.Instance.CurrentWorkbook.InputCells.Clear();
            //DataModel.Instance.CurrentWorkbook.IntermediateCells.Clear();
            //DataModel.Instance.CurrentWorkbook.OutputCells.Clear();
        }

        /// <summary>
        /// Define a input cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineInputCell_Click(object sender, RibbonControlEventArgs e)
        {
            WorkbookModel.CellDefinitionOption option;
            var currentWorkbook = DataModel.Instance.CurrentWorkbook;
            var selectedCells = CellManager.Instance.GetSelectedCells(currentWorkbook);

            if (!currentWorkbook.InputCells.Contains(selectedCells.First()))
            {
                option = WorkbookModel.CellDefinitionOption.Define;
            }
            else
            {
                option = WorkbookModel.CellDefinitionOption.Undefine;
            }

            currentWorkbook.DefineInputCell(selectedCells, option);
        }

        /// <summary>
        /// Define a intermediate cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineIntermediateCell_Click(object sender, RibbonControlEventArgs e)
        {
            WorkbookModel.CellDefinitionOption option;
            var currentWorkbook = DataModel.Instance.CurrentWorkbook;
            var selectedCells = CellManager.Instance.GetSelectedCells(currentWorkbook);

            if (!currentWorkbook.IntermediateCells.Contains(selectedCells.First()))
            {
                option = WorkbookModel.CellDefinitionOption.Define;
            }
            else
            {
                option = WorkbookModel.CellDefinitionOption.Undefine;
            }

            currentWorkbook.DefineIntermediateCell(selectedCells, option);
        }

        /// <summary>
        /// Define a result cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineResultCell_Click(object sender, RibbonControlEventArgs e)
        {
            WorkbookModel.CellDefinitionOption option;
            var currentWorkbook = DataModel.Instance.CurrentWorkbook;
            var selectedCells = CellManager.Instance.GetSelectedCells(currentWorkbook);

            if (!currentWorkbook.OutputCells.Contains(selectedCells.First()))
            {
                option = WorkbookModel.CellDefinitionOption.Define;
            }
            else
            {
                option = WorkbookModel.CellDefinitionOption.Undefine;
            }

            currentWorkbook.DefineOutputCell(selectedCells, option);
        }

        /// <summary>
        /// Will be registert to the workbook selection changed event
        /// </summary>
        /// <param name="sh"></param>
        /// <param name="target"></param>
        private void Ribbon_WorkbookSelectionChanged(object sh, Range target)
        {
            this.WorkbookSelectionChanged();
        }

        private void Ribbon_WorkbookSelectionChanged(object sender, EventArgs data)
        {
            this.WorkbookSelectionChanged();
        }

        /// <summary>
        /// Updates the toggle buttons
        /// </summary>
        private void WorkbookSelectionChanged()
        {
            var firstSelectedCell = CellManager.Instance.GetFirstSelectedCell(DataModel.Instance.CurrentWorkbook);

            //set input cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.InputCells.Contains(firstSelectedCell))
            {
                this.inputCellToggleButton.Checked = true;
            }
            else
            {
                this.inputCellToggleButton.Checked = false;
            }

            //set intermediate cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.IntermediateCells.Contains(firstSelectedCell))
            {
                this.intermediateCellToggleButton.Checked = true;
            }
            else
            {
                this.intermediateCellToggleButton.Checked = false;
            }

            //set intermediate cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.OutputCells.Contains(firstSelectedCell))
            {
                this.resultCellToggleButton.Checked = true;
            }
            else
            {
                this.resultCellToggleButton.Checked = false;
            }

        }

        private void DefineCells_Click(object sender, RibbonControlEventArgs e)
        {
            var pane = Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Define Cells")];

            pane.Visible = !pane.Visible;
        }

        private void NewScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // set scenario buttons styles
            SetScenarioCreationButtonStyles(true);

            // set 

            // start scenario creation
            ScenarioCore.ScenarioUICreator.Instance.Start(DataModel.Instance.CurrentWorkbook);


        }

        private void SubmitScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // validate and show warnings
            #region validate
            var emptyInputs = ScenarioCore.ScenarioUICreator.Instance.GetEmptyEntrysCount(typeof(Cells.InputCell));
            var emptyIntermediates = ScenarioCore.ScenarioUICreator.Instance.GetEmptyEntrysCount(typeof(Cells.IntermediateCell));
            var emptyResults = ScenarioCore.ScenarioUICreator.Instance.GetEmptyEntrysCount(typeof(Cells.OutputCell));

            if (ScenarioCore.ScenarioUICreator.Instance.NoValue(typeof(Cells.InputCell)))
            {
                //message for no result cell values
                MessageBox.Show("A scenario needs at least one input cell value.", "error", MessageBoxButtons.OK);

                //back to the scenario editor
                return;
            }
            else if (ScenarioCore.ScenarioUICreator.Instance.NoValue(typeof(Cells.IntermediateCell)) 
                     && ScenarioCore.ScenarioUICreator.Instance.NoValue(typeof(Cells.OutputCell)))
            {
                //message for no input cell values
                MessageBox.Show("A scenario needs at least one result cell value or one intermediate cell value.", "error", MessageBoxButtons.OK);

                //back to the scenario editor
                return;
            }
            else if (emptyInputs > 0 | emptyIntermediates > 0 | emptyResults > 0)
            {
                // message for some empty fields
                #region create message
                var messageList = new List<Tuple<string, int>>();
                if (emptyInputs > 0) messageList.Add(new Tuple<string, int>("input cells", emptyInputs));
                if (emptyIntermediates > 0) messageList.Add(new Tuple<string, int>("intermediate cells", emptyIntermediates));
                if (emptyResults > 0) messageList.Add(new Tuple<string, int>("result cells", emptyResults));

                var message = new StringBuilder();
                message.Append("Maybe your scenario isn't complete. ");
                message.Append("The scenario has ");
                foreach (var p in messageList)
                {
                    message.Append(p.Item2 + " empty fields for " + p.Item1);
                    if (messageList.IndexOf(p) < messageList.Count - 2)
                    {
                        message.Append(", ");
                    }
                    else if (messageList.IndexOf(p) == messageList.Count - 2)
                    {
                        message.Append(" and ");
                    }
                }
                message.Append(".");

                #endregion

                var result = MessageBox.Show(
                    message.ToString(),
                    "warning",
                    MessageBoxButtons.OKCancel);

                //back to the scenario editor
                if (result == DialogResult.Cancel) return;
            }
            #endregion

            // end scenario creation
            var newScenario = ScenarioCore.ScenarioUICreator.Instance.End();

            if (newScenario != null)
            {
                DataModel.Instance.CurrentWorkbook.Scenarios.Add(newScenario);

                // Open scenario pane
                // Find the correct task pane for the currently active workbook
                var pane = Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Scenarios")];
                pane.Visible = true;
            }

            // set button styles
            SetScenarioCreationButtonStyles(false);
        }

        private void cancelScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // end scenario creation
            ScenarioCore.ScenarioUICreator.Instance.End();

            // set button styles
            SetScenarioCreationButtonStyles(false);
        }

        /// <summary>
        /// Disables or aktivates ribbon buttons, if the scenario creating process is started or completed
        /// </summary>
        /// <param name="create">true, if the scenario creating process is started now, else false</param>
        private void SetScenarioCreationButtonStyles(bool create)
        {
            // set scenario buttons styles
            this.submitScenarioButton.Visible = create;
            this.cancelScenarioButton.Visible = create;
            this.newScenarioButton.Enabled = !create;

            // set define cells buttons styles
            this.inputCellToggleButton.Enabled = !create;
            this.intermediateCellToggleButton.Enabled = !create;
            this.resultCellToggleButton.Enabled = !create;
        }

    }
}
