using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.ScenarioCore;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
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

        public Cell ToCellType(Type t)
        {
            if (t == typeof(InputCell)) return ToInputCell();
            if (t == typeof(IntermediateCell)) return ToIntermediateCell();
            if (t == typeof(OutputCell)) return ToOutputCell();

            return null;
        }

        #endregion

        #region Methods

        public Cell()
        {
        }

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
