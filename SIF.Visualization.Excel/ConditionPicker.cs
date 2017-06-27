using System;
using SIF.Visualization.Excel.Core.Rules;
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
    public partial class ConditionPicker : Form
    {
        string chosenType;
        SIF.Visualization.Excel.Core.Rules.Rule rule;
        public ConditionPicker(SIF.Visualization.Excel.Core.Rules.Rule rule)
        {
            this.rule = rule;
            InitializeComponent();
            ShowDialog();
        }

        public ConditionPicker(Condition condition)
        {
            InitializeComponent();
            ConfigurePicker(condition);            
            ShowDialog();

        }

        private void ConfigurePicker(Condition condition)
        {
            HideFirstBoxes();
            ResetColourScheme();
            switch (condition.Type)
            {
                case Condition.ConditionType.Regex:
                    ChooseRegexButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
                    ConditionNameTextBox.Text = condition.Name;
                    RegexTextBox.Text = condition.Value;
                    RegexTextBox.Visible = true;
                    break;
                case Condition.ConditionType.CharacterCount:

                    break;
                default:
                    //Meldung
                    break;
            }
        }

        private void ChooseRegex_Click(object sender, EventArgs e)
        {
            this.ChooseRegexButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ConditionFirstPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ConditionSecondPanelLabel.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_ConditionPicker_ChooseRegex;
            HideFirstBoxes();
            this.RegexTextBox.Visible = true;
            chosenType = "Regex";
        }

        private void ChooseCharacterCountButton_Click(object sender, EventArgs e)
        {
            this.ChooseCharacterCountButton.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ConditionFirstPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ConditionSecondPanelLabel.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_ConditionPicker_ChooseCharacterCount;
            HideFirstBoxes();
            this.CharacterCountTextBox.Visible = true;
            chosenType = "CharacterCount";
        }

        private void HideFirstBoxes()
        {
            this.RegexTextBox.Visible = false;
            this.CharacterCountTextBox.Visible = false;
        }
        /// <summary>
        /// TODO: Resets BUtton and Panel highlighting
        /// </summary>
        private void ResetColourScheme()
        {
            
        }

        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                switch (chosenType)
                {
                    case "Regex":
                        //Checken
                        RuleCreator.Instance.AddRegexCondition(ConditionNameTextBox.Text, RegexTextBox.Text);
                        break;
                    case "CharacterCount":
                        //Checken
                        RuleCreator.Instance.AddCharacterCondition(ConditionNameTextBox.Text, CharacterCountTextBox.Text);
                        break;
                }
                
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
            RuleEditor.Instance.Open(rule);
            Close();
            
            
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            try
            {
                Dispose();
                RuleEditor.Instance.Open(RuleCreator.Instance.GetRule());
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
                       
        }


    }
}
