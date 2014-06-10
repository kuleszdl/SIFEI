using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.Networking;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.ScenarioCore;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<Finding> findings;
        private ObservableCollection<FalsePositive> falsePositives;
        private ObservableCollection<SIF.Visualization.Excel.ScenarioCore.Scenario> scenarios;
        private Boolean sanityWarnings = true;

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
        /// Gets or sets the findings of the current document.
        /// </summary>
        public ObservableCollection<Finding> Findings
        {
            get
            {
                if (this.findings == null) this.findings = new ObservableCollection<Finding>();
                return this.findings;
            }
            set { this.SetProperty(ref this.findings, value); }
        }

        /// <summary>
        /// Gets or sets the false positives of the current document.
        /// </summary>
        public ObservableCollection<FalsePositive> FalsePositives
        {
            get
            {
                if (this.falsePositives == null) this.falsePositives = new ObservableCollection<FalsePositive>();
                return this.falsePositives;
            }
            set { this.SetProperty(ref this.falsePositives, value); }
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
        /// Gets or sets the Excel workbook of this model.
        /// </summary>
        public Microsoft.Office.Interop.Excel.Workbook Workbook
        {
            get { return this.workbook; }
            set { this.SetProperty(ref this.workbook, value); }
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
                   this.Findings.SequenceEqual(other.Findings) &&
                   this.FalsePositives.SequenceEqual(other.FalsePositives) &&
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

            // Load the false positives
            var falsePositivesXml = XMLPartManager.Instance.LoadXMLPart(this, "FalsePositives");
            if (falsePositivesXml != null)
            {
                // Add them to the FalsePositives collection
                (from p in falsePositivesXml.Elements(XName.Get("falsepositive"))
                 select new FalsePositive(p)).ToList().ForEach(p => this.FalsePositives.Add(p));
            }

            // Load cell definitions
            this.Accept(new XMLToCellDefinitionVisitor(XMLPartManager.Instance.LoadXMLPart(this, "CellDefinitions")));

            // Load the scenarios
            this.Accept(new XMLToScenarioVisitor(XMLPartManager.Instance.LoadXMLPart(this, "Scenario")));

            this.Workbook.BeforeSave += Workbook_BeforeSave;
            this.Workbook.BeforeClose += Workbook_BeforeClose;
        }

        /// <summary>
        /// Saves the custom XML parts that are used to persist the cells, scenarios and false positives.
        /// </summary>
        private void Workbook_BeforeSave(bool SaveAsUI, ref bool Cancel)
        {
            // Save the false positives
            XMLPartManager.Instance.SaveXMLPart(this, new XElement(XName.Get("falsepositives"), from p in this.FalsePositives
                                                                                                select p.ToXElement()), "FalsePositives");

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
        public void Inspect()
        {
            this.Inspect(InspectionMode.All);
        }

        /// <summary>
        /// Launches an inspection job for this workbook.
        /// </summary>
        public void Inspect(InspectionMode inspectionMode)
        {
            // Save a copy of this workbook temporarily
            var workbookFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + System.IO.Path.DirectorySeparatorChar + Guid.NewGuid().ToString() + ".xls";
            this.Workbook.SaveCopyAs(workbookFile);

            // Create the rules
            var xmlDoc = new XDocument();
            switch (inspectionMode)
            {
                case InspectionMode.All:
                    xmlDoc.Add(this.Accept(new Sprudel1_2XMLVisitor()) as XElement);
                    //TODO add static
                    break;
                case InspectionMode.Dynamic:
                    xmlDoc.Add(this.Accept(new Sprudel1_2XMLVisitor()) as XElement);
                    break;
                case InspectionMode.Static:
                    //TODO change to static
                    xmlDoc.Add(this.Accept(new Sprudel1_2XMLVisitor()) as XElement);
                    break;
            };

            Debug.WriteLine(xmlDoc.ToString());

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
            //this.InputCells.Clear();
            //this.IntermediateCells.Clear();
            //this.OutputCells.Clear();

            this.Findings.Clear();

            XElement rootElement = XElement.Parse(xml);

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

            // Findings
            var findings = rootElement.Element(XName.Get("findings"));
            this.Findings.Clear();
            if (findings != null)
            {
                var tempCollection = (from p in findings.Elements(XName.Get("rule"))
                                      select new Finding(p, this.Workbook)).ToList();

                // Check for false positives
                foreach (var singleViolation in from p in tempCollection
                                                from q in p.Violations
                                                where q is SingleViolation
                                                select q as SingleViolation)
                {
                    // Check, whether there is a false positive for this violation
                    foreach (var name in singleViolation.Cell.FalsePositiveNames)
                    {
                        // Check, whether this false positive still applies
                        var falsePositive = (from p in this.FalsePositives
                                             where p.Name == name.Name
                                             select p).FirstOrDefault();
                        if (falsePositive != null)
                        {
                            if (falsePositive.Content == singleViolation.Cell.Content && falsePositive.ViolationName == singleViolation.CausingElement + singleViolation.Description)
                            {
                                // This false positive is still valid
                                singleViolation.SetFalsePositiveSilently(true);
                            }
                        }
                    }
                }

                tempCollection.ForEach(p => this.Findings.Add(p));
            }

            /*
             * Refresh the UI
             */
            foreach (var finding in this.Findings)
            {
                finding.CreateControls();
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
            /*(from p in root.Elements(XName.Get("cell"))
             select new Cell(p, this.Workbook).ToCellType(cellType)).ToList().ForEach(p => { if (!targetCollection.Contains(p)) targetCollection.Add(p); });*/

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
                    {}
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

        #endregion

    }
}
