using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class FalsePositive : BindableBase
    {
        #region Fields

        private string name;
        private string violationName;
        private string content;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cell name reference of this false positive.
        /// </summary>
        public string Name
        {
            get { return this.name; }
            set { this.SetProperty(ref this.name, value); }
        }

        /// <summary>
        /// Gets or sets the name of the corresponging violation.
        /// </summary>
        public string ViolationName
        {
            get { return this.violationName; }
            set { this.SetProperty(ref this.violationName, value); }
        }

        /// <summary>
        /// Gets or sets the cell content of this false positive.
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
            FalsePositive other = obj as FalsePositive;
            if ((object)other == null) return false;

            return this.Name == other.Name &&
                   this.ViolationName == other.ViolationName &&
                   this.Content == other.Content;
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
        public static bool operator ==(FalsePositive a, FalsePositive b)
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
        public static bool operator !=(FalsePositive a, FalsePositive b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        public FalsePositive()
        {
        }

        public FalsePositive(XElement element)
        {
            this.Name = element.Attribute(XName.Get("name")).Value;
            this.ViolationName = element.Attribute(XName.Get("violationname")).Value;
            this.Content = element.Attribute(XName.Get("content")).Value;
        }

        public XElement ToXElement()
        {
            var element = new XElement(XName.Get("falsepositive"));
            element.SetAttributeValue(XName.Get("name"), this.Name);
            element.SetAttributeValue(XName.Get("violationname"), this.ViolationName);
            element.SetAttributeValue(XName.Get("content"), this.Content);
            return element;
        }

        #endregion
    }
}
