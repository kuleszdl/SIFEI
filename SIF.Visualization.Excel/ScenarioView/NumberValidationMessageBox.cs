using System.Windows.Forms;

namespace SIF.Visualization.Excel.ScenarioView
{
    /// <summary>
    /// Asks the user whether the thousands seperator character was intended
    /// return codes
    /// yes: ignore in this field
    /// no: replace in this field
    /// ignore: always ignore it
    /// </summary>
    public partial class NumberValidationMessageBox : Form
    {
        public NumberValidationMessageBox()
        {
            InitializeComponent();
        }


    }
}
