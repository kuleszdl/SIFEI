using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using SIF.Visualization.Excel.Core.Rules;
using SIF.Visualization.Excel.Core.Scenarios;
using SIF.Visualization.Excel.Helper;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.View.CustomRules;
using MessageBox = System.Windows.MessageBox;
using MSExcel = Microsoft.Office.Interop.Excel;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    ///     Main model class holding all the important data
    /// </summary>
    public class WorkbookModel : BindableBase
    {
        /// <summary>
        ///     Defines if a cell should be defined or undefined
        /// </summary>
        public enum CellDefinitionOption
        {
            /// <summary>
            ///     The cell is being defined.
            /// </summary>
            Define,

            /// <summary>
            ///     The cell is being undefined.
            /// </summary>
            Undefine
        }

        /// <summary>
        ///     Decides how the inspection should be done or what should be inspected
        /// </summary>
        public enum InspectionMode
        {
            /// <summary>
            ///     Starts the Inspection with the scenarios, without static tests but with dynamic tests
            /// </summary>
            Dynamic,

            /// <summary>
            ///     Starts the inspection with static tests, whitout scenarios.
            /// </summary>
            Static,

            /// <summary>
            ///     Start the inspection with all available tests.
            /// </summary>
            All
        }

        public class Tabs
        {
            public const int OpenViolations = 0;
            public const int LaterViolations = 1;
            public const int IgnoredViolations = 2;
            public const int ArchivedViolations = 3;
            public const int ScenarioCells = 4;
            public const int Scenarios = 5;
            public const int Rules = 6;
        }

        #region Fields

        private string _title;
        private Dictionary<string, Cell> _cells = new Dictionary<string, Cell>();
        private ObservableCollection<Cell> _scenarioCells;
        private ObservableCollection<Cell> _ruleCells;
        private ObservableCollection<Cell> _sanityCells;
        private ObservableCollection<Violation> _violations;
        private ObservableCollection<Violation> _visibleViolations;
        private ObservableCollection<Scenario> _scenarios;
        private ObservableCollection<Rule> _rules;
        private bool _sanityWarnings = true;
        private int _selectedTabIndex;
        private string _selectedTabLabel = "undefined";
        private MSExcel.Workbook _workbook;
        private PolicyConfigurationModel _policySettings;

        #endregion

        #region Properties

        /// <summary>
        ///     Should a workbook be scaned after the saving process
        /// </summary>
        public bool ShouldScanAfterSave { get; set; }

        /// <summary>
        ///     Gets or Sets the settings of the policy
        /// </summary>
        public PolicyConfigurationModel PolicySettings
        {
            get
            {
                if (_policySettings == null) _policySettings = new PolicyConfigurationModel();
                return _policySettings;
            }
            set { SetProperty(ref _policySettings, value); }
        }

        /// <summary>
        ///     Gets or sets the title of the current inspection.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public Dictionary<string, Cell> Cells
        {
            get { return _cells; }
            set { SetProperty(ref _cells, value); }
        }

        /// <summary>
        ///     Gets or sets the cells with rules of the current document.
        /// </summary
        public ObservableCollection<Cell> RuleCells
        {
            get
            {
                if (_ruleCells == null) _ruleCells = new ObservableCollection<Cell>();
                return _ruleCells;
            }
            set { SetProperty(ref _ruleCells, value); }
        }

        /// <summary>
        ///     Gets or sets the scenario cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> ScenarioCells
        {
            get
            {
                if (_scenarioCells == null) _scenarioCells = new ObservableCollection<Cell>();
                return _scenarioCells;
            }
            set { SetProperty(ref _scenarioCells, value); }
        }


        /// <summary>
        ///     Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityCells
        {
            get
            {
                if (_sanityCells == null) _sanityCells = new ObservableCollection<Cell>();
                return _sanityCells;
            }
            set { SetProperty(ref _sanityCells, value); }
        }

        public bool SanityWarnings
        {
            get { return _sanityWarnings; }
            set { SetProperty(ref _sanityWarnings, value); }
        }

        /// <summary>
        ///     Gets or sets the violations of the current document
        /// </summary>
        [XmlArray("Violations")]
        public ObservableCollection<Violation> Violations
        {
            get
            {
                if (_violations == null) _violations = new ObservableCollection<Violation>();

                return _violations;
            }
            set { SetProperty(ref _violations, value); }
        }

        /// <summary>
        ///     Gets the visible violations of the current document
        /// </summary>
        public ObservableCollection<Violation> VisibleViolations
        {
            get
            {
                if (_visibleViolations == null) _visibleViolations = new ObservableCollection<Violation>();
                return _visibleViolations;
            }
        }

        /// <summary>
        ///     Gets or sets the Rules of the current document.
        /// </summary>
        public ObservableCollection<Rule> Rules
        {
            get
            {
                if (_rules == null) _rules = new ObservableCollection<Rule>();
                return _rules;
            }
            set { SetProperty(ref _rules, value); }
        }

        /// <summary>
        ///     Gets or sets the scenarios of the current document.
        /// </summary>
        public ObservableCollection<Scenario> Scenarios
        {
            get
            {
                if (_scenarios == null) _scenarios = new ObservableCollection<Scenario>();
                return _scenarios;
            }
            set { SetProperty(ref _scenarios, value); }
        }

        /// <summary>
        ///     Gets or sets the count of the unread violations
        /// </summary>
        public int UnreadViolationCount
        {
            get
            {
                var num = 0;
                foreach (var vio in VisibleViolations) if (!vio.IsRead) num++;
                return num;
            }
        }

        /// <summary>
        ///     Gets or sets the Excel workbook of this model.
        /// </summary>
        public MSExcel.Workbook Workbook
        {
            get { return _workbook; }
            set { SetProperty(ref _workbook, value); }
        }

        /// <summary>
        ///     Gets or sets the translated name of the current tab (for the label)
        /// </summary>
        public string SelectedTabLabel
        {
            get { return _selectedTabLabel; }
            set { SetProperty(ref _selectedTabLabel, value); }
        }

        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                SetProperty(ref _selectedTabIndex, value);
                RecalculateViewModel();
            }
        }

        #endregion

        #region Methods

        #region Lifecycle Events

        /// <summary>
        ///     Initializes a new instance of the WorkbookModel class with a given workbook.
        /// </summary>
        /// <param name="workbook">The workbook that is used for initialization.</param>
        public WorkbookModel(MSExcel.Workbook workbook)
        {
            ShouldScanAfterSave = false;
            _workbook = workbook;
            _workbook.BeforeSave += workbook_BeforeSave;
            _workbook.BeforeClose += Workbook_BeforeClose;
            _workbook.AfterSave += Workbook_AfterSave;
            //_workbook.SheetSelectionChange += sheet_SelectionChange;
            // Occurs after any worksheet is recalculated or after any changed data is plotted on a chart.
            _workbook.SheetCalculate += workbook_SheetCalculate;
        }

        /// <summary>
        ///     Loads all the data related to violations and scenarios
        /// </summary>
        public void LoadExtraInformation()
        {
            disableScreenUpdating();
            var error = string.Empty;
            try
            {
                var violationsElement = XMLPartManager.Instance.LoadXMLPart(this, "ArrayOfViolation");
                if (violationsElement != null)
                {
                    _violations =
                        XMLPartManager.Instance.Deserialize<ObservableCollection<Violation>>(
                            violationsElement.ToString());
                    NotifyPropertyChanged("Violations");
                }
            }
            catch (Exception)
            {
                error += "Loading the violations failed.\n";
            }

            try
            {
                var rulesElement = XMLPartManager.Instance.LoadXMLPart(this, "ArrayOfRule");
                if (rulesElement != null)
                {
                    _rules = XMLPartManager.Instance.Deserialize<ObservableCollection<Rule>>(rulesElement.ToString());
                    NotifyPropertyChanged("Rules");
                }
            }
            catch (Exception)
            {
                error += "Loading the Rules failed.\n";
            }

            try
            {
                var scenariosElement = XMLPartManager.Instance.LoadXMLPart(this, "ArrayOfScenario");
                if (scenariosElement != null)
                {
                    _scenarios =
                        XMLPartManager.Instance
                            .Deserialize<ObservableCollection<Scenario>>(scenariosElement.ToString());
                    NotifyPropertyChanged("Scenarios");
                }
            }
            catch (Exception)
            {
                error += "Loading the scenarios failed.\n";
            }


            try
            {
                var policyElement = XMLPartManager.Instance.LoadXMLPart(this, "PolicyConfigurationModel");
                if (policyElement != null)
                    _policySettings =
                        XMLPartManager.Instance.Deserialize<PolicyConfigurationModel>(policyElement.ToString());
            }
            catch (Exception)
            {
                error += "Loading the policy settings failed.\n";
            }
            enableScreenUpdating();

            if (!string.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(Resources.tl_Load_Failed + error, Resources.tl_Load_Failed_Title);
            }
            else
            {
                // iterate over all violations and update cells
                UpdateCellViolations();
                // recalulate visibility for violations
                RecalculateViewModel();
            }
        }

        //private void sheet_SelectionChange(object sh, MSExcel.Range target)
        //{
        //    // mark all cells as not selected
        //    var selectedCells = CellManager.Instance.GetCellsFromRange(target);
        //    foreach (Cell cell in Cells.Values)
        //    {
        //        if (selectedCells.Contains(cell))
        //        {
        //            cell.IsSelected = true;
        //        }
        //        else
        //        {
        //            cell.IsSelected = false;
        //        }

        //    }
        //}

        /// <summary>
        ///     Gets the name / location of a column in the workbook
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private string GetExcelColumnName(int columnNumber)
        {
            var dividend = columnNumber;
            var columnName = string.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo) + columnName;
                dividend = (dividend - modulo) / 26;
            }

            return columnName;
        }

        /// <summary>
        ///     Happens after a Workbook gets saved
        /// </summary>
        /// <param name="success"> Gives back if the saving process was succesfull</param>
        private void Workbook_AfterSave(bool success)
        {
            enableScreenUpdating();
            // recalculate visible violations and recreate cell error infos
            RecalculateViewModel();

            if (ShouldScanAfterSave) return;
            // Run a scan if necessary
            if (Settings.Default.AutomaticScans)
                if (Globals.Ribbons.Ribbon.scanButton.Enabled) if (Workbook.Path.Length > 0) Inspect();
        }

        /// <summary>
        ///     Occurs after any workbook is recalculated or after any changed data is plotted on a chart.
        /// </summary>
        /// <param name="sh"></param>
        private void workbook_SheetCalculate(object sh)
        {
            // Run a scan if necessary
            if (!PolicySettings.HasAutomaticScans() || !Settings.Default.AutomaticScans) return;
            if (!Globals.Ribbons.Ribbon.scanButton.Enabled) return;
            DataModel.Instance.CurrentWorkbook.Inspect();
        }


        /// <summary>
        ///     Saves the custom XML parts that are used to persist the cells, scenarios and false positives.
        /// </summary>
        private void workbook_BeforeSave(bool saveAsUi, ref bool cancel)
        {
            ClearCellErrorInfo();
            disableScreenUpdating();
            //Save the violations
            var violationsElement = XElement.Parse(XMLPartManager.Instance.Serialize(_violations));
            XMLPartManager.Instance.SaveXMLPart(this, violationsElement, "ArrayOfViolation");

            // Save the scenarios
            var scenariosElement = XElement.Parse(XMLPartManager.Instance.Serialize(_scenarios));
            XMLPartManager.Instance.SaveXMLPart(this, scenariosElement, "ArrayOfScenario");

            // Save the policy configuration
            var policyElement = XElement.Parse(XMLPartManager.Instance.Serialize(_policySettings));
            XMLPartManager.Instance.SaveXMLPart(this, policyElement, "PolicyConfigurationModel");

            // Save the rules
            var ruleElement = XElement.Parse(XMLPartManager.Instance.Serialize(_rules));
            XMLPartManager.Instance.SaveXMLPart(this, ruleElement, "ArrayOfRule");
        }


        /// <summary>
        ///     Handle the scenario controls in the cells before close.
        /// </summary>
        /// <param name="cancel"></param>
        private void Workbook_BeforeClose(ref bool cancel)
        {
            ShouldScanAfterSave = true;
            ScenarioUICreator.Instance.End();
            RuleCreator.Instance.End();
            try
            {
                RuleEditor.Instance.End();
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }

            // Deletes all controls that might be in the cells (markers)
            foreach (MSExcel.Worksheet worksheet in Workbook.Worksheets)
                try
                {
                    var worksheet2 = Globals.Factory.GetVstoObject(worksheet);

                    var controlsToRemove = new ArrayList();

                    // Get all of the Windows Forms controls.
                    foreach (var control in worksheet2.Controls)
                        if (control is Control)
                            controlsToRemove.Add(control);

                    // Remove all of the Windows Forms controls from the document.
                    foreach (var control in controlsToRemove)
                        worksheet2.Controls.Remove(control);
                }
                catch (Exception f)
                {
                    MessageBox.Show(f.ToString());
                }
        }

        #endregion

        /// <summary>
        ///     Launches an inspection job for this workbook.
        /// </summary>
        public void Inspect()
        {
            Globals.ThisAddIn.Application.StatusBar = Resources.tl_ProcessingScan;
            Globals.Ribbons.Ribbon.scanButton.Enabled = false;
            Globals.Ribbons.Ribbon.scanButton.Label = Resources.tl_NoScanPossible;

            var fileFormat = Workbook.FileFormat;
            var extension = "";
            if (fileFormat == MSExcel.XlFileFormat.xlOpenXMLWorkbook) extension = ".xlsx";
            else if (fileFormat == MSExcel.XlFileFormat.xlExcel8) extension = ".xls";
            else return;

            var policyFile = "";
            policyFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                         Path.DirectorySeparatorChar + "inspectionRequest.xml";
            var spreadsheetFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                  Path.DirectorySeparatorChar + "spreadsheet" + extension;
            try
            {
                // create xml document
                var xmlDoc = new XDocument();
                // Create the rules
                xmlDoc.Add(Accept(new SprudelXMLVisitor()) as XElement);
                // @TODO: reenable schema validation
                //xmlDoc.Validate(XMLPartManager.Instance.GetRequestSchema(), null);
                // save as policy file
                xmlDoc.Save(policyFile);

                // Save a copy of this workbook as spreadsheet file
                Workbook.SaveCopyAs(spreadsheetFile);

                // Enqueue this inspection
                // @TODO http web service request

                InspectionEngine.Instance.doInspection(this, policyFile, spreadsheetFile);
            }
            catch (Exception f)
            {
                MessageBox.Show("Nicht prüfen, wenn gerade eine Zelle editiert wird!\n" + f);
            }
            Globals.Ribbons.Ribbon.scanButton.Enabled = true;
        }

        /// <summary>
        ///     This method loads the XML report generated by the SIF into this workbook model.
        /// </summary>
        public void Load(string xml)
        {
            try
            {
                var rootElement = XElement.Parse(xml);
                // @TODO
                //XDocument d = new XDocument(rootElement);
                //d.Validate(XMLPartManager.Instance.getReportSchema(), null);
                LoadViolations(rootElement);
                ScanHelper.ScanSuccessful();
            }
            catch (Exception e)
            {
                ScanHelper.ScanUnsuccessful(Resources.Error_FaultyResponse);
                Debug.WriteLine(e);
            }
        }


        /// <summary>
        ///     This method loads the violations of the xml report
        /// </summary>
        private void LoadViolations(XElement rootElement)
        {
            var scanTime = DateTime.Now;
            XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
            var validationReports = rootElement.Element(XName.Get("validationReports"));
            var violations = new List<Violation>();

            if (validationReports != null)
            {
                foreach (var validationReport in validationReports.Elements(XName.Get("validationReport")))
                {
                    // parse policy
                    var policy = new Policy(validationReport.Element(XName.Get("policy")));
                    // parse violations
                    var violationsElement = validationReport.Element(XName.Get("violations"));
                    if (violationsElement != null)
                        foreach (var violation in violationsElement.Elements(XName.Get("violation")))
                            if (violation != null)
                                violations.Add(new Violation(violation, _workbook, scanTime, policy));
                }
                // disable screen updates
                disableScreenUpdating();
                // Add only new violations
                AddNewViolations(violations);
                // mark all solved violations from the Open Category
                MarkSolvedViolations(scanTime);
                // iterate over all violations and update cells
                UpdateCellViolations();
                // enable screen updates
                enableScreenUpdating();
                // recalulate visibility for violations
                RecalculateViewModel();
            }
        }

        private void UpdateCellViolations()
        {
            foreach (var vio in Violations)
            {
                var cell = GetCell(vio.Location);
                if (!cell.Violations.Contains(vio)) cell.Violations.Add(vio);
            }
        }

        /// <summary>
        ///     Marks all solved Violations as marked
        /// </summary>
        /// <param name="scanTime"></param>
        private void MarkSolvedViolations(DateTime scanTime)
        {
            foreach (var vio in _violations)
                if (vio.FoundAgain)
                {
                    vio.FoundAgain = false;
                }
                else
                {
                    // If it didnt get found again means it didnt appear again ergo its solved
                    vio.SolvedTime = scanTime;
                    vio.ViolationState = ViolationState.SOLVED;
                }
        }

        public void ClearCellErrorInfo()
        {
            foreach (var cell in Cells.Values) cell.RemoveIcon();
        }

        /// <summary>
        ///     Adds all the new Violations to the Violations collection
        /// </summary>
        /// <param name="violations"></param>
        private void AddNewViolations(List<Violation> violations)
        {
            foreach (var violation in violations)
                if (_violations.Contains(violation))
                {
                    _violations[_violations.IndexOf(violation)].FoundAgain = true;
                }
                else
                {
                    // we set found again to true, or all new violations will be archived immediatly
                    violation.FoundAgain = true;
                    _violations.Add(violation);
                }
        }

        public Cell GetCell(string currentLocation)
        {
            if (Cells.ContainsKey(currentLocation))
                return Cells[currentLocation];
            var cell = new Cell(_workbook, currentLocation);
            Cells[currentLocation] = cell;
            return cell;
        }

        public void RecalculateViewModel()
        {
            VisibleViolations.Clear();
            ScenarioCells.Clear();
            SanityCells.Clear();
            RuleCells.Clear();

            ViolationState state;
            switch (SelectedTabIndex)
            {
                case Tabs.OpenViolations:
                    SelectedTabLabel = Resources.tl_Sidebar_Open;
                    state = ViolationState.OPEN;
                    break;
                case Tabs.ArchivedViolations:
                    SelectedTabLabel = Resources.tl_Sidebar_Archived;
                    state = ViolationState.SOLVED;
                    break;
                case Tabs.IgnoredViolations:
                    SelectedTabLabel = Resources.tl_Sidebar_Ignored;
                    state = ViolationState.IGNORE;
                    break;
                case Tabs.LaterViolations:
                    SelectedTabLabel = Resources.tl_Sidebar_Later;
                    state = ViolationState.LATER;
                    break;
                case Tabs.ScenarioCells:
                    SelectedTabLabel = Resources.tl_Sidebar_Cells;
                    state = ViolationState.NONE;
                    break;
                case Tabs.Scenarios:
                    SelectedTabLabel = Resources.tl_Sidebar_Scenarios;
                    state = ViolationState.NONE;
                    break;
                case Tabs.Rules:
                    SelectedTabLabel = Resources.tl_Sidebar_Rules;
                    state = ViolationState.NONE;
                    break;
                default:
                    state = ViolationState.NONE;
                    break;
            }

            foreach (var vio in Violations) if (vio.ViolationState == state) VisibleViolations.Add(vio);
            // create all cell error infos and add marked cells to lists
            var cellList = new List<Cell>(Cells.Values);
            foreach (var cell in cellList)
            {
                cell.RecalculateVisibleViolations();

                if (cell.ScenarioCellType != ScenarioCellType.NONE) ScenarioCells.Add(cell);
                if (cell.SanityCellType != SanityCellType.NONE) SanityCells.Add(cell);
                if (cell.RuleCellType == RuleCellType.CELL) RuleCells.Add(cell);
            }
            NotifyPropertyChanged("UnreadViolationCount");
        }

        public void NotifyUnreadViolationsChanged()
        {
            NotifyPropertyChanged("UnreadViolationCount");
        }

        private void disableScreenUpdating()
        {
            Globals.ThisAddIn.Application.ScreenUpdating = false;
        }

        private void enableScreenUpdating()
        {
            Globals.ThisAddIn.Application.ScreenUpdating = true;
        }

        #region Accept Visitors

        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }

        #endregion

        #endregion
    }
}