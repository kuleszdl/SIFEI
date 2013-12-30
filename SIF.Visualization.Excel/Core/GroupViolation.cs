using Microsoft.Office.Interop.Excel;
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
        private string location;
        private ObservableCollection<SingleViolation> violations;
        private SingleViolation selectedViolation;

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
        /// Gets or sets the location of this group violation.
        /// </summary>
        public string Location
        {
            get { return this.location; }
            set { this.SetProperty(ref this.location, value); }
        }

        /// <summary>
        /// Gets or sets the selected single violation of this group violation.
        /// </summary>
        public SingleViolation SelectedViolation
        {
            get { return this.selectedViolation; }
            set { this.SetProperty(ref this.selectedViolation, value); }
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
                   this.Location == other.Location &&
                   this.Violations.SequenceEqual(other.Violations) &&
                   this.SelectedViolation == other.SelectedViolation;
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

        public GroupViolation()
        {
        }

        public GroupViolation(XElement root, Workbook workbook, Finding finding)
            : base(root, workbook, finding)
        {
            (from p in root.Elements(XName.Get("singleviolation"))
             select new SingleViolation(p, workbook, finding)).ToList().ForEach(p => this.Violations.Add(p));

            this.VisibilityChanged += GroupViolation_VisibilityChanged;
        }

        private void GroupViolation_VisibilityChanged(object sender, System.EventArgs e)
        {
            foreach (var violation in this.Violations)
                violation.IsVisible = this.IsVisible;
        }

        private void Violations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Severity = this.Violations.Sum(p => p.Severity);
        }

        public override void CreateControls()
        {
            foreach (var violation in this.Violations)
            {
                violation.CreateControls();
            }
        }

        public override string ToString()
        {
            return this.Description + ", " + this.CausingElement;
        }

        #endregion
    }
}
