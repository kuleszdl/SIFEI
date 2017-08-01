using System;
using System.Collections.Generic;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Rules;

namespace SIF.Visualization.Excel.View.CustomRules
{
    public partial class RuleEditor : Form
    {
        private int pointX;
        private int pointY;
        private int panelX = 10;
        private int panelY = 3;
        private int totalRows = 0;

        public Boolean edited = false;
        private Button deleteRowButton;
        private List<Panel> condiPanels = new List<Panel>();
        private string[] avaibleConditions = {  
                                  "Regex", 
                                  global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_Condition_CharacterCount
                              };

        private static volatile RuleEditor instance;
        private static object syncRoot = new Object();
        private static Object syncEditor = new Object();

        /// <summary>
        /// Singelton
        /// </summary>
        public static RuleEditor Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RuleEditor();
                    }
                }
                return instance;
            }
        }
        
        
        public RuleEditor()
        {
            
        }

        /// <summary>
        /// Calls the Rule Editor Interface 
        /// </summary>
        public void Start() {
            lock (syncEditor)
            {
                InitializeComponent();
                SetDesigner();
                RuleCreator.Instance.BlankStart();
                Show();            
            }            
        }
        
        /// <summary>
        /// Shows the Rule Editor Interface with an existing rule
        /// </summary>
        /// <param name="rule"></param>

        public void Open(SIF.Visualization.Excel.Core.Rules.Rule rule)
        {
            lock (syncEditor)
            {
                InitializeComponent();
                foreach (Condition existingCondition in rule.Conditions)
                {
                    AddExistingRow(existingCondition);
                }
                SetDesigner();
                UpdateInformations(rule);
                
                RuleCreator.Instance.OpenRule(rule);
                Show();
            }
        }
        /// <summary>
        /// Sets the text and Tooltips to localized string, because Visual studio keeps resetting them
        /// </summary>
        private void SetDesigner()
        {
            //Buttons
            this.CancelButton.Text = Properties.Resources.tl_Cancel;
            this.ConfirmButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_Confirm;
            this.ChooseCellButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_CellPicker;
            this.NewConditionButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_NewCondition;
            //Labels
            this.ConditionLabel.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_Condition;
            this.RuleNameLabel.Text = "Name";
            this.RuleAreaLabel.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_RuleArea;
            this.DescriptionLabel.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_RuleDescription;
            //Tooltips
            this.ToolTipName.SetToolTip(this.TooltipLabelName, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_Name);
            this.ToolTipCellArea.SetToolTip(this.ToolTipLabelCellArea, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_CellArea);
            this.ToolTipDescription.SetToolTip(this.ToolTipLabelDescription, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_Description);
            this.ToolTipCondition.SetToolTip(this.TooltipLabelCondition, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_Condition);
        }
        /// <summary>
        /// Updates the GUI with the details of the rule
        /// </summary>
        /// <param name="rule"></param>
        public void UpdateInformations(SIF.Visualization.Excel.Core.Rules.Rule rule)
        {
            if (RuleNameTextBox.Text == "" )
                RuleNameTextBox.Text = rule.Title;
            if (RuleDescriptionTextBox.Text == "")
                RuleDescriptionTextBox.Text = rule.Description;
            if (rule.RuleCells != null)
            {
                string output = "";
                foreach (RuleCells rulecells in rule.RuleCells)
                {
                    output = output + rulecells.Target.ToString();                    
                }
                this.CellAreaBox.Text = output;
            }            
        }


        /// <summary>
        /// Opens the ConditionPicker Window and saves the current Inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewConditionButton_Click(object sender, EventArgs e)
        {
            try
            {
                RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
                End();
                ConditionPicker conditionPicker = new ConditionPicker(RuleCreator.Instance.GetRule());
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }            
        }

        
        /// <summary>
        /// Adds existing Conditions to the ConditionPanel
        /// </summary>
        /// <param name="existingCondition"></param>
        private void AddExistingRow(Condition existingCondition)
        {
            pointX = NewConditionButton.Location.X;
            pointY = NewConditionButton.Location.Y;
            totalRows = condiPanels.Count;

            // Creates Panel where everything is contained
            Panel condiPanel = new Panel();
            ConditionPanel.Controls.Add(condiPanel);
            condiPanels.Add(condiPanel);
            condiPanel.Location = new System.Drawing.Point(panelX, panelY);
            condiPanel.Name = "panel" + totalRows.ToString();
            condiPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            condiPanel.Size = new System.Drawing.Size(570, 50);
            condiPanel.BackColor = System.Drawing.SystemColors.ControlDark;
            condiPanel.Padding = new System.Windows.Forms.Padding(10);
            panelY = panelY + 55;

            Button condiButton = new Button();
            condiPanel.Controls.Add(condiButton);
            condiButton.Text = existingCondition.Name;
            condiButton.Size = new System.Drawing.Size(150, 30);
            condiButton.AutoSize = true;
            condiButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Right)));
            condiButton.Location = new System.Drawing.Point(10, 10);
            condiButton.Click += condiButton_Click;
            // condition bearbeiten Button Event, param: condition


            // Creates the delete Button for the Panel
            deleteRowButton = new Button();
            condiPanel.Controls.Add(deleteRowButton);
            deleteRowButton.Margin = new System.Windows.Forms.Padding(10);
            deleteRowButton.Location = new System.Drawing.Point(500, 10);
            deleteRowButton.Name = "delete" + totalRows.ToString();
            deleteRowButton.Size = new System.Drawing.Size(30, 30);
            deleteRowButton.Image = global::SIF.Visualization.Excel.Properties.Resources.delete;
            deleteRowButton.Click += deleteRowButton_Click;

            // Moves down newConditionButton 
            NewConditionButton.Location = new System.Drawing.Point(pointX, pointY + 55);
        }

        /// <summary>
        /// Opens existing Condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void condiButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;
            foreach (Condition condition in RuleCreator.Instance.GetRule().Conditions)
            {
                if (condition.Name == button.Text)
                {
                    ConditionPicker conditionPicker = new ConditionPicker(condition);
                }
            }
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
        /// Checks and commits the Data with the RuleCreator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            // Check for Rulecells
            if (CheckInputs())
            {
                RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
                
                var newRule = RuleCreator.Instance.End();
                if (newRule != null)
                    {
                        DataModel.Instance.CurrentWorkbook.Rules.Add(newRule);
                        End();
                    }
                else
                {
                    MessageBox.Show("keine RuleCells");
                }                
            }            
        }

        /// <summary>
        /// Checks the Rule Editor for empty or invalid inputs
        /// </summary>
        /// <returns></returns>
        private bool CheckInputs()
        {
            if(RuleCreator.Instance.GetRule().Conditions.Count <= 0) {
                MessageBox.Show(global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_NoCondition);
                return false; 
            }

            if (RuleNameTextBox.Text == "") {
                    MessageBox.Show(global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_NoName);
                    return false;
                }
            if (RuleCreator.Instance.GetRule().RuleCells.Count <= 0)
            {
                MessageBox.Show(global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_NoRuleCells);
                return false;
            }
            if (RuleDescriptionTextBox.Text == "")
            {
                MessageBox.Show(global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_NoDescription);
                return true;
            }
            return true;
            
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            //check for edited
            RuleCreator.Instance.End();
            End();
        }
        /// <summary>
        /// Opens the CellChooser Window and saves current Input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseCellButton_Click(object sender, EventArgs e)
        {
            RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
            End();
            CellPickerWF cellpicker = new CellPickerWF();            
        }

        public void End()
        {
            if (instance == null) {

            }
            lock (syncEditor)
            {
                instance = null;
                Close();
                Dispose();
            }            
        }
    }
}