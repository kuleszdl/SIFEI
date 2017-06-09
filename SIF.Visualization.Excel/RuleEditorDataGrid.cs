using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Rules;
using SIF.Visualization.Excel.Helper;
using SIF.Visualization.Excel.Properties;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace SIF.Visualization.Excel
{
    public partial class RuleEditorDataGrid : Form
    {
        Microsoft.Office.Interop.Excel.Worksheet ws;
        int pointX;
        int pointY;
        int totalRows = 0;
        
        
        
        private ComboBox firstConditionBox;
        private TextBox regexBox;
        private TextBox characterBox;
        private Button deleteRowButton;
        string[] avaibleConditions = { 
                                  "Regex", 
                                  "Character Count"
                              };
        

        //public RuleEditor()
        //{
        //    InitializeComponent();
        //    ShowDialog();
        //}

        //public RuleEditor(System.Data.Rule rule)
        //{
        //    // TODO: Anzeige einer vorhandenen Regel
        //    InitializeComponent();
        //    ShowDialog();
        //}


        private void NewConditionButton_Click(object sender, EventArgs e)
        {
            try
            {
                //AddNewRow();

                
                
               AddNewDataRow();
                
                    
                
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        private void AddNewDataRow()
        {
            DataGridViewComboBoxColumn firstConditionBox = new DataGridViewComboBoxColumn();
            
            firstConditionBox.Items.AddRange(avaibleConditions);
            firstConditionBox.Name = totalRows.ToString();
            firstConditionBox.Visible = true;
            
            conditionDataGridView.Rows.Add(firstConditionBox);
            totalRows++;
        }

        private void conditionDataGridView_EditControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e) { 
            if (e.Control.GetType() == typeof(DataGridViewComboBoxEditingControl))
            {
                ComboBox cmb = (ComboBox)e.Control;
                cmb.SelectedIndexChanged += new EventHandler(FirstConditionBox_SelectedIndexChanged);
            }
        }
      
        private void FirstConditionBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                
                var comboBox = sender as ComboBox;
                string currentRow = comboBox.Name;
                string selected = comboBox.SelectedItem.ToString();

                switch (selected)
                {
                    case "Regex":
                        //RemoveOtherBoxes();
                        //AddRegexBox(Int32.Parse(currentRow));
                        AddDataGridRegexBox(currentRow);
                        break;
                    case "Character Count":
                        AddCharacterBox(Int32.Parse(currentRow));
                        break;

                }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
           
        }

        private void AddDataGridRegexBox(object sender)
        {
            
        }

        

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string ruleTitle = RuleNameTextBox.Text;
            
            // Get List of Conditions
            GetConditions();
            

            // Check for Rulecells
            // Startet den Rule Creator 
            RuleCreator.Instance.Start(DataModel.Instance.CurrentWorkbook, ruleTitle, totalRows);

            try
            {
                var newRule = RuleCreator.Instance.End();
                if (newRule != null)
                {
                    DataModel.Instance.CurrentWorkbook.Rules.Add(newRule);
                }
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
            Close();

        }

        private void GetConditions()
        {
            for (int i = totalRows; i > 0; i--)
            {

            }
        }

        private void CheckConditions(String checkBoxName)
        {
            foreach (Control control in this.Controls)
            {
                if (control.Name == checkBoxName)
                {
                    String conditionValue = control.Text;
                    MessageBox.Show(conditionValue);
                }
            }
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //RuleCreator.Instance.End();
            Close();
        }

        private void ChooseCellButton_Click(object sender, EventArgs e)
        {
            // siehe DefineResultCell Event in Ribbon
            CellPickerWF cellpicker = new CellPickerWF();
        }
        private void AddRegexBox(int currentRow)
        {
            regexBox = new TextBox();
            ConditionPanel.Controls.Add(regexBox);
            regexBox.Location = new Point(245, 11 + currentRow * 30); //Hardcoded, eventuell ändern
            regexBox.Text = "insert Regex";
            regexBox.Name = "regex" + currentRow.ToString();
            regexBox.Visible = true;
        }

        private void AddCharacterBox(int p)
        {
            characterBox = new TextBox();
            ConditionPanel.Controls.Add(characterBox);
            characterBox.Location = new Point(245, 11 + p * 30); //Hardcoded, eventuell ändern
            characterBox.Text = "insert maximum Character Count";
            characterBox.Name = "character" + p.ToString();
            characterBox.Visible = true;
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
            firstConditionBox.Items.AddRange(avaibleConditions);
            firstConditionBox.Name = totalRows.ToString();
            firstConditionBox.Size = new System.Drawing.Size(105, 21);
            firstConditionBox.TabIndex = 10;
            firstConditionBox.Visible = true;
            firstConditionBox.SelectedIndexChanged += FirstConditionBox_SelectedIndexChanged;

            deleteRowButton = new Button();
            ConditionPanel.Controls.Add(deleteRowButton);
            deleteRowButton.Location = new Point(pointX + 223, pointY);
            deleteRowButton.Name = "delete" + totalRows.ToString();
            deleteRowButton.Size = new System.Drawing.Size(94, 23);
            deleteRowButton.Text = "delete this Row";
            deleteRowButton.Click += deleteRowButton_Click;

            NewConditionButton.Location = new System.Drawing.Point(pointX, pointY + 30);

            totalRows++;
        }

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            try
            {
                throw new NotImplementedException();
            }
            catch
            {
                MessageBox.Show(e.ToString());
            }
        }

               
        
    }
}
