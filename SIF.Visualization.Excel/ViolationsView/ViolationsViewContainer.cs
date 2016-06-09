using System.Windows.Forms;

namespace SIF.Visualization.Excel.ViolationsView
{
    public partial class ViolationsViewContainer : UserControl
    {

        /// <summary>
        /// Gets the findings pane.
        /// </summary>
        public ViolationsView  ViolationsView
        {
            get
            {
                if (elementHost1 != null && elementHost1.Child != null)
                    return elementHost1.Child as ViolationsView;
                else return null;
            }
        }
        public ViolationsViewContainer()
        {
            InitializeComponent();
        }
    }
}
