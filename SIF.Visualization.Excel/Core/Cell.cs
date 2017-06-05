using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Xml.Serialization;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// Represents a cell in the Worksheet (Only the ones that contain violations or at some moment contained violations
    /// </summary>
    public class Cell : BindableBase
    {
        #region Fields

        private int id;
        private string content;
        private string worksheetKey;
        private string columnKey;
        private string rowKey;
        private ScenarioCellType scenarioCellType = ScenarioCellType.NONE;
        private SanityCellType sanityCellType = SanityCellType.NONE;
        private RuleCellType ruleCellType = RuleCellType.CELL;
        private bool isSelected;
        private Workbook workbook;
        private Worksheet worksheet;
        private Violation selectedViolation;
        private Microsoft.Office.Tools.Excel.ControlSite control;
        private string controlName;
        private ListCollectionView violationsPane;
        private ObservableCollection<Violation> violations;
        private ObservableCollection<Violation> visibleViolations;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id of this cell.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        [XmlIgnore]
        public string Location
        {
            get { return worksheetKey + "!" + columnKey + rowKey; }
        }

        [XmlIgnore]
        public string ScenarioCellIcon
        {
            get {
                switch (ScenarioCellType) {
                    case Core.ScenarioCellType.INPUT:
                        return "../Resources/Icons/input_clear.png";
                    case Core.ScenarioCellType.INVARIANT:
                        return "../Resources/Icons/intermediate_clear.png";
                    case Core.ScenarioCellType.CONDITION:
                        return "../Resources/Icons/output_clear.png";
                    default:
                        return "../Resources/Icons/input_clear.png";
                }
            }
        }
        

        [XmlIgnore]
        public string ShortLocation {
            get { return columnKey + rowKey; }
        }

        public bool IsSelected {
            get { return isSelected; }
            set {
                isSelected = value;
                foreach (Violation vio in Violations) {
                    vio.IsCellSelected = value;
                }
                NotifyPropertyChanged("IsSelected");
                
            }
        }

        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        public string WorksheetKey {
            get { return worksheetKey; }
            set { SetProperty(ref worksheetKey, value); }
        }

        public string ColumnKey {
            get { return columnKey; }
            set { SetProperty(ref columnKey, value); }
        }

        public string RowKey {
            get { return rowKey; }
            set { SetProperty(ref rowKey, value); }
        }

        [XmlIgnore]
        public ObservableCollection<Violation> Violations {
            get {
                if (violations == null) violations = new ObservableCollection<Violation>();
                return violations;
            }
            set { SetProperty(ref violations, value); }
        }

        [XmlIgnore]
        public ObservableCollection<Violation> VisibleViolations {
            get {
                if (visibleViolations == null) visibleViolations = new ObservableCollection<Violation>();
                return visibleViolations;
            }
            set { SetProperty(ref visibleViolations, value); }
        }

        [XmlIgnore]
        public Violation SelectedViolation {
            get { return selectedViolation; }
            set { SetProperty(ref selectedViolation, value); }
        }

        public ScenarioCellType ScenarioCellType {
            get { return scenarioCellType; }
            set { SetProperty(ref scenarioCellType, value); }
        }

        public SanityCellType SanityCellType {
            get { return sanityCellType; }
            set { SetProperty(ref sanityCellType, value); }
        }

        public RuleCellType RuleCellType
        {
            get
            {
                return ruleCellType;
            }
            set
            {
                SetProperty(ref ruleCellType, value);
            }
        }

        [XmlIgnore]
        public ListCollectionView ViolationsPane {
            get { return violationsPane; }
            set { SetProperty(ref violationsPane, value); }
        }

        [XmlIgnore]
        public Workbook Workbook {
            get { return workbook; }
            set { workbook = value; }
        }

        [XmlIgnore]
        public Worksheet Worksheet {
            get { return worksheet; }
            set { worksheet = value; }
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
            Cell other = obj as Cell;
            if ((object)other == null) return false;

            return Location.Equals(other.Location);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode() {
            return base.GetHashCode();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Serialization Constructor of a cell
        /// </summary>
        public Cell() {}

        /// <summary>
        /// Constructor of a cell
        /// </summary>
        public Cell(Workbook workbook, string address)
        {
            this.workbook = workbook;
            extractLocation(address);
        }

        private void extractLocation(string address) {
            address = address.Replace("$", string.Empty);
            worksheetKey = address.Substring(0, address.IndexOf('!'));

            // Find the right worksheet depending on location
            var worksheet = (from Worksheet p in workbook.Worksheets where p.Name == worksheetKey select p).FirstOrDefault();
            // Could not find a worksheet...
            if (worksheet == null)
                throw new ArgumentException("Could not find the right worksheet inside this workbook model.");
            else 
                this.worksheet = worksheet;

            string shortAddress = address.Substring(address.IndexOf('!') + 1);
            columnKey = Regex.Match(shortAddress, "[A-Z]+").Value.ToUpper();
            rowKey = shortAddress.Replace(columnKey, string.Empty);
        }

        public void RecalculateVisibleViolations() {
            VisibleViolations.Clear();
            ViolationState state;
            switch (DataModel.Instance.CurrentWorkbook.SelectedTabIndex) {
                case WorkbookModel.Tabs.OpenViolations:
                    state = ViolationState.OPEN;
                    break;
                case WorkbookModel.Tabs.ArchivedViolations:
                    state = ViolationState.SOLVED;
                    break;
                case WorkbookModel.Tabs.IgnoredViolations:
                    state = ViolationState.IGNORE;
                    break;
                case WorkbookModel.Tabs.LaterViolations:
                    state = ViolationState.LATER;
                    break;
                default:
                    state = ViolationState.NONE;
                    break;
            }

            foreach (var vio in Violations) {
                if (vio.ViolationState == state) {
                    VisibleViolations.Add(vio);
                }
            }

            if (VisibleViolations.Count > 0) {
                DrawIcon();
                SetVisibility();
                ViolationsPane = new ListCollectionView(VisibleViolations);
                ViolationsPane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
                ViolationsPane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));
            } else {
                RemoveIcon();
                SelectedViolation = null;
            }
        }

        /// <summary>
        /// Draws the respecting Icon for this cell
        /// </summary>
        public void DrawIcon() {
            if (control == null) {
                try {
                    var container = new CellErrorInfoContainer();
                    container.SuspendLayout();
                    container.ElementHost.Child = new CellErrorInfo {
                        DataContext = this,
                        Visibility = Visibility.Visible
                    };
                    container.ResumeLayout(true);
                    var vsto = Globals.Factory.GetVstoObject(worksheet);
                    controlName = Guid.NewGuid().ToString();
                    control = vsto.Controls.AddControl(container, worksheet.Range[ShortLocation], controlName);
                    control.Width = 13;
                    control.Height = 13;
                } catch (Exception e) {
                    Debug.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Removes the icon from this cell
        /// </summary>
        public void RemoveIcon() {
            if (!string.IsNullOrWhiteSpace(controlName)) {
                var vsto = Globals.Factory.GetVstoObject(worksheet);
                vsto.Controls.Remove(controlName);
                controlName = null;
                control = null;
            }
        }

        /// <summary>
        /// Decides weather the icon of this cell should be visible.
        /// Depending on which Tab is open and which status the violations in these cells are
        /// </summary>
        /// <param name="tab">The tab whose visibiliy should be set</param>
        public void SetVisibility() {
            var tab = DataModel.Instance.CurrentWorkbook.SelectedTabIndex;
            if (control != null) {
                foreach (var violation in violations) {
                    if (violation.ViolationState.Equals(ViolationState.OPEN) && tab.Equals(WorkbookModel.Tabs.OpenViolations)) {
                        control.Visible = true;
                        return;
                    } else if (violation.ViolationState.Equals(ViolationState.LATER) && tab.Equals(WorkbookModel.Tabs.LaterViolations)) {
                        control.Visible = true;
                        return;
                    } else if (violation.ViolationState.Equals(ViolationState.IGNORE) && tab.Equals(WorkbookModel.Tabs.IgnoredViolations)) {
                        control.Visible = true;
                        return;
                    } else if (violation.ViolationState.Equals(ViolationState.SOLVED) && tab.Equals(WorkbookModel.Tabs.ArchivedViolations)) {
                        control.Visible = true;
                        return;
                    }
                }
                control.Visible = false;
            } 
        }

        #endregion
    }
}
