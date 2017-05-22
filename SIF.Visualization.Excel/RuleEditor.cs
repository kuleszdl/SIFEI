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
        Microsoft.Office.Interop.Excel.Worksheet ws;
        int pointX;
        int pointY;
        int row = 2;
        

        public RuleEditor()
        {
            InitializeComponent();
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
               // AddNewComboBox();

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
                FirstBox.Name = "ConditionFirstComboBox" + rows.ToString();
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

        

        private void FirstBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            
                
        }


        private void ConditionFirstCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            var comboBox = sender as ComboBox;
            try
            {
                switch ((comboBox.SelectedItem.ToString()))
                {
                    case "Regex":
                        ConditionRegexTextBox.Visible = true;
                        break;
                    case "Character Count":
                        ConditionRegexTextBox.Visible = false;
                        break;

                }
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
           
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // TODO: Sendet die eingegeben Daten ab

        }

        private void ChooseCellButton_Click(object sender, EventArgs e)
        {
            CellPickerWF cellpicker = new CellPickerWF();
        }


        public System.Windows.Forms.ComboBox AddNewComboBox()
        {
            pointX = NewConditionButton.Location.X;
            pointY = NewConditionButton.Location.Y;
            ComboBox[] firstBoxes = new ComboBox[row];
            firstBoxes[row] = new ComboBox();

            this.ConditionPanel.Controls.Add(firstBoxes[row]);

            firstBoxes[row].Text = "Choose Condition";
            firstBoxes[row].Items.AddRange(new object[] {
                    "Regex",
                    "Character Count",
                    "Includes"});
            firstBoxes[row].Visible = true;
            firstBoxes[row].Size = new System.Drawing.Size(105, 21);
            firstBoxes[row].Location = new System.Drawing.Point(pointX, pointY);
            firstBoxes[row].Text = "Bedingung wählen";
            firstBoxes[row].DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            firstBoxes[row].FormattingEnabled = true;
            firstBoxes[row].ImeMode = System.Windows.Forms.ImeMode.Disable;
            firstBoxes[row].TabIndex = 10;

            NewConditionButton.Location = new System.Drawing.Point(pointX, pointY + 30);
            row++;
            return firstBoxes[row];
        }


        
    }
}
