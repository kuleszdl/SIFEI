using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public abstract class CellData : BindableBase
    {
        #region Fields

        private string location;
        private string sifLocation;
        private string content;
        private TestInputType cellType;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the cell name (Location) of the current cell data.
        /// </summary>
        public string Location
        {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        /// <summary> 
        /// Gets or sets the auto generated cell name (Location) of the current cell data.
        /// </summary>
        public string SifLocation
        {
            get { return sifLocation; }
            set { SetProperty(ref sifLocation, value); }
        }

        /// <summary>
        /// Gets or sets the cell data (e.g. test value).
        /// </summary>
        public string Content
        {
            get { return content; }
            set { SetProperty(ref content, value); }
        }

        /// <summary>
        /// Gets or sets the cell type of the current cell data.
        /// </summary>
        public TestInputType CellType
        {
            get { return cellType; }
            set { SetProperty(ref cellType, value); }
        }

        #endregion

        #region Methods

        public CellData()
        {
        }

        #endregion
    }
}
