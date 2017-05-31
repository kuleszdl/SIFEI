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
        //Excel.WorkSheet ws;
        Microsoft.Office.Interop.Excel.Worksheet ws;         
        public CellPickerWF()
        {
            InitializeComponent();
            ShowDialog();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            

        }

        void ws_SelectionChange(Microsoft.Office.Interop.Excel.Range Target)
        {
            this.textBox1.Text = Target.Address;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            ws.SelectionChange -= ws_SelectionChange;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public Microsoft.Office.Interop.Excel.Worksheet workbook { get; set; }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            
        }
    }
}
