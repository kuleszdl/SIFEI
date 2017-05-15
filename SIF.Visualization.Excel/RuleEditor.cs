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
                int rows = 0;
                ComboBox FirstBox = new ComboBox();
                FirstBox.Text = "Bedingung wählen";
                FirstBox.Location = new Point(pointX, pointY);
                ConditionPanel.Controls.Add(FirstBox);

                FirstBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
                FirstBox.FormattingEnabled = true;
                FirstBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
                FirstBox.Items.AddRange(new object[] {
            "Regex",
            "Character Count"});
                FirstBox.Name = "ConditionFirstComboBox"+ rows.ToString();
                FirstBox.Size = new System.Drawing.Size(105, 21);
                FirstBox.TabIndex = 10;
                FirstBox.Visible = true;
                FirstBox.SelectedIndexChanged += ConditionFirstCombobox_SelectedIndexChanged;

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
