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
        private decimal severity;
        private string controlName;
        private CellErrorInfo cellErrorInfo;
        private Microsoft.Office.Tools.Excel.ControlSite control;
        private bool groupedViolation;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates whether this violation is selected in the user interface.
        /// </summary>
        public override bool IsSelected
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
                    if (this.Control != null)
                    {
                        this.Control.Visible = this.IsVisible;
                    }
                }
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Empty constructor
        /// </summary>
        public SingleViolation()
        {
        }

        /// <summary>
        /// Constructor for the xml file from SIF
        /// </summary>
        /// <param name="root">The root XElement</param>
        /// <param name="workbook">The current workbook</param>
        /// <param name="scanTime">The time of the scan</param>
        /// <param name="rule">The rule of this violation</param>
        public SingleViolation(XElement root, Workbook workbook, DateTime scanTime, Rule rule, bool groupedViolation)
            : base(root, workbook, scanTime, rule)
        {
            this.Severity = decimal.Parse(root.Attribute(XName.Get("severity")).Value.Replace(".0", ""));
            this.groupedViolation = groupedViolation;
        }

        /// <summary>
        /// Constructor for the xml file, that is stored in the .xls file
        /// </summary>
        /// <param name="element">The root XElement of the xml</param>
        /// <param name="workbook">The current workbook</param>
        public SingleViolation(XElement element, Workbook workbook)
            : base(element, workbook)
        {
            this.severity = Decimal.Parse(element.Attribute(XName.Get("severity")).Value);
            this.controlName = element.Attribute(XName.Get("controlname")).Value;
            this.groupedViolation = Convert.ToBoolean(element.Attribute(XName.Get("groupedviolation")).Value);
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

        /// <summary>
        /// Renders the controls in the spreadsheet
        /// </summary>
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
        }

        /// <summary>
        /// Writes this violation to a XElement obejct
        /// </summary>
        /// <param name="name">the name of the node in the xml</param>
        /// <returns>the object with the data of this violation</returns>
        public override XElement ToXElement(String name)
        {
            var element = this.SuperClassToXElement(new XElement(XName.Get(name)));

            // own fields
            element.SetAttributeValue(XName.Get("severity"), this.severity);
            element.SetAttributeValue(XName.Get("controlname"), this.controlName);
            element.SetAttributeValue(XName.Get("groupedviolation"), this.groupedViolation);
            return element;
        }

        protected override void HandleNewState(Violation.ViolationType type)
        {
            if (!load && !groupedViolation)
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
            }
        }

        #endregion
    }
}
