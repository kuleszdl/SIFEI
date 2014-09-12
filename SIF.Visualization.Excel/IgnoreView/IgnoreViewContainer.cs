using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel.IgnoreView
{
    public partial class IgnoreViewContainer : UserControl
    {

        /// <summary>
        /// Gets the findings pane.
        /// </summary>
        public IgnoreView FalsePositiveView
        {
            get
            {
                if (this.elementHost1 != null && this.elementHost1.Child != null)
                    return this.elementHost1.Child as IgnoreView;
                else return null;
            }
        }

        public IgnoreViewContainer()
        {
            InitializeComponent();
        }
    }
}
