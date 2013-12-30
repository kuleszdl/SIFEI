using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the letter (e.g. EF) of this cell.
        /// </summary>
        public string Letter
        {
            get { return this.letter; }
            set { this.SetProperty(ref this.letter, value); }
        }

        /// <summary>
        /// Gets or sets the number (e.g. 43) of this cell.
        /// </summary>
        public int Number
        {
            get { return this.number; }
            set { this.SetProperty(ref this.number, value); }
        }


        /// <summary>
        /// Gets or sets the worksheet of this cell.
        /// </summary>
        public Worksheet Worksheet
        {
            get { return this.worksheet; }
            set { this.SetProperty(ref this.worksheet, value); }
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

            return this.Letter == other.Letter &&
                   this.Number == other.Number &&
                   Object.ReferenceEquals(this.Worksheet, other.Worksheet);
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
            get { return "=" + this.Worksheet.Name + "!$" + this.Letter + "$" + this.Number; }
        }

        /// <summary>
        /// Gets the short location of this cell (e.g. "A34").
        /// </summary>
        public string ShortLocation
        {
            get { return this.Letter + this.Number; }
        }

        /// <summary>
        /// Gets the names that are associated with this cell.
        /// </summary>
        public IEnumerable<Name> Names
        {
            get { return from Name p in this.Worksheet.Application.Names where (p.RefersTo as string) == this.Location select p; }
        }

        /// <summary>
        /// Gets the scenario cell names.
        /// </summary>
        public IEnumerable<Name> ScenarioNames
        {
            get { return from p in this.Names where p.Name.StartsWith(Settings.Default["CellNameTag"] as string) select p; }
        }

        /// <summary>
        /// Gets the false positive cell names.
        /// </summary>
        public IEnumerable<Name> FalsePositiveNames
        {
            get { return from p in this.Names where p.Name.StartsWith(Settings.Default["FalsePositivePrefix"] as string) select p; }
        }

        /// <summary>
        /// Gets the user names assigned to this cell.
        /// </summary>
        public IEnumerable<Name> UserNames
        {
            get { return this.Names.Except(this.ScenarioNames).Except(this.FalsePositiveNames); }
        }

        /// <summary>
        /// Gets or sets the content of this cell.
        /// </summary>
        public string Content
        {
            get { return this.Worksheet.Range[this.ShortLocation].Formula as string; }
            set { this.Worksheet.Range[this.ShortLocation].Formula = value; }
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
            this.Letter = Regex.Match(cell, "[A-Z]*").Value.ToUpper();
            this.Number = int.Parse(cell.Replace(this.Letter, ""));
        }

        /// <summary>
        /// Initializes a new instance of the cell location class.
        /// </summary>
        /// <param name="worksheet">The worksheet that contains this cell location.</param>
        /// <param name="location">The location of this cell, must be in A1 notation, e.g. "=Rechner!A34" or "=Rechner!$A$34", or "$A$34", or "A34".</param>
        public CellLocation(Worksheet worksheet, string location)
            : this(ref location)
        {
            this.Worksheet = worksheet;
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

            this.Worksheet = worksheet;
        }

        /// <summary>
        /// Converts this cell location into A1 notation (e.g. "=Rechner!$A$34").
        /// </summary>
        public override string ToString()
        {
            return this.Location;
        }

        #region Cell Names

        /// <summary>
        /// Adds a name to this cell.
        /// </summary>
        /// <param name="name">The name of the cell.</param>
        /// <param name="visible">Determines whether this cell name should be visible to the user.</param>
        protected Name InternalAddName(string name, bool visible)
        {
            return this.Worksheet.Application.Names.Add(name, this.Location, visible);
        }

        /// <summary>
        /// Adds a name to this cell.
        /// </summary>
        /// <param name="name">The prefix of the cell's name. A Guid will be appended to this prefix.</param>
        /// <param name="visible">Determines whether this cell name should be visible to the user.</param>
        public Name AddName(string prefix, bool visible)
        {
            var name = prefix + Guid.NewGuid().ToString().Replace("-", "");
            return this.InternalAddName(name, visible);
        }

        /// <summary>
        /// Deletes the specified name from this cell.
        /// </summary>
        /// <param name="name">The name that is to be deleted.</param>
        public void DeleteName(string name)
        {
            if (this.HasName(name))
            {
                this.GetName(name).Delete();
            }
        }

        /// <summary>
        /// Gets the specified name of this cell.
        /// </summary>
        /// <param name="name">The name that is to be returned.</param>
        /// <returns>The specified name, null if that name does not exist.</returns>
        public Name GetName(string name)
        {
            if (this.HasName(name))
            {
                return this.Names.Where(p => p.Name == name).FirstOrDefault();
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
            return this.Names.Where(p => p.Name == name).Count() > 0;
        }

        #endregion

        /// <summary>
        /// Selects this cell.
        /// </summary>
        public void Select()
        {
            this.Worksheet.Activate();
            this.Worksheet.Range[this.ShortLocation].Select();
        }

        /// <summary>
        /// Scrolls this cell into view.
        /// </summary>
        public void ScrollIntoView()
        {
            this.Worksheet.Activate();
            this.Worksheet.Range[this.ShortLocation].Show();
        }

        #endregion
    }
}