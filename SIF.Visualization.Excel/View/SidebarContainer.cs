using System.Windows.Forms;

namespace SIF.Visualization.Excel.View
{
    public partial class SidebarContainer : UserControl
    {
        public SidebarContainer()
        {
            InitializeComponent();
        }

        public Sidebar Sidebar
        {
            get
            {
                if (SidebarHost != null && SidebarHost.Child != null)
                    return SidebarHost.Child as Sidebar;
                return null;
            }
        }
    }
}