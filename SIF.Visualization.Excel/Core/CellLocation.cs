using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Properties;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Ink;
using System.Windows.Shapes;
using Microsoft.Office.Core;
using Excel_Range = Microsoft.Office.Interop.Excel.Range;
using Rectangle = System.Windows.Shapes.Rectangle;
using Shape = System.Windows.Shapes.Shape;

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

        public ViolationType ViolationType
        {
            get { return violationType; }
            set { SetProperty(ref violationType, value); }
        }

        public bool ViolationSelected
        {
            get { return violationSelected; }
            set { SetProperty(ref violationSelected, value); }
        }

        public Violation SelectedViolation
        {
            get { return selectedViolation; }
            set { SetProperty(ref selectedViolation, value); }
        }

        public CellLocation Cell
        {
            get { return this; }
        }

        public ListCollectionView ViolationsPane
        {
            get { return violationsPane; }
            set { SetProperty(ref violationsPane, value); }
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
            location = location.Replace("$", "");
            location = location.Replace("=", "");

            // Remove the optional [spreadsheet.xls]
            if (location.Contains('[') && location.Contains(']'))
            {
                location = location.Replace(Regex.Match(location, "\\[{1}.*\\]{1}").Value, "");
            }

            string cell = location;

            // Remove all left of the exclamation mark
            if (cell.Contains('!')) cell = cell.Substring(cell.IndexOf('!') + 1);

            // Parse letter and number
            Letter = Regex.Match(cell, "[A-Z]*").Value.ToUpper();
            if (cell.Contains(":")) cell = cell.Substring(letter.Length, cell.IndexOf(":") - 1);
            Number = int.Parse(cell.Replace(Letter, ""));
            Violations.CollectionChanged += Violations_CollectionChanged;
        }

        public void Violations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
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
        /// Initializes a new instance of the cell location class.
        /// </summary>
        /// <param name="worksheet">The worksheet that contains this cell location.</param>
        /// <param name="location">The location of this cell, must be in A1 notation, e.g. "=Rechner!A34" or "=Rechner!$A$34", or "$A$34", or "A34".</param>
        public CellLocation(Worksheet worksheet, string location)
            : this(ref location)
        {
            Worksheet = worksheet;
        }

        /// <summary>
        /// Initializes a new instance of the cell location class.
        /// </summary>
        /// <param name="workbook">The workbook that contains the worksheet containing this cell location.</param>
        /// <param name="location">The location of this cell, must be in A1 notation, e.g. "=Rechner!A34" or "=Rechner!$A$34".</param>
        public CellLocation(Workbook workbook, string location)
            : this(ref location)
        {
            if (!location.Contains('!'))
                throw new ArgumentException("The cell location must contain a worksheet name.");

            // Get the worksheet name
            location = location.Substring(0, location.IndexOf('!'));

            // Find the right worksheet depending on location
            var worksheet = (from Worksheet p in workbook.Worksheets
                             where p.Name == location
                             select p).FirstOrDefault();

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
            var name = prefix + Guid.NewGuid().ToString().Replace("-", "");
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
            ((_Worksheet)Worksheet).Activate();
            Worksheet.Range[ShortLocation].Select();
        }

        public void Select(Violation violation)
        {
            Select();
            SelectedViolation = violation;
            ViolationSelected = true;
        }

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
            ((_Worksheet)Worksheet).Activate();
            Worksheet.Range[ShortLocation].Show();
        }

        private void DrawIcon()
        {
            try
            {
                var container = new CellErrorInfoContainer();
                container.ElementHost.Child = new CellErrorInfo()
                {
                    DataContext = this
                };

                
                var vsto = Globals.Factory.GetVstoObject(Worksheet);
               // Range left = this.Worksheet.Range = control.Ra= Cell.ShortLocation.
                controlName = Guid.NewGuid().ToString();

                control = vsto.Controls.AddControl(container, Worksheet.Range[ShortLocation], controlName);
                
                Range left = Worksheet.get_Range(ShortLocation);
                int distLeft =(int) left.Left;
                int disttop = (int) left.Top;

   
                
                control.Width = control.Height + 4;
                control.Placement = XlPlacement.xlMove;
                  
                control.AutoLoad = true;
                
            }
            catch (Exception ex) { Console.Out.WriteLine(ex.ToString()); }
        }

        public void RemoveIcon()
        {
            if (!string.IsNullOrWhiteSpace(controlName))
            {
                var vsto = Globals.Factory.GetVstoObject(Worksheet);
                vsto.Controls.Remove(controlName);
                controlName = null;
            }
        }

        public void SetVisibility(SharedTabs tab)
        {
            if (control != null)
            {
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
        }

        #endregion
    }
}