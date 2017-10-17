using System.Windows.Forms;

namespace SIF.Visualization.Excel.View
{
    public partial class ScenarioDataFieldContainer : UserControl
    {
        public ScenarioDataFieldContainer()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }

        public ScenarioDataField ScenarioDataField
        {
            get
            {
                if (scenarioDataFieldHost != null && scenarioDataFieldHost.Child != null)
                    return scenarioDataFieldHost.Child as ScenarioDataField;
                return null;
            }
        }
    }
}