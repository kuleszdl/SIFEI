using Microsoft.Office.Interop.Excel;
using System;
using System.Linq;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public abstract class Violation : BindableBase
    {
        #region Events

        public event EventHandler VisibilityChanged;
        protected void OnVisibilityChanged()
        {
            if (this.VisibilityChanged != null)
            {
                this.VisibilityChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        #region Fields

        private int id;
        private string causingElement;
        private string description;
        private CellLocation cell;
        private bool? isVisible;

        private Finding finding;

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
        /// Gets or sets a value that indicates whether this violation is visible in the spreadsheet.
        /// </summary>
        public bool? IsVisible
        {
            get { return this.isVisible; }
            set { if (this.SetProperty(ref this.isVisible, value)) this.OnVisibilityChanged(); }
        }

        /// <summary>
        /// Gets or sets the finding that contains this violation.
        /// </summary>
        public Finding Finding
        {
            get { return this.finding; }
            set { this.SetProperty(ref this.finding, value); }
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
                   this.IsVisible == other.IsVisible;
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

        #region Methods

        public Violation()
        {
        }

        public Violation(XElement root, Workbook workbook, Finding finding)
        {
            this.Finding = finding;

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

            this.IsVisible = true;
        }

        public abstract void CreateControls();

        #endregion
    }
}
