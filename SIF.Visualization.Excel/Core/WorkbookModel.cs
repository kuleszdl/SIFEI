using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.Networking;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.ScenarioCore;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using System.Xml.Schema;
using SIF.Visualization.Excel.Helper;
using MessageBox = System.Windows.MessageBox;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// This is the model class for one 
    /// 
    /// 
    /// .
    /// </summary>
    public class WorkbookModel : BindableBase, IAcceptVisitor
    {
        /// <summary>
        /// Defines if a cell should be defined or undefined
        /// </summary>
        public enum CellDefinitionOption
        {
            /// <summary>
            /// The cell is being defined.
            /// </summary>
            Define,

            /// <summary>
            /// The cell is being undefined.
            /// </summary>
            Undefine
        };

        /// <summary>
        /// Decides how the inspection should be done or what should be inspected
        /// </summary>
        public enum InspectionMode
        {
            /// <summary>
            /// Starts the Inspection with the scenarios, without static tests but with dynamic tests
            /// </summary>
            Dynamic,

            /// <summary>
            /// Starts the inspection with static tests, whitout scenarios.
            /// </summary>
            Static,

            /// <summary>
            /// Start the inspection with all available tests.
            /// </summary>
            All
        };

        #region Fields

        private string _title;
        private string _spreadsheet;
        private string _policyPath;
        private Policy _policy;
        private ObservableCollection<Cell> _inputCells;
        private ObservableCollection<Cell> _sanityValueCells;
        private ObservableCollection<Cell> _sanityConstraintCells;
        private ObservableCollection<Cell> _sanityExplanationCells;
        private ObservableCollection<Cell> _sanityCheckingCells;
        private ObservableCollection<Cell> _intermediateCells;
        private ObservableCollection<Cell> _outputCells;
        private ObservableCollection<Violation> _violations;
        private ObservableCollection<Violation> _ignoredViolations;
        private ObservableCollection<Violation> _laterViolations;
        private ObservableCollection<Violation> _solvedViolations;
        private ObservableCollection<CellLocation> _violatedCells;
        private int _unreadViolationCount;
        private ObservableCollection<ScenarioCore.Scenario> _scenarios;
        private Boolean _sanityWarnings = true;
        private SharedTabs _selectedTab;
        private string _selectedTabLabel = "unnamed";

        private Workbook _workbook;
        private PolicyConfigurationModel _policySettings;
        private int tries = 0;

        #endregion

        #region Properties

        /// <summary>
        /// Should a workbook be scaned after the saving process
        /// </summary>
        public bool ShouldScanAfterSave { get; set; }


        /// <summary>
        /// Gets or Sets the settings of the policy
        /// </summary>
        public PolicyConfigurationModel PolicySettings
        {
            get
            {
                if (_policySettings == null)
                {
                    _policySettings = new PolicyConfigurationModel();
                }
                return _policySettings;
            }
            set { _policySettings = value; }
        }

        /// <summary>
        /// Gets or sets the title of the current inspection.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// Gets or sets the file path of the inspected spreadsheet.
        /// </summary>
        public string Spreadsheet
        {
            get { return _spreadsheet; }
            set { SetProperty(ref _spreadsheet, value); }
        }

        /// <summary>
        /// Gets or sets the policy path of the inspected spreadsheet.
        /// </summary>
        public string PolicyPath
        {
            get { return _policyPath; }
            set { SetProperty(ref _policyPath, value); }
        }

        /// <summary>
        /// Gets or sets the policy of the inspected spreadsheet.
        /// </summary>
        public Policy Policy
        {
            get { return _policy; }
            set { SetProperty(ref _policy, value); }
        }

        /// <summary>
        /// Gets or sets the input cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> InputCells
        {
            get
            {
                if (_inputCells == null) _inputCells = new ObservableCollection<Cell>();
                return _inputCells;
            }
            set { SetProperty(ref _inputCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> IntermediateCells
        {
            get
            {
                if (_intermediateCells == null) _intermediateCells = new ObservableCollection<Cell>();
                return _intermediateCells;
            }
            set { SetProperty(ref _intermediateCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityValueCells
        {
            get
            {
                if (_sanityValueCells == null) _sanityValueCells = new ObservableCollection<Cell>();
                return _sanityValueCells;
            }
            set { SetProperty(ref _sanityValueCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityConstraintCells
        {
            get
            {
                if (_sanityConstraintCells == null) _sanityConstraintCells = new ObservableCollection<Cell>();
                return _sanityConstraintCells;
            }
            set { SetProperty(ref _sanityConstraintCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityExplanationCells
        {
            get
            {
                if (_sanityExplanationCells == null) _sanityExplanationCells = new ObservableCollection<Cell>();
                return _sanityExplanationCells;
            }
            set { SetProperty(ref _sanityExplanationCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityCheckingCells
        {
            get
            {
                if (_sanityCheckingCells == null) _sanityCheckingCells = new ObservableCollection<Cell>();
                return _sanityCheckingCells;
            }
            set { SetProperty(ref _sanityCheckingCells, value); }
        }

        /// <summary>
        /// The Cells in this Workbook that contain violations
        /// </summary>
        public ObservableCollection<CellLocation> ViolatedCells
        {
            get
            {
                if (_violatedCells == null) _violatedCells = new ObservableCollection<CellLocation>();
                return _violatedCells;
            }
            set { SetProperty(ref _violatedCells, value); }
        }


        public Boolean SanityWarnings
        {
            get { return _sanityWarnings; }
            set { _sanityWarnings = value; }
        }

        /// <summary>
        /// Gets or sets the output cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> OutputCells
        {
            get
            {
                if (_outputCells == null) _outputCells = new ObservableCollection<Cell>();
                return _outputCells;
            }
            set { SetProperty(ref _outputCells, value); }
        }

        /// <summary>
        /// Gets or sets the violations of the current document
        /// </summary>
        public ObservableCollection<Violation> Violations
        {
            get
            {
                if (_violations == null)
                {
                    _violations = new ObservableCollection<Violation>();
                    _violations.CollectionChanged += violations_CollectionChanged;
                }

                return _violations;
            }
            set { SetProperty(ref _violations, value); }
        }


        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<Violation> IgnoredViolations
        {
            get
            {
                if (_ignoredViolations == null) _ignoredViolations = new ObservableCollection<Violation>();
                return _ignoredViolations;
            }
            set { SetProperty(ref _ignoredViolations, value); }
        }

        /// <summary>
        /// Gets or sets the violations that got marked with later
        /// </summary>
        public ObservableCollection<Violation> LaterViolations
        {
            get
            {
                if (_laterViolations == null) _laterViolations = new ObservableCollection<Violation>();
                return _laterViolations;
            }
            set { SetProperty(ref _laterViolations, value); }
        }

        /// <summary>
        /// Gets or sets the solved violations of the current document
        /// </summary>
        public ObservableCollection<Violation> SolvedViolations
        {
            get
            {
                if (_solvedViolations == null)
                {
                    _solvedViolations = new ObservableCollection<Violation>();
                }
                return _solvedViolations;
            }
            set { SetProperty(ref _solvedViolations, value); }
        }

        /// <summary>
        /// Gets or sets the scenarios of the current document.
        /// </summary>
        public ObservableCollection<ScenarioCore.Scenario> Scenarios
        {
            get
            {
                if (_scenarios == null) _scenarios = new ObservableCollection<ScenarioCore.Scenario>();
                return _scenarios;
            }
            set { SetProperty(ref _scenarios, value); }
        }

        /// <summary>
        /// Gets or sets the count of the unread violations
        /// </summary>
        public int UnreadViolationCount
        {
            get { return _unreadViolationCount; }
            set { SetProperty(ref _unreadViolationCount, value); }
        }

        /// <summary>
        /// Gets or sets the Excel workbook of this model.
        /// </summary>
        public Workbook Workbook
        {
            get { return _workbook; }
            set { SetProperty(ref _workbook, value); }
        }

        /// <summary>
        /// Gets or sets the translated name of the current tab (for the label)
        /// </summary>
        public string SelectedTabLabel
        {
            get { return _selectedTabLabel; }
            set { SetProperty(ref _selectedTabLabel, value); }
        }

        /// <summary>
        /// Gets or sets which tab of the sidepane is selected
        /// </summary>
        public SharedTabs SelectedTab
        {
            get { return _selectedTab; }
            set
            {
                SetProperty(ref _selectedTab, value);
                ViolatedCells.ToList().ForEach(vc => vc.SetVisibility(value));
            }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            WorkbookModel other = obj as WorkbookModel;
            if ((object) other == null) return false;

            return Title == other.Title &&
                   Spreadsheet == other.Spreadsheet &&
                   PolicyPath == other.PolicyPath &&
                   Policy == other.Policy &&
                   InputCells.SequenceEqual(other.InputCells) &&
                   IntermediateCells.SequenceEqual(other.IntermediateCells) &&
                   OutputCells.SequenceEqual(other.OutputCells) &&
                   IgnoredViolations.SequenceEqual(other.IgnoredViolations) &&
                   Scenarios.SequenceEqual(other.Scenarios) &&
                   ReferenceEquals(Workbook, other.Workbook);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines, whether two objects are equal.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>true, if the given instances are equal; otherwise, false.</returns>
        public static bool operator ==(WorkbookModel a, WorkbookModel b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (((object) a == null) || ((object) b == null)) return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Determines, whether two objects are inequal.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>true, if the given instances are inequal; otherwise, false.</returns>
        public static bool operator !=(WorkbookModel a, WorkbookModel b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        #region Lifecycle Events

        /// <summary>
        /// Initializes a new instance of the WorkbookModel class with a given workbook.
        /// </summary>
        /// <param name="workbook">The workbook that is used for initialization.</param>
        public WorkbookModel(Workbook workbook)
        {
            ShouldScanAfterSave = false;
            Workbook = workbook;

            Workbook.BeforeSave += workbook_BeforeSave;
            Workbook.BeforeClose += Workbook_BeforeClose;
            Workbook.AfterSave += Workbook_AfterSave;
            Workbook.SheetSelectionChange += sheet_SelectionChange;
            // Occurs after any worksheet is recalculated or after any changed data is plotted on a chart.
            this._workbook.SheetCalculate += workbook_SheetCalculate;
        }

        /// <summary>
        /// Loads all the data related to violations and scenarios
        /// </summary>
        public void LoadExtraInformation()
        {
            String error = string.Empty;
            try
            {
                // Load cell definitions
                Accept(new XMLToCellDefinitionVisitor(XMLPartManager.Instance.LoadXMLPart(this, "CellDefinitions")));
            }
            catch (Exception)
            {
                error += "Loading the cell definitions failed.\n";
            }
            try
            {
                // Load the scenarios
                Accept(new XMLToScenarioVisitor(XMLPartManager.Instance.LoadXMLPart(this, "Scenario")));
            }
            catch (Exception)
            {
                error += "Loading the scenarios failed.\n";
            }

            try
            {
                // Load the violations
                var violationsXml = XMLPartManager.Instance.LoadXMLPart(this, "Violations");
                if (violationsXml != null)
                {
                    // Add them to the violations collection
                    (from p in violationsXml.Elements(XName.Get("violation"))
                        select new Violation(p, _workbook)).ToList().ForEach(p => Violations.Add(p));
                }
            }
            catch (Exception)
            {
                error += "Loading the displayed violations failed.\n";
            }

            try
            {
                // Load the ignored
                var ignoredXml = XMLPartManager.Instance.LoadXMLPart(this, "IgnoredViolations");
                if (ignoredXml != null)
                {
                    // Add them to the ignored violations collection
                    (from p in ignoredXml.Elements(XName.Get("ignoredviolations"))
                        select new Violation(p, _workbook)).ToList().ForEach(p => IgnoredViolations.Add(p));
                }
            }
            catch (Exception)
            {
                error += "Loading the ignored violations failed.\n";
            }

            try
            {
                // Load the later violations
                var laterXml = XMLPartManager.Instance.LoadXMLPart(this, "LaterViolations");
                if (laterXml != null)
                {
                    // Add them to the later violations collection
                    (from p in laterXml.Elements(XName.Get("laterviolation"))
                        select new Violation(p, _workbook)).ToList().ForEach(p => LaterViolations.Add(p));
                }
            }
            catch (Exception)
            {
                error += "Loading the 'later' violations failed.\n";
            }

            try
            {
                // Load the archived violations
                var archivedXml = XMLPartManager.Instance.LoadXMLPart(this, "ArchivedViolations");
                if (archivedXml != null)
                {
                    // Add them to the archived violations collection
                    (from p in archivedXml.Elements(XName.Get("archivedviolation"))
                        select new Violation(p, _workbook)).ToList().ForEach(p => SolvedViolations.Add(p));
                }
            }
            catch (Exception)
            {
                error += "Loading the archived violations failed.\n";
            }

            XElement polSettings = XMLPartManager.Instance.LoadXMLPart(this, "policySettings");
            PolicyConfigurationModel polModel = new PolicyConfigurationModel();
            try
            {
                polModel.loadXML(polSettings);
            }
            catch (Exception e)
            {
                // no settings existed, use the default config
                polModel = new PolicyConfigurationModel();
            }
            _policySettings = polModel;

            if (!String.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(Resources.tl_Load_Failed + error,
                    Resources.tl_Load_Failed_Title);
            }
        }

        private void sheet_SelectionChange(object sh, Range target)
        {
            if (target.Cells.Count == 1)
            {
                int row = target.Cells.Row;
                int column = target.Cells.Column;
                string location = "=" + target.Worksheet.Name + "!" + GetExcelColumnName(column) + row;
                CellLocation cell = new CellLocation(_workbook, location);
                switch (SelectedTab)
                {
                    case SharedTabs.Open:
                        Violations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in Violations where vi.Cell.EqualsWithoutType(cell) select vi).ToList()
                            .ForEach(vi => vi.IsCellSelected = true);
                        break;
                    case SharedTabs.Later:
                        LaterViolations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in LaterViolations where vi.Cell.EqualsWithoutType(cell) select vi).ToList()
                            .ForEach(vi => vi.IsCellSelected = true);
                        break;
                    case SharedTabs.Ignore:
                        IgnoredViolations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in IgnoredViolations where vi.Cell.EqualsWithoutType(cell) select vi).ToList()
                            .ForEach(vi => vi.IsCellSelected = true);
                        break;
                    case SharedTabs.Archive:
                        SolvedViolations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in SolvedViolations where vi.Cell.EqualsWithoutType(cell) select vi).ToList()
                            .ForEach(vi => vi.IsCellSelected = true);
                        break;
                }
            }
        }

        /// <summary>
        /// Gets the name / location of a column in the workbook
        /// </summary>
        /// <param name="columnNumber"></param>
        /// <returns></returns>
        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1)%26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int) ((dividend - modulo)/26);
            }

            return columnName;
        }

        /// <summary>
        /// Happens after a Workbook gets saved
        /// </summary>
        /// <param name="success"> Gives back if the saving process was succesfull</param>
        private void Workbook_AfterSave(bool success)
        {
            if (ShouldScanAfterSave) return;
            // Run a scan if necessary
            if (PolicySettings.HasAutomaticScans() && Settings.Default.AutomaticScans)
            {
                // Checks if if would be allowed to scan
                if (Globals.Ribbons.Ribbon.scanButton.Enabled)
                {
                    // Makes sure the file is saved before starting the Scan after Save.
                    // Important: DON'T DELETE seems redundant and unnecessary since the Method only seems to get called
                    // after the File is saved. But it is also called when a Saving process got aborted (e.g. by the user by pressing no
                    // in the dialog box)
                    if (Workbook.Path.Length > 0)
                    {
                        Inspect(InspectionType.LIVE);
                    }
                }
            }
        }

        /// <summary>
        /// Occurs after any workbook is recalculated or after any changed data is plotted on a chart.
        /// </summary>
        /// <param name="sh"></param>
        private void workbook_SheetCalculate(object sh)
        {
            // Run a scan if necessary
            if (!PolicySettings.HasAutomaticScans() || !Settings.Default.AutomaticScans) return;
            if (!Globals.Ribbons.Ribbon.scanButton.Enabled) return;
            // SIFCore can't handle if it the documents is not saved. So if an inspection is started it is assured the file is saved somewhere.
            ScanHelper.SaveBefore(InspectionType.LIVE);
        }


        /// <summary>
        /// Saves the custom XML parts that are used to persist the cells, scenarios and false positives.
        /// </summary>
        private void workbook_BeforeSave(bool saveAsUi, ref bool cancel)
        {
            //Save the violations
            XMLPartManager.Instance.SaveXMLPart(this,
                new XElement(XName.Get("violations"), from p in Violations select p.ToXElement("violation")),
                "Violations");

            //Save the false positives
            XMLPartManager.Instance.SaveXMLPart(this,
                new XElement(XName.Get("ignoredviolations"),
                    from p in IgnoredViolations select p.ToXElement("ignoredviolations")), "IgnoredViolations");

            // Save the later violations
            XMLPartManager.Instance.SaveXMLPart(this,
                new XElement(XName.Get("laterviolations"),
                    from p in LaterViolations select p.ToXElement("laterviolation")), "LaterViolations");

            // Save the solved violations
            XMLPartManager.Instance.SaveXMLPart(this,
                new XElement(XName.Get("archivedviolations"),
                    from p in SolvedViolations select p.ToXElement("archivedviolation")), "ArchivedViolations");

            // Save the scenarios
            XMLPartManager.Instance.SaveXMLPart(this, Accept(new ScenarioToXMLVisitor()) as XElement, "Scenario");

            // Save the cell definitions
            XMLPartManager.Instance.SaveXMLPart(this, Accept(new CellDefinitionToXMLVisitor()) as XElement,
                "CellDefinitions");

            // Save the policy configuration
            XElement polSettings = new XElement("policySettings");
            _policySettings.saveXML(polSettings);
            XMLPartManager.Instance.SaveXMLPart(this, polSettings, "policySettings");
        }


        /// <summary>
        /// Handle the scenario controls in the cells before close.
        /// </summary>
        /// <param name="cancel"></param>
        void Workbook_BeforeClose(ref bool cancel)
        {
            ShouldScanAfterSave = true;
            ScenarioUICreator.Instance.End();
            // Deletes all controls that might be in the cells (markers)
            foreach (Worksheet worksheet in Workbook.Worksheets)
            {
                var worksheet2 = Globals.Factory.GetVstoObject(worksheet);

                System.Collections.ArrayList controlsToRemove =
                    new System.Collections.ArrayList();

                // Get all of the Windows Forms controls.
                foreach (object control in worksheet2.Controls)
                {
                    if (control is System.Windows.Forms.Control)
                    {
                        controlsToRemove.Add(control);
                    }
                }

                // Remove all of the Windows Forms controls from the document.
                foreach (object control in controlsToRemove)
                {
                    worksheet2.Controls.Remove(control);
                }
            }
        }

        #endregion

        /// <summary>
        /// Launches an inspection job for this workbook with all available tests.
        /// </summary>
        public void Inspect(InspectionType inspectionType)
        {
            Inspect(InspectionMode.All, inspectionType);
        }


        private void Inspect(object inspectionType)
        {
            InspectionType insp;
            insp = (InspectionType) (inspectionType);
            Inspect(InspectionMode.All, insp);
        }

        /// <summary>
        /// Launches an inspection job for this workbook.
        /// </summary>
        public void Inspect(InspectionMode inspectionMode, InspectionType inspectionType)
        {
            Globals.ThisAddIn.Application.StatusBar = Resources.tl_ProcessingScan;
            Globals.Ribbons.Ribbon.scanButton.Enabled = false;
            Globals.Ribbons.Ribbon.scanButton.Label = Resources.tl_NoScanPossible;

            // Save a copy of this workbook temporarily
            string workbookFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                  System.IO.Path.DirectorySeparatorChar + Guid.NewGuid().ToString() + ".xls";
            Workbook.SaveCopyAs(workbookFile);


            var xmlDoc = new XDocument();

            // Create the rules
            switch (inspectionMode)
            {
                case InspectionMode.All:
                    xmlDoc.Add(Accept(new Sprudel1_5XMLVisitor(inspectionType)) as XElement);
                    //TODO add static
                    break;
                case InspectionMode.Dynamic:
                    xmlDoc.Add(Accept(new Sprudel1_5XMLVisitor(inspectionType)) as XElement);
                    break;
                case InspectionMode.Static:
                    //TODO change to static
                    xmlDoc.Add(Accept(new Sprudel1_5XMLVisitor(inspectionType)) as XElement);
                    break;
            }

            var x = xmlDoc.Element(XName.Get("policyList"));
            var a = x.Element(XName.Get("dynamicPolicy"));
            var b = a.Element(XName.Get("spreadsheetFilePath"));
            b.Value = workbookFile;

            xmlDoc.Validate(XMLPartManager.Instance.GetRequestSchema(), null);

            // Enqueue this inspection
            InspectionEngine.Instance.InspectionQueue.Add(new InspectionJob(this, workbookFile, xmlDoc));
        }

        /// <summary>
        /// This method loads the XML report generated by the SIF into this workbook model.
        /// </summary>
        public void Load(string xml)
        {
            /*
             * Load the data
             */
            if (xml == null || xml.Length <= 0)
            {
                ScanHelper.ScanUnsuccessful(Resources.tl_SIFCorecrashed);
            }
            else
            {
                try
                {
                    XElement rootElement = LoadXml(xml);
                    LoadViolations(rootElement);

                    ScanHelper.ScanSuccessful();
                    tries = 0;
                }
                catch (Exception ex)
                {
                    if (tries <= 3)
                    {
                        tries++;
                        Load(xml);
                    }
                    else
                    {
                        ScanHelper.ScanUnsuccessful();
                        tries = 0;
                    }
                }
            }
            
        }

        /// <summary>
        /// Loads the Information saved in the xml
        /// </summary>
        /// <param name="xml"></param>
        private XElement LoadXml(string xml)
        {
            XElement rootElement = XElement.Parse(xml);
                    XDocument d = new XDocument(rootElement);
                    d.Validate(XMLPartManager.Instance.getReportSchema(), null);

                    // General attributes
                    var titleAttribute = rootElement.Attribute(XName.Get("title"));
                    if (titleAttribute != null) Title = titleAttribute.Value;
                    else Title = null;

                    var spreadsheetAttribute = rootElement.Attribute(XName.Get("file"));
                    if (_spreadsheet != null) Spreadsheet = spreadsheetAttribute.Value;
                    else Spreadsheet = null;

                    // Policy
                    var policyElement = rootElement.Element(XName.Get("policy"));
                    if (policyElement != null) Policy = new Policy(policyElement);
                    else Policy = null;

                    // Cells
                    var cellsElement = rootElement.Element(XName.Get("cells"));
                    if (cellsElement != null)
                    {
                        var cells = cellsElement;

                        // Input cells
                        cells.Element(XName.Get("input"));
                        //this.ParseCells(inputCells, this.InputCells, typeof(InputCell)); /*functionality dosn't right, we need a inteligent mergeing*/

                        // Intermediate cells
                        cells.Element(XName.Get("intermediate"));
                        //this.ParseCells(intermediateCells, this.IntermediateCells, typeof(IntermediateCell));

                        // Output cells
                        cells.Element(XName.Get("output"));
                        //this.ParseCells(outputCells, this.OutputCells, typeof(OutputCell));
                    }
                    else
                    {
                        InputCells.Clear();
                        IntermediateCells.Clear();
                        OutputCells.Clear();
                    }
            return rootElement;
        }


        /// <summary>
        /// This method loads the violations of the xml report
        /// </summary>
        private void LoadViolations(XElement rootElement)
        {
            try
            {
                DateTime scanTime = DateTime.Now;
                XNamespace ns = "http://www.w3.org/2001/XMLSchema-instance";
                var findings = rootElement.Element(XName.Get("findings"));
                var violations = new List<Violation>();
                foreach (var ruleXml in findings.Elements(XName.Get("testedRule")))
                {
                    Rule rule = new Rule(ruleXml.Element(XName.Get("testedPolicy")));

                    // Parse violations
                    var xmlVio = ruleXml.Elements(XName.Get("violations"));
                    foreach (XElement vio in xmlVio)
                    {
                        var x = vio.Attribute(ns + "type");
                        if (x.Value.Equals("singleviolation"))
                        {
                            violations.Add(new Violation(vio, _workbook, scanTime, rule));
                        }
                        else if (x.Value.Equals("violationgroup"))
                        {
                            (from p in vio.Elements(XName.Get("singleviolation"))
                                select new Violation(p, _workbook, scanTime, rule)).ToList()
                                .ForEach(p => violations.Add(p));
                        }
                    }
                }
                // Add only new violations
                AddNewViolations(violations);
                // mark all solved violations from the Open Category
                MarkSolvedViolations(scanTime, Violations);
                // mark all solved violations from the Later Category
                MarkSolvedViolations(scanTime, LaterViolations);
                // mark all solved violations
                tries = 0;
            }
            catch (Exception ex)
            {
                if (tries <= 3)
                {
                    tries++;
                    LoadViolations(rootElement);
                }
                else
                {
                    ScanHelper.ScanUnsuccessful();
                    tries = 0;
                }
            }
        }

        /// <summary>
        /// Marks all solved Violations as marked
        /// </summary>
        /// <param name="scanTime"></param>
        private void MarkSolvedViolations(DateTime scanTime,ObservableCollection<Violation> Violations)
        {
            try
            {
                for (int i = Violations.Count - 1; i >= 0; i--)
                {
                    if (Violations.ElementAt(i).FoundAgain)
                    {
                        Violations.ElementAt(i).FoundAgain = false;
                    }
                    // If it didnt get found again means it didnt appear again ergo its solved
                    else
                    {
                        Violations.ElementAt(i).SolvedTime = scanTime;
                        Violations.ElementAt(i).ViolationState = ViolationType.SOLVED;
                    }
                }
            }
            catch (COMException) { }
            catch (TargetInvocationException){}
        }

        /// <summary>
        /// Adds all the new Violations to the Violations collection
        /// </summary>
        /// <param name="violations"></param>
        private void AddNewViolations(List<Violation> violations)
        {
            foreach (Violation violation in violations)
            {
                if (Violations.Contains(violation))
                {
                    Violations.ElementAt(Violations.IndexOf(violation)).FoundAgain = true;
                }

                else if ((from vi in IgnoredViolations
                          where
                              (vi.Cell.Letter.Equals(violation.Cell.Letter) &&
                               vi.Cell.Number.Equals(violation.Cell.Number))
                          select vi).Count() > 0)
                {
                    // nothing to do here
                }
                else if (LaterViolations.Contains(violation))
                {
                    LaterViolations.ElementAt(LaterViolations.IndexOf(violation)).FoundAgain = true;
                }
                else
                {
                    violation.PersistCellLocation();
                    Violations.Add(violation);
                }
            }
        }


        /// <summary>
        /// Adds automatic cells to the cell definitions list.
        /// </summary>
        /// <param name="root">XML List of cells</param>
        /// <param name="targetCollection">Target collection e.g. input cells, intermediate cells, or result cells</param>
        /// <param name="cellType">Class of the cell type</param>
        private void ParseCells(XElement root, ObservableCollection<Cell> targetCollection, Type cellType)
        {
            var cellElements = root.Elements(XName.Get("cell"));
            if (cellElements != null)
            {
                foreach (var element in cellElements)
                {
                    var cell = new Cell(element, _workbook);
                    if (cellType == typeof (InputCell))
                    {
                        var list = new List<Cell>();
                        list.Add(cell);
                        DefineInputCell(list, CellDefinitionOption.Define);
                    }
                    else if (cellType == typeof (IntermediateCell))
                    {
                        var list = new List<Cell>();
                        list.Add(cell);
                        DefineIntermediateCell(list, CellDefinitionOption.Define);
                    }
                    else if (cellType == typeof (OutputCell))
                    {
                        var list = new List<Cell>();
                        list.Add(cell);
                        DefineOutputCell(list, CellDefinitionOption.Define);
                    }
                }
            }
        }

        #region define cells

        /// <summary>
        /// Defines or undefines a input cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the input cell list</param>
        public void DefineInputCell(List<Cell> inputs, CellDefinitionOption define)
        {
            switch (define)
            {
                case CellDefinitionOption.Define:
                    foreach (var c in inputs)
                    {
                        // create sif cell name
                        if (c.SifLocation == null)
                        {
                            c.SifLocation = CellManager.Instance.CreateSIFCellName(this,
                                CellManager.Instance.GetA1Adress(this, c.Location));
                        }

                        //remove from the other lists
                        IntermediateCells.Remove(c);
                        OutputCells.Remove(c);

                        //add to input list
                        if (!_inputCells.Contains(c))
                        {
                            InputCells.Add(c.ToInputCell());
                        }
                    }
                    break;
                case CellDefinitionOption.Undefine:
                    //remove from input list
                    foreach (var c in inputs)
                    {
                        try
                        {
                            InputCells.Remove(c);
                        }
                        catch (Exception e)
                        {
                            Console.Out.WriteLine(e);
                        }
                    }
                    break;
            }
            OnCellDefinitionChanged(new EventArgs());
        }


       
        /// <summary>
        /// Defines or undefines a intermediate cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public
        void DefineIntermediateCell
            (List<Cell> intermediates, CellDefinitionOption define)
        {
            if (define == CellDefinitionOption.Define)
            {
                foreach (var c in intermediates)
                {
                    // create sif cell name
                    if (c.SifLocation == null)
                    {
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this,
                            CellManager.Instance.GetA1Adress(this, c.Location));
                    }
                    //remove from the other lists
                    InputCells.Remove(c);
                    OutputCells.Remove(c);

                    //add to intermediate list
                    if (!_intermediateCells.Contains(c))
                    {
                        _intermediateCells.Add(c.ToIntermediateCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in intermediates)
                {
                    _intermediateCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity value cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public
            void DefineSanityValueCell
            (List<Cell> sanityValues, CellDefinitionOption define)
        {
            if (define == CellDefinitionOption.Define)
            {
                foreach (var c in sanityValues)
                {
                    // create sif cell name
                    if (c.SifLocation == null)
                    {
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this,
                            CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    InputCells.Remove(c);
                    OutputCells.Remove(c);
                    IntermediateCells.Remove(c);
                    _sanityCheckingCells.Remove(c);
                    _sanityExplanationCells.Remove(c);
                    _sanityConstraintCells.Remove(c);
                    //add to sanityValue list
                    if (!_sanityValueCells.Contains(c))
                    {
                        _sanityValueCells.Add(c.ToSanityValueCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityValues)
                {
                    _sanityValueCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity value cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public
            void DefineSanityConstraintCell
            (List<Cell> sanityConstraints, CellDefinitionOption define)
        {
            if (define == CellDefinitionOption.Define)
            {
                foreach (var c in sanityConstraints)
                {
                    // create sif cell name
                    if (c.SifLocation == null)
                    {
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this,
                            CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    InputCells.Remove(c);
                    OutputCells.Remove(c);
                    IntermediateCells.Remove(c);
                    _sanityCheckingCells.Remove(c);
                    _sanityExplanationCells.Remove(c);
                    _sanityValueCells.Remove(c);
                    //add to sanityValue list
                    if (!_sanityConstraintCells.Contains(c))
                    {
                        _sanityConstraintCells.Add(c.ToSanityConstraintCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityConstraints)
                {
                    _sanityConstraintCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity value cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public
            void DefineSanityExplanationCell
            (List<Cell> sanityExplanations, CellDefinitionOption define)
        {
            if (define == CellDefinitionOption.Define)
            {
                foreach (var c in sanityExplanations)
                {
                    // create sif cell name
                    if (c.SifLocation == null)
                    {
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this,
                            CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    InputCells.Remove(c);
                    OutputCells.Remove(c);
                    IntermediateCells.Remove(c);
                    _sanityCheckingCells.Remove(c);
                    _sanityValueCells.Remove(c);
                    _sanityConstraintCells.Remove(c);
                    //add to sanityExplanation list
                    if (!_sanityExplanationCells.Contains(c))
                    {
                        _sanityExplanationCells.Add(c.ToSanityExplanationCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityExplanations)
                {
                    _sanityExplanationCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity Checking cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public
            void DefineSanityCheckingCell
            (List<Cell> sanityCheckings, CellDefinitionOption define)
        {
            if (define == CellDefinitionOption.Define)
            {
                foreach (var c in sanityCheckings)
                {
                    // create sif cell name
                    if (c.SifLocation == null)
                    {
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this,
                            CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    InputCells.Remove(c);
                    OutputCells.Remove(c);
                    IntermediateCells.Remove(c);
                    _sanityValueCells.Remove(c);
                    _sanityExplanationCells.Remove(c);
                    _sanityConstraintCells.Remove(c);
                    //add to sanityChecking list
                    if (!_sanityCheckingCells.Contains(c))
                    {
                        _sanityCheckingCells.Add(c.ToSanityCheckingCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityCheckings)
                {
                    _sanityCheckingCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a output cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the output cell list</param>
        public
        void DefineOutputCell
            (List<Cell> outputs, CellDefinitionOption define)
        {
            if (define == CellDefinitionOption.Define)
            {
                foreach (var c in outputs)
                {
                    // create sif cell name
                    if (c.SifLocation == null)
                    {
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this,
                            CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    InputCells.Remove(c);
                    IntermediateCells.Remove(c);

                    //add to intermediate list
                    if (!_outputCells.Contains(c))
                    {
                        _outputCells.Add(c.ToOutputCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in outputs)
                {
                    _outputCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        #endregion

        #region Accept Visitor

        public
            object Accept
            (IVisitor
                v)
        {
            return v.Visit(this);
        }

        #endregion

        #endregion

        #region Event Handling

        public
            delegate
            void CellDefinitionChangeHandler
            (object sender, EventArgs data);

        public event
            CellDefinitionChangeHandler CellDefinitionChange;

        protected
            void OnCellDefinitionChanged
            (EventArgs
                data)
        {
            if (CellDefinitionChange != null)
            {
                CellDefinitionChange(this, data);
            }
        }

        /// <summary>
        /// Sets the unread violations count when the violations collection changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private
            void violations_CollectionChanged
            (object sender, NotifyCollectionChangedEventArgs e)
        {
            UnreadViolationCount = (from vi in _violations where vi.IsRead == false select vi).Count();
        }

        #endregion
    }
}
