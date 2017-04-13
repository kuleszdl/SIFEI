using SIF.Visualization.Excel.Core;
using System.Windows.Forms;

namespace SIF.Visualization.Excel.View {

    public partial class ScenarioDataFieldContainer : UserControl {

        public ScenarioDataField ScenarioDataField {
            get {
                if (scenarioDataFieldHost != null && scenarioDataFieldHost.Child != null) {
                    return scenarioDataFieldHost.Child as ScenarioDataField;
                } else {
                    return null;
                }
            }
        }

        public ScenarioDataFieldContainer() {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            InitializeComponent();
        }
    }
}
