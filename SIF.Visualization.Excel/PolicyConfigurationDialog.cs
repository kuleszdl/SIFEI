using System;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel
{
    /// <summary>
    ///     The class representing a Dialog to configure the Policies for which should be checked
    /// </summary>
    public partial class PolicyConfigurationDialog : Form
    {
        /// <summary>
        ///     An instance of a dialog in which the policies to check can be defined
        /// </summary>
        public PolicyConfigurationDialog()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            PolicyConfigurationModel = DataModel.Instance.CurrentWorkbook.PolicySettings;

            ErrorInCells.Checked = PolicyConfigurationModel.ErrorInCells;
            FormulaComplexity.Checked = PolicyConfigurationModel.FormulaComplexity;
            FormulaComplexityMaxNesting.Text = PolicyConfigurationModel.FormulaComplexityMaxDepth.ToString();
            FormulaComplexityMaxOperations.Text = PolicyConfigurationModel.FormulaComplexityMaxOperations.ToString();
            MultipleSameRef.Checked = PolicyConfigurationModel.MultipleSameRef;
            NoConstantsInFormulas.Checked = PolicyConfigurationModel.NoConstantsInFormulas;
            NonConsideredConstants.Checked = PolicyConfigurationModel.NonConsideredConstants;
            OneAmongOthers.Checked = PolicyConfigurationModel.OneAmongOthers;
            OneAmongOthersLength.Text = PolicyConfigurationModel.OneAmongOthersLength.ToString();

            if (PolicyConfigurationModel.OneAmongOthersStyle == "vertical") OneAmongOthersStyleVertical.Checked = true;
            else if (PolicyConfigurationModel.OneAmongOthersStyle == "horizontal")
                OneAmongOthersStyleHorizontal.Checked = true;
            else OneAmongOthersStyleBoth.Checked = true;

            ReadingDirection.Checked = PolicyConfigurationModel.ReadingDirection;
            ReadingDirectionLeftRight.Checked = PolicyConfigurationModel.ReadingDirectionLeftRight;
            ReadingDirectionTopBottom.Checked = PolicyConfigurationModel.ReadingDirectionTopBottom;
            RefToNull.Checked = PolicyConfigurationModel.RefToNull;
            StringDistance.Checked = PolicyConfigurationModel.StringDistance;
            StringDistanceMinDistance.Text = PolicyConfigurationModel.StringDistanceMinDist.ToString();

            ShowDialog();
        }

        public PolicyConfigurationModel PolicyConfigurationModel { get; set; }

        /// <summary>
        ///     Eventhandler for when the ok Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OK_Click(object sender, EventArgs e)
        {
            PolicyConfigurationModel.ErrorInCells = ErrorInCells.Checked;
            PolicyConfigurationModel.FormulaComplexity = FormulaComplexity.Checked;
            try
            {
                PolicyConfigurationModel.FormulaComplexityMaxDepth = int.Parse(FormulaComplexityMaxNesting.Text);
                PolicyConfigurationModel.FormulaComplexityMaxOperations =
                    int.Parse(FormulaComplexityMaxOperations.Text);
            }
            catch (Exception)
            {
                PolicyConfigurationModel.FormulaComplexityMaxDepth = 0;
                PolicyConfigurationModel.FormulaComplexityMaxOperations = 0;
            }

            PolicyConfigurationModel.MultipleSameRef = MultipleSameRef.Checked;
            PolicyConfigurationModel.NoConstantsInFormulas = NoConstantsInFormulas.Checked;
            PolicyConfigurationModel.NonConsideredConstants = NonConsideredConstants.Checked;
            PolicyConfigurationModel.OneAmongOthers = OneAmongOthers.Checked;
            try
            {
                PolicyConfigurationModel.OneAmongOthersLength = int.Parse(OneAmongOthersLength.Text);
            }
            catch (Exception)
            {
                PolicyConfigurationModel.OneAmongOthersLength = 0;
            }

            if (OneAmongOthersStyleHorizontal.Checked) PolicyConfigurationModel.OneAmongOthersStyle = "horizontal";
            else if (OneAmongOthersStyleVertical.Checked) PolicyConfigurationModel.OneAmongOthersStyle = "vertical";
            else PolicyConfigurationModel.OneAmongOthersStyle = "both";

            PolicyConfigurationModel.ReadingDirection = ReadingDirection.Checked;
            PolicyConfigurationModel.ReadingDirectionLeftRight = ReadingDirectionLeftRight.Checked;
            PolicyConfigurationModel.ReadingDirectionTopBottom = ReadingDirectionTopBottom.Checked;
            PolicyConfigurationModel.RefToNull = RefToNull.Checked;
            PolicyConfigurationModel.StringDistance = StringDistance.Checked;
            try
            {
                PolicyConfigurationModel.StringDistanceMinDist = int.Parse(StringDistanceMinDistance.Text);
            }
            catch (Exception)
            {
                PolicyConfigurationModel.StringDistanceMinDist = 0;
            }

            DataModel.Instance.CurrentWorkbook.PolicySettings = PolicyConfigurationModel;
            DataModel.Instance.CurrentWorkbook.Workbook.Saved = false;

            Close();
        }

        /// <summary>
        ///     Closes the Dialog when the Cancel Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void CB_Constraints_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_ReadingDirection_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_FormulaComplexity_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_MultipleSameRef_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_NonConsideredConstants_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_RefToNull_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_OneAmongOthers_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_ErrorInCells_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void CB_StringDistance_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void label2_Click(object sender, EventArgs e)
        {
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}