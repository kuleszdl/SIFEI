using System.Windows.Forms;
using System.Windows.Forms.Integration;

namespace SIF.Visualization.Excel
{
    public partial class CellErrorInfoContainer : UserControl
    {
        /// <summary>
        /// Gets the element host containing the cell error info.
        /// </summary>
        public ElementHost ElementHost
        {
            get { return elementHost1; }
        }

        public CellErrorInfoContainer()
        {
            //The SetStyle comands increase the drawing speed by a bit and reduces flickering
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            
            InitializeComponent();
        }

        private void elementHost1_ChildChanged(object sender, ChildChangedEventArgs e)
        {

        }
    }
}
