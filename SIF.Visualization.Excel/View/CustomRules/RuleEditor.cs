using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Rules;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.View.CustomRules
{
    public partial class RuleEditor : Form
    {
        private static volatile RuleEditor _instance;
        private static readonly object SyncRoot = new object();
        private static readonly object SyncEditor = new object();
        private readonly List<Panel> _condiPanels = new List<Panel>();

        private readonly int panelX = 10;
        private Button _deleteRowButton;
        private int _panelY = 3;
        private int _pointX;
        private int _pointY;
        private int _totalRows;


        /// <summary>
        ///     Singelton
        /// </summary>
        public static RuleEditor Instance
        {
            get
            {
                if (_instance == null)
                    lock (SyncRoot)
                    {
                        if (_instance == null)
                            _instance = new RuleEditor();
                    }
                return _instance;
            }
        }

        /// <summary>
        ///     Calls the Rule Editor Interface
        /// </summary>
        public void Start()
        {
            lock (SyncEditor)
            {
                InitializeComponent();
                SetDesigner();
                RuleCreator.Instance.BlankStart();
                Show();
            }
        }

        /// <summary>
        ///     Shows the Rule Editor Interface with an existing rule
        /// </summary>
        /// <param name="rule"></param>
        public void Open(Rule rule)
        {
            lock (SyncEditor)
            {
                InitializeComponent();
                SetDesigner();
                UpdateInformations(rule);
                RuleCreator.Instance.OpenRule(rule);
                Show();
            }
        }

        /// <summary>
        ///     Sets the text and Tooltips to localized string, because Visual studio keeps resetting them
        /// </summary>
        private void SetDesigner()
        {
            //Buttons
            CancelButton.Text = Resources.tl_Cancel;
            ConfirmButton.Text = Resources.tl_RuleEditor_Confirm;
            ChooseCellButton.Text = Resources.tl_RuleEditor_CellPicker;
            NewConditionButton.Text = Resources.tl_RuleEditor_NewCondition;
            NewConditionButton.Location = new Point(13, 13);
            //Labels
            ConditionLabel.Text = Resources.tl_RuleEditor_Condition;
            RuleNameLabel.Text = "Name";
            RuleAreaLabel.Text = Resources.tl_RuleEditor_RuleArea;
            DescriptionLabel.Text = Resources.tl_RuleEditor_RuleDescription;
            //Tooltips
            ToolTipName.SetToolTip(TooltipLabelName, Resources.tl_RuleEditor_ToolTip_Name);
            ToolTipCellArea.SetToolTip(ToolTipLabelCellArea, Resources.tl_RuleEditor_ToolTip_CellArea);
            ToolTipDescription.SetToolTip(ToolTipLabelDescription, Resources.tl_RuleEditor_ToolTip_Description);
            ToolTipCondition.SetToolTip(TooltipLabelCondition, Resources.tl_RuleEditor_ToolTip_Condition);
        }

        /// <summary>
        ///     Updates the GUI with the details of the rule
        /// </summary>
        /// <param name="rule"></param>
        public void UpdateInformations(Rule rule)
        {
            if (RuleNameTextBox.Text == "")
                RuleNameTextBox.Text = rule.Title;
            if (RuleDescriptionTextBox.Text == "")
                RuleDescriptionTextBox.Text = rule.Description;
            if (rule.RuleCells != null)
            {
                var output = "";
                foreach (var rulecells in rule.RuleCells)
                    output = output + rulecells.Target;
                CellAreaBox.Text = output;
            }
            foreach (var existingCondition in rule.Conditions)
                AddExistingRow(existingCondition);
        }

        private void ContentChanged(object sender, EventArgs e)
        {
            RuleCreator.Instance.edited = true;
            ConfirmButton.Enabled = true;
        }

        private void ContentChanged()
        {
            RuleCreator.Instance.edited = true;
            ConfirmButton.Enabled = true;
        }

        /// <summary>
        ///     Opens the ConditionPicker Window and saves the current Inputs
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewConditionButton_Click(object sender, EventArgs e)
        {
            try
            {
                ContentChanged();
                RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
                End();
                var conditionPicker = new ConditionPicker(RuleCreator.Instance.GetRule());
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }


        /// <summary>
        ///     Adds existing Conditions to the ConditionPanel
        /// </summary>
        /// <param name="existingCondition"></param>
        private void AddExistingRow(Condition existingCondition)
        {
            _pointX = NewConditionButton.Location.X;
            _pointY = NewConditionButton.Location.Y;
            _totalRows = _condiPanels.Count;

            // Creates Panel where everything is contained
            var condiPanel = new Panel();
            ConditionPanel.Controls.Add(condiPanel);
            _condiPanels.Add(condiPanel);
            condiPanel.Location = new Point(panelX, _panelY);
            condiPanel.Name = "panel" + _totalRows;
            condiPanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            condiPanel.Size = new Size(570, 50);
            condiPanel.BackColor = SystemColors.ControlDark;
            condiPanel.Padding = new Padding(10);
            _panelY = _panelY + 55;

            // Creates Button for editing the Condition with ConditionPicker
            var condiButton = new Button();
            condiPanel.Controls.Add(condiButton);
            condiButton.Text = existingCondition.Name;
            condiButton.Size = new Size(150, 30);
            condiButton.AutoSize = true;
            condiButton.Anchor = AnchorStyles.Right;
            condiButton.Location = new Point(10, 10);
            condiButton.Click += condiButton_Click;
            // condition bearbeiten Button Event, param: condition


            // Creates the delete Button for the Panel
            _deleteRowButton = new Button();
            condiPanel.Controls.Add(_deleteRowButton);
            _deleteRowButton.Margin = new Padding(10);
            _deleteRowButton.Location = new Point(500, 10);
            _deleteRowButton.Name = existingCondition.Name;
            _deleteRowButton.Size = new Size(30, 30);
            _deleteRowButton.Image = Resources.delete;
            _deleteRowButton.Click += deleteRowButton_Click;

            // Moves down newConditionButton 
            NewConditionButton.Location = new Point(_pointX, _pointY + 55);
        }

        /// <summary>
        ///     Opens existing Condition
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void condiButton_Click(object sender, EventArgs e)
        {
            try
            {
                ContentChanged();
                var button = sender as Button;
                foreach (var condition in RuleCreator.Instance.GetRule().Conditions)
                    if (condition.Name == button.Text)
                    {
                        ContentChanged();
                        RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
                        End();
                        var conditionPicker = new ConditionPicker(condition, RuleCreator.Instance.GetRule());
                        break;
                    }
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        /// <summary>
        ///     Gets the panel where the Events was triggered and deletes its content
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void deleteRowButton_Click(object sender, EventArgs e)
        {
            var button = sender as Button;

            try
            {
                foreach (var condition in RuleCreator.Instance.GetRule().Conditions)
                    if (condition.Name == button.Name)
                    {
                        ContentChanged();
                        RuleCreator.Instance.GetRule().Conditions.Remove(condition);
                        break;
                    }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }

            RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
            End();
            Instance.Open(RuleCreator.Instance.GetRule());
        }


        /// <summary>
        ///     Checks and commits the Data with the RuleCreator
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmButton_Click(object sender, EventArgs e)
        {
            if (CheckInputs())
            {
                RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
                RuleCreator.Instance.edited = true;
                var newRule = RuleCreator.Instance.End();
                if (newRule != null)
                {
                    DataModel.Instance.CurrentWorkbook.Rules.Add(newRule);

                    End();
                }
                else
                {
                    MessageBox.Show("Es existiert keine Regel zum Erstellen.");
                }
            }
        }

        /// <summary>
        ///     Checks the Rule Editor for empty or invalid inputs
        /// </summary>
        /// <returns></returns>
        private bool CheckInputs()
        {
            if (RuleCreator.Instance.GetRule().Conditions.Count <= 0)
            {
                MessageBox.Show(Resources.tl_RuleEditor_NoCondition);
                return false;
            }

            if (RuleNameTextBox.Text == "")
            {
                MessageBox.Show(Resources.tl_RuleEditor_NoName);
                return false;
            }
            if (RuleCreator.Instance.GetRule().RuleCells.Count <= 0)
            {
                MessageBox.Show(Resources.tl_RuleEditor_NoRuleCells);
                return false;
            }
            if (RuleDescriptionTextBox.Text == "")
            {
                MessageBox.Show(Resources.tl_RuleEditor_NoDescription);
                return true;
            }
            return true;
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            RuleCreator.Instance.edited = false;
            RuleCreator.Instance.End();
            End();
        }

        /// <summary>
        ///     Opens the CellChooser Window and saves current Input
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ChooseCellButton_Click(object sender, EventArgs e)
        {
            RuleCreator.Instance.SetProperties(RuleNameTextBox.Text, RuleDescriptionTextBox.Text);
            End();
            var cellpicker = new CellPickerWF();
        }

        public void End()
        {
            if (_instance == null)
            {
            }
            lock (SyncEditor)
            {
                _instance = null;
                Close();
                Dispose();
            }
        }
    }
}