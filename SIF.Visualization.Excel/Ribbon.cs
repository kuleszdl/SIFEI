using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Tools.Ribbon;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Scenarios;
using SIF.Visualization.Excel.Helper;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.View.CustomRules;
using MSExcel = Microsoft.Office.Interop.Excel;

namespace SIF.Visualization.Excel
{
    /// <summary>
    ///     The class responsible to crete a new Ribbon for Excel
    /// </summary>
    public partial class Ribbon
    {
        Boolean serverstarted = false;
        Process process = new Process();
        private void Ribbon_Load(object sender, RibbonUIEventArgs e)
        {
            DataModel.Instance.WorkbookSelectionChangedEventHandler += Ribbon_WorkbookSelectionChanged;
            //DataModel.Instance.CellDefinitionChangedEventHandler += Ribbon_WorkbookSelectionChanged;
            automaticScanCheckBox.Checked = Settings.Default.AutomaticScans;
        }

        /// <summary>
        ///     Handler when the Scan Button got clicked
        ///     For example checking if scan is allowed and then statrting it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void startserver()
        {


            Process process = new Process();
            String jarFile = System.AppDomain.CurrentDomain.BaseDirectory + "../../server.jar";
            GlobalSettingsDialog dialog = new GlobalSettingsDialog();
            String[] delemitter = dialog.getSifUrlTextBox().Split(':');

            String ip = delemitter[0] + ":" + delemitter[1];
            ip = ip.Replace("http://", "");
            ; int port = Int32.Parse(delemitter[2]);
            var arguments = String.Format(" -jar {0} -Ip {1} -Port {2}", jarFile, ip, port);

            // indicate, that you want to capture the application output
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            // create a process instance
            var javaExecutable = "java.exe";
            // and instruct it to start java with the given parameters
            var processStartInfo = new ProcessStartInfo(javaExecutable, arguments);
            process.StartInfo = processStartInfo;
            // start the process
            process.Start();
            // read the output from the started appplication
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();


        }
        private void scanButton_Click(object sender, RibbonControlEventArgs e)
        {
           
            if (!AllowedToScan()) return;
            if (DataModel.Instance.CurrentWorkbook.PolicySettings.hasManualScans() ||
                DataModel.Instance.CurrentWorkbook.Scenarios.Count > 0 ||
                DataModel.Instance.CurrentWorkbook.Rules.Count > 0) DataModel.Instance.CurrentWorkbook.Inspect();
            else ScanHelper.ScanUnsuccessful(Resources.tl_Ribbon_MessageNoPolicies);
        }

        /// <summary>
        ///     Checks weather a Scan is allowed (Not allowed if Scenarios are being created)
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
        ///     Opens the correct task pane for the currently active workbook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SidebarButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Find the correct task pane for the currently active workbook
            var pane = Globals.ThisAddIn.TaskPanes[
                new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Sidebar")];
            pane.Visible = !pane.Visible;
        }

        /// <summary>
        ///     Removes all controls from the current workbook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clearButton_Click(object sender, RibbonControlEventArgs e)
        {
            // Remove all controls from this workbook
            DataModel.Instance.CurrentWorkbook.ClearCellErrorInfo();
        }

        /// <summary>
        ///     Define a input cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineInputCell_Click(object sender, RibbonControlEventArgs e)
        {
            intermediateCellToggleButton.Checked = false;
            resultCellToggleButton.Checked = false;
            ScenarioCellType cellType;
            var selectedCells = CellManager.Instance.GetSelectedCells();

            if (selectedCells.First().ScenarioCellType == ScenarioCellType.INPUT) cellType = ScenarioCellType.NONE;
            else cellType = ScenarioCellType.INPUT;

            foreach (var cell in selectedCells) cell.ScenarioCellType = cellType;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
        }

        /// <summary>
        ///     Define a intermediate cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineIntermediateCell_Click(object sender, RibbonControlEventArgs e)
        {
            inputCellToggleButton.Checked = false;
            resultCellToggleButton.Checked = false;
            ScenarioCellType cellType;
            var selectedCells = CellManager.Instance.GetSelectedCells();

            if (selectedCells.First().ScenarioCellType == ScenarioCellType.INVARIANT) cellType = ScenarioCellType.NONE;
            else cellType = ScenarioCellType.INVARIANT;

            foreach (var cell in selectedCells) cell.ScenarioCellType = cellType;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
        }

        /// <summary>
        ///     Define a SanityValue cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityValueCell_Click(object sender, RibbonControlEventArgs e)
        {
            sanityConstraintCellToggleButton.Checked = false;
            sanityExplanationCellToggleButton.Checked = false;
            sanityCheckingCellToggleButton.Checked = false;
            SanityCellType cellType;
            var selectedCells = CellManager.Instance.GetSelectedCells();

            if (selectedCells.First().SanityCellType == SanityCellType.VALUE) cellType = SanityCellType.NONE;
            else cellType = SanityCellType.VALUE;

            foreach (var cell in selectedCells) cell.SanityCellType = cellType;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
        }

        /// <summary>
        ///     Define a SanityChecking cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityCheckingCell_Click(object sender, RibbonControlEventArgs e)
        {
            sanityConstraintCellToggleButton.Checked = false;
            sanityExplanationCellToggleButton.Checked = false;
            sanityValueCellToggleButton.Checked = false;
            SanityCellType cellType;
            var selectedCells = CellManager.Instance.GetSelectedCells();

            if (selectedCells.First().SanityCellType == SanityCellType.CHECKING) cellType = SanityCellType.NONE;
            else cellType = SanityCellType.CHECKING;

            foreach (var cell in selectedCells) cell.SanityCellType = cellType;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
        }

        /// <summary>
        ///     Define a SanityExplanation cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityExplanationCell_Click(object sender, RibbonControlEventArgs e)
        {
            sanityConstraintCellToggleButton.Checked = false;
            sanityCheckingCellToggleButton.Checked = false;
            sanityValueCellToggleButton.Checked = false;
            SanityCellType cellType;
            var selectedCells = CellManager.Instance.GetSelectedCells();

            if (selectedCells.First().SanityCellType == SanityCellType.EXPLANATION) cellType = SanityCellType.NONE;
            else cellType = SanityCellType.EXPLANATION;

            foreach (var cell in selectedCells) cell.SanityCellType = cellType;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
        }

        /// <summary>
        ///     Define a SanityValue cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineSanityConstraintCell_Click(object sender, RibbonControlEventArgs e)
        {
            sanityValueCellToggleButton.Checked = false;
            sanityCheckingCellToggleButton.Checked = false;
            sanityExplanationCellToggleButton.Checked = false;
            SanityCellType cellType;
            var selectedCells = CellManager.Instance.GetSelectedCells();

            if (selectedCells.First().SanityCellType == SanityCellType.CONSTRAINT) cellType = SanityCellType.NONE;
            else cellType = SanityCellType.CONSTRAINT;

            foreach (var cell in selectedCells) cell.SanityCellType = cellType;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
        }

        /// <summary>
        ///     Define a result cell. Toggle betwen defined and undefined
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DefineResultCell_Click(object sender, RibbonControlEventArgs e)
        {
            inputCellToggleButton.Checked = false;
            intermediateCellToggleButton.Checked = false;
            ScenarioCellType cellType;
            var selectedCells = CellManager.Instance.GetSelectedCells();

            if (selectedCells.First().ScenarioCellType == ScenarioCellType.CONDITION) cellType = ScenarioCellType.NONE;
            else cellType = ScenarioCellType.CONDITION;

            foreach (var cell in selectedCells) cell.ScenarioCellType = cellType;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
        }

        /// <summary>
        ///     Will be registert to the workbook selection changed event
        /// </summary>
        /// <param name="sh"></param>
        /// <param name="target"></param>
        private void Ribbon_WorkbookSelectionChanged(object sh, MSExcel.Range target)
        {
            WorkbookSelectionChanged();
        }

        private void Ribbon_WorkbookSelectionChanged(object sender, EventArgs data)
        {
            WorkbookSelectionChanged();
        }


        /// <summary>
        ///     Gets fired when other cells of the Workbook get selected
        /// </summary>
        private void WorkbookSelectionChanged()
        {
            var firstSelectedCell = CellManager.Instance.GetFirstSelectedCell(DataModel.Instance.CurrentWorkbook);
            Debug.WriteLine("Changed selected cell to: " + firstSelectedCell.Location);
            SetCellToggleButtons(firstSelectedCell);
            SetSanityToggleButtons(firstSelectedCell);
        }

        /// <summary>
        ///     Sets the sanitycells acordingly
        /// </summary>
        /// <param name="firstSelectedCell"></param>
        private void SetSanityToggleButtons(Cell firstSelectedCell)
        {
            //set SanityValue cell toggle button
            sanityValueCellToggleButton.Checked = false;
            if (firstSelectedCell != null)
                if (firstSelectedCell.SanityCellType == SanityCellType.VALUE)
                    sanityValueCellToggleButton.Checked = true;

            sanityConstraintCellToggleButton.Checked = false;
            if (firstSelectedCell != null)
                if (firstSelectedCell.SanityCellType == SanityCellType.CONSTRAINT)
                    sanityConstraintCellToggleButton.Checked = true;

            //set sanityExplanation cell toggle button
            sanityExplanationCellToggleButton.Checked = false;
            if (firstSelectedCell != null)
                if (firstSelectedCell.SanityCellType == SanityCellType.EXPLANATION)
                    sanityExplanationCellToggleButton.Checked = true;

            //set sanityChecking cell toggle button
            sanityCheckingCellToggleButton.Checked = false;
            if (firstSelectedCell != null)
                if (firstSelectedCell.SanityCellType == SanityCellType.CHECKING)
                    sanityCheckingCellToggleButton.Checked = true;
        }


        /// <summary>
        ///     Sets the toggle Buttons of the cells acordingly
        /// </summary>
        /// <param name="firstSelectedCell">First checked cell</param>
        private void SetCellToggleButtons(Cell firstSelectedCell)
        {
            //set input cell toggle button
            inputCellToggleButton.Checked = false;
            if (firstSelectedCell != null)
                if (firstSelectedCell.ScenarioCellType == ScenarioCellType.INPUT) inputCellToggleButton.Checked = true;

            //set intermediate cell toggle button
            intermediateCellToggleButton.Checked = false;
            if (firstSelectedCell != null)
                if (firstSelectedCell.ScenarioCellType == ScenarioCellType.INVARIANT)
                    intermediateCellToggleButton.Checked = true;

            //set output cell toggle button
            resultCellToggleButton.Checked = false;
            if (firstSelectedCell != null)
                if (firstSelectedCell.ScenarioCellType == ScenarioCellType.CONDITION)
                    resultCellToggleButton.Checked = true;
        }

        private void NewScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            string title = null;

            var inputDialog = new CustomInputDialog(
                Resources.tl_NewScenarioDialog_Question,
                Resources.tl_NewScenarioDialog_Title,
                Resources.tl_NewScenarioDialog_DefaultAnswer);
            if (inputDialog.ShowDialog() == true) title = inputDialog.Answer;

            // If the user did not cancel the dialog, proceed with the scenario creation process
            if (!string.IsNullOrEmpty(title))
            {
                // set scenario buttons styles
                SetScenarioCreationButtonStyles(true);

                // start scenario creation
                ScenarioUICreator.Instance.Start(DataModel.Instance.CurrentWorkbook, title);
            }
        }

        private void SubmitScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // validate and show warnings

            #region validate

            var emptyInputs = ScenarioUICreator.Instance.GetEmptyInputsCount();
            var emptyResults = ScenarioUICreator.Instance.GetEmptyConditionsCount();

            if (emptyInputs > 0 || emptyResults > 0)
            {
                // FIXME: q&d temporary fix without translations: Do not allow creating scenarios when not all cells are filled
                MessageBox.Show(
                    Resources.tl_Scenario_Notallcellsfilled, Resources.tl_Scenario_CantCreate,
                    MessageBoxButtons.OK);
                return;
            }

            #endregion

            // end scenario creation
            var newScenario = ScenarioUICreator.Instance.End();

            if (newScenario != null) DataModel.Instance.CurrentWorkbook.Scenarios.Add(newScenario);

            // set button styles
            SetScenarioCreationButtonStyles(false);
        }

        /// <summary>
        ///     Cancels a scenario creation
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cancelScenarioButton_Click(object sender, RibbonControlEventArgs e)
        {
            // end scenario creation
            ScenarioUICreator.Instance.End();

            // set button styles
            SetScenarioCreationButtonStyles(false);
        }

        /// <summary>
        ///     Disables or aktivates ribbon buttons, if the scenario creating process is started or completed
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
        ///     Checks weather automatic Scans got enabled and if so starts a first scan
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
            var settingsDialog = new PolicyConfigurationDialog();
        }
      
        /// <summary>
        ///     Opens the global settings dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void globalSettings_Click(object sender, RibbonControlEventArgs e)
        {
            var settingsDialog = new GlobalSettingsDialog();
        }


        private void CB_SanityControls_Click(object sender, RibbonControlEventArgs e)
        {
            var oldState = sanityGroup.Visible;
            if (oldState) sanityGroup.Visible = false;
            else sanityGroup.Visible = true;
        }

        /// <summary>
        ///     Opens the Rule Editor for a new Rule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RuleEdit_Click(object sender, RibbonControlEventArgs e)
        {
            RuleEditor.Instance.Start();
        }


        private void button1_Click_1(object sender, RibbonControlEventArgs e)
        {
            if (automaticScanCheckBox.Visible)
            {
                automaticScanCheckBox.Visible = false;
                MessageBox.Show("Off");
            }
            else
            {
                automaticScanCheckBox.Visible = true;
                MessageBox.Show("On");
            }
        }

        private void testButton_Click(object sender, RibbonControlEventArgs e)
        {
           
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {

        }

        private void button2_Click_1(object sender, RibbonControlEventArgs e)
        {
            HelperGuide guid = new HelperGuide();
            guid.Show();
        }

        private void button2_MouseEnter_1(object sender, RibbonControlEventArgs e)
        {
           
        }

        private void serverStartenButton_Click(object sender, RibbonControlEventArgs e)
        {
            startserver();


        }
    }


}