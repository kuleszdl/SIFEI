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
    public partial class CreateScenarioDataFieldContainer : UserControl
    {
        public CreateScenarioDataField createScenarioDataField
        {
            get
            {
                if (this.createScenarioDataFieldHost != null && this.createScenarioDataFieldHost.Child != null)
                {
                    return this.createScenarioDataFieldHost.Child as CreateScenarioDataField;
                }
                else
                {
                    return null;
                }
            }
        }

        public CreateScenarioDataFieldContainer()
        {
            InitializeComponent();
        }
    }
}
