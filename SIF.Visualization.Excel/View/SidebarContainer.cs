using System.Windows.Forms;

namespace SIF.Visualization.Excel.View
{
    public partial class SidebarContainer : UserControl
    {
        public Sidebar Sidebar
        {
            get
            {
                if (SidebarHost != null && SidebarHost.Child != null)
                {
                    return SidebarHost.Child as Sidebar;
                }
                else
                {
                    return null;
                }
            }
        }

        public SidebarContainer()
        {
            InitializeComponent();
        }
    }
}
