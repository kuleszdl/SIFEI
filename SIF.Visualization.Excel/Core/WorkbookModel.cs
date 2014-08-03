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
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Linq;
using System.Xml.Schema;

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
        private ObservableCollection<SIF.Visualization.Excel.ScenarioCore.Scenario> scenarios;
        private Boolean sanityWarnings = true;
        private SharedTabs selectedTab;

        private Workbook workbook;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title of the current inspection.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets the file path of the inspected spreadsheet.
        /// </summary>
        public string Spreadsheet
        {
            get { return this.spreadsheet; }
            set { this.SetProperty(ref this.spreadsheet, value); }
        }

        /// <summary>
        /// Gets or sets the policy path of the inspected spreadsheet.
        /// </summary>
        public string PolicyPath
        {
            get { return this.policyPath; }
            set { this.SetProperty(ref this.policyPath, value); }
        }

        /// <summary>
        /// Gets or sets the policy of the inspected spreadsheet.
        /// </summary>
        public Policy Policy
        {
            get { return this.policy; }
            set { this.SetProperty(ref this.policy, value); }
        }

        /// <summary>
        /// Gets or sets the input cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> InputCells
        {
            get
            {
                if (this.inputCells == null) this.inputCells = new ObservableCollection<Cell>();
                return this.inputCells;
            }
            set { this.SetProperty(ref this.inputCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> IntermediateCells
        {
            get
            {
                if (this.intermediateCells == null) this.intermediateCells = new ObservableCollection<Cell>();
                return this.intermediateCells;
            }
            set { this.SetProperty(ref this.intermediateCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityValueCells
        {
            get
            {
                if (this.sanityValueCells == null) this.sanityValueCells = new ObservableCollection<Cell>();
                return this.sanityValueCells;
            }
            set { this.SetProperty(ref this.sanityValueCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityConstraintCells
        {
            get
            {
                if (this.sanityConstraintCells == null) this.sanityConstraintCells = new ObservableCollection<Cell>();
                return this.sanityConstraintCells;
            }
            set { this.SetProperty(ref this.sanityConstraintCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityExplanationCells
        {
            get
            {
                if (this.sanityExplanationCells == null) this.sanityExplanationCells = new ObservableCollection<Cell>();
                return this.sanityExplanationCells;
            }
            set { this.SetProperty(ref this.sanityExplanationCells, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> SanityCheckingCells
        {
            get
            {
                if (this.sanityCheckingCells == null) this.sanityCheckingCells = new ObservableCollection<Cell>();
                return this.sanityCheckingCells;
            }
            set { this.SetProperty(ref this.sanityCheckingCells, value); }
        }

        public ObservableCollection<CellLocation> ViolatedCells
        {
            get
            {
                if (this.violatedCells == null) this.violatedCells = new ObservableCollection<CellLocation>();
                return this.violatedCells;
            }
            set { this.SetProperty(ref this.violatedCells, value); }
        }

        public Boolean SanityWarnings
        {
            get
            {
                return this.sanityWarnings;
            }
            set
            {
                this.sanityWarnings = value;
            }

        }

        /// <summary>
        /// Gets or sets the output cells of the current document.
        /// </summary>
        public ObservableCollection<Cell> OutputCells
        {
            get
            {
                if (this.outputCells == null) this.outputCells = new ObservableCollection<Cell>();
                return this.outputCells;
            }
            set { this.SetProperty(ref this.outputCells, value); }
        }

        /// <summary>
        /// Gets or sets the violations of the current document
        /// </summary>
        public ObservableCollection<Violation> Violations
        {
            get
            {
                if (this.violations == null)
                {
                    this.violations = new ObservableCollection<Violation>();
                    this.violations.CollectionChanged += violations_CollectionChanged;
                }

                return this.violations;
            }
            set
            {
                this.SetProperty(ref this.violations, value);
            }
        }


        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<Violation> IgnoredViolations
        {
            get
            {
                if (this.ignoredViolations == null) this.ignoredViolations = new ObservableCollection<Violation>();
                return this.ignoredViolations;
            }
            set { this.SetProperty(ref this.ignoredViolations, value); }
        }

        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<Violation> LaterViolations
        {
            get
            {
                if (this.laterViolations == null) this.laterViolations = new ObservableCollection<Violation>();
                return this.laterViolations;
            }
            set { this.SetProperty(ref this.laterViolations, value); }
        }

        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<Violation> SolvedViolations
        {
            get
            {
                if (this.solvedViolations == null)
                {
                    this.solvedViolations = new ObservableCollection<Violation>();
                }
                return this.solvedViolations;
            }
            set { this.SetProperty(ref this.solvedViolations, value); }
        }

        /// <summary>
        /// Gets or sets the scenarios of the current document.
        /// </summary>
        public ObservableCollection<SIF.Visualization.Excel.ScenarioCore.Scenario> Scenarios
        {
            get
            {
                if (this.scenarios == null) this.scenarios = new ObservableCollection<SIF.Visualization.Excel.ScenarioCore.Scenario>();
                return this.scenarios;
            }
            set { this.SetProperty(ref this.scenarios, value); }
        }

        /// <summary>
        /// Gets or sets the count of the unread violations
        /// </summary>
        public int UnreadViolationCount
        {
            get { return this.unreadViolationCount; }
            set { this.SetProperty(ref this.unreadViolationCount, value); }
        }

        /// <summary>
        /// Gets or sets the Excel workbook of this model.
        /// </summary>
        public Microsoft.Office.Interop.Excel.Workbook Workbook
        {
            get { return this.workbook; }
            set { this.SetProperty(ref this.workbook, value); }
        }

        public SharedTabs SelectedTab
        {
            get { return this.selectedTab; }
            set { this.SetProperty(ref this.selectedTab, value); }
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
            if ((object)other == null) return false;

            return this.Title == other.Title &&
                   this.Spreadsheet == other.Spreadsheet &&
                   this.PolicyPath == other.PolicyPath &&
                   this.Policy == other.Policy &&
                   this.InputCells.SequenceEqual(other.InputCells) &&
                   this.IntermediateCells.SequenceEqual(other.IntermediateCells) &&
                   this.OutputCells.SequenceEqual(other.OutputCells) &&
                   this.IgnoredViolations.SequenceEqual(other.IgnoredViolations) &&
                   this.Scenarios.SequenceEqual(other.Scenarios) &&
                   Object.ReferenceEquals(this.Workbook, other.Workbook);
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
            if (System.Object.ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;

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
            this.Workbook = workbook;

            this.Workbook.BeforeSave += Workbook_BeforeSave;
            this.Workbook.BeforeClose += Workbook_BeforeClose;
            this.Workbook.AfterSave += Workbook_AfterSave;
            this.Workbook.SheetSelectionChange += Sheet_SelectionChange;
            this.workbook.SheetCalculate += Workbook_SheetCalculate;

            // Load cell definitions
            this.Accept(new XMLToCellDefinitionVisitor(XMLPartManager.Instance.LoadXMLPart(this, "CellDefinitions")));

            // Load the scenarios
            this.Accept(new XMLToScenarioVisitor(XMLPartManager.Instance.LoadXMLPart(this, "Scenario")));

            // Load the violations
            var violationsXml = XMLPartManager.Instance.LoadXMLPart(this, "Violations");
            if (violationsXml != null)
            {
                // Add them to the violations collection
                (from p in violationsXml.Elements(XName.Get("violation"))
                 select new Violation(p, workbook)).ToList().ForEach(p => this.Violations.Add(p));
            }

            // Load the false positives
            var falsePositivesXml = XMLPartManager.Instance.LoadXMLPart(this, "FalsePositives");
            if (falsePositivesXml != null)
            {
                // Add them to the FalsePositives collection
                (from p in falsePositivesXml.Elements(XName.Get("falsepositive"))
                 select new Violation(p, workbook)).ToList().ForEach(p => this.IgnoredViolations.Add(p));
            }

            // Load the later violations
            var laterXml = XMLPartManager.Instance.LoadXMLPart(this, "LaterViolations");
            if (laterXml != null)
            {
                // Add them to the FalsePositives collection
                (from p in laterXml.Elements(XName.Get("laterviolation"))
                 select new Violation(p, workbook)).ToList().ForEach(p => this.LaterViolations.Add(p));
            }

            // Load the solved violations
            var solvedXml = XMLPartManager.Instance.LoadXMLPart(this, "SolvedViolations");
            if (solvedXml != null)
            {
                // Add them to the FalsePositives collection
                (from p in solvedXml.Elements(XName.Get("solvedviolation"))
                 select new Violation(p, workbook)).ToList().ForEach(p => this.SolvedViolations.Add(p));
            }
        }

        private void Sheet_SelectionChange(object Sh, Range Target)
        {
            if (Target.Cells.Count == 1)
            {
                int row = Target.Cells.Row;
                int column = Target.Cells.Column;
                string location = "=" + Target.Worksheet.Name + "!" + getExcelColumnName(column) + row;
                CellLocation cell = new CellLocation(this.workbook, location);
                switch (SelectedTab)
                {
                    case SharedTabs.Open:
                        this.Violations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in Violations where vi.Cell.Equals(cell) select vi).ToList().ForEach(vi => vi.IsCellSelected = true);
                        break;
                    case SharedTabs.Later:
                        this.LaterViolations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in LaterViolations where vi.Cell.Equals(cell) select vi).ToList().ForEach(vi => vi.IsCellSelected = true);
                        break;
                    case SharedTabs.Ignore:
                        this.IgnoredViolations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in IgnoredViolations where vi.Cell.Equals(cell) select vi).ToList().ForEach(vi => vi.IsCellSelected = true);
                        break;
                    case SharedTabs.Archive:
                        this.SolvedViolations.ToList().ForEach(vi => vi.IsCellSelected = false);
                        (from vi in SolvedViolations where vi.Cell.Equals(cell) select vi).ToList().ForEach(vi => vi.IsCellSelected = true);
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
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        private void Workbook_AfterSave(bool Success)
        {
            // Run a scan if necessary
            if (Settings.Default.AutomaticScans && Success)
            {
                this.Inspect(InspectionType.LIVE);
            }
        }

        private void Workbook_SheetCalculate(object Sh)
        {
            // Run a scan if necessary
            if (Settings.Default.AutomaticScans)
            {
                this.Inspect(InspectionType.LIVE);
            }
        }


        /// <summary>
        /// Saves the custom XML parts that are used to persist the cells, scenarios and false positives.
        /// </summary>
        private void Workbook_BeforeSave(bool SaveAsUI, ref bool Cancel)
        {
            //Save the violations
            XMLPartManager.Instance.SaveXMLPart(this, new XElement(XName.Get("violations"), from p in this.Violations select p.ToXElement("violation")), "Violations");

            //Save the false positives
            XMLPartManager.Instance.SaveXMLPart(this, new XElement(XName.Get("falsepositives"), from p in this.IgnoredViolations select p.ToXElement("falsepositive")), "FalsePositives");

            // Save the later violations
            XMLPartManager.Instance.SaveXMLPart(this, new XElement(XName.Get("laterviolations"), from p in this.LaterViolations select p.ToXElement("laterviolation")), "LaterViolations");

            // Save the solved violations
            XMLPartManager.Instance.SaveXMLPart(this, new XElement(XName.Get("solvedviolations"), from p in this.SolvedViolations select p.ToXElement("solvedviolation")), "SolvedViolations");

            // Save the scenarios
            XMLPartManager.Instance.SaveXMLPart(this, this.Accept(new ScenarioToXMLVisitor()) as XElement, "Scenario");

            // Save the cell definitions
            XMLPartManager.Instance.SaveXMLPart(this, this.Accept(new CellDefinitionToXMLVisitor()) as XElement, "CellDefinitions");
        }


        /// <summary>
        /// Handle the scenario controls in the cells before close.
        /// </summary>
        /// <param name="Cancel"></param>
        void Workbook_BeforeClose(ref bool Cancel)
        {
            ScenarioCore.ScenarioUICreator.Instance.End();
        }

        #endregion

        /// <summary>
        /// Launches an inspection job for this workbook with all available tests.
        /// </summary>
        public void Inspect(InspectionType inspectionType)
        {
            this.Inspect(InspectionMode.All, inspectionType);
        }

        /// <summary>
        /// Launches an inspection job for this workbook.
        /// </summary>
        public void Inspect(InspectionMode inspectionMode, InspectionType inspectionType)
        {
            // Save a copy of this workbook temporarily
            var workbookFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + System.IO.Path.DirectorySeparatorChar + Guid.NewGuid().ToString() + ".xls";
            this.Workbook.SaveCopyAs(workbookFile);

            // Create the rules
            var xmlDoc = new XDocument();
            switch (inspectionMode)
            {
                case InspectionMode.All:
                    xmlDoc.Add(this.Accept(new Sprudel1_3XMLVisitor()) as XElement);
                    //TODO add static
                    break;
                case InspectionMode.Dynamic:
                    xmlDoc.Add(this.Accept(new Sprudel1_3XMLVisitor()) as XElement);
                    break;
                case InspectionMode.Static:
                    //TODO change to static
                    xmlDoc.Add(this.Accept(new Sprudel1_3XMLVisitor()) as XElement);
                    break;
            };

            Debug.WriteLine(xmlDoc.ToString());
            xmlDoc.Validate(XMLPartManager.Instance.GetRequestSchema(), null);

            // Enqueue this inspectio
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
            if (xml != null && xml.Length > 0)
            {

                XElement rootElement = XElement.Parse(xml);
                XDocument d = new XDocument(rootElement);
                d.Validate(XMLPartManager.Instance.getReportSchema(), null);

                // General attributes
                var titleAttribute = rootElement.Attribute(XName.Get("title"));
                if (titleAttribute != null) this.Title = titleAttribute.Value;
                else this.Title = null;

                var spreadsheetAttribute = rootElement.Attribute(XName.Get("file"));
                if (spreadsheet != null) this.Spreadsheet = spreadsheetAttribute.Value;
                else this.Spreadsheet = null;

                // Policy
                var policyElement = rootElement.Element(XName.Get("policy"));
                if (policyElement != null) this.Policy = new Policy(policyElement);
                else this.Policy = null;

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
                    this.InputCells.Clear();
                    this.IntermediateCells.Clear();
                    this.OutputCells.Clear();
                }
                this.LoadViolations(rootElement);
            }
        }
        /// <summary>
        /// This method loads the violations of the xml report
        /// </summary>
        private void LoadViolations(XElement rootElement)
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
                         select new Violation(p, workbook, scanTime, rule)).ToList().ForEach(p => this.Violations.Add(p));
                    }
                }

            }

            // Add only new violations
            foreach (Violation violation in violations)
            {
                if (this.Violations.Contains(violation))
                {
                    this.Violations.ElementAt(this.Violations.IndexOf(violation)).FoundAgain = true;
                }
                else if (this.IgnoredViolations.Contains(violation) || this.SolvedViolations.Contains(violation))
                {
                    // nothing to do here
                }
                else if (this.LaterViolations.Contains(violation))
                {
                    this.LaterViolations.ElementAt(this.LaterViolations.IndexOf(violation)).FoundAgain = true;
                }
                else
                {
                    this.Violations.Add(violation);
                }
            }

            // mark all solved violations
            for (int i = this.Violations.Count - 1; i >= 0; i--)
            {
                if (this.Violations.ElementAt(i).FoundAgain == true)
                {
                    this.Violations.ElementAt(i).FoundAgain = false;
                }
                else
                {
                    this.Violations.ElementAt(i).SolvedTime = scanTime;
                    this.Violations.ElementAt(i).ViolationState = ViolationType.SOLVED;
                }
            }
            for (int i = this.LaterViolations.Count - 1; i >= 0; i--)
            {
                if (this.LaterViolations.ElementAt(i).FoundAgain == true)
                {
                    this.LaterViolations.ElementAt(i).FoundAgain = false;
                }
                else
                {
                    this.LaterViolations.ElementAt(i).SolvedTime = scanTime;
                    this.LaterViolations.ElementAt(i).ViolationState = ViolationType.SOLVED;
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
                    var cell = new Cell(element, this.workbook);
                    if (cellType == typeof(InputCell))
                    {
                        var list = new List<Cell>();
                        list.Add(cell);
                        this.DefineInputCell(list, CellDefinitionOption.Define);
                    }
                    else if (cellType == typeof(IntermediateCell))
                    {
                        var list = new List<Cell>();
                        list.Add(cell);
                        this.DefineIntermediateCell(list, CellDefinitionOption.Define);
                    }
                    else if (cellType == typeof(OutputCell))
                    {
                        var list = new List<Cell>();
                        list.Add(cell);
                        this.DefineOutputCell(list, CellDefinitionOption.Define);
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
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this, CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    this.IntermediateCells.Remove(c);
                    this.OutputCells.Remove(c);

                    //add to input list
                    if (!this.inputCells.Contains(c))
                    {
                        this.InputCells.Add(c.ToInputCell());
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
                        this.InputCells.Remove(c);
                    }
                    catch (Exception)
                    { }
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
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this, CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    this.InputCells.Remove(c);
                    this.OutputCells.Remove(c);

                    //add to intermediate list
                    if (!this.intermediateCells.Contains(c))
                    {
                        this.intermediateCells.Add(c.ToIntermediateCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in intermediates)
                {
                    this.intermediateCells.Remove(c);
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
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this, CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    this.InputCells.Remove(c);
                    this.OutputCells.Remove(c);
                    this.IntermediateCells.Remove(c);
                    this.sanityCheckingCells.Remove(c);
                    this.sanityExplanationCells.Remove(c);
                    this.sanityConstraintCells.Remove(c);
                    //add to sanityValue list
                    if (!this.sanityValueCells.Contains(c))
                    {
                        this.sanityValueCells.Add(c.ToSanityValueCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityValues)
                {
                    this.sanityValueCells.Remove(c);
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
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this, CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    this.InputCells.Remove(c);
                    this.OutputCells.Remove(c);
                    this.IntermediateCells.Remove(c);
                    this.sanityCheckingCells.Remove(c);
                    this.sanityExplanationCells.Remove(c);
                    this.sanityValueCells.Remove(c);
                    //add to sanityValue list
                    if (!this.sanityConstraintCells.Contains(c))
                    {
                        this.sanityConstraintCells.Add(c.ToSanityConstraintCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityConstraints)
                {
                    this.sanityConstraintCells.Remove(c);
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
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this, CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    this.InputCells.Remove(c);
                    this.OutputCells.Remove(c);
                    this.IntermediateCells.Remove(c);
                    this.sanityCheckingCells.Remove(c);
                    this.sanityValueCells.Remove(c);
                    this.sanityConstraintCells.Remove(c);
                    //add to sanityExplanation list
                    if (!this.sanityExplanationCells.Contains(c))
                    {
                        this.sanityExplanationCells.Add(c.ToSanityExplanationCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityExplanations)
                {
                    this.sanityExplanationCells.Remove(c);
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
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this, CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    this.InputCells.Remove(c);
                    this.OutputCells.Remove(c);
                    this.IntermediateCells.Remove(c);
                    this.sanityValueCells.Remove(c);
                    this.sanityExplanationCells.Remove(c);
                    this.sanityConstraintCells.Remove(c);
                    //add to sanityChecking list
                    if (!this.sanityCheckingCells.Contains(c))
                    {
                        this.sanityCheckingCells.Add(c.ToSanityCheckingCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in sanityCheckings)
                {
                    this.sanityCheckingCells.Remove(c);
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
                        c.SifLocation = CellManager.Instance.CreateSIFCellName(this, CellManager.Instance.GetA1Adress(this, c.Location));
                    }

                    //remove from the other lists
                    this.InputCells.Remove(c);
                    this.IntermediateCells.Remove(c);

                    //add to intermediate list
                    if (!this.outputCells.Contains(c))
                    {
                        this.outputCells.Add(c.ToOutputCell());
                    }
                }
            }
            else if (define == CellDefinitionOption.Undefine)
            {
                //remove from intermediate list
                foreach (var c in outputs)
                {
                    this.outputCells.Remove(c);
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
            this.UnreadViolationCount = (from vi in violations where vi.IsRead == false select vi).Count();
        }


        #endregion

    }
}
