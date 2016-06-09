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
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Schema;
using Microsoft.Office.Tools.Outlook;
using MessageBox = System.Windows.MessageBox;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// This is the model class for one worksheet.
    /// </summary>
    public class WorkbookModel : BindableBase, IAcceptVisitor
    {
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

        public enum InspectionMode
        {
            /// <summary>
            /// Starts the Inspection with the scenarios, without static tests
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

        // private Object lockObject = new Object();

        #region Fields

        private string title;
        private string spreadsheet;
        private string policyPath;
        private Policy policy;
        private ObservableCollection<Cell> inputCells;
        private ObservableCollection<Cell> sanityValueCells;
        private ObservableCollection<Cell> sanityConstraintCells;
        private ObservableCollection<Cell> sanityExplanationCells;
        private ObservableCollection<Cell> sanityCheckingCells;
        private ObservableCollection<Cell> intermediateCells;
        private ObservableCollection<Cell> outputCells;
        private ObservableCollection<Violation> violations;
        private ObservableCollection<Violation> ignoredViolations;
        private ObservableCollection<Violation> laterViolations;
        private ObservableCollection<Violation> solvedViolations;
        private ObservableCollection<CellLocation> violatedCells;
        private int unreadViolationCount;
        private ObservableCollection<ScenarioCore.Scenario> scenarios;
        private Boolean sanityWarnings = true;
        private SharedTabs selectedTab;
        private string selectedTabLabel = "unnamed";

        private Workbook workbook;
        private PolicyConfigurationModel policySettings;

        #endregion

        #region Properties

        public PolicyConfigurationModel PolicySettings
        {
            get
            {
                if (policySettings == null)
                {
                    policySettings = new PolicyConfigurationModel();
                }
                return policySettings;
            }
            set { policySettings = value; }
        }

        /// <summary>
        /// Gets or sets the title of the current inspection.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        /// <summary>
        /// Gets or sets the file path of the inspected spreadsheet.
        /// </summary>
        public string Spreadsheet
        {
            get { return spreadsheet; }
            set { SetProperty(ref spreadsheet, value); }
        }

        /// <summary>
        /// Gets or sets the policy path of the inspected spreadsheet.
        /// </summary>
        public string PolicyPath
        {
            get { return policyPath; }
            set { SetProperty(ref policyPath, value); }
        }

        /// <summary>
        /// Gets or sets the policy of the inspected spreadsheet.
        /// </summary>
        public Policy Policy
        {
            get { return policy; }
            set { SetProperty(ref policy, value); }
        }

        /// <summary>
        /// Gets or sets the input cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> InputCells
        {
            get
            {
                if (inputCells == null) inputCells = new ObservableCollection<Cell>();
                return inputCells;
            }
            set { SetProperty(ref inputCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> IntermediateCells
        {
            get
            {
                if (intermediateCells == null) intermediateCells = new ObservableCollection<Cell>();
                return intermediateCells;
            }
            set { SetProperty(ref intermediateCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityValueCells
        {
            get
            {
                if (sanityValueCells == null) sanityValueCells = new ObservableCollection<Cell>();
                return sanityValueCells;
            }
            set { SetProperty(ref sanityValueCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityConstraintCells
        {
            get
            {
                if (sanityConstraintCells == null) sanityConstraintCells = new ObservableCollection<Cell>();
                return sanityConstraintCells;
            }
            set { SetProperty(ref sanityConstraintCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityExplanationCells
        {
            get
            {
                if (sanityExplanationCells == null) sanityExplanationCells = new ObservableCollection<Cell>();
                return sanityExplanationCells;
            }
            set { SetProperty(ref sanityExplanationCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityCheckingCells
        {
            get
            {
                if (sanityCheckingCells == null) sanityCheckingCells = new ObservableCollection<Cell>();
                return sanityCheckingCells;
            }
            set { SetProperty(ref sanityCheckingCells, value); }
        }

        public ObservableCollection<CellLocation> ViolatedCells
        {
            get
            {
                if (violatedCells == null) violatedCells = new ObservableCollection<CellLocation>();
                return violatedCells;
            }
            set { SetProperty(ref violatedCells, value); }
        }

        public Boolean SanityWarnings
        {
            get { return sanityWarnings; }
            set { sanityWarnings = value; }
        }

        /// <summary>
        /// Gets or sets the output cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> OutputCells
        {
            get
            {
                if (outputCells == null) outputCells = new ObservableCollection<Cell>();
                return outputCells;
            }
            set { SetProperty(ref outputCells, value); }
        }

        /// <summary>
        /// Gets or sets the violations of the current document
        /// </summary>
        public ObservableCollection<Violation> Violations
        {
            get
            {
                if (violations == null)
                {
                    violations = new ObservableCollection<Violation>();
                    violations.CollectionChanged += violations_CollectionChanged;
                }

                return violations;
            }
            set { SetProperty(ref violations, value); }
        }


        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<Violation> IgnoredViolations
        {
            get
            {
                if (ignoredViolations == null) ignoredViolations = new ObservableCollection<Violation>();
                return ignoredViolations;
            }
            set { SetProperty(ref ignoredViolations, value); }
        }

        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<Violation> LaterViolations
        {
            get
            {
                if (laterViolations == null) laterViolations = new ObservableCollection<Violation>();
                return laterViolations;
            }
            set { SetProperty(ref laterViolations, value); }
        }

        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<Violation> SolvedViolations
        {
            get
            {
                if (solvedViolations == null)
                {
                    solvedViolations = new ObservableCollection<Violation>();
                }
                return solvedViolations;
            }
            set { SetProperty(ref solvedViolations, value); }
        }

        /// <summary>
        /// Gets or sets the scenarios of the current document.
        /// </summary>
        public ObservableCollection<ScenarioCore.Scenario> Scenarios
        {
            get
            {
                if (scenarios == null) scenarios = new ObservableCollection<ScenarioCore.Scenario>();
                return scenarios;
            }
            set { SetProperty(ref scenarios, value); }
        }

        /// <summary>
        /// Gets or sets the count of the unread violations
        /// </summary>
        public int UnreadViolationCount
        {
            get { return unreadViolationCount; }
            set { SetProperty(ref unreadViolationCount, value); }
        }

        /// <summary>
        /// Gets or sets the Excel workbook of this model.
        /// </summary>
        public Workbook Workbook
        {
            get { return workbook; }
            set { SetProperty(ref workbook, value); }
        }

        /// <summary>
        /// Gets or sets the translated name of the current tab (for the label)
        /// </summary>
        public string SelectedTabLabel
        {
            get { return selectedTabLabel; }
            set { SetProperty(ref selectedTabLabel, value); }
        }


        public SharedTabs SelectedTab
        {
            get { return selectedTab; }
            set
            {
                SetProperty(ref selectedTab, value);
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
            Workbook = workbook;

            Workbook.BeforeSave += Workbook_BeforeSave;
            Workbook.BeforeClose += Workbook_BeforeClose;
            Workbook.AfterSave += Workbook_AfterSave;
            Workbook.SheetSelectionChange += Sheet_SelectionChange;
            // Occurs after any worksheet is recalculated or after any changed data is plotted on a chart.
            this.workbook.SheetCalculate += Workbook_SheetCalculate;
        }

        public void LoadExtraInformation()
        {
            String error = "";
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
                        select new Violation(p, workbook)).ToList().ForEach(p => Violations.Add(p));
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
                        select new Violation(p, workbook)).ToList().ForEach(p => IgnoredViolations.Add(p));
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
                        select new Violation(p, workbook)).ToList().ForEach(p => LaterViolations.Add(p));
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
                        select new Violation(p, workbook)).ToList().ForEach(p => SolvedViolations.Add(p));
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
            policySettings = polModel;

            if (!String.IsNullOrWhiteSpace(error))
            {
                MessageBox.Show(Resources.tl_Load_Failed + error,
                   Resources.tl_Load_Failed_Title);
            }
        }

        private void Sheet_SelectionChange(object Sh, Range Target)
        {
            if (Target.Cells.Count == 1)
            {
                int row = Target.Cells.Row;
                int column = Target.Cells.Column;
                string location = "=" + Target.Worksheet.Name + "!" + getExcelColumnName(column) + row;
                CellLocation cell = new CellLocation(workbook, location);
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

        private string getExcelColumnName(int columnNumber)
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

        private void Workbook_AfterSave(bool Success)
        {
            // Run a scan if necessary
            if (PolicySettings.hasAutomaticScans() && Settings.Default.AutomaticScans)
            {
                // Cecks if if would be allowed to scan
                if (Globals.Ribbons.Ribbon.scanButton.Enabled)
                {
                    // Makes sure the file is saved before starting the Scan after Save.
                    // Important: DON'T DELETE seems redundant and unnecessary since the Method only seems to get called
                    // after the File is saved. But it is also called when a Saving process got abortet (e.g. by the user by pressing no
                    // in the dialog box)
                    if (Workbook.Saved)
                    {
                        Inspect(InspectionType.LIVE);
                    }
                }
            }
        }

        /// <summary>
        /// Occurs after any worksheet is recalculated or after any changed data is plotted on a chart.
        /// </summary>
        /// <param name="Sh"></param>
        private void Workbook_SheetCalculate(object Sh)
        {
            // Run a scan if necessary
            if (PolicySettings.hasAutomaticScans() && Settings.Default.AutomaticScans)
            {
                if (Globals.Ribbons.Ribbon.scanButton.Enabled)
                {
                    Inspect(InspectionType.LIVE);
                }
            }
        }


        /// <summary>
        /// Saves the custom XML parts that are used to persist the cells, scenarios and false positives.
        /// </summary>
        private void Workbook_BeforeSave(bool SaveAsUI, ref bool Cancel)
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
            policySettings.saveXML(polSettings);
            XMLPartManager.Instance.SaveXMLPart(this, polSettings, "policySettings");
        }


        /// <summary>
        /// Handle the scenario controls in the cells before close.
        /// </summary>
        /// <param name="Cancel"></param>
        void Workbook_BeforeClose(ref bool Cancel)
        {
            ScenarioUICreator.Instance.End();
            //RemoveIcon();
        }

        #endregion

        /// <summary>
        /// Launches an inspection job for this workbook with all available tests.
        /// </summary>
        public void Inspect(InspectionType inspectionType)
        {
            Inspect(InspectionMode.All, inspectionType);
            
            // Thread thread = new Thread(new ParameterizedThreadStart(Inspect));
            // thread.Start(inspectionType);
            //Task task1 = Task.Factory.StartNew(Inspect, inspectionType);
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
                // lock (lockObject)
                {
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


        }

        /// <summary>
        /// This method loads the XML report generated by the SIF into this workbook model.
        /// </summary>
        public void Load(string xml)
        {
            /*
             * Load the data
             */
            if (xml != null && xml.Length > 0)
            {
                try
                {
                    //lock(lockObject)
                    {
                        XElement rootElement = XElement.Parse(xml);


                        XDocument d = new XDocument(rootElement);
                        d.Validate(XMLPartManager.Instance.getReportSchema(), null);

                        // General attributes
                        var titleAttribute = rootElement.Attribute(XName.Get("title"));
                        if (titleAttribute != null) Title = titleAttribute.Value;
                        else Title = null;

                        var spreadsheetAttribute = rootElement.Attribute(XName.Get("file"));
                        if (spreadsheet != null) Spreadsheet = spreadsheetAttribute.Value;
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
                            var inputCells = cells.Element(XName.Get("input"));
                            //this.ParseCells(inputCells, this.InputCells, typeof(InputCell)); /*functionality dosn't right, we need a inteligent mergeing*/

                            // Intermediate cells
                            var intermediateCells = cells.Element(XName.Get("intermediate"));
                            //this.ParseCells(intermediateCells, this.IntermediateCells, typeof(IntermediateCell));

                            // Output cells
                            var outputCells = cells.Element(XName.Get("output"));
                            //this.ParseCells(outputCells, this.OutputCells, typeof(OutputCell));
                        }
                        else
                        {
                            InputCells.Clear();
                            IntermediateCells.Clear();
                            OutputCells.Clear();
                        }
                        LoadViolations(rootElement);
                    }
                    Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_successful;
                    Globals.Ribbons.Ribbon.scanButton.Enabled = true;
                    Globals.Ribbons.Ribbon.scanButton.Label =
                        Resources.tl_Ribbon_AreaScan_ScanButton;
                    StatusbarControlBack();
                }
                catch (Exception ex)
                {
                    Console.Out.WriteLine(ex);
                    Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_unsuccessful;
                    Globals.Ribbons.Ribbon.scanButton.Enabled = true;
                    Globals.Ribbons.Ribbon.scanButton.Label =
                        Resources.tl_Ribbon_AreaScan_ScanButton;
                    StatusbarControlBack();
                }
            }
        }

        /// <summary>
        /// Gives the control over the statusbar back to Excel after 10 sec
        /// </summary>
        /// <returns></returns>
        public async Task StatusbarControlBack()
        {
            await Task.Delay(10000);
            Globals.ThisAddIn.Application.StatusBar = false;
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
                foreach (var ruleXML in findings.Elements(XName.Get("testedRule")))
                {
                    Rule rule = new Rule(ruleXML.Element(XName.Get("testedPolicy")));

                    // Parse violations
                    var xmlVio = ruleXML.Elements(XName.Get("violations"));
                    foreach (XElement vio in xmlVio)
                    {
                        var x = vio.Attribute(ns + "type");
                        if (x.Value.Equals("singleviolation"))
                        {
                            violations.Add(new Violation(vio, workbook, scanTime, rule));
                        }
                        else if (x.Value.Equals("violationgroup"))
                        {
                            (from p in vio.Elements(XName.Get("singleviolation"))
                                select new Violation(p, workbook, scanTime, rule)).ToList()
                                .ForEach(p => violations.Add(p));
                        }
                    }
                }

                // Add only new violations
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

                // mark all solved violations
                for (int i = Violations.Count - 1; i >= 0; i--)
                {
                    if (Violations.ElementAt(i).FoundAgain == true)
                    {
                        Violations.ElementAt(i).FoundAgain = false;
                    }
                    else
                    {
                        Violations.ElementAt(i).SolvedTime = scanTime;
                        Violations.ElementAt(i).ViolationState = ViolationType.SOLVED;
                    }
                }
                for (int i = LaterViolations.Count - 1; i >= 0; i--)
                {
                    if (LaterViolations.ElementAt(i).FoundAgain == true)
                    {
                        LaterViolations.ElementAt(i).FoundAgain = false;
                    }
                    else
                    {
                        LaterViolations.ElementAt(i).SolvedTime = scanTime;
                        LaterViolations.ElementAt(i).ViolationState = ViolationType.SOLVED;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
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
                    var cell = new Cell(element, workbook);
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
            if (define == CellDefinitionOption.Define)
            {
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
                    if (!inputCells.Contains(c))
                    {
                        InputCells.Add(c.ToInputCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from input list
                foreach (var c in inputs)
                {
                    try
                    {
                        InputCells.Remove(c);
                    }
                    catch (Exception)
                    {
                    }
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a intermediate cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public void DefineIntermediateCell(List<Cell> intermediates, CellDefinitionOption define)
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
                    if (!intermediateCells.Contains(c))
                    {
                        intermediateCells.Add(c.ToIntermediateCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in intermediates)
                {
                    intermediateCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity value cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public void DefineSanityValueCell(List<Cell> sanityValues, CellDefinitionOption define)
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
                    sanityCheckingCells.Remove(c);
                    sanityExplanationCells.Remove(c);
                    sanityConstraintCells.Remove(c);
                    //add to sanityValue list
                    if (!sanityValueCells.Contains(c))
                    {
                        sanityValueCells.Add(c.ToSanityValueCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityValues)
                {
                    sanityValueCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity value cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public void DefineSanityConstraintCell(List<Cell> sanityConstraints, CellDefinitionOption define)
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
                    sanityCheckingCells.Remove(c);
                    sanityExplanationCells.Remove(c);
                    sanityValueCells.Remove(c);
                    //add to sanityValue list
                    if (!sanityConstraintCells.Contains(c))
                    {
                        sanityConstraintCells.Add(c.ToSanityConstraintCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityConstraints)
                {
                    sanityConstraintCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity value cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public void DefineSanityExplanationCell(List<Cell> sanityExplanations, CellDefinitionOption define)
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
                    sanityCheckingCells.Remove(c);
                    sanityValueCells.Remove(c);
                    sanityConstraintCells.Remove(c);
                    //add to sanityExplanation list
                    if (!sanityExplanationCells.Contains(c))
                    {
                        sanityExplanationCells.Add(c.ToSanityExplanationCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityExplanations)
                {
                    sanityExplanationCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a sanity Checking cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the intermediate cell list</param>
        public void DefineSanityCheckingCell(List<Cell> sanityCheckings, CellDefinitionOption define)
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
                    sanityValueCells.Remove(c);
                    sanityExplanationCells.Remove(c);
                    sanityConstraintCells.Remove(c);
                    //add to sanityChecking list
                    if (!sanityCheckingCells.Contains(c))
                    {
                        sanityCheckingCells.Add(c.ToSanityCheckingCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityCheckings)
                {
                    sanityCheckingCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        /// <summary>
        /// Defines or undefines a output cell.
        /// </summary>
        /// <param name="inputs">List of cells</param>
        /// <param name="define">Optíon to set wheter the cells will add, or remove from the output cell list</param>
        public void DefineOutputCell(List<Cell> outputs, CellDefinitionOption define)
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
                    if (!outputCells.Contains(c))
                    {
                        outputCells.Add(c.ToOutputCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in outputs)
                {
                    outputCells.Remove(c);
                }
            }
            OnCellDefinitionChanged(new EventArgs());
        }

        #endregion

        #region Accept Visitor

        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }

        #endregion

        #endregion

        #region Event Handling

        public delegate void CellDefinitionChangeHandler(object sender, EventArgs data);

        public event CellDefinitionChangeHandler CellDefinitionChange;

        protected void OnCellDefinitionChanged(EventArgs data)
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
        private void violations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            UnreadViolationCount = (from vi in violations where vi.IsRead == false select vi).Count();
        }

        #endregion
    }
}
