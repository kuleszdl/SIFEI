using System;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Rules;


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
            this.CancelButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_Cancel;
            this.ConfirmButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_CellPicker_Confirm;
            this.CellPickerLabel.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_CellPicker_Label;
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
