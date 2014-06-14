using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        private ObservableCollection<SanityValueCellData> sanityValues;
        private ObservableCollection<SanityConstraintCellData> sanityConstraints;
        private ObservableCollection<SanityExplanationCellData> sanityExplanations;
        private ObservableCollection<SanityCheckingCellData> sanityCheckings;
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

        /// <summary>
        /// Gets or sets the sanity value cells of the current document.
        /// </summary>
        public ObservableCollection<SanityValueCellData> SanityValueCells
        {
            get
            {
                if (this.sanityValues == null) this.sanityValues = new ObservableCollection<SanityValueCellData>();
                return this.sanityValues;
            }
            set { this.SetProperty(ref this.sanityValues, value); }
        }

        /// <summary>
        /// Gets or sets the sanity constraint cells of the current document.
        /// </summary>
        public ObservableCollection<SanityConstraintCellData> SanityConstraintCells
        {
            get
            {
                if (this.sanityConstraints == null) this.sanityConstraints = new ObservableCollection<SanityConstraintCellData>();
                return this.sanityConstraints;
            }
            set { this.SetProperty(ref this.sanityConstraints, value); }
        }

        /// <summary>
        /// Gets or sets the sanity explanation cells of the current document.
        /// </summary>
        public ObservableCollection<SanityExplanationCellData> SanityExplanationCells
        {
            get
            {
                if (this.sanityExplanations == null) this.sanityExplanations = new ObservableCollection<SanityExplanationCellData>();
                return this.sanityExplanations;
            }
            set { this.SetProperty(ref this.sanityExplanations, value); }
        }

        /// <summary>
        /// Gets or sets the sanity checking cells of the current document.
        /// </summary>
        public ObservableCollection<SanityCheckingCellData> SanityCheckingCells
        {
            get
            {
                if (this.sanityCheckings == null) this.sanityCheckings = new ObservableCollection<SanityCheckingCellData>();
                return this.sanityCheckings;
            }
            set { this.SetProperty(ref this.sanityCheckings, value); }
        }


        #endregion

        #region Operators

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
