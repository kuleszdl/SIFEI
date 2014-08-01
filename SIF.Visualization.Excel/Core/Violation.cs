using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class Violation : BindableBase
    {
        /// <summary>
        /// Enum for the Violation Type
        /// </summary>
        public enum ViolationType { NEW, IGNORE, LATER, SOLVED };

        #region Fields

        private int id;
        private string causingElement;
        private string description;
        private CellLocation cell;
        private DateTime firstOccurrence;
        private DateTime solvedTime;
        private bool foundAgain;
        private Rule rule;
        private bool isVisible;
        private bool isRead;
        private bool cellSelected;
        private bool isSelected;
        private bool load;
        private ViolationType violationState;
        private decimal severity;
        private string controlName;
        private CellErrorInfo cellErrorInfo;
        private Microsoft.Office.Tools.Excel.ControlSite control;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the number of this violation.
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        /// <summary>
        /// Gets or sets the element causing this violation.
        /// </summary>
        public string CausingElement
        {
            get { return this.causingElement; }
            set { this.SetProperty(ref this.causingElement, value); }
        }

        /// <summary>
        /// Gets or sets the description of this violation.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets the cell of this violation.
        /// </summary>
        public CellLocation Cell
        {
            get { return this.cell; }
            set { this.SetProperty(ref this.cell, value); }
        }

        /// <summary>
        /// Gets or sets the severity of this violation.
        /// </summary>
        public decimal Severity
        {
            get { return this.severity; }
            set { this.SetProperty(ref this.severity, value); }
        }

        /// <summary>
        /// Gets or sets the first occurrence of this violation.
        /// </summary>
        public DateTime FirstOccurrence
        {
            get { return this.firstOccurrence; }
            set { this.SetProperty(ref this.firstOccurrence, value); }
        }

        /// <summary>
        /// Gets or sets the found again value of this violation.
        /// </summary>
        public bool FoundAgain
        {
            get { return this.foundAgain; }
            set { this.SetProperty(ref this.foundAgain, value); }
        }

        /// <summary>
        /// Gets or sets the rule of this violation.
        /// </summary>
        public Rule Rule
        {
            get { return this.rule; }
            set { this.SetProperty(ref this.rule, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this violation is visible in the spreadsheet.
        /// </summary>
        public bool IsVisible
        {
            get { return this.isVisible; }
            set
            {
                if (this.SetProperty(ref this.isVisible, value))
                {
                    if (this.Control != null)
                    {
                        this.Control.Visible = this.IsVisible;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that shows whether this violation has been read or not
        /// </summary>
        public bool IsRead
        {
            get { return this.isRead; }
            set { this.SetProperty(ref this.isRead, value); }
        }

        /// <summary>
        /// Gets or sets a value that shows whether this violation has been read or not
        /// </summary>
        public bool IsCellSelected
        {
            get { return this.cellSelected; }
            set { this.SetProperty(ref this.cellSelected, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this violation is selected in the user interface.
        /// </summary>
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.SetProperty(ref this.isSelected, value))
                {
                    // Make control topmost
                    if (this.Control != null && value)
                    {
                        this.Control.BringToFront();
                        this.IsRead = true;
                        this.cell.Select();
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the time when this violation has been solved
        /// </summary>
        public DateTime SolvedTime
        {
            get { return this.solvedTime; }
            set { this.SetProperty(ref this.solvedTime, value); }
        }

        /// <summary>
        /// Gets or sets the state of this violation.
        /// </summary>
        public Violation.ViolationType ViolationState
        {
            get { return this.violationState; }
            set
            {
                // delete from old list
                this.HandleOldState();
                // set value
                this.SetProperty(ref this.violationState, value);
                // Add to new list
                this.HandleNewState(value);
            }
        }

        /// <summary>
        /// Gets or sets the name of the cell error info control.
        /// </summary>
        public string ControlName
        {
            get { return this.controlName; }
            set { this.SetProperty(ref this.controlName, value); }
        }

        /// <summary>
        /// Gets or sets the cell error info control.
        /// </summary>
        public CellErrorInfo CellErrorInfo
        {
            get { return this.cellErrorInfo; }
            set { this.SetProperty(ref this.cellErrorInfo, value); }
        }

        /// <summary>
        /// Gets or sets the control site of this control.
        /// </summary>
        public Microsoft.Office.Tools.Excel.ControlSite Control
        {
            get { return this.control; }
            set { this.SetProperty(ref this.control, value); }
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
            Violation other = obj as Violation;
            if ((object)other == null) return false;

            return base.Equals(other) &&
                   this.Id == other.Id &&
                   this.CausingElement == other.CausingElement &&
                   this.Description == other.Description &&
                   this.Cell == other.Cell &&
                   this.Rule == other.Rule &&
                   this.IsSelected == other.IsSelected &&
                   this.Severity == other.Severity;
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
        public static bool operator ==(Violation a, Violation b)
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
        public static bool operator !=(Violation a, Violation b)
        {
            return !(a == b);
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor of a violation
        /// </summary>
        /// <param name="root">the root XML element</param>
        /// <param name="workbook">the current workbook</param>
        /// <param name="scanTime">the time when this violation has been occurred</param>
        /// <param name="rule">the rule of this violation</param>
        public Violation(XElement root, Workbook workbook, DateTime scanTime, Rule rule)
        {
            //this.Id = Convert.ToInt32(root.Attribute(XName.Get("number")).Value);
            this.CausingElement = root.Attribute(XName.Get("causingelement")).Value;
            this.Description = root.Attribute(XName.Get("description")).Value;
            this.Severity = decimal.Parse(root.Attribute(XName.Get("severity")).Value.Replace(".0", ""));

            var location = root.Attribute(XName.Get("location")).Value;
            if (!string.IsNullOrWhiteSpace(location))
            {
                // Split the location string into its components
                // Input might be: [example.xlsx]Sheet1!B12
                location = location.Substring(location.IndexOf(']') + 1);
                this.Cell = new CellLocation(workbook, location);
            }

            this.firstOccurrence = scanTime;
            this.rule = rule;
            this.foundAgain = true;
            this.violationState = ViolationType.NEW;
            this.isVisible = true;
        }

        /// <summary>
        /// Constructor for the xml file, that is stored in the .xls file
        /// </summary>
        /// <param name="element">The root XElement of the xml</param>
        /// <param name="workbook">The current workbook</param>
        public Violation(XElement element, Workbook workbook)
        {
            this.load = true;
            this.Id = Int32.Parse(element.Attribute(XName.Get("id")).Value);
            this.CausingElement = element.Attribute(XName.Get("causingelement")).Value;
            this.Description = element.Attribute(XName.Get("description")).Value;
            this.Cell = new CellLocation(workbook, element.Attribute(XName.Get("cell")).Value);
            this.FirstOccurrence = DateTime.Parse(element.Attribute(XName.Get("firstoccurrence")).Value);
            this.ViolationState = (ViolationType)Enum.Parse(typeof(ViolationType), element.Attribute(XName.Get("violationstate")).Value);
            this.SolvedTime = DateTime.Parse(element.Attribute(XName.Get("solvedtime")).Value);
            this.isVisible = Convert.ToBoolean(element.Attribute(XName.Get("isvisible")).Value);
            this.isRead = Convert.ToBoolean(element.Attribute(XName.Get("isread")).Value);
            this.isSelected = Convert.ToBoolean(element.Attribute(XName.Get("isselected")).Value);
            this.severity = Decimal.Parse(element.Attribute(XName.Get("severity")).Value);
            this.controlName = element.Attribute(XName.Get("controlname")).Value;
            this.Rule = new Rule(element.Element(XName.Get("rule")));
            this.load = false;
            this.IsCellSelected = false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Writes the fields of this superclass to a XElement
        /// </summary>
        /// <param name="element">the xml element</param>
        /// <returns>the element with the added attributes</returns>
        public XElement ToXElement(String name)
        {
            var element = new XElement(XName.Get(name));
            element.SetAttributeValue(XName.Get("id"), this.Id);
            element.SetAttributeValue(XName.Get("causingelement"), this.CausingElement);
            element.SetAttributeValue(XName.Get("description"), this.Description);
            element.SetAttributeValue(XName.Get("cell"), this.Cell.ToString());
            element.SetAttributeValue(XName.Get("firstoccurrence"), this.FirstOccurrence);
            element.SetAttributeValue(XName.Get("violationstate"), this.ViolationState);
            element.SetAttributeValue(XName.Get("solvedtime"), this.SolvedTime);
            element.SetAttributeValue(XName.Get("isvisible"), this.isVisible);
            element.SetAttributeValue(XName.Get("isread"), this.IsRead);
            element.SetAttributeValue(XName.Get("isselected"), this.isSelected);
            element.SetAttributeValue(XName.Get("severity"), this.severity);
            element.SetAttributeValue(XName.Get("controlname"), this.controlName);
            element.Add(this.Rule.ToXElement());
            this.IsCellSelected = false;
            return element;
        }

        /// <summary>
        /// Renders the controls in the spreadsheet
        /// </summary>
        public void CreateControls()
        {
            var container = new CellErrorInfoContainer();

            this.CellErrorInfo = new CellErrorInfo() { DataContext = this };
            container.ElementHost.Child = this.CellErrorInfo;

            var vsto = Globals.Factory.GetVstoObject(this.Cell.Worksheet);

            // Remove the old control
            if (!string.IsNullOrWhiteSpace(this.ControlName))
            {
                vsto.Controls.Remove(this.ControlName);
                this.ControlName = null;
            }

            this.ControlName = Guid.NewGuid().ToString();

            this.Control = vsto.Controls.AddControl(container, this.Cell.Worksheet.Range[this.Cell.ShortLocation], this.ControlName);
            this.Control.Width = this.Control.Height;
            this.Control.Placement = Microsoft.Office.Interop.Excel.XlPlacement.xlMove;
        }

        private void HandleNewState(Violation.ViolationType type)
        {
            if (!load)
            {
                switch (type)
                {
                    case ViolationType.NEW:
                        DataModel.Instance.CurrentWorkbook.Violations.Add(this);
                        break;
                    case ViolationType.IGNORE:
                        this.IsRead = true;
                        DataModel.Instance.CurrentWorkbook.IgnoredViolations.Add(this);
                        break;
                    case ViolationType.LATER:
                        this.IsRead = true;
                        DataModel.Instance.CurrentWorkbook.LaterViolations.Add(this);
                        break;
                    case ViolationType.SOLVED:
                        this.IsRead = true;
                        DataModel.Instance.CurrentWorkbook.SolvedViolations.Add(this);
                        break;
                }
            }
        }

        /// <summary>
        /// Handles the action when a ol ViolationState is removed
        /// </summary>
        private void HandleOldState()
        {
            if (!load)
            {
                switch (this.violationState)
                {
                    case ViolationType.NEW:
                        DataModel.Instance.CurrentWorkbook.Violations.Remove(this);
                        break;
                    case ViolationType.IGNORE:
                        DataModel.Instance.CurrentWorkbook.IgnoredViolations.Remove(this);
                        break;
                    case ViolationType.LATER:
                        DataModel.Instance.CurrentWorkbook.LaterViolations.Remove(this);
                        break;
                    case ViolationType.SOLVED:
                        DataModel.Instance.CurrentWorkbook.SolvedViolations.Remove(this);
                        break;
                }
                this.IsCellSelected = false;
                this.IsVisible = false;
            }
        }
        #endregion
    }
}
