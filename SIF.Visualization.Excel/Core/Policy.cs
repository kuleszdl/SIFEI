using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// Class that represents a policy
    /// </summary>
    public class Policy : BindableBase
    {
        #region Fields

        private string name;
        private string description;
        private string author;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the name of this finding.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// Gets or sets the description of this finding.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// Gets or sets the author of this finding.
        /// </summary>
        public string Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
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
            Policy other = obj as Policy;
            if ((object)other == null) return false;

            return Author == other.Author &&
                   Description == other.Description &&
                   Name == other.Name;
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
        public static bool operator ==(Policy a, Policy b)
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
        public static bool operator !=(Policy a, Policy b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods
        /// <summary>
        /// Empty Constructor for this class
        /// </summary>
        public Policy()
        {
        }

        /// <summary>
        /// Creating a new policy defining the author, the description and the name
        /// </summary>
        /// <param name="root"></param>
        public Policy(XElement root)
        {
            Author = root.Attribute(XName.Get("author")).Value;
            Description = root.Attribute(XName.Get("description")).Value;
            Name = root.Attribute(XName.Get("name")).Value;
        }

        #endregion
    }
}
