using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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
        /// Gets or sets the cell name of the current cell data.
        /// </summary>
        public string Location
        {
            get { return this.location; }
            set { this.SetProperty(ref this.location, value); }
        }

        /// <summary>
        /// Gets or sets the auto generated cell name of the current cell data.
        /// </summary>
        public string SifLocation
        {
            get { return this.sifLocation; }
            set { this.SetProperty(ref this.sifLocation, value); }
        }

        /// <summary>
        /// Gets or sets the cell data (e.g. test value).
        /// </summary>
        public string Content
        {
            get { return this.content; }
            set { this.SetProperty(ref this.content, value); }
        }

        /// <summary>
        /// Gets or sets the cell type of the current cell data.
        /// </summary>
        public TestInputType CellType
        {
            get { return this.cellType; }
            set { this.SetProperty(ref this.cellType, value); }
        }

        #endregion

        #region Methods

        public CellData()
        {
        }

        #endregion
    }
}
