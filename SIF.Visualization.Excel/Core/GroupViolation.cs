using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class GroupViolation : Violation
    {
        #region Fields

        private decimal severity;
        private ObservableCollection<SingleViolation> violations;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the severity of this violation.
        /// </summary>
        public override decimal Severity
        {
            get { return this.severity; }
            set { this.SetProperty(ref this.severity, value); }
        }

        /// <summary>
        /// Gets the single violations belonging to this group violation.
        /// </summary>
        public ObservableCollection<SingleViolation> Violations
        {
            get
            {
                if (this.violations == null)
                {
                    this.violations = new ObservableCollection<SingleViolation>();
                    this.violations.CollectionChanged += Violations_CollectionChanged;
                }
                return this.violations;
            }
        }

        /// <summary>
        /// Gets or sets a value that shows whether this violation has been selected or not
        /// </summary>
        public override bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.SetProperty(ref this.isSelected, value))
                {
                    violations.ToList().ForEach(v => v.IsSelected = value);
                    if (value)
                    {
                        this.IsRead = true;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this violation is visible in the spreadsheet.
        /// </summary>
        public override bool IsVisible
        {
            get { return this.isVisible; }
            set
            {
                if (this.SetProperty(ref this.isVisible, value))
                {
                    this.violations.ToList().ForEach(vi => vi.IsVisible = value);
                }
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
            GroupViolation other = obj as GroupViolation;
            if ((object)other == null) return false;

            return base.Equals(other) &&
                   this.Severity == other.Severity &&
                   this.Violations.SequenceEqual(other.Violations);
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
        public static bool operator ==(GroupViolation a, GroupViolation b)
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
        public static bool operator !=(GroupViolation a, GroupViolation b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Empty constructor
        /// </summary>
        public GroupViolation()
        {
        }

        /// <summary>
        /// Constructor for the xml file from SIF
        /// </summary>
        /// <param name="root">The root XElement</param>
        /// <param name="workbook">The current workbook</param>
        /// <param name="scanTime">The time of the scan</param>
        /// <param name="rule">The rule of this violation</param>
        public GroupViolation(XElement root, Workbook workbook, DateTime scanTime, Rule rule)
            : base(root, workbook, scanTime, rule)
        {
            (from p in root.Elements(XName.Get("singleviolation"))
             select new SingleViolation(p, workbook, scanTime, rule, true)).ToList().ForEach(p => this.Violations.Add(p));
        }

        /// <summary>
        /// Constructor for the xml file, that is stored in the .xls file
        /// </summary>
        /// <param name="element">The root XElement of the xml</param>
        /// <param name="workbook">The current workbook</param>
        public GroupViolation(XElement element, Workbook workbook)
            : base(element, workbook)
        {
            (from p in element.Elements(XName.Get("groupedviolation"))
             select new SingleViolation(p, workbook)).ToList().ForEach(p => this.Violations.Add(p));
            this.load = false;
        }

        /// <summary>
        /// Recalculates the serverity of this violation when the violations list changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Violations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Severity = this.Violations.Max(p => p.Severity);
        }

        /// <summary>
        /// Handles the changes of the visibility property
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GroupViolation_VisibilityChanged(object sender, System.EventArgs e)
        {
            foreach (var violation in this.Violations)
                violation.IsVisible = this.IsVisible;
        }

        /// <summary>
        /// Renders the controls in the spreadsheet
        /// </summary>
        public override void CreateControls()
        {
            foreach (var violation in this.Violations)
            {
                violation.CreateControls();
            }
        }

        /// <summary>
        /// Writes this violation to a XElement obejct
        /// </summary>
        /// <param name="name">the name of the node in the xml</param>
        /// <returns>the object with the data of this violation</returns>
        public override XElement ToXElement(String name)
        {
            var element = this.SuperClassToXElement(new XElement(XName.Get(name + "group")));

            element.Add(XName.Get("groupedviolations"), XName.Get("groupedviolation"), from p in this.violations select p.ToXElement("groupedviolation"));
            return element;
        }

        protected override void HandleNewState(Violation.ViolationType type)
        {
            if (!load)
            {
                switch (type)
                {
                    case ViolationType.NEW:
                        DataModel.Instance.CurrentWorkbook.Violations.Add(this);
                        break;
                    case ViolationType.FALSEPOSITIVE:
                        this.IsRead = true;
                        DataModel.Instance.CurrentWorkbook.FalsePositives.Add(this);
                        break;
                    case ViolationType.LATER:
                        this.IsRead = true;
                        DataModel.Instance.CurrentWorkbook.LaterViolations.Add(this);
                        break;
                    case ViolationType.SOLVED:
                        this.IsRead = false;
                        DataModel.Instance.CurrentWorkbook.SolvedViolations.Add(this);
                        break;
                }
                this.Violations.ToList().ForEach(vi => vi.ViolationState = type);
            }
        }

        #endregion
    }
}
