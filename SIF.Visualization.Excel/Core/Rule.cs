using System;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class Rule : BindableBase
    {
        /// <summary>
        /// The type of the rule
        /// </summary>
        public enum RuleType { STATIC, DYNAMIC, SANITY, COMPOSITE };

        #region Fields

        private String author;
        private String background;
        private String description;
        private String name;
        private String possibleSolution;
        private RuleType type;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the author
        /// </summary>
        public String Author
        {
            get { return author; }
            set { SetProperty(ref author, value); }
        }

        /// <summary>
        /// Gets or sets the background description
        /// </summary>
        public String Background
        {
            get { return background; }
            set { SetProperty(ref background, value); }
        }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public String Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public String Name
        {
            get { return name; }
            set { SetProperty(ref name, value); }
        }

        /// <summary>
        /// Gets or sets the possible solution
        /// </summary>
        public String PossibleSolution
        {
            get { return possibleSolution; }
            set { SetProperty(ref possibleSolution, value); }
        }

        /// <summary>
        /// Gets or sets the rule type
        /// </summary>
        public RuleType Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that loads all fields from a xml
        /// </summary>
        /// <param name="root">the root node of the xml for this rule</param>
        public Rule(XElement root)
        {
            Author = root.Attribute(XName.Get("author")).Value;
            Background = root.Attribute(XName.Get("background")).Value;
            Description = root.Attribute(XName.Get("description")).Value;
            Name = root.Attribute(XName.Get("name")).Value;
            XAttribute ps = root.Attribute(XName.Get("possibleSolution"));
            if (ps != null)
                PossibleSolution = ps.Value;
            Type = (RuleType)Enum.Parse(typeof(RuleType), root.Attribute(XName.Get("type")).Value);
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
            Rule other = obj as Rule;
            if ((object)other == null) return false;

            return base.Equals(other) &&
                   Author == other.Author &&
                   Background == other.Background &&
                   Description == other.Description &&
                   Name == other.Name &&
                   PossibleSolution == other.PossibleSolution &&
                   type == other.type;
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
        public static bool operator ==(Rule a, Rule b)
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
        public static bool operator !=(Rule a, Rule b)
        {
            return !(a == b);
        }

        public XElement ToXElement()
        {
            var element = new XElement(XName.Get("rule"));
            element.SetAttributeValue("author", author);
            element.SetAttributeValue("background", background);
            element.SetAttributeValue("description", description);
            element.SetAttributeValue("name", name);
            element.SetAttributeValue("solution", possibleSolution);
            element.SetAttributeValue("type", type);
            return element;
        }

        #endregion
    }
}
