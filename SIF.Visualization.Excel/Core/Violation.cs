using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public abstract class Violation : BindableBase
    {
        /// <summary>
        /// Enum for the Violation Type
        /// </summary>
        public enum ViolationType { NEW, IGNORE, LATER, SOLVED };

        public enum ViolationKind { GROUP, SINGLE };

        #region Fields

        private int id;
        private string causingElement;
        private string description;
        protected CellLocation cell;
        private DateTime firstOccurrence;
        private DateTime solvedTime;
        private bool foundAgain;
        private Rule rule;
        protected bool isVisible;
        protected bool isRead;
        protected bool cellSelected;
        protected bool isSelected;
        protected bool load;
        protected ViolationType violationState;

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
        public abstract decimal Severity
        {
            get;
            set;
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
        public abstract bool IsVisible
        {
            get;
            set;
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
        /// Gets or sets a value that shows whether this violation has been selected or not
        /// </summary>
        public abstract bool IsSelected
        {
            get;
            set;
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
        /// Gets or sets a value that indicates whether this violation is visible in the spreadsheet.
        /// </summary>
        public abstract ViolationKind Kind
        {
            get;
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
                   this.Rule == other.Rule;
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
        /// Empty constructor
        /// </summary>
        public Violation()
        {
        }

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
        protected Violation(XElement element, Workbook workbook)
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
        protected XElement SuperClassToXElement(XElement element)
        {
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
            element.Add(this.Rule.ToXElement());
            this.IsCellSelected = false;
            return element;
        }

        /// <summary>
        /// Writes this violation to a XElement obejct
        /// </summary>
        /// <param name="name">the name of the node in the xml</param>
        /// <returns>the object with the data of this violation</returns>
        public abstract XElement ToXElement(String name);

        /// <summary>
        /// Renders the controls in the spreadsheet
        /// </summary>
        public abstract void CreateControls();

        /// <summary>
        /// Handles the action when a new ViolationState is set
        /// </summary>
        /// <param name="type">the type of the new violation State</param>
        protected abstract void HandleNewState(ViolationType type);

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
