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
    public partial class DefineCellsPaneContainer : UserControl
    {
        public DefineCellsPane DefineCellsPane
        {
            get
            {
                if (this.DefineCellsPaneHost != null && this.DefineCellsPaneHost.Child != null)
                {
                    return this.DefineCellsPaneHost.Child as DefineCellsPane;
                }
                else
                {
                    return null;
                }
            }
        }

        public DefineCellsPaneContainer()
        {
            InitializeComponent();
        }
    }
}
