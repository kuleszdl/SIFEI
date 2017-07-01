using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Rules;
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
        private int maxCells = 10000;
        public CellPickerWF()
        {
            InitializeComponent();
            Show();

        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Dispose();
                RuleEditor.Instance.Open(RuleCreator.Instance.GetRule());
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            RuleCellType cellType = RuleCellType.CELL;
            var selectedCells = CellManager.Instance.GetSelectedCells();
            foreach (var cell in selectedCells)
                {
                    cell.RuleCellType = cellType;
                    DataModel.Instance.CurrentWorkbook.RuleCells.Add(cell);
                }
                RuleCreator.Instance.SetRuleCells(DataModel.Instance.CurrentWorkbook);
                DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
                DataModel.Instance.CurrentWorkbook.RuleCells.Clear();
                Dispose();
                RuleEditor.Instance.Open(RuleCreator.Instance.GetRule());
                        
                       
        }
    }
}
