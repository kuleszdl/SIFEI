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
            // set button styles
            this.submitScenarioButton.Visible = true;
            this.cancelScenarioButton.Visible = true;
            this.newScenarioButton.Enabled = false;

            // start scenario creation
            ScenarioCore.ScenarioUICreator.Instance.Start(DataModel.Instance.CurrentWorkbook);


        }

        private void SubmitScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
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
            else
            {
                MessageBox.Show("You can't create a empty scenario.", "Error");

            }

            // set button styles
            this.submitScenarioButton.Visible = false;
            this.cancelScenarioButton.Visible = false;
            this.newScenarioButton.Enabled = true;
        }

        private void cancelScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // end scenario creation
            ScenarioCore.ScenarioUICreator.Instance.End();

            // set button styles
            this.submitScenarioButton.Visible = false;
            this.cancelScenarioButton.Visible = false;
            this.newScenarioButton.Enabled = true;
        }

        
    }
}
