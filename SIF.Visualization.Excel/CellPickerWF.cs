using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SIF.Visualization.Excel
{
    public partial class CellPickerWF : Form
    {
        public CellPickerWF()
        {
            InitializeComponent();
            Show();

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            RuleCellType cellType = RuleCellType.CELL;
            var selectedCells = CellManager.Instance.GetSelectedCells();
            string test = "";
            foreach (var cell in selectedCells)
            {
                cell.RuleCellType = cellType;
                test = test + cell.Location;
            }
            MessageBox.Show(test);

            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
            Close();
        }
    }
}
