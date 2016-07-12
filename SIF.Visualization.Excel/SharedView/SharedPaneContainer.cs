using System.Windows.Forms;

namespace SIF.Visualization.Excel.SharedView
{
    public partial class SharedPaneContainer : UserControl
    {
        public SharedPane SharedPane
        {
            get
            {
                if (sharedPaneHost != null && sharedPaneHost.Child != null)
                {
                    return sharedPaneHost.Child as SharedPane;
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
