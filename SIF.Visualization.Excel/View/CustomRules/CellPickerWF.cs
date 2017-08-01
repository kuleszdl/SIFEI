using System;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Rules;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.View.CustomRules
{
    public partial class CellPickerWF : Form
    {
        public CellPickerWF()
        {
            InitializeComponent();
            SetLocalisation();
            Show();
        }

        private void SetLocalisation()
        {
            CancelButton.Text = Resources.tl_Cancel;
            ConfirmButton.Text = Resources.tl_CellPicker_Confirm;
            CellPickerLabel.Text = Resources.tl_CellPicker_Label;
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

        /// <summary>
        ///     Sets chosen Rulecells in the Workbook and adds them to the current Rule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            var cellType = RuleCellType.CELL;
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