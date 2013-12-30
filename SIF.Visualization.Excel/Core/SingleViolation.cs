using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class SingleViolation : Violation
    {
        #region Fields

        private bool isSelected;
        private bool isFalsePositive;
        private decimal severity;

        private string controlName;
        private CellErrorInfo cellErrorInfo;
        private Microsoft.Office.Tools.Excel.ControlSite control;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates whether this violation is selected in the user interface.
        /// </summary>
        public bool IsSelected
        {
            get { return this.isSelected; }
            set
            {
                if (this.SetProperty(ref this.isSelected, value) && this.IsVisible == true)
                {
                    // Make control topmost
                    if (this.Control != null) this.Control.BringToFront();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this is a false positive.
        /// </summary>
        public bool IsFalsePositive
        {
            get { return this.isFalsePositive; }
            set
            {
                if (this.SetProperty(ref this.isFalsePositive, value))
                {
                    this.Control.Visible = !this.IsFalsePositive;
                    if (this.IsFalsePositive)
                    {
                        // Save the information about this false positive
                        var name = this.Cell.AddName(Settings.Default["FalsePositivePrefix"] as string, true);
                        DataModel.Instance.CurrentWorkbook.FalsePositives.Add(new FalsePositive() { Name = name.Name, ViolationName = this.CausingElement + this.Description, Content = this.Cell.Worksheet.Range[this.Cell.ShortLocation].Formula as string });
                    }
                    else
                    {
                        // Remove the information about this false positive
                        foreach (var name in this.Cell.FalsePositiveNames)
                        {
                            var falsePositive = DataModel.Instance.CurrentWorkbook.FalsePositives.Where(p => p.Name == name.Name).FirstOrDefault();
                            if (falsePositive != null && falsePositive.ViolationName == this.CausingElement + this.Description)
                            {
                                DataModel.Instance.CurrentWorkbook.FalsePositives.Remove(falsePositive);
                            }

                            this.Cell.DeleteName(name.Name);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the severity of this violation.
        /// </summary>
        public override decimal Severity
        {
            get { return this.severity; }
            set { this.SetProperty(ref this.severity, value); }
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
            SingleViolation other = obj as SingleViolation;
            if ((object)other == null) return false;

            return base.Equals(other) &&
                   this.IsFalsePositive == other.IsFalsePositive &&
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
        public static bool operator ==(SingleViolation a, SingleViolation b)
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
        public static bool operator !=(SingleViolation a, SingleViolation b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        public SingleViolation()
        {
        }

        public SingleViolation(XElement root, Workbook workbook, Finding finding)
            : base(root, workbook, finding)
        {
            this.Severity = decimal.Parse(root.Attribute(XName.Get("severity")).Value.Replace(".0", ""));
            this.VisibilityChanged += SingleViolation_VisibilityChanged;
        }

        public void SetFalsePositiveSilently(bool falsePositive)
        {
            this.isFalsePositive = falsePositive;
        }

        private void SingleViolation_VisibilityChanged(object sender, EventArgs e)
        {
            if (this.Control != null)
                this.Control.Visible = this.IsVisible ?? false;
        }

        public override void CreateControls()
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

            this.Control.Visible = !this.IsFalsePositive;
        }

        #endregion
    }
}
