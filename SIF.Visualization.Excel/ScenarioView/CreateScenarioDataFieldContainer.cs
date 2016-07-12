using System.Windows.Forms;

namespace SIF.Visualization.Excel.ScenarioView
{
    public partial class CreateScenarioDataFieldContainer : UserControl
    {
        public CreateScenarioDataField createScenarioDataField
        {
            get
            {
                if (createScenarioDataFieldHost != null && createScenarioDataFieldHost.Child != null)
                {
                    return createScenarioDataFieldHost.Child as CreateScenarioDataField;
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
