using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.ScenarioCore;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// Represents a cell in the Worksheet (Only the ones that contain violations or at some moment contained violations
    /// </summary>
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
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        /// <summary>
        /// Gets or sets the location of this cell.
        /// </summary>
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        /// <summary>
        /// Gets or sets the auto generated location of this cell
        /// </summary>
        public string SifLocation
        {
            get { return sifLocation; }
            set { SetProperty(ref sifLocation, value); }
        }

        /// <summary>
        /// Gets or sets the content of this cell.
        /// </summary>
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
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

            return Id == other.Id &&
                //this.Content == other.Content &&
                   Location == other.Location;
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
        public static bool operator !=(Cell a, Cell b)
        {
            return !(a == b);
        }

        #endregion

        #region Converters

        /// <summary>
        /// Converts the cell to a normal cell (not involved in a scenario)
        /// </summary>
        /// <returns></returns>
        public Cell ToCell()
        {
            var cell = new Cell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }

        /// <summary>
        /// Defines this cell as an input cell for a scenario
        /// </summary>
        /// <returns></returns>
        public InputCell ToInputCell()
        {
            var cell = new InputCell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }

        /// <summary>
        /// Defines this cell as an intermediate cell for a scenario
        /// </summary>
        /// <returns></returns>
        public IntermediateCell ToIntermediateCell()
        {
            var cell = new IntermediateCell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }

        /// <summary>
        /// Defines this cell as an Sanity value cell
        /// </summary>
        /// <returns></returns>
        public SanityValueCell ToSanityValueCell()
        {
            var cell = new SanityValueCell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }

        /// <summary>
        /// Defines this cell as an Sanity checking cell
        /// </summary>
        /// <returns></returns>
        public SanityCheckingCell ToSanityCheckingCell()
        {
            var cell = new SanityCheckingCell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }
        /// <summary>
        /// Defines this cell as an Sanity constrained cell
        /// </summary>
        /// <returns></returns>
        public SanityConstraintCell ToSanityConstraintCell()
        {
            var cell = new SanityConstraintCell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }

        /// <summary>
        /// Defines this cell as an Sanity explanation cell
        /// </summary>
        /// <returns></returns>
        public SanityExplanationCell ToSanityExplanationCell()
        {
            var cell = new SanityExplanationCell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }

        /// <summary>
        /// Defines this cell as an output cell for a scenario
        /// </summary>
        /// <returns></returns>
        public OutputCell ToOutputCell()
        {
            var cell = new OutputCell()
            {
                Id = Id,
                Content = Content,
                Location = Location,
                SifLocation = SifLocation
            };
            return cell;
        }

        /// <summary>
        /// Converts this cell to the specified celltype
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public Cell ToCellType(Type t)
        {
            if (t == typeof(InputCell)) return ToInputCell();
            if (t == typeof(IntermediateCell)) return ToIntermediateCell();
            if (t == typeof(OutputCell)) return ToOutputCell();

            return null;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor
        /// </summary>
        public Cell()
        {
        }

        /// <summary>
        /// Creates a new cell in the defined workbook
        /// </summary>
        /// <param name="root"></param>
        /// <param name="workbook"></param>
        public Cell(XElement root, Workbook workbook)
        {
            Id = Convert.ToInt32(root.Attribute(XName.Get("number")).Value);
            Content = root.Attribute(XName.Get("content")).Value;
            Location = new CellLocation(workbook, root.Attribute(XName.Get("location")).Value).Location;
        }

        #endregion

        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
    }
}
