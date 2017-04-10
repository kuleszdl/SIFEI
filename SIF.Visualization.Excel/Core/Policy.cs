using System;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class Policy : BindableBase
    {
        /// <summary>
        /// The type of the Policy
        /// </summary>
        public enum PolicyType { STATIC, DYNAMIC, SANITY };

        #region Fields

        private String background;
        private String description;
        private String name;
        private String solution;
        private PolicyType type;

        #endregion

        #region Properties

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
        public String Solution
        {
            get { return solution; }
            set { SetProperty(ref solution, value); }
        }

        /// <summary>
        /// Gets or sets the Policy type
        /// </summary>
        public Policy.PolicyType Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        #endregion

        #region Constructor

        /// <summary>
        /// Serialization Constructor
        /// </summary>
        public Policy() {}

        /// <summary>
        /// Constructor that loads all fields from a xml
        /// </summary>
        /// <param name="root">the root node of the xml for this Policy</param>
        public Policy(XElement xmlPolicy)
        {
            Name = (string) xmlPolicy.Element(XName.Get("name"));
            Description =  (string) xmlPolicy.Element(XName.Get("description"));
            Background = (string) xmlPolicy.Element(XName.Get("background"));
            Solution = (string) xmlPolicy.Element(XName.Get("solution"));
            Type = (PolicyType) Enum.Parse(typeof(PolicyType), (string) xmlPolicy.Element(XName.Get("policyType")));
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

            return base.Equals(other) &&
                   Background == other.Background &&
                   Description == other.Description &&
                   Name == other.Name &&
                   Solution == other.Solution &&
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

        public XElement ToXElement()
        {
            var element = new XElement(XName.Get("policy"));
            element.SetAttributeValue("background", background);
            element.SetAttributeValue("description", description);
            element.SetAttributeValue("name", name);
            element.SetAttributeValue("solution", solution);
            element.SetAttributeValue("type", type);
            return element;
        }

        #endregion
    }
}
