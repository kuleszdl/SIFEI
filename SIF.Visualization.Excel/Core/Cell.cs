using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.ScenarioCore;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class Cell : BindableBase, IAcceptVisitor
    {
        #region Fields

        private int id;
        private string location;
        private string sifLocation;
        private string content;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the id of this cell.
        /// </summary>
        public int Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }

        /// <summary>
        /// Gets or sets the location of this cell.
        /// </summary>
        public string Location
        {
            get { return this.location; }
            set { this.SetProperty(ref this.location, value); }
        }

        /// <summary>
        /// Gets or sets the auto generated location of this cell
        /// </summary>
        public string SifLocation
        {
            get { return this.sifLocation; }
            set { this.SetProperty(ref this.sifLocation, value); }
        }

        /// <summary>
        /// Gets or sets the content of this cell.
        /// </summary>
        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
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

            return this.Id == other.Id &&
                //this.Content == other.Content &&
                   this.Location == other.Location;
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
        public static bool operator ==(Cell a, Cell b)
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
        public static bool operator !=(Cell a, Cell b)
        {
            return !(a == b);
        }

        #endregion

        #region Converters
        public Cell ToCell()
        {
            var cell = new Cell()
            {
                Id = this.Id,
                Content = this.Content,
                Location = this.Location,
                SifLocation = this.SifLocation
            };
            return cell;
        }

        public Cells.InputCell ToInputCell()
        {
            var cell = new Cells.InputCell()
            {
                Id = this.Id,
                Content = this.Content,
                Location = this.Location,
                SifLocation = this.SifLocation
            };
            return cell;
        }

        public Cells.IntermediateCell ToIntermediateCell()
        {
            var cell = new Cells.IntermediateCell()
            {
                Id = this.Id,
                Content = this.Content,
                Location = this.Location,
                SifLocation = this.SifLocation
            };
            return cell;
        }

        public Cells.OutputCell ToOutputCell()
        {
            var cell = new Cells.OutputCell()
            {
                Id = this.Id,
                Content = this.Content,
                Location = this.Location,
                SifLocation = this.SifLocation
            };
            return cell;
        }

        public Cell ToCellType(Type t)
        {
            if (t == typeof(InputCell)) return this.ToInputCell();
            if (t == typeof(IntermediateCell)) return this.ToIntermediateCell();
            if (t == typeof(OutputCell)) return this.ToOutputCell();

            return null;
        }

        #endregion

        #region Methods

        public Cell()
        {
        }

        public Cell(XElement root, Workbook workbook)
        {
            this.Id = Convert.ToInt32(root.Attribute(XName.Get("number")).Value);
            this.Content = root.Attribute(XName.Get("content")).Value;
            this.Location = new CellLocation(workbook, root.Attribute(XName.Get("location")).Value).Location;
        }

        #endregion

        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
    }
}
