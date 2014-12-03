using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.ObjectModel;


namespace SIF.Visualization.Excel.ScenarioCore
{
    public class Scenario : BindableBase, IAcceptVisitor
    {
        #region Fields
        private Guid id;
        private string title;
        private string description;
        private string author;
        private DateTime creationDate;
        private double rating;
        private ObservableCollection<InputCellData> inputs;
        private ObservableCollection<IntermediateCellData> intermediates;
        private ObservableCollection<ResultCellData> results;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the title of the current scenario.
        /// </summary>
        public string Title
        {
            get { return this.title; }
            set { this.SetProperty(ref this.title, value); }
        }

        /// <summary>
        /// Gets or sets the description of the current scenario.
        /// </summary>
        public string Description
        {
            get { return this.description; }
            set { this.SetProperty(ref this.description, value); }
        }

        /// <summary>
        /// Gets or sets the author of the current scenario.
        /// </summary>
        public string Author
        {
            get { return this.author; }
            set { this.SetProperty(ref this.author, value); }
        }

        /// <summary>
        /// Gets or sets the date of creation of the current scenario.
        /// </summary>
        public DateTime CrationDate
        {
            get { return this.creationDate; }
            set { this.SetProperty(ref this.creationDate, value); }
        }

        /// <summary>
        /// Gets or sets the severity rating of the current scenario.
        /// </summary>
        public double Rating
        {
            get { return this.rating; }
            set { this.SetProperty(ref this.rating, value); }
        }

        /// <summary>
        /// Gets or sets the input cell data of the current scenario.
        /// </summary>
        public ObservableCollection<InputCellData> Inputs
        {
            get
            {
                if (this.inputs == null) this.inputs = new ObservableCollection<InputCellData>();
                return this.inputs;
            }
            set { this.SetProperty(ref this.inputs, value); }
        }

        /// <summary>
        /// Gets or sets the intermediate cell data of the current scenario.
        /// </summary>
        public ObservableCollection<IntermediateCellData> Intermediates
        {
            get
            {
                if (this.intermediates == null) this.intermediates = new ObservableCollection<IntermediateCellData>();
                return this.intermediates;
            }
            set { this.SetProperty(ref this.intermediates, value); }
        }

        /// <summary>
        /// Gets or sets the result cell data of the current scenario.
        /// </summary>
        public ObservableCollection<ResultCellData> Results
        {
            get
            {
                if (this.results == null) this.results = new ObservableCollection<ResultCellData>();
                return this.results;
            }
            set { this.SetProperty(ref this.results, value); }
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

            if (this.id.Equals(other.id))
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

        #region Accept Visitor
        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
        #endregion

        #endregion
    }
}
