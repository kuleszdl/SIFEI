﻿using System;
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
        private Condition[] array;
        private List<Condition> Conditions;
        string[] avaibleConditions = { 
                                  "Regex", 
                                  global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_Condition_CharacterCount
                              };
        

        public RuleEditor()
        {
            InitializeComponent();
            Show();
            
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
           
            // Creates a new Panel for the new Condition
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
            
            // Creates the main Condition Box
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
//            firstConditionBox.Size = new System.Drawing.Size(105, 21);
            firstConditionBox.TabIndex = 10;
            firstConditionBox.Visible = true;
            firstConditionBox.SelectedIndexChanged += FirstConditionBox_SelectedIndexChanged;

            // Creates the delete Button for the Panel
            deleteRowButton = new Button();
            condiPanel.Controls.Add(deleteRowButton);
            deleteRowButton.Margin = new System.Windows.Forms.Padding(10);
            deleteRowButton.Location = new System.Drawing.Point(500 ,5);
            deleteRowButton.Name = "delete" + totalRows.ToString();
            deleteRowButton.Size = new System.Drawing.Size(30, 23);
            deleteRowButton.Image = global::SIF.Visualization.Excel.Properties.Resources.delete;
            deleteRowButton.Click += deleteRowButton_Click;

            // Moves down newConditionButton 
            NewConditionButton.Location = new System.Drawing.Point(pointX, pointY + 35);

            
        }

        /// <summary>
        /// Gets the panel where the Events was triggered and deletes its content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
             
        /// <summary>
        /// Creates the next Controls, depending on the Selection of the main Box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    regexBox.Location = new System.Drawing.Point(130, 5); //Hardcoded, eventuell ändern
                    regexBox.Text = "insert Regex";
                    regexBox.Name = "regex" + currentRow.ToString();
                    regexBox.Margin = new System.Windows.Forms.Padding(10);
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
                    characterBox.Location = new Point(130, 5); //Hardcoded, eventuell ändern
                    characterBox.Text = "insert maximum Character Count";
                    characterBox.Name = "character" + currentRow.ToString();
                    characterBox.Visible = true;
                }
                
            }
            
        }

        
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            string ruleTitle = RuleNameTextBox.Text;
            
            // Check for Rulecells
            // Startet den Rule Creator 
            RuleCreator.Instance.Start(DataModel.Instance.CurrentWorkbook, RuleNameTextBox.Text, ruleTitle);

            for (int i = 0; i < condiPanels.Count; i++)
            {
                CheckConditions(i.ToString());
            }

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


        private void CheckConditions(String checkBoxName)
        {
            foreach (Control panel in this.ConditionPanel.Controls)
            {
                if (panel.Name == "panel" + checkBoxName)
                {
                    foreach (Control box in panel.Controls)
                    {
                        switch (box.Text)
                        {
                            case "Regex":
                                foreach (Control textBoxControl in panel.Controls)
                                {
                                    if (textBoxControl.Name == "regex" + checkBoxName)
                                    {
                                        RuleCreator.Instance.AddRegexCondition(textBoxControl.Text);
                                        // Condition anfügen MessageBox.Show(conditionValue);
                                    }
                                }

                                break;
                            case "Character Count":
                            case "Gesamtanzahl Zeichen":
                                foreach (Control textBoxControl in panel.Controls)
                                {
                                    if (textBoxControl.Name == "character" + checkBoxName)
                                    {
                                        RuleCreator.Instance.AddCharacterCondition(textBoxControl.Text);
                                        // Condition anfügen MessageBox.Show(conditionValue);
                                    }
                                }
                                break;
                        }
                    }
                    
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
            //GetConditions();
        }

       

        

        

               
        
    }
}
