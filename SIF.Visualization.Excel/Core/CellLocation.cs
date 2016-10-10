using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using Excel_Range = Microsoft.Office.Interop.Excel.Range;
using Workbook = Microsoft.Office.Interop.Excel.Workbook;
using Worksheet = Microsoft.Office.Interop.Excel.Worksheet;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// This class contains useful cell location methods.
    /// </summary>
    public class CellLocation : BindableBase
    {
        #region Fields

        private string letter;
        private int number;
        private Worksheet worksheet;
        private ObservableCollection<Violation> violations;
        private string controlName;
        private ViolationType violationType;
        private Microsoft.Office.Tools.Excel.ControlSite control;
        private bool violationSelected;
        private Violation selectedViolation;
        private ListCollectionView violationsPane;
        private int hasMultiple;

        /// <summary>
        /// Contained Violationtypes in this cell 
        /// [0] = Dynamic
        /// [1] = Static
        /// [2] = Sanity
        /// </summary>
        private int[] _ruleOccurrences = new int[3];

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the letter (e.g. EF) of this cell.
        /// </summary>
        public string Letter
        {
            get { return letter; }
            set { SetProperty(ref letter, value); }
        }

        /// <summary>
        /// Gets or sets the number (e.g. 43) of this cell.
        /// </summary>
        public int Number
        {
            get { return number; }
            set { SetProperty(ref number, value); }
        }


        /// <summary>
        /// Gets or sets the worksheet of this cell.
        /// </summary>
        public Worksheet Worksheet
        {
            get { return worksheet; }
            set { SetProperty(ref worksheet, value); }
        }

        /// <summary>
        /// Gets or sets the Violations of this cell
        /// </summary>
        public ObservableCollection<Violation> Violations
        {
            get
            {
                if (violations == null)
                {
                    violations = new ObservableCollection<Violation>();
                }
                return violations;
            }
            set { SetProperty(ref violations, value); }
        }

        /// <summary>
        /// Gets or sets the Violationtype of this cell
        /// </summary>
        public ViolationType ViolationType
        {
            get { return violationType; }
            set { SetProperty(ref violationType, value); }
        }

        /// <summary>
        /// Checks weather one of the contained Violations is selected
        /// </summary>
        public bool ViolationSelected
        {
            get { return violationSelected; }
            set { SetProperty(ref violationSelected, value); }
        }

        /// <summary>
        /// Gets or sets the selected violation of this cell
        /// </summary>
        public Violation SelectedViolation
        {
            get { return selectedViolation; }
            set { SetProperty(ref selectedViolation, value); }
        }

        /// <summary>
        /// Gets the Cell this celllocation belongs to
        /// </summary>
        public CellLocation Cell
        {
            get { return this; }
        }

        /// <summary>
        /// Gets or Sets the ViolationPane of the cell
        /// </summary>
        public ListCollectionView ViolationsPane
        {
            get { return violationsPane; }
            set { SetProperty(ref violationsPane, value); }
        }

        /// <summary>
        /// Gets or sets the ammount of Rule Occurances in this cell
        /// </summary>
        public int[] RuleOccurrences
        {
            get { return _ruleOccurrences; }
            set { _ruleOccurrences = value; }
        }

        /// <summary>
        /// If it has Multiple it should return the int 2 otherwise 0
        /// </summary>
        public int HasMultiple
        {
            get { return hasMultiple; }
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
            CellLocation other = obj as CellLocation;
            if ((object)other == null) return false;

            return Letter == other.Letter &&
                   Number == other.Number &&
                   ViolationType == other.ViolationType &&
                   ReferenceEquals(Worksheet, other.Worksheet);
        }

        public bool EqualsWithoutType(object obj)
        {
            CellLocation other = obj as CellLocation;
            if ((object)other == null) return false;

            return Letter == other.Letter &&
                   Number == other.Number &&
                   ReferenceEquals(Worksheet, other.Worksheet);
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
        public static bool operator ==(CellLocation a, CellLocation b)
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
        public static bool operator !=(CellLocation a, CellLocation b)
        {
            return !(a == b);
        }

        #endregion

        #region Fake Properties

        /// <summary>
        /// Gets the location of this cell in A1 notation (e.g. "=Rechner!$A$34").
        /// </summary>
        public string Location
        {
            get { return "=" + Worksheet.Name + "!$" + Letter + "$" + Number; }
        }

        /// <summary>
        /// Gets the short location of this cell (e.g. "A34").
        /// </summary>
        public string ShortLocation
        {
            get { return Letter + Number; }
        }

        /// <summary>
        /// Gets the names that are associated with this cell.
        /// </summary>
        public IEnumerable<Name> Names
        {
            get { return from Name p in Worksheet.Application.Names where (p.RefersTo as string) == Location select p; }
        }

        /// <summary>
        /// Gets the scenario cell names.
        /// </summary>
        public IEnumerable<Name> ScenarioNames
        {
            get { return from p in Names where p.Name.StartsWith(Settings.Default["CellNameTag"] as string) select p; }
        }

        /// <summary>
        /// Gets the false positive cell names.
        /// </summary>
        public IEnumerable<Name> FalsePositiveNames
        {
            get { return from p in Names where p.Name.StartsWith(Settings.Default["FalsePositivePrefix"] as string) select p; }
        }

        /// <summary>
        /// Gets the user names assigned to this cell.
        /// </summary>
        public IEnumerable<Name> UserNames
        {
            get { return Names.Except(ScenarioNames).Except(FalsePositiveNames); }
        }

        /// <summary>
        /// Gets or sets the content of this cell.
        /// </summary>
        public string Content
        {
            get { return Worksheet.Range[ShortLocation].Formula as string; }
            set { Worksheet.Range[ShortLocation].Formula = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new instance of the cell location class.
        /// </summary>
        /// <param name="location">The location of this cell, must be in A1 notation, e.g. "=Rechner!A34" or "=Rechner!$A$34", or "$A$34", or "A34".</param>
        protected CellLocation(ref string location)
        {
            location = location.Trim();
            location = location.Replace("$", string.Empty);
            location = location.Replace("=", string.Empty);

            // Remove the optional [spreadsheet.xls]
            if (location.Contains('[') && location.Contains(']'))
            {
                location = location.Replace(Regex.Match(location, "\\[{1}.*\\]{1}").Value, string.Empty);
            }

            string cell = location;

            // Remove all left of the exclamation mark
            if (cell.Contains('!')) cell = cell.Substring(cell.IndexOf('!') + 1);

            // Parse letter and number
            Letter = Regex.Match(cell, "[A-Z]*").Value.ToUpper();
            if (cell.Contains(":")) cell = cell.Substring(letter.Length, cell.IndexOf(":") - 1);
            Number = int.Parse(cell.Replace(Letter, string.Empty));
            Violations.CollectionChanged += Violations_CollectionChanged;
        }

        /// <summary>
        /// Handler when the Violations collection changed (Especially when Violations get added or removed)
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void Violations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            CalculateOccurances();
            if (Violations.Count > 0)
            {
                if (Violations.Count == 1)
                {
                    SelectedViolation = violations.ElementAt(0);
                    ViolationSelected = true;
                    if (control == null)
                    {
                        DrawIcon();
                    }
                }
                else
                {
                    ViolationSelected = false;
                }
                SetVisibility(DataModel.Instance.CurrentWorkbook.SelectedTab);
                violationsPane = new ListCollectionView(Violations);
                violationsPane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
                ViolationsPane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));
                OnPropertyChanged("Violations");
            }
            else
            {
                RemoveIcon();
                DataModel.Instance.CurrentWorkbook.ViolatedCells.Remove(this);
            }
        }

        /// <summary>
        /// Calculates how many Violations of whoch kind are located in this cell
        /// </summary>
        private void CalculateOccurances()
        {
            hasMultiple = 0;
            //Checks how many violations of what type appear in this location
            Array.Clear(RuleOccurrences, 0, RuleOccurrences.Length - 1);
            foreach (Violation violation in Violations.Where(violation => violation != null))
            {
                switch (violation.Rule.Type)
                {
                    case Rule.RuleType.DYNAMIC:
                        RuleOccurrences[0]++;
                        hasMultiple = 10;
                        break;
                    case Rule.RuleType.STATIC:
                        RuleOccurrences[1]++;
                        hasMultiple = 10;
                        break;
                    case Rule.RuleType.SANITY:
                        RuleOccurrences[2]++;
                        hasMultiple = 10;
                        break;
                    case Rule.RuleType.COMPOSITE:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the cell location class.
        /// </summary>
        /// <param name="worksheet">The worksheet that contains this cell location.</param>
        /// <param name="location">The location of this cell, must be in A1 notation, e.g. "=Rechner!A34" or "=Rechner!$A$34", or "$A$34", or "A34".</param>
        public CellLocation(Worksheet worksheet, string location) : this(ref location)
        {
            Worksheet = worksheet;
        }

        /// <summary>
        /// Initializes a new instance of the cell location class.
        /// </summary>
        /// <param name="workbook">The workbook that contains the worksheet containing this cell location.</param>
        /// <param name="location">The location of this cell, must be in A1 notation, e.g. "=Rechner!A34" or "=Rechner!$A$34".</param>
        public CellLocation(Workbook workbook, string location) : this(ref location)
        {
            if (!location.Contains('!'))
                throw new ArgumentException("The cell location must contain a worksheet name.");

            // Get the worksheet name
            location = location.Substring(0, location.IndexOf('!'));

            // Find the right worksheet depending on location
            var worksheet = (from Worksheet p in workbook.Worksheets where p.Name == location select p).FirstOrDefault();

            // Could not find a worksheet...
            if (worksheet == null) throw new ArgumentException("Could not find the right worksheet inside this workbook model.");

            Worksheet = worksheet;
        }

        /// <summary>
        /// Converts this cell location into A1 notation (e.g. "=Rechner!$A$34").
        /// </summary>
        public override string ToString()
        {
            return Location;
        }

        #region Cell Names

        /// <summary>
        /// Adds a name to this cell.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="visible">Determines whether this cell name should be visible to the user.</param>
        protected Name InternalAddName(string name, bool visible)
        {
            return Worksheet.Application.Names.Add(name, Location, visible);
        }

        /// <summary>
        /// Adds a name to this cell.
        /// </summary>
        /// <param name="name">The prefix of the cell's name. A Guid will be appended to this prefix.</param>
        /// <param name="visible">Determines whether this cell name should be visible to the user.</param>
        public Name AddName(string prefix, bool visible)
        {
            var name = prefix + Guid.NewGuid().ToString().Replace("-", string.Empty);
            return InternalAddName(name, visible);
        }

        /// <summary>
        /// Deletes the specified name from this cell.
        /// </summary>
        /// <param name="name">The name that is to be deleted.</param>
        public void DeleteName(string name)
        {
            if (HasName(name))
            {
                GetName(name).Delete();
            }
        }

        /// <summary>
        /// Gets the specified name of this cell.
        /// </summary>
        /// <param name="name">The name that is to be returned.</param>
        /// <returns>The specified name, null if that name does not exist.</returns>
        public Name GetName(string name)
        {
            if (HasName(name))
            {
                return Names.Where(p => p.Name == name).FirstOrDefault();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Checks whether this cell has a certain name pointing to it.
        /// </summary>
        /// <param name="name">The name to be checked.</param>
        /// <returns>True, if that name is pointing to this cell, otherwise false.</returns>
        public bool HasName(string name)
        {
            return Names.Where(p => p.Name == name).Count() > 0;
        }

        #endregion

        /// <summary>
        /// Selects this cell.
        /// </summary>
        public void Select()
        {
            ((_Worksheet) Worksheet).Activate();
            Worksheet.Range[ShortLocation].Select();
        }

        /// <summary>
        /// If a violation gets selected also this cell gets selected
        /// </summary>
        /// <param name="violation"></param>
        public void Select(Violation violation)
        {
            Select();
            SelectedViolation = violation;
            ViolationSelected = true;
        }

        /// <summary>
        /// Unselects this cell
        /// </summary>
        public void Unselect()
        {
            if (Violations.Count > 1)
            {
                ViolationSelected = false;
            }
        }

        /// <summary>
        /// Scrolls this cell into view.
        /// </summary>
        public void ScrollIntoView()
        {
            ((_Worksheet) Worksheet).Activate();
            Worksheet.Range[ShortLocation].Show();
        }

        /// <summary>
        /// Draws the respecting Icon for this cell
        /// </summary>
        private void DrawIcon()
        {
            try
            {
                var container = new CellErrorInfoContainer();
                container.SuspendLayout();
                container.ElementHost.Child = new CellErrorInfo
                {
                    DataContext = this, Visibility = Visibility.Visible, Opacity = 1.00
                };
                container.ElementHost.Child.UpdateLayout();
                var vsto = Globals.Factory.GetVstoObject(Worksheet);
                controlName = Guid.NewGuid().ToString();
                container.Visible = false;
                container.ResumeLayout(true);
                control = vsto.Controls.AddControl(container, Worksheet.Range[ShortLocation], controlName);
                control.Visible = false;
                control.Width = control.Height + 4;
                control.Placement = XlPlacement.xlMoveAndSize;
                control.AutoLoad = true;
                container.Visible = true;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Removes the icon from this cell
        /// </summary>
        public void RemoveIcon()
        {
            if (!string.IsNullOrWhiteSpace(controlName))
            {
                var vsto = Globals.Factory.GetVstoObject(Worksheet);
                vsto.Controls.Remove(controlName);
                controlName = null;
            }
        }

        /// <summary>
        /// Decides weather the icon of this cell should be visible.
        /// Depending on which Tab is open and which status the violations in these cells are
        /// </summary>
        /// <param name="tab">The tab whose visibiliy should be set</param>
        public void SetVisibility(SharedTabs tab)
        {
            if (control == null) return;
            if (violationType.Equals(ViolationType.OPEN) && tab.Equals(SharedTabs.Open))
            {
                control.Visible = true;
            }
            else if (violationType.Equals(ViolationType.LATER) && tab.Equals(SharedTabs.Later))
            {
                control.Visible = true;
            }
            else if (violationType.Equals(ViolationType.IGNORE) && tab.Equals(SharedTabs.Ignore))
            {
                control.Visible = true;
            }
            else if (violationType.Equals(ViolationType.SOLVED) && tab.Equals(SharedTabs.Archive))
            {
                control.Visible = true;
            }
            else
            {
                control.Visible = false;
            }
        }

        #endregion
    }
}