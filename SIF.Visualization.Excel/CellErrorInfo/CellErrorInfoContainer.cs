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

        /// <summary>
        /// Instanciates a new Container setting the Paintingstyles
        /// </summary>
        public CellErrorInfoContainer()
        {
            //The SetStyle commands increase the drawing speed by a bit and reduce flickering
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            
            InitializeComponent();
        }

        private void ElementHost1_ChildChanged(object sender, ChildChangedEventArgs e)
        {

        }
    }
}
