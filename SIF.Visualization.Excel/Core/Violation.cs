using Microsoft.Office.Interop.Excel;
using System;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// Models one Violation in the Workbook
    /// </summary>
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
            get { return causingElement; }
            set { SetProperty(ref causingElement, value); }
        }

        /// <summary>
        /// Gets or sets the description of this violation.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// Gets or sets the cell of this violation.
        /// </summary>
        public CellLocation Cell
        {
            get { return cell; }
            set { SetProperty(ref cell, value); }
        }

        /// <summary>
        /// Gets or sets the severity of this violation.
        /// </summary>
        public decimal Severity
        {
            get { return severity; }
            set { SetProperty(ref severity, value); }
        }

        /// <summary>
        /// Gets or sets the first occurrence of this violation.
        /// </summary>
        public DateTime FirstOccurrence
        {
            get { return firstOccurrence; }
            set { SetProperty(ref firstOccurrence, value); }
        }

        /// <summary>
        /// Gets or sets the found again value of this violation.
        /// </summary>
        public bool FoundAgain
        {
            get { return foundAgain; }
            set { SetProperty(ref foundAgain, value); }
        }

        /// <summary>
        /// Gets or sets the rule of this violation.
        /// </summary>
        public Rule Rule
        {
            get { return rule; }
            set { SetProperty(ref rule, value); }
        }

        /// <summary>
        /// Gets or sets a value that shows whether this violation has been read or not
        /// </summary>
        public bool IsRead
        {
            get { return isRead; }
            set { SetProperty(ref isRead, value); }
        }

        /// <summary>
        /// Gets or sets a value that shows whether this violation has been read or not
        /// </summary>
        public bool IsCellSelected
        {
            get { return cellSelected; }
            set { SetProperty(ref cellSelected, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this violation is selected in the user interface.
        /// </summary>
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (SetProperty(ref isSelected, value))
                {
                    if (value)
                    {
                        IsRead = true;
                        foreach (CellLocation cell in DataModel.Instance.CurrentWorkbook.ViolatedCells)
                        {
                            Regex rgx = new Regex(@"\$|=");
                            if (cell.ViolationType.Equals(violationState) && rgx.Replace(cell.Location, string.Empty).Equals(rgx.Replace(Cell.Location, string.Empty)))
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
                            if (cell.ViolationType.Equals(violationState) && rgx.Replace(cell.Location, string.Empty).Equals(rgx.Replace(Cell.Location, String.Empty)))
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
            get { return solvedTime; }
            set { SetProperty(ref solvedTime, value); }
        }

        /// <summary>
        /// Gets or sets the state of this violation.
        /// </summary>
        public ViolationType ViolationState
        {
            get { return violationState; }
            set
            {
                // delete from old list
                HandleOldState();
                // set value
                SetProperty(ref violationState, value);
                // Add to new list
                HandleNewState(value);
                
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
                   CausingElement == other.CausingElement &&
                   Description == other.Description &&
                   Cell == other.Cell &&
                   Rule == other.Rule &&
                   Severity == other.Severity;
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
            if (ReferenceEquals(a, b)) return true;
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
            CausingElement = root.Attribute(XName.Get("causingelement")).Value;
            Description = root.Attribute(XName.Get("description")).Value;
            Severity = decimal.Parse(root.Attribute(XName.Get("severity")).Value.Replace(".0", String.Empty));

            this.workbook = workbook;

            firstOccurrence = scanTime;
            this.rule = rule;
            foundAgain = true;
            FindCellLocation(root.Attribute(XName.Get("location")).Value);
            //Before there was the following Code written here:
            //this.violationState = ViolationType.OPEN;
            // It got commented out because like this any finding marked with later got overriten
            // with violationtype open.
        }

        /// <summary>
        /// Constructor for the xml file, that is stored in the .xls file
        /// </summary>
        /// <param name="element">The root XElement of the xml</param>
        /// <param name="workbook">The current workbook</param>
        public Violation(XElement element, Workbook workbook)
        {
            load = true;
            this.workbook = workbook;
            CausingElement = element.Attribute(XName.Get("causingelement")).Value;
            Description = element.Attribute(XName.Get("description")).Value;
            FirstOccurrence = DateTime.Parse(element.Attribute(XName.Get("firstoccurrence")).Value);
            ViolationState = (ViolationType)Enum.Parse(typeof(ViolationType), element.Attribute(XName.Get("violationstate")).Value);
            SolvedTime = DateTime.Parse(element.Attribute(XName.Get("solvedtime")).Value);
            isRead = Convert.ToBoolean(element.Attribute(XName.Get("isread")).Value);
            isSelected = Convert.ToBoolean(element.Attribute(XName.Get("isselected")).Value);
            severity = Decimal.Parse(element.Attribute(XName.Get("severity")).Value);
            Rule = new Rule(element.Element(XName.Get("rule")));
            FindCellLocation(element.Attribute(XName.Get("cell")).Value);
            load = false;
            IsCellSelected = false;
            
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
            element.SetAttributeValue(XName.Get("causingelement"), CausingElement);
            element.SetAttributeValue(XName.Get("description"), Description);
            element.SetAttributeValue(XName.Get("cell"), Cell.ToString());
            element.SetAttributeValue(XName.Get("firstoccurrence"), FirstOccurrence);
            element.SetAttributeValue(XName.Get("violationstate"), ViolationState);
            element.SetAttributeValue(XName.Get("solvedtime"), SolvedTime);
            element.SetAttributeValue(XName.Get("isread"), IsRead);
            element.SetAttributeValue(XName.Get("isselected"), isSelected);
            element.SetAttributeValue(XName.Get("severity"), severity);
            element.Add(Rule.ToXElement());
            IsCellSelected = false;
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
                        IsRead = true;
                        DataModel.Instance.CurrentWorkbook.IgnoredViolations.Add(this);
                        break;
                    case ViolationType.LATER:
                        IsRead = true;
                        DataModel.Instance.CurrentWorkbook.LaterViolations.Add(this);
                        break;
                    case ViolationType.SOLVED:
                        IsRead = true;
                        DataModel.Instance.CurrentWorkbook.SolvedViolations.Add(this);
                        break;
                }
                
                PersistCellLocation();
            }
        }

        /// <summary>
        /// Handles the action when an old ViolationState is removed
        /// </summary>
        private void HandleOldState()
        { 
            if (load) return;
            IsCellSelected = false;
            switch (violationState)
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
                        Cell = cell;
                        return;
                    }
                }
                this.cell = new CellLocation(workbook, location);
            }
        }

        /// <summary>
        /// Checks if the cell of the violation already has other violations and adds the new one or adds a new cell to the cellswithviolations and adds the violation to the
        /// respecting cell
        /// </summary>
        public void PersistCellLocation()
        {
            if (this.cell == null)
            {
                throw new NullReferenceException("The cell location is null. You have to call FindCellLocation before");
            }
            string location = Cell.Location;
            if (!string.IsNullOrWhiteSpace(location))
            {
                location = location.Substring(location.IndexOf(']') + 1);
                foreach (CellLocation cell in DataModel.Instance.CurrentWorkbook.ViolatedCells)
                {
                    Regex rgx = new Regex(@"\$|=");
                    if (cell.ViolationType.Equals(this.violationState) && rgx.Replace(cell.Location, "").Equals(rgx.Replace(location, "")))
                    {
                        Cell = cell;
                        cell.Violations.Add(this);
                        // Should the current Violation be visible (depending of the tab/categorie that is clicked in the sidepane)
                        cell.SetVisibility(DataModel.Instance.CurrentWorkbook.SelectedTab);
                        return;
                    }
                }
                //Only happens if until now there was no violation with matching cell found
                this.cell.ViolationType = violationState;
                this.cell.Violations.Add(this);
                DataModel.Instance.CurrentWorkbook.ViolatedCells.Add(this.cell);
            }
        }

        private void RemovefromCellLocation()
        {
            string location = Cell.Location;
            for (int i = DataModel.Instance.CurrentWorkbook.ViolatedCells.Count - 1; i >= 0; i--)
            {
                CellLocation cell = DataModel.Instance.CurrentWorkbook.ViolatedCells[i];
                Regex rgx = new Regex(@"\$|=");
                if (cell.ViolationType.Equals(violationState) && rgx.Replace(cell.Location, string.Empty).Equals(rgx.Replace(location, string.Empty)))
                {
                    cell.Violations.Remove(this);
                    return;
                }
            }
        }

        #endregion
    }
}
