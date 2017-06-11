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
    public partial class RuleEditor : Form
    {
        Microsoft.Office.Interop.Excel.Worksheet ws;
        int pointX;
        int pointY;
        int panelX = 128;
        int panelY = 3;
        int totalRows = 0;

        private ComboBox firstConditionBox;
        private TextBox regexBox;
        private TextBox characterBox;
        private Button deleteRowButton;
        private List<Panel> condiPanels = new List<Panel>();
        string[] avaibleConditions = { 
                                  "Regex", 
                                  "Character Count"
                              };
        

        public RuleEditor()
        {
            InitializeComponent();
            ShowDialog();
            
        }

        public RuleEditor(System.Data.Rule rule)
        {
            // TODO: Anzeige einer vorhandenen Regel
            InitializeComponent();
            ShowDialog();
        }


        private void NewConditionButton_Click(object sender, EventArgs e)
        {
            try
            {
                AddNewRow();
                                
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        public void AddNewRow()
        {
            pointX = NewConditionButton.Location.X;
            pointY = NewConditionButton.Location.Y;
            totalRows = condiPanels.Count;
           
            Panel condiPanel = new Panel();
            ConditionPanel.Controls.Add(condiPanel);
            condiPanels.Add(condiPanel);
            condiPanel.Location = new System.Drawing.Point(panelX, panelY);
            condiPanel.Name = "panel"+totalRows.ToString();
            condiPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            condiPanel.Size = new System.Drawing.Size(570, 30);
            condiPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            condiPanel.Padding = new System.Windows.Forms.Padding(10);
            panelY = panelY + 35;
            
            firstConditionBox = new ComboBox();
            condiPanel.Controls.Add(firstConditionBox);
            firstConditionBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            firstConditionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)));
            firstConditionBox.Location = new System.Drawing.Point(5,5);
            firstConditionBox.Margin = new System.Windows.Forms.Padding(10);
            firstConditionBox.FormattingEnabled = true;
            firstConditionBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            firstConditionBox.Items.AddRange(avaibleConditions);
            firstConditionBox.Name = totalRows.ToString();
            firstConditionBox.Size = new System.Drawing.Size(105, 21);
            firstConditionBox.TabIndex = 10;
            firstConditionBox.Visible = true;
            firstConditionBox.SelectedIndexChanged += FirstConditionBox_SelectedIndexChanged;

            deleteRowButton = new Button();
            condiPanel.Controls.Add(deleteRowButton);
            deleteRowButton.Location = new System.Drawing.Point(500 ,5);
            deleteRowButton.Name = "delete" + totalRows.ToString();
            deleteRowButton.Size = new System.Drawing.Size(30, 23);
            deleteRowButton.Image = global::SIF.Visualization.Excel.Properties.Resources.delete;
            deleteRowButton.Click += deleteRowButton_Click;

            NewConditionButton.Location = new System.Drawing.Point(pointX, pointY + 35);

            totalRows++;
        }

        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            try
            {
                var button= sender as Button;
                var parent = button.Parent as Panel;
                parent.Dispose();
            }
            catch
            {
                MessageBox.Show(e.ToString());
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
                        AddRegexBox(Int32.Parse(currentRow));        
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

        private void AddRegexBox(int currentRow)
        {
            foreach (Panel panel in condiPanels)
            {
                if (panel.Name == "panel" + currentRow)
                {
                    regexBox = new TextBox();
                    panel.Controls.Add(regexBox);
                    regexBox.Location = new System.Drawing.Point(115, 5); //Hardcoded, eventuell ändern
                    regexBox.Text = "insert Regex";
                    regexBox.Name = "regex" + currentRow.ToString();
                    regexBox.Visible = true;
                }
                
            }
            
        }

        private void AddCharacterBox(int currentRow)
        {
            foreach (Panel panel in condiPanels)
            {
                if (panel.Name == "panel" + currentRow)
                {
                    characterBox = new TextBox();
                    panel.Controls.Add(characterBox);
                    characterBox.Location = new Point(115, 5); //Hardcoded, eventuell ändern
                    characterBox.Text = "insert maximum Character Count";
                    characterBox.Name = "character" + currentRow.ToString();
                    characterBox.Visible = true;
                }
                
            }
            
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
            
            for (int i = 0; i < totalRows ; i++)
            {
                CheckConditions(i.ToString());
            }
        }

        private void CheckConditions(String checkBoxName)
        {
            foreach (Control control in this.ConditionPanel.Controls)
            {
                switch (control.Text) {
                    case "Regex":
                        foreach (Control textBoxControl in this.ConditionPanel.Controls) {
                            if (textBoxControl.Name == "regex"+checkBoxName)
                            {
                                String conditionValue = textBoxControl.Text;
                                MessageBox.Show(conditionValue); 
                            }
                        }
                        
                        break;
                    case "Character Count":
                        break;

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
            //CellPickerWF cellpicker = new CellPickerWF();
            GetConditions();
        }

       

        

        

               
        
    }
}
