using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel.ScenarioView
{
    public partial class ScenarioDetailPaneContainer : UserControl
    {
        public ScenarioDetailPane ScenarioDetailPane
        {
            get
            {
                if (this.ScenarioDetailPaneHost != null && this.ScenarioDetailPaneHost.Child != null)
                    return this.ScenarioDetailPaneHost.Child as ScenarioDetailPane;
                else return null;
            }
        }

        public ScenarioDetailPaneContainer()
        {
            InitializeComponent();
        }
    }
}
