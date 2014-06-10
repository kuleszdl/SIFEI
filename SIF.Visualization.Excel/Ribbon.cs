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
        }

        private void warnings_Click(object sender, RibbonControlEventArgs e)
        {
            DataModel.Instance.CurrentWorkbook.SanityWarnings = sanityWarnCheckbox.Checked;
        }

        private void StaticScan_Click(object sender, RibbonControlEventArgs e)
        {
            // Inspect the current workbook
            DataModel.Instance.CurrentWorkbook.Inspect(WorkbookModel.InspectionMode.Static);
        }

        private void DynamicScan_Click(object sender, RibbonControlEventArgs e)
        {
            // Inspect the current workbook
            DataModel.Instance.CurrentWorkbook.Inspect(WorkbookModel.InspectionMode.Dynamic);
        }

        private void sharedPaneButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Find the correct task pane for the currently active workbook
            var pane = Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "shared Pane")];

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
        /// Define a SanityValue cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityValueCell_Click(object sender, RibbonControlEventArgs e)
        {
            WorkbookModel.CellDefinitionOption option;
            var currentWorkbook = DataModel.Instance.CurrentWorkbook;
            var selectedCells = CellManager.Instance.GetSelectedCells(currentWorkbook);

            if (!currentWorkbook.SanityValueCells.Contains(selectedCells.First()))
            {
                option = WorkbookModel.CellDefinitionOption.Define;
            }
            else
            {
                option = WorkbookModel.CellDefinitionOption.Undefine;
            }

            currentWorkbook.DefineSanityValueCell(selectedCells, option);
        }

        /// <summary>
        /// Define a SanityChecking cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityCheckingCell_Click(object sender, RibbonControlEventArgs e)
        {
            WorkbookModel.CellDefinitionOption option;
            var currentWorkbook = DataModel.Instance.CurrentWorkbook;
            var selectedCells = CellManager.Instance.GetSelectedCells(currentWorkbook);

            if (!currentWorkbook.SanityCheckingCells.Contains(selectedCells.First()))
            {
                option = WorkbookModel.CellDefinitionOption.Define;
            }
            else
            {
                option = WorkbookModel.CellDefinitionOption.Undefine;
            }

            currentWorkbook.DefineSanityCheckingCell(selectedCells, option);
        }

        /// <summary>
        /// Define a SanityExplanation cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityExplanationCell_Click(object sender, RibbonControlEventArgs e)
        {
            WorkbookModel.CellDefinitionOption option;
            var currentWorkbook = DataModel.Instance.CurrentWorkbook;
            var selectedCells = CellManager.Instance.GetSelectedCells(currentWorkbook);

            if (!currentWorkbook.SanityExplanationCells.Contains(selectedCells.First()))
            {
                option = WorkbookModel.CellDefinitionOption.Define;
            }
            else
            {
                option = WorkbookModel.CellDefinitionOption.Undefine;
            }

            currentWorkbook.DefineSanityExplanationCell(selectedCells, option);
        }

        /// <summary>
        /// Define a SanityValue cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityConstraintCell_Click(object sender, RibbonControlEventArgs e)
        {
            WorkbookModel.CellDefinitionOption option;
            var currentWorkbook = DataModel.Instance.CurrentWorkbook;
            var selectedCells = CellManager.Instance.GetSelectedCells(currentWorkbook);

            if (!currentWorkbook.SanityConstraintCells.Contains(selectedCells.First()))
            {
                option = WorkbookModel.CellDefinitionOption.Define;
            }
            else
            {
                option = WorkbookModel.CellDefinitionOption.Undefine;
            }

            currentWorkbook.DefineSanityConstraintCell(selectedCells, option);
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

            //set output cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.OutputCells.Contains(firstSelectedCell))
            {
                this.resultCellToggleButton.Checked = true;
            }
            else
            {
                this.resultCellToggleButton.Checked = false;
            }

            //set SanityValue cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.SanityValueCells.Contains(firstSelectedCell))
            {
                this.sanityValueCellToggleButton.Checked = true;
            }
            else
            {
                this.sanityValueCellToggleButton.Checked = false;
            }

            //set sanityConstraint cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.SanityConstraintCells.Contains(firstSelectedCell))
            {
                this.sanityConstraintCellToggleButton.Checked = true;
            }
            else
            {
                this.sanityConstraintCellToggleButton.Checked = false;
            }

            //set sanityExplanation cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.SanityExplanationCells.Contains(firstSelectedCell))
            {
                this.sanityExplanationCellToggleButton.Checked = true;
            }
            else
            {
                this.sanityExplanationCellToggleButton.Checked = false;
            }

            //set sanityChecking cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.SanityCheckingCells.Contains(firstSelectedCell))
            {
                this.sanityCheckingCellToggleButton.Checked = true;
            }
            else
            {
                this.sanityCheckingCellToggleButton.Checked = false;
            }
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
            this.CreateNewScenarioButton.Enabled = !create;

            // set define cells buttons styles
            this.inputCellToggleButton.Enabled = !create;
            this.intermediateCellToggleButton.Enabled = !create;
            this.resultCellToggleButton.Enabled = !create;
        }

        

    }
}
