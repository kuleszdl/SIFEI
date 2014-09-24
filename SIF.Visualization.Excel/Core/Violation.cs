using Microsoft.Office.Interop.Excel;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class Violation : BindableBase
    {
        #region Fields

        private string causingElement;
        private string description;
        private CellLocation cell;
        private DateTime firstOccurrence;
        private DateTime solvedTime;
        private bool foundAgain;
        private Rule rule;
        private bool isRead;
        private bool cellSelected;
        private bool isSelected;
        private bool load;
        private ViolationType violationState;
        private decimal severity;
        private Workbook workbook;

        #endregion

        #region Properties

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
                    if (value)
                    {
                        this.IsRead = true;
                        foreach (CellLocation cell in DataModel.Instance.CurrentWorkbook.ViolatedCells)
                        {
                            Regex rgx = new Regex(@"\$|=");
                            if (cell.ViolationType.Equals(this.violationState) && rgx.Replace(cell.Location, "").Equals(rgx.Replace(this.Cell.Location, "")))
                            {
                                cell.Select(this);
                                return;
                            }
                        }
                    }
                    else
                    {
                        foreach (CellLocation cell in DataModel.Instance.CurrentWorkbook.ViolatedCells)
                        {
                            Regex rgx = new Regex(@"\$|=");
                            if (cell.ViolationType.Equals(this.violationState) && rgx.Replace(cell.Location, "").Equals(rgx.Replace(this.Cell.Location, "")))
                            {
                                cell.Unselect();
                                return;
                            }
                        }
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
        public ViolationType ViolationState
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
                   this.CausingElement == other.CausingElement &&
                   this.Description == other.Description &&
                   this.Cell == other.Cell &&
                   this.Rule == other.Rule &&
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

            this.workbook = workbook;

            this.firstOccurrence = scanTime;
            this.rule = rule;
            this.foundAgain = true;
            FindCellLocation(root.Attribute(XName.Get("location")).Value);
            this.violationState = ViolationType.OPEN;
        }

        /// <summary>
        /// Constructor for the xml file, that is stored in the .xls file
        /// </summary>
        /// <param name="element">The root XElement of the xml</param>
        /// <param name="workbook">The current workbook</param>
        public Violation(XElement element, Workbook workbook)
        {
            this.load = true;
            this.workbook = workbook;
            this.CausingElement = element.Attribute(XName.Get("causingelement")).Value;
            this.Description = element.Attribute(XName.Get("description")).Value;
            this.FirstOccurrence = DateTime.Parse(element.Attribute(XName.Get("firstoccurrence")).Value);
            this.ViolationState = (ViolationType)Enum.Parse(typeof(ViolationType), element.Attribute(XName.Get("violationstate")).Value);
            this.SolvedTime = DateTime.Parse(element.Attribute(XName.Get("solvedtime")).Value);
            this.isRead = Convert.ToBoolean(element.Attribute(XName.Get("isread")).Value);
            this.isSelected = Convert.ToBoolean(element.Attribute(XName.Get("isselected")).Value);
            this.severity = Decimal.Parse(element.Attribute(XName.Get("severity")).Value);
            this.Rule = new Rule(element.Element(XName.Get("rule")));
            FindCellLocation(element.Attribute(XName.Get("cell")).Value);
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
            element.SetAttributeValue(XName.Get("causingelement"), this.CausingElement);
            element.SetAttributeValue(XName.Get("description"), this.Description);
            element.SetAttributeValue(XName.Get("cell"), this.Cell.ToString());
            element.SetAttributeValue(XName.Get("firstoccurrence"), this.FirstOccurrence);
            element.SetAttributeValue(XName.Get("violationstate"), this.ViolationState);
            element.SetAttributeValue(XName.Get("solvedtime"), this.SolvedTime);
            element.SetAttributeValue(XName.Get("isread"), this.IsRead);
            element.SetAttributeValue(XName.Get("isselected"), this.isSelected);
            element.SetAttributeValue(XName.Get("severity"), this.severity);
            element.Add(this.Rule.ToXElement());
            this.IsCellSelected = false;
            return element;
        }

        private void HandleNewState(ViolationType type)
        {
            if (!load)
            {
                switch (type)
                {
                    case ViolationType.OPEN:
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
                PersistCellLocation();
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
                    case ViolationType.OPEN:
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
                RemovefromCellLocation();
            }
        }

        private void FindCellLocation(String location)
        {
            if (!string.IsNullOrWhiteSpace(location))
            {
                location = location.Substring(location.IndexOf(']') + 1);
                foreach (CellLocation cell in DataModel.Instance.CurrentWorkbook.ViolatedCells)
                {
                    Regex rgx = new Regex(@"\$|=");
                    if (cell.ViolationType.Equals(this.violationState) && rgx.Replace(cell.Location, "").Equals(rgx.Replace(location, "")))
                    {
                        this.Cell = cell;
                        return;
                    }
                }
                this.cell = new CellLocation(workbook, location);
                this.cell.ViolationType = this.violationState;
            }
        }

        public void PersistCellLocation()
        {
            if (this.cell == null) {
                throw new NullReferenceException("The cell location is null. You have to call FindCellLocation before");
            }
            string location = this.Cell.Location;
            if (!string.IsNullOrWhiteSpace(location))
            {
                location = location.Substring(location.IndexOf(']') + 1);
                foreach (CellLocation cell in DataModel.Instance.CurrentWorkbook.ViolatedCells)
                {
                    Regex rgx = new Regex(@"\$|=");
                    if (cell.ViolationType.Equals(this.violationState) && rgx.Replace(cell.Location, "").Equals(rgx.Replace(location, "")))
                    {
                        this.Cell = cell;
                        cell.Violations.Add(this);
                        cell.SetVisibility(DataModel.Instance.CurrentWorkbook.SelectedTab);
                        return;
                    }
                }
                this.cell = new CellLocation(workbook, location);
                this.cell.ViolationType = this.violationState;
                this.cell.Violations.Add(this);
                DataModel.Instance.CurrentWorkbook.ViolatedCells.Add(this.cell);
            }
        }

        private void RemovefromCellLocation()
        {
            string location = this.Cell.Location;
            for (int i = DataModel.Instance.CurrentWorkbook.ViolatedCells.Count-1; i >=0; i--) 
            {
                CellLocation cell = DataModel.Instance.CurrentWorkbook.ViolatedCells[i];
                Regex rgx = new Regex(@"\$|=");
                if (cell.ViolationType.Equals(this.violationState) && rgx.Replace(cell.Location, "").Equals(rgx.Replace(location, "")))
                {
                    cell.Violations.Remove(this);
                    return;
                }
            }
        }
        #endregion
    }
}
