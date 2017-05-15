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
        int pointX ;
        int pointY ;
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
            try
            {
                int pointX = NewConditionButton.Location.X;
                int pointY = 40;
                ConditionPanel.Controls.Clear();
                TextBox a = new TextBox();
                a.Text = "Passt so";
                a.Location = new Point(pointX, pointY);
                ConditionPanel.Controls.Add(a);
                ConditionPanel.Show();
                pointY += 20;
                
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
            //System.Windows.Forms.TextBox RegexTextBox = new System.Windows.Forms.TextBox();
            //myControl = new UserControl();
            //myControl.Controls.Add(new TextBox());
            
            
        }

        private void ConditionFirstCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch((ConditionFirstComboBox.SelectedItem.ToString()))
            {
                case "Regex":
                    //myControl = new UserControl();
                    //myControl.Controls.Add(new TextBox());

                    ConditionRegexTextBox.Visible = true;
                    break;
                case "Character Count":
                    ConditionRegexTextBox.Visible = false;
                    break;
                
            }
        }

        private void ChooseAreaCheckbox_Changed(object sender, EventArgs e)
        {
            // Excel Cell Area Select
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // TODO: Sendet die eingegeben Daten ab
            try
            {
                pointX = NewConditionButton.Location.X;
                pointY = NewConditionButton.Location.Y;
                ConditionPanel.Controls.Clear();
                for (int i = 0; i < 5; i++)
                {
                    TextBox a = new TextBox();
                    a.Text = "Passt so";
                    a.Location = new Point(pointX, pointY);
                    ConditionPanel.Controls.Add(a);
                    ConditionPanel.Show();
                    pointY += 20;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
        }
    }
}
