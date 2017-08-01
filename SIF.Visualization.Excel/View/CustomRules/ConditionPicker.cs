using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core.Rules;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.View.CustomRules
{
    public partial class ConditionPicker : Form
    {
        private string chosenType;
        private readonly Rule rule;

        public ConditionPicker(Rule rule)
        {
            this.rule = rule;
            InitializeComponent();
            SetText();
            ShowDialog();
        }


        public ConditionPicker(Condition condition)
        {
            InitializeComponent();
            ConfigurePicker(condition);
            SetText();
            ShowDialog();
        }

        /// <summary>
        ///     Sets Localisation
        /// </summary>
        private void SetText()
        {
            //Buttons
            ConfirmButton.Text = Resources.tl_ConditionPicker_Confirm;
            ChooseEmptyButton.Text = Resources.tl_ConditionPicker_Empty;
            CancelButton.Text = Resources.tl_Cancel;
            ChooseCharacterCountButton.Text = Resources.tl_RuleEditor_Condition_CharacterCount;
            ChooseOnlyNumbersButton.Text = Resources.tl_RuleEditor_Condition_OnlyNumbers;
            Choose1CommaButton.Text = Resources.tl_RuleEditor_Condition_1Comma;
            Choose2CommaButton.Text = Resources.tl_RuleEditor_Condition_2Comma;
            //Labels
            ConditionNameLabel.Text = Resources.tl_ConditionPicker_ConditionName;
            ConditionFirstPanelLabel.Text = Resources.tl_ConditionPicker_ChooseConditionType;
            //default name
            //Check if neu oder editieren
            try
            {
                if (rule.Conditions.Count != 0)
                {
                    var count = rule.Conditions.Count() + 1;
                    ConditionNameTextBox.Text = "unbenannte Bedingung " + count;
                }
            }
            catch
            {
                // no condititions
            }
        }

        /// <summary>
        ///     Resets the Layout and desplays the current Condition
        /// </summary>
        /// <param name="condition"></param>
        private void ConfigurePicker(Condition condition)
        {
            HideFirstBoxes();
            ResetColourScheme();
            switch (condition.Type)
            {
                case Condition.ConditionType.Regex:
                    ChooseRegexButton.BackColor = SystemColors.GradientActiveCaption;
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
            HideFirstBoxes();
            ResetColourScheme();
            ChooseRegexButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseRegex;
            RegexTextBox.Visible = true;
            chosenType = "Regex";
        }

        private void ChooseEmptyButton_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            ChooseEmptyButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseEmpty + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            chosenType = "Empty";
        }

        private void ChooseCharacterCountButton_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            ChooseCharacterCountButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseCharacterCount1 + " \n" +
                                             Resources.tl_ConditionPicker_ChooseCharacterCount2;
            CharacterCountTextBox.Visible = true;
            chosenType = "CharacterCount";
        }

        private void ChooseOnlyNumbers_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            ChooseOnlyNumbersButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseOnlyNumbers + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            chosenType = "OnlyNumbers";
        }

        private void Choose1CommaButton_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            Choose1CommaButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_Choose1Comma + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            chosenType = "1Comma";
        }

        private void Choose2CommaButton_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            Choose2CommaButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_Choose2Comma + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            chosenType = "2Comma";
        }

        /// <summary>
        ///     Hides unused extra Boxes
        /// </summary>
        private void HideFirstBoxes()
        {
            RegexTextBox.Visible = false;
            CharacterCountTextBox.Visible = false;
        }

        /// <summary>
        ///     TODO: Resets BUtton and Panel highlighting
        /// </summary>
        private void ResetColourScheme()
        {
            //Panels
            ConditionFirstPanel.BackColor = SystemColors.Control;
            ConditionSecondPanel.BackColor = SystemColors.Control;
            //Buttons
            ChooseRegexButton.BackColor = SystemColors.Control;
            ChooseCharacterCountButton.BackColor = SystemColors.Control;
            ChooseEmptyButton.BackColor = SystemColors.Control;
            ChooseOnlyNumbersButton.BackColor = SystemColors.Control;
            Choose1CommaButton.BackColor = SystemColors.Control;
            Choose2CommaButton.BackColor = SystemColors.Control;
        }

        /// <summary>
        ///     Checks and Adds ConditionType and optional User Input to the Rule
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (CheckConditions())
                    switch (chosenType)
                    {
                        case "Regex":
                            if (RegexTextBox.Text == "")
                            {
                                MessageBox.Show(Resources.tl_ConditionPicker_NoRegex);
                                break;
                            }
                            RuleCreator.Instance.AddRegexCondition(ConditionNameTextBox.Text, RegexTextBox.Text);
                            RuleEditor.Instance.Open(rule);
                            Close();
                            break;
                        case "CharacterCount":
                            int value;
                            if (CharacterCountTextBox.Text == "")
                            {
                                MessageBox.Show(Resources.tl_ConditionPicker_NoCharCount);
                                break;
                            }
                            if (!int.TryParse(CharacterCountTextBox.Text, out value))
                            {
                                MessageBox.Show(Resources.tl_ConditionPicker_NoCharCount);
                                break;
                            }
                            RuleCreator.Instance.AddCharacterCondition(ConditionNameTextBox.Text,
                                CharacterCountTextBox.Text);
                            RuleEditor.Instance.Open(rule);
                            Close();
                            break;
                        case "Empty":
                            RuleCreator.Instance.AddEmptyCondition(ConditionNameTextBox.Text);
                            RuleEditor.Instance.Open(rule);
                            Close();
                            break;
                        case "OnlyNumbers":
                            RuleCreator.Instance.AddOnlyNumbersCondition(ConditionNameTextBox.Text);
                            RuleEditor.Instance.Open(rule);
                            Close();
                            break;
                        case "1Comma":
                            RuleCreator.Instance.AddRegexCondition(ConditionNameTextBox.Text,
                                "((^|\\W)([0-9]+?((,|\\.)[0-9])+?)($|\\W))|((^)\\d*($|\\W))");
                            RuleEditor.Instance.Open(rule);
                            Close();
                            break;
                        case "2Comma":
                            RuleCreator.Instance.AddRegexCondition(ConditionNameTextBox.Text,
                                "((^|\\W)([0-9]+?((,|\\.)([0-9]{1,2}))+?)($|\\W))|((^)\\d*($|\\W))");
                            RuleEditor.Instance.Open(rule);
                            Close();
                            break;
                        default:
                            MessageBox.Show(Resources.tl_ConditionPicker_NoCondition);
                            break;
                    }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        /// <summary>
        ///     Checks for empty Name
        /// </summary>
        /// <returns></returns>
        private bool CheckConditions()
        {
            if (ConditionNameTextBox.Text == "")
            {
                MessageBox.Show(Resources.tl_ConditionPicker_NoName);
                return false;
            }
            return true;
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