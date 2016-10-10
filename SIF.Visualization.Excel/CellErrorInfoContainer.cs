using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace SIF.Visualization.Excel
{
    public partial class CellErrorInfoContainer : UserControl
    {
        /// <summary>
        /// Gets the element host containing the cell error info.
        /// </summary>
        public ElementHost ElementHost
        {
            get { return this.elementHost1; }
        }

        public CellErrorInfoContainer()
        {
            InitializeComponent();  
        }
    }
}
