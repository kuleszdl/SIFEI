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


namespace SIF.Visualization.Excel
{   
    public partial class RuleEditor : Form
    {
        int pointX ;
        int pointY ;
        // private Control myControl;
        public RuleEditor()
        {
            InitializeComponent();
            //  ConditionFirstComboBox.Controls.Add(myControl);
           // FormBorderStyle = FormBorderStyle.FixedDialog;
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
                int pointY = NewConditionButton.Location.Y;
                ComboBox ConditionFirstComboBox = new ComboBox();
                ConditionFirstComboBox.Text = "Bedingung wählen";
                ConditionFirstComboBox.Location = new Point(pointX, pointY);
                ConditionPanel.Controls.Add(ConditionFirstComboBox);

                ConditionFirstComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                ConditionFirstComboBox.FormattingEnabled = true;
                ConditionFirstComboBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
                ConditionFirstComboBox.Items.AddRange(new object[] {
            "Regex",
            "Character Count"});
                ConditionFirstComboBox.Location = new System.Drawing.Point(255, 39);
                ConditionFirstComboBox.Name = "ConditionFirstComboBox";
                ConditionFirstComboBox.Size = new System.Drawing.Size(105, 21);
                ConditionFirstComboBox.TabIndex = 10;
                ConditionFirstComboBox.Visible = true;
                ConditionFirstComboBox.SelectedIndexChanged += ConditionFirstCombobox_SelectedIndexChanged;

                ConditionPanel.Show();
                NewConditionButton.Location = new System.Drawing.Point(pointX, pointY + 30);

            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
           
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
