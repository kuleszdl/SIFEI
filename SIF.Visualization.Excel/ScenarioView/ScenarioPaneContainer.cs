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
    public partial class ScenarioPaneContainer : UserControl
    {
        public ScenarioPane ScenarioPane
        {
            get
            {
                if (this.scenarioPaneHost != null && this.scenarioPaneHost.Child != null)
                    return this.scenarioPaneHost.Child as ScenarioPane;
                else return null;
            }
        }

        public ScenarioPaneContainer()
        {
            InitializeComponent();
        }
    }
}
