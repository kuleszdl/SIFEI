using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel.FalsePositiveView
{
    public partial class FalsePositiveViewContainer : UserControl
    {

        /// <summary>
        /// Gets the findings pane.
        /// </summary>
        public FalsePositiveView FalsePositiveView
        {
            get
            {
                if (this.elementHost1 != null && this.elementHost1.Child != null)
                    return this.elementHost1.Child as FalsePositiveView;
                else return null;
            }
        }

        public FalsePositiveViewContainer()
        {
            InitializeComponent();
        }
    }
}
