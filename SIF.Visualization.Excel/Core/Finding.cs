using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class Finding : BindableBase
    {
        #region Fields

        private string name;
        private string description;
        private string author;
        private string background;
        private string possibleSolution;
        private decimal severity;
        private bool? isVisible;
        private ObservableCollection<Violation> violations;

        private ListCollectionView violationsView;
        private bool isSettingVisibility = false;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of this finding.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets the description of this finding.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets the author of this finding.
        /// </summary>
        public string Author
        {
            get { return this.author; }
            set { this.SetProperty(ref this.author, value); }
        }

        /// <summary>
        /// Gets or sets the background of this finding.
        /// </summary>
        public string Background
        {
            get { return this.background; }
            set { this.SetProperty(ref this.background, value); }
        }

        /// <summary>
        /// Gets or sets the possible solution for this finding.
        /// </summary>
        public string PossibleSolution
        {
            get { return this.possibleSolution; }
            set { this.SetProperty(ref this.possibleSolution, value); }
        }

        /// <summary>
        /// Gets or sets the severity of this finding.
        /// </summary>
        public decimal Severity
        {
            get { return this.severity; }
            set { this.SetProperty(ref this.severity, value); }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this finding's violations are visible in the spreadsheet.
        /// </summary>
        public bool? IsVisible
        {
            get { return this.isVisible; }
            set
            {
                bool changed = this.SetProperty(ref this.isVisible, value);
                if (changed && this.Violations.Count > 0 && this.IsVisible != null)
                {
                    isSettingVisibility = true;
                    foreach (var violation in this.Violations)
                    {
                        violation.IsVisible = this.IsVisible;
                    }
                    isSettingVisibility = false;
                }
            }
        }

        /// <summary>
        /// Gets or sets the violations of this finding.
        /// </summary>
        public ObservableCollection<Violation> Violations
        {
            get
            {
                if (this.violations == null)
                {
                    this.violations = new ObservableCollection<Violation>();
                    this.violations.CollectionChanged += Violations_CollectionChanged;
                }
                return this.violations;
            }
        }

        /// <summary>
        /// Gets or sets the violations of this finding in a sortable view.
        /// </summary>
        public ListCollectionView ViolationsView
        {
            get
            {
                if (this.violationsView == null)
                {
                    this.violationsView = new ListCollectionView(this.Violations);
                    this.violationsView.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));
                }
                return this.violationsView;
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
            Finding other = obj as Finding;
            if ((object)other == null) return false;

            return this.Author == other.Author &&
                   this.Background == other.Background &&
                   this.Description == other.Description &&
                   this.Name == other.Name &&
                   this.PossibleSolution == other.PossibleSolution &&
                   this.Severity == other.Severity &&
                   this.IsVisible == other.IsVisible &&
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
        public static bool operator ==(Finding a, Finding b)
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
        public static bool operator !=(Finding a, Finding b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        public Finding()
        {
            this.IsVisible = true;
        }

        public Finding(XElement root, Workbook workbook)
        {
            this.Author = root.Attribute(XName.Get("author")).Value;
            this.Background = root.Attribute(XName.Get("background")).Value;
            this.Description = root.Attribute(XName.Get("description")).Value;
            this.Name = root.Attribute(XName.Get("name")).Value;
            this.PossibleSolution = root.Attribute(XName.Get("solution")).Value;
            this.Severity = decimal.Parse(root.Attribute(XName.Get("severity")).Value.Replace(".0", ""));
            this.IsVisible = true;

            // Parse violations
            (from p in root.Elements(XName.Get("singleviolation"))
             select new SingleViolation(p, workbook, this)).ToList().ForEach(p => this.Violations.Add(p));
            (from p in root.Elements(XName.Get("violationgroup"))
             select new GroupViolation(p, workbook, this)).ToList().ForEach(p => this.Violations.Add(p));
        }

        public void CreateControls()
        {
            foreach (var violation in this.Violations)
            {
                violation.CreateControls();
            }
        }

        private void Violations_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.Severity = this.Violations.Sum(p => (p is SingleViolation && (p as SingleViolation).IsFalsePositive) ? 0 : p.Severity);

            // Register for notifications
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (var element in e.NewItems)
                    {
                        (element as Violation).PropertyChanged += Finding_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Move:
                    foreach (var element in e.OldItems)
                    {
                        (element as Violation).PropertyChanged -= Finding_PropertyChanged;
                    }
                    foreach (var element in e.NewItems)
                    {
                        (element as Violation).PropertyChanged += Finding_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (var element in e.OldItems)
                    {
                        (element as Violation).PropertyChanged -= Finding_PropertyChanged;
                    }
                    break;
                case NotifyCollectionChangedAction.Replace:
                    foreach (var element in e.OldItems)
                    {
                        (element as Violation).PropertyChanged -= Finding_PropertyChanged;
                    }
                    foreach (var element in e.NewItems)
                    {
                        (element as Violation).PropertyChanged += Finding_PropertyChanged;
                    }
                    break;
                default:
                    foreach (var element in this.Violations)
                    {
                        element.PropertyChanged -= Finding_PropertyChanged;
                        element.PropertyChanged += Finding_PropertyChanged;
                    }
                    break;
            }
        }

        private void Finding_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsFalsePositive")
            {
                this.Severity = this.Violations.Sum(p => (p is SingleViolation && (p as SingleViolation).IsFalsePositive) ? 0 : p.Severity);
            }
            if (e.PropertyName == "IsVisible" && isSettingVisibility == false)
            {
                // Recalculate the own IsVisible property
                int falseElements = 0;
                int nullElements = 0;
                int trueElements = 0;

                foreach (var element in this.Violations)
                {
                    if (element.IsVisible == null)
                        nullElements++;

                    if (element.IsVisible == false)
                        falseElements++;

                    if (element.IsVisible == true)
                        trueElements++;
                }

                bool? value = null;

                if (falseElements == 0 && nullElements == 0)
                {
                    value = true;
                }
                else if (trueElements == 0 && nullElements == 0)
                {
                    value = false;
                }
                else
                {
                    value = null;
                }

                if (this.isVisible != value)
                {
                    this.isVisible = value;
                    this.OnPropertyChanged("IsVisible");
                }
            }
        }

        #endregion
    }
}
