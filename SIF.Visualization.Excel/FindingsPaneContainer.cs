using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel
{
    public partial class FindingsPaneContainer : UserControl
    {
        /// <summary>
        /// Gets the findings pane.
        /// </summary>
        public FindingsPane FindingsPane
        {
            get
            {
                if (this.elementHost1 != null && this.elementHost1.Child != null)
                    return this.elementHost1.Child as FindingsPane;
                else return null;
            }
        }

        public FindingsPaneContainer()
        {
            InitializeComponent();
        }
    }
}
