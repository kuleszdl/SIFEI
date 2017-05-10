using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SIF.Visualization.Excel.Properties;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SIF.Visualization.Excel
{   
    public partial class RuleEditor : Form
    {
        private Control myControl;
        public RuleEditor()
        {
            InitializeComponent();
            ConditionFirstComboBox.Controls.Add(myControl);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowDialog();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void NewConditionButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.TextBox RegexTextBox = new System.Windows.Forms.TextBox();
            myControl = new UserControl();
            myControl.Controls.Add(new TextBox());
            ConditionRegexTextBox.Visible = true;
        }

        private void ConditionFirstCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch((ConditionFirstComboBox.SelectedItem.ToString()))
            {
                case "Regex":
                    myControl = new UserControl();
                    myControl.Controls.Add(new TextBox());
                    ConditionRegexTextBox.Visible = true;
                    break;
                
            }
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // Sends the information for a new rule
        }
    }
}
