using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Core.Rules
{
    public class Rule : BindableBase
    {
        #region Fields
        private Guid id;
        private string title;
        private string description;
        private ObservableCollection<RuleData> ruleData;
        private ObservableCollection<Condition> condition;
        #endregion

        #region Properties
        //TODO Creation Set, XML Add
        public string Title
        {
            get { 
                return title; 
            }
            set { 
                SetProperty(ref title, value); 
            }
        }

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                SetProperty(ref description, value);
            }
        }

        public ObservableCollection<RuleData> RuleData
        {
            get
            {
                if (ruleData == null) ruleData= new ObservableCollection<RuleData>();
                return ruleData;
            }
            set
            {
                SetProperty(ref ruleData, value);
            }
        }

        public ObservableCollection<Condition> Conditions
        {
            get
            {
                if (condition == null)condition = new ObservableCollection<Condition>();
                return condition;
            }
            set
            {
                SetProperty(ref condition, value);
            }
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
            if (!(obj is Rule)) return false;

            var other = obj as Rule;

            if (id.Equals(other.id))
            {
                return true;
            }
            else
            {
                return false;
            }

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
        #endregion

        #region Methods
        public Rule()
        {
            id = Guid.NewGuid();
        }
        #endregion


    }
}
