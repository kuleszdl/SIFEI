using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows;
using Microsoft.Office.Tools.Ribbon;
using SIF.Visualization.Excel.Core;
using Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.Helper;
using SIF.Visualization.Excel.ScenarioCore;
using MessageBox = System.Windows.Forms.MessageBox;

namespace SIF.Visualization.Excel
{
    /// <summary>
    /// The class responsible to crete a new Ribbon for Excel
    /// </summary>
    public partial class Ribbon
    {
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            DataModel.Instance.WorkbookSelectionChangedEventHandler += Ribbon_WorkbookSelectionChanged;
            DataModel.Instance.CellDefinitionChangedEventHandler += Ribbon_WorkbookSelectionChanged;

            automaticScanCheckBox.Checked = Settings.Default.AutomaticScans;
        }

        /// <summary>
        /// Handler when the Scan Button got clicked
        /// For example checking if scan is allowed and then statrting it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void scanButton_Click(object sender, RibbonControlEventArgs e)
        {
            if (!AllowedToScan()) return;
            if (DataModel.Instance.CurrentWorkbook.PolicySettings.hasManualScans()
                || DataModel.Instance.CurrentWorkbook.Scenarios.Count > 0)
            {
                // SIFCore can't handle if it the documents is not saved. So if an inspection is started it is assured the file is saved somewhere.
                ScanHelper.SaveBefore(InspectionType.MANUAL);
            }
            else
            {
                ScanHelper.ScanUnsuccessful(Resources.tl_Ribbon_MessageNoPolicies);
            }
        }

        /// <summary>
        /// Checks weather a Scan is allowed (Not allowed if Scenarios are being created)
        /// </summary>
        /// <returns></returns>
        private bool AllowedToScan()
        {
            
            // Do not allow scans while creating a scenario
            // FIXME: There must be a cleaner way to check the state other than inspecting the enabled/disabled state of the button!
            if (CreateNewScenarioButton.Enabled) return true;
            //message if starting a scan while in scenario creation
            MessageBox.Show(Resources.tl_Ribbon_MessageNoScansInScnearioMode,
                Resources.tl_Ribbon_MessageNoScansInScnearioModeTitle, MessageBoxButtons.OK);
            return false;
        }

        private void warnings_Click(object sender, RibbonControlEventArgs e)
        {
            DataModel.Instance.CurrentWorkbook.SanityWarnings = sanityWarnCheckbox.Checked;
        }

        /// <summary>
        /// Opens the correct task pane for the currently active workbook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sharedPaneButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Find the correct task pane for the currently active workbook
            var pane =
                Globals.ThisAddIn.TaskPanes[
                    new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "shared Pane")];

            pane.Visible = !pane.Visible;
        }

        /// <summary>
        /// Removes all controls from the current workbook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Remove all controls from this workbook
            foreach (CellLocation cl in DataModel.Instance.CurrentWorkbook.ViolatedCells)
            {
                cl.RemoveIcon();
            }
            DataModel.Instance.CurrentWorkbook.ViolatedCells.Clear();
            DataModel.Instance.CurrentWorkbook.Violations.Clear();
            DataModel.Instance.CurrentWorkbook.IgnoredViolations.Clear();
            DataModel.Instance.CurrentWorkbook.SolvedViolations.Clear();
            DataModel.Instance.CurrentWorkbook.LaterViolations.Clear();
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
            WorkbookSelectionChanged();
        }

        private void Ribbon_WorkbookSelectionChanged(object sender, EventArgs data)
        {
            WorkbookSelectionChanged();
        }

        
        /// <summary>
        /// Gets fired when other cells of the Workbook get selected
        /// </summary>
        private void WorkbookSelectionChanged()
        {
            var firstSelectedCell = CellManager.Instance.GetFirstSelectedCell(DataModel.Instance.CurrentWorkbook);

            SetCellToggleButtons(firstSelectedCell);
            SetSanityToggleButtons(firstSelectedCell);
            
        }
        /// <summary>
        /// Sets the sanitycells acordingly
        /// </summary>
        /// <param name="firstSelectedCell"></param>
        private void SetSanityToggleButtons(Cell firstSelectedCell)
        {
            //set SanityValue cell toggle button
            if (firstSelectedCell != null &&
                DataModel.Instance.CurrentWorkbook.SanityValueCells.Contains(firstSelectedCell))
            {
                sanityValueCellToggleButton.Checked = true;
            }
            else
            {
                sanityValueCellToggleButton.Checked = false;
            }

            //set sanityConstraint cell toggle button
            if (firstSelectedCell != null &&
                DataModel.Instance.CurrentWorkbook.SanityConstraintCells.Contains(firstSelectedCell))
            {
                sanityConstraintCellToggleButton.Checked = true;
            }
            else
            {
                sanityConstraintCellToggleButton.Checked = false;
            }

            //set sanityExplanation cell toggle button
            if (firstSelectedCell != null &&
                DataModel.Instance.CurrentWorkbook.SanityExplanationCells.Contains(firstSelectedCell))
            {
                sanityExplanationCellToggleButton.Checked = true;
            }
            else
            {
                sanityExplanationCellToggleButton.Checked = false;
            }

            //set sanityChecking cell toggle button
            if (firstSelectedCell != null &&
                DataModel.Instance.CurrentWorkbook.SanityCheckingCells.Contains(firstSelectedCell))
            {
                sanityCheckingCellToggleButton.Checked = true;
            }
            else
            {
                sanityCheckingCellToggleButton.Checked = false;
            }
        }


        /// <summary>
        /// Sets the toggle Buttons of the cells acordingly
        /// </summary>
        /// <param name="firstSelectedCell">First checked cell</param>
        private void SetCellToggleButtons(Cell firstSelectedCell)
        {
            //set input cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.InputCells.Contains(firstSelectedCell))
            {
                inputCellToggleButton.Checked = true;
            }
            else
            {
                inputCellToggleButton.Checked = false;
            }

            //set intermediate cell toggle button
            if (firstSelectedCell != null &&
                DataModel.Instance.CurrentWorkbook.IntermediateCells.Contains(firstSelectedCell))
            {
                intermediateCellToggleButton.Checked = true;
            }
            else
            {
                intermediateCellToggleButton.Checked = false;
            }

            //set output cell toggle button
            if (firstSelectedCell != null && DataModel.Instance.CurrentWorkbook.OutputCells.Contains(firstSelectedCell))
            {
                resultCellToggleButton.Checked = true;
            }
            else
            {
                resultCellToggleButton.Checked = false;
            }
        }

        private void NewScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            string title = null;

            CustomInputDialog inputDialog = new CustomInputDialog(
                Resources.tl_NewScenarioDialog_Question,
                Resources.tl_NewScenarioDialog_Title,
                Resources.tl_NewScenarioDialog_DefaultAnswer);
            if (inputDialog.ShowDialog() == true)
            {
                title = inputDialog.Answer;
            }

            // If the user did not can the dialog, proceed with the scenario creation process
            if (title != null)
            {
                // set scenario buttons styles
                SetScenarioCreationButtonStyles(true);

                // start scenario creation
                ScenarioCore.ScenarioUICreator.Instance.Start(DataModel.Instance.CurrentWorkbook, title);
            }
        }

        private void SubmitScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // validate and show warnings

            #region validate

            var emptyInputs = ScenarioCore.ScenarioUICreator.Instance.GetEmptyEntrysCount(typeof (InputCell));
            var emptyIntermediates =
                ScenarioCore.ScenarioUICreator.Instance.GetEmptyEntrysCount(typeof (IntermediateCell));
            var emptyResults = ScenarioCore.ScenarioUICreator.Instance.GetEmptyEntrysCount(typeof (OutputCell));

            if (emptyInputs > 0 | emptyIntermediates > 0 | emptyResults > 0)
            {
                // FIXME: q&d temporary fix without translations: Do not allow creating scenarios when not all cells are filled
                MessageBox.Show(
                    Resources.tl_Scenario_Notallcellsfilled, Resources.tl_Scenario_CantCreate,
                    MessageBoxButtons.OK);
                return;
            }
            if (ScenarioCore.ScenarioUICreator.Instance.NoValue(typeof (InputCell)))
            {
                //message for no result cell values
                MessageBox.Show(Resources.tl_Scenario_MinOneInput, Resources.tl_MessageBox_Error, MessageBoxButtons.OK);

                //back to the scenario editor
                return;
            }
            else if (ScenarioCore.ScenarioUICreator.Instance.NoValue(typeof (IntermediateCell))
                     && ScenarioCore.ScenarioUICreator.Instance.NoValue(typeof (OutputCell)))
            {
                //message for no input cell values
                MessageBox.Show(Resources.tl_Scenario_MinOneOutput, Resources.tl_MessageBox_Error, MessageBoxButtons.OK);

                //back to the scenario editor
                return;
            }
            //else if (emptyInputs > 0 | emptyIntermediates > 0 | emptyResults > 0)
            //{
            // FIXME: Re-enable logic and remove hotfix once the issue has been solved (some inputs made by the user are missing in created scenarios when continuing with incomplete scenario)

            //// message for some empty fields
            //#region create message
            //var messageList = new List<Tuple<string, int>>();
            //if (emptyInputs > 0) messageList.Add(new Tuple<string, int>("input cells", emptyInputs));
            //if (emptyIntermediates > 0) messageList.Add(new Tuple<string, int>("intermediate cells", emptyIntermediates));
            //if (emptyResults > 0) messageList.Add(new Tuple<string, int>("result cells", emptyResults));

            //var message = new StringBuilder();
            //message.Append("Maybe your scenario isn't complete. ");
            //message.Append("The scenario has ");
            //foreach (var p in messageList)
            //{
            //    message.Append(p.Item2 + " empty fields for " + p.Item1);
            //    if (messageList.IndexOf(p) < messageList.Count - 2)
            //    {
            //        message.Append(", ");
            //    }
            //    else if (messageList.IndexOf(p) == messageList.Count - 2)
            //    {
            //        message.Append(" and ");
            //    }
            //}
            //message.Append(".");

            //#endregion

            //var result = MessageBox.Show(
            //    message.ToString(),
            //    "warning",
            //    MessageBoxButtons.OKCancel);

            ////back to the scenario editor
            //if (result == DialogResult.Cancel) return;

            //}

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

        /// <summary>
        /// Cancles a scenariocreation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            submitScenarioButton.Visible = create;
            cancelScenarioButton.Visible = create;
            CreateNewScenarioButton.Enabled = !create;

            // set define cells buttons styles
            inputCellToggleButton.Enabled = !create;
            intermediateCellToggleButton.Enabled = !create;
            resultCellToggleButton.Enabled = !create;
        }

        /// <summary>
        /// Checks weather automatic Scans got enabled and if so starts a first scan
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void automaticScanCheckBox_Click(object sender, RibbonControlEventArgs e)
        {
            Settings.Default.AutomaticScans = automaticScanCheckBox.Checked;
            if (automaticScanCheckBox.Checked) scanButton_Click(sender, e);
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            PolicyConfigurationDialog settingsDialog = new PolicyConfigurationDialog();
        }

        /// <summary>
        /// Opens the global settings dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void globalSettings_Click(object sender, RibbonControlEventArgs e)
        {
            GlobalSettingsDialog settingsDialog = new GlobalSettingsDialog();
        }


        private void CB_SanityControls_Click(object sender, RibbonControlEventArgs e)
        {
            Boolean oldState = sanityGroup.Visible;
            if (oldState == true)
            {
                sanityGroup.Visible = false;
            }
            else
            {
                sanityGroup.Visible = true;
            }
        }
    }
}
