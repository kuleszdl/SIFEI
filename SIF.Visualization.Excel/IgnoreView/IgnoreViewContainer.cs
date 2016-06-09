using System.Windows.Forms;

namespace SIF.Visualization.Excel.IgnoreView
{
    public partial class IgnoreViewContainer : UserControl
    {

        /// <summary>
        /// Gets the findings pane.
        /// </summary>
        public IgnoreView FalsePositiveView
        {
            get
            {
                if (elementHost1 != null && elementHost1.Child != null)
                    return elementHost1.Child as IgnoreView;
                else return null;
            }
        }

        public IgnoreViewContainer()
        {
            InitializeComponent();
        }
    }
}
