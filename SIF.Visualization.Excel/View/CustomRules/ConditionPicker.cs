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
        private readonly Condition _currentCondition;
        private readonly Rule _rule;
        private string _chosenType = "none";
        
        public ConditionPicker(Rule rule)
        {
            this._rule = rule;
            InitializeComponent();
            SetText();
            ShowDialog();
        }


        public ConditionPicker(Condition condition, Rule rule)
        {
            this._rule = rule;
            _currentCondition = condition;
            InitializeComponent();
            SetText();
            ConfigurePicker(condition);
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
                if (_rule.Conditions.Count != 0)
                {
                    var count = _rule.Conditions.Count() + 1;
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
            ConditionNameTextBox.Text = condition.Name;
            switch (condition.Type)
            {
                case Condition.ConditionType.Regex:
                    ChooseRegexButton.BackColor = SystemColors.GradientActiveCaption;
                    ConditionNameTextBox.Text = condition.Name;
                    RegexTextBox.Text = condition.Value;
                    RegexTextBox.Visible = true;
                    _chosenType = "Regex";
                    break;
                case Condition.ConditionType.CharacterCount:
                    ChooseCharacterCountButton.BackColor = SystemColors.GradientActiveCaption;
                    ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
                    ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseCharacterCount1 + " \n" +
                                                     Resources.tl_ConditionPicker_ChooseCharacterCount2;
                    CharacterCountTextBox.Visible = true;
                    _chosenType = "CharacterCount";
                    break;
                case Condition.ConditionType.OnlyNumbers:
                    ChooseOnlyNumbersButton.BackColor = SystemColors.GradientActiveCaption;
                    ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
                    ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseOnlyNumbers + " \n" +
                                                     Resources.tl_ConditionPicker_NoInputReq;
                    _chosenType = "OnlyNumbers";
                    break;
                case Condition.ConditionType.Empty:
                    ChooseEmptyButton.BackColor = SystemColors.GradientActiveCaption;
                    ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
                    ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseEmpty + " \n" +
                                                     Resources.tl_ConditionPicker_NoInputReq;
                    _chosenType = "Empty";
                    break;
                default:
                    MessageBox.Show(Resources.tl_ConditionPicker_Error);
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
            _chosenType = "Regex";
            ConfirmButton.Enabled = true;
        }

        private void ChooseEmptyButton_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            ChooseEmptyButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseEmpty + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            _chosenType = "Empty";
            ConfirmButton.Enabled = true;
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
            _chosenType = "CharacterCount";
            ConfirmButton.Enabled = true;
        }

        private void ChooseOnlyNumbers_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            ChooseOnlyNumbersButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_ChooseOnlyNumbers + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            _chosenType = "OnlyNumbers";
            ConfirmButton.Enabled = true;
        }

        private void Choose1CommaButton_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            Choose1CommaButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_Choose1Comma + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            _chosenType = "1Comma";
            ConfirmButton.Enabled = true;
        }

        private void Choose2CommaButton_Click(object sender, EventArgs e)
        {
            HideFirstBoxes();
            ResetColourScheme();
            Choose2CommaButton.BackColor = SystemColors.GradientActiveCaption;
            ConditionFirstPanel.BackColor = SystemColors.GradientActiveCaption;
            ConditionSecondPanelLabel.Text = Resources.tl_ConditionPicker_Choose2Comma + " \n" +
                                             Resources.tl_ConditionPicker_NoInputReq;
            _chosenType = "2Comma";
            ConfirmButton.Enabled = true;
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
        ///     TODO: Resets Button and Panel highlighting
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

        private void ContentChanged(object sender, EventArgs e)
        {
            ConfirmButton.Enabled = true;
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
                {
                    switch (_chosenType)
                    {
                        case "Regex":
                            if (RegexTextBox.Text == "")
                            {
                                MessageBox.Show(Resources.tl_ConditionPicker_NoRegex);
                                break;
                            }
                            RuleCreator.Instance.AddRegexCondition(ConditionNameTextBox.Text, RegexTextBox.Text);
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
                            Close();
                            break;
                        case "Empty":
                            RuleCreator.Instance.AddEmptyCondition(ConditionNameTextBox.Text);
                            Close();
                            break;
                        case "OnlyNumbers":
                            RuleCreator.Instance.AddOnlyNumbersCondition(ConditionNameTextBox.Text);
                            Close();
                            break;
                        case "1Comma":
                            RuleCreator.Instance.AddRegexCondition(ConditionNameTextBox.Text,
                                "((^|\\W)([0-9]+?((,|\\.)[0-9])+?)($|\\W))|((^)\\d*($|\\W))");
                            Close();
                            break;
                        case "2Comma":
                            RuleCreator.Instance.AddRegexCondition(ConditionNameTextBox.Text,
                                "((^|\\W)([0-9]+?((,|\\.)([0-9]{1,2}))+?)($|\\W))|((^)\\d*($|\\W))");
                            Close();
                            break;
                        default:
                            MessageBox.Show(Resources.tl_ConditionPicker_NoCondition);
                            break;
                    }
                    RuleCreator.Instance.GetRule().Conditions.Remove(_currentCondition);
                    RuleEditor.Instance.Open(_rule);
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
            if (_chosenType == "none")
            {
                MessageBox.Show(Resources.tl_ConditionPicker_NoCondition);
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