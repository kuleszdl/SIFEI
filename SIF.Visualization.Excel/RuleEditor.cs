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
        int row = 0;
        private ComboBox firstConditionBox;
        private TextBox regexBox;
        string[] conditions = { "Regex", "Character Count"};
        

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
                AddNewRow();
            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
        }
        public void AddNewRow()
        {
            pointX = NewConditionButton.Location.X;
            pointY = NewConditionButton.Location.Y;

            firstConditionBox = new ComboBox();
            ConditionPanel.Controls.Add(firstConditionBox);
            firstConditionBox.Location = new Point(pointX, pointY);
            firstConditionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            firstConditionBox.FormattingEnabled = true;
            firstConditionBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            firstConditionBox.Items.AddRange(conditions);
            firstConditionBox.Name = row.ToString();
            firstConditionBox.Size = new System.Drawing.Size(105, 21);
            firstConditionBox.TabIndex = 10;
            firstConditionBox.Visible = true;
            firstConditionBox.SelectedIndexChanged += ConditionFirstCombobox_SelectedIndexChanged;

            
            
            NewConditionButton.Location = new System.Drawing.Point(pointX, pointY + 30);


            row++;
        }

        public void AddRegexBox(int currentRow)
        {
            regexBox = new TextBox();
            ConditionPanel.Controls.Add(regexBox);
            regexBox.Location = new Point(245, 11+currentRow*30); //Hardcoded, eventuell ändern
            regexBox.Text = "insert Regex String";
            regexBox.Name = "regex" + currentRow.ToString();
            regexBox.Visible = true;
        }
        

        private void FirstBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {
                MessageBox.Show(e.ToString());
            }
            
                
        }


        private void ConditionFirstCombobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                var comboBox = sender as ComboBox;
                string currentRow = comboBox.Name;
                string selected = comboBox.SelectedItem.ToString();

                switch (selected)
                {
                    case "Regex":
                        ConditionRegexTextBox.Visible = true;
                        MessageBox.Show(comboBox.Name); // debug
                        AddRegexBox(Int32.Parse(currentRow));
                        break;
                    case "Character Count":
                        ConditionRegexTextBox.Visible = false;
                        MessageBox.Show(comboBox.Name); // debug
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
            MessageBox.Show(regexBox.Name);

        }

        private void ChooseCellButton_Click(object sender, EventArgs e)
        {
            CellPickerWF cellpicker = new CellPickerWF();
        }

        private void ConditionFirstComboBox_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }


       
        
    }
}
