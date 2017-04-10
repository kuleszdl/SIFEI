using System;
using System.Collections.ObjectModel;

namespace SIF.Visualization.Excel.Core.Scenarios
{
    public class Scenario : BindableBase
    {
        #region Fields
        private Guid id;
        private string title;
        private string description;
        private DateTime creationDate;
        private ObservableCollection<InputData> inputs;
        private ObservableCollection<InvariantData> invariants;
        private ObservableCollection<ConditionData> conditions;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the title of the current scenario.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

        /// <summary>
        /// Gets or sets the description of the current scenario.
        /// </summary>
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// Gets or sets the date of creation of the current scenario.
        /// </summary>
        public DateTime CreationDate
        {
            get { return creationDate; }
            set { SetProperty(ref creationDate, value); }
        }

        /// <summary>
        /// Gets or sets the input cell data of the current scenario.
        /// </summary>
        public ObservableCollection<InputData> Inputs
        {
            get
            {
                if (inputs == null) inputs = new ObservableCollection<InputData>();
                return inputs;
            }
            set { SetProperty(ref inputs, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cell data of the current scenario.
        /// </summary>
        public ObservableCollection<InvariantData> Invariants
        {
            get
            {
                if (invariants == null) invariants = new ObservableCollection<InvariantData>();
                return invariants;
            }
            set { SetProperty(ref invariants, value); }
        }

        /// <summary>
        /// Gets or sets the result cell data of the current scenario.
        /// </summary>
        public ObservableCollection<ConditionData> Conditions
        {
            get
            {
                if (conditions == null) conditions = new ObservableCollection<ConditionData>();
                return conditions;
            }
            set { SetProperty(ref conditions, value); }
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
            if (!(obj is Scenario)) return false;

            var other = obj as Scenario;

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
        public static bool operator ==(Scenario a, Scenario b)
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
        public static bool operator !=(Scenario a, Scenario b)
        {
            return !(a == b);
        }
        #endregion

        #region Methods
        public Scenario()
        {
            id = Guid.NewGuid();
        }
        #endregion
    }
}
