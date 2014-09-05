using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            get { return this.author; }
            set { this.SetProperty(ref this.author, value); }
        }

        /// <summary>
        /// Gets or sets the background description
        /// </summary>
        public String Background
        {
            get { return this.background; }
            set { this.SetProperty(ref this.background, value); }
        }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public String Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public String Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets the possible solution
        /// </summary>
        public String PossibleSolution
        {
            get { return this.possibleSolution; }
            set { this.SetProperty(ref this.possibleSolution, value); }
        }

        /// <summary>
        /// Gets or sets the rule type
        /// </summary>
        public RuleType Type
        {
            get { return this.type; }
            set { this.SetProperty(ref this.type, value); }
        }

        #endregion

        #region Constructor
        /// <summary>
        /// Constructor that loads all fields from a xml
        /// </summary>
        /// <param name="root">the root node of the xml for this rule</param>
        public Rule(XElement root)
        {
            this.Author = root.Attribute(XName.Get("author")).Value;
            this.Background = root.Attribute(XName.Get("background")).Value;
            this.Description = root.Attribute(XName.Get("description")).Value;
            this.Name = root.Attribute(XName.Get("name")).Value;
            XAttribute ps = root.Attribute(XName.Get("possibleSolution"));
            if (ps != null)
                this.PossibleSolution = ps.Value;
            this.Type = (RuleType)Enum.Parse(typeof(RuleType), root.Attribute(XName.Get("type")).Value);
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
                   this.Author == other.Author &&
                   this.Background == other.Background &&
                   this.Description == other.Description &&
                   this.Name == other.Name &&
                   this.PossibleSolution == other.PossibleSolution &&
                   this.type == other.type;
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
        public static bool operator !=(Rule a, Rule b)
        {
            return !(a == b);
        }

        public XElement ToXElement()
        {
            var element = new XElement(XName.Get("rule"));
            element.SetAttributeValue("author", this.author);
            element.SetAttributeValue("background", this.background);
            element.SetAttributeValue("description", this.description);
            element.SetAttributeValue("name", this.name);
            element.SetAttributeValue("solution", this.possibleSolution);
            element.SetAttributeValue("type", this.type);
            return element;
        }

        #endregion
    }
}
