using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel.SharedView
{
    public partial class SharedPaneContainer : UserControl
    {
        public SharedPane SharedPane
        {
            get
            {
                if (this.sharedPaneHost != null && this.sharedPaneHost.Child != null)
                {
                    return this.sharedPaneHost.Child as SharedPane;
                }
                else
                {
                    return null;
                }
            }
        }

        public SharedPaneContainer()
        {
            InitializeComponent();
        }
    }
}
