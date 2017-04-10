using SIF.Visualization.Excel.Properties;
using System;
using System.Drawing;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel
{
    /// <summary>
    /// The class representing a Dialog to configure the Plocies for which should be checked
    /// </summary>
    public partial class PolicyConfigurationDialog : Form {

        private PolicyConfigurationModel settings;

        public PolicyConfigurationModel PolicyConfigurationModel {
            get { return settings; }
            set { settings = value; }
        }

        /// <summary>
        /// An instance of a dialog in which the policies to check can be defined
        /// </summary>
        public PolicyConfigurationDialog()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;
            settings = DataModel.Instance.CurrentWorkbook.PolicySettings;

            ErrorInCells.Checked = settings.ErrorInCells;
            FormulaComplexity.Checked = settings.FormulaComplexity;
            FormulaComplexityMaxNesting.Text = settings.FormulaComplexityMaxDepth.ToString();
            FormulaComplexityMaxOperations.Text = settings.FormulaComplexityMaxOperations.ToString();
            MultipleSameRef.Checked = settings.MultipleSameRef;
            NoConstantsInFormulas.Checked = settings.NoConstantsInFormulas;
            NonConsideredConstants.Checked = settings.NonConsideredConstants;
            OneAmongOthers.Checked = settings.OneAmongOthers;
            OneAmongOthersLength.Text = settings.OneAmongOthersLength.ToString();

            if (settings.OneAmongOthersStyle == "vertical") {
                OneAmongOthersStyleVertical.Checked = true;
            } else if (settings.OneAmongOthersStyle == "horizontal") {
                OneAmongOthersStyleHorizontal.Checked = true;
            } else {
                OneAmongOthersStyleBoth.Checked = true;
            }
            
            ReadingDirection.Checked = settings.ReadingDirection;
            ReadingDirectionLeftRight.Checked = settings.ReadingDirectionLeftRight;
            ReadingDirectionTopBottom.Checked = settings.ReadingDirectionTopBottom;
            RefToNull.Checked = settings.RefToNull;
            StringDistance.Checked = settings.StringDistance;
            StringDistanceMinDistance.Text = settings.StringDistanceMinDist.ToString();

            ShowDialog();
        }

        /// <summary>
        /// Eventhandler for when the ok Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OK_Click(object sender, EventArgs e)
        {
            settings.ErrorInCells = ErrorInCells.Checked;
            settings.FormulaComplexity = FormulaComplexity.Checked;
            try {
                settings.FormulaComplexityMaxDepth = Int32.Parse(FormulaComplexityMaxNesting.Text);
                settings.FormulaComplexityMaxOperations = Int32.Parse(FormulaComplexityMaxOperations.Text);
            } catch (Exception) {
                settings.FormulaComplexityMaxDepth = 0;
                settings.FormulaComplexityMaxOperations = 0;
            }         
            
            settings.MultipleSameRef = MultipleSameRef.Checked;
            settings.NoConstantsInFormulas = NoConstantsInFormulas.Checked;
            settings.NonConsideredConstants = NonConsideredConstants.Checked;
            settings.OneAmongOthers = OneAmongOthers.Checked;
            try {
                settings.OneAmongOthersLength = Int32.Parse(OneAmongOthersLength.Text);
            } catch (Exception) {
                settings.OneAmongOthersLength = 0;
            }

            if (OneAmongOthersStyleHorizontal.Checked) {
                settings.OneAmongOthersStyle = "horizontal";
            } else if (OneAmongOthersStyleVertical.Checked) {
                settings.OneAmongOthersStyle = "vertical";
            } else {
                settings.OneAmongOthersStyle = "both";
            }

            settings.ReadingDirection = ReadingDirection.Checked;
            settings.ReadingDirectionLeftRight = ReadingDirectionLeftRight.Checked;
            settings.ReadingDirectionTopBottom = ReadingDirectionTopBottom.Checked;
            settings.RefToNull = RefToNull.Checked;
            settings.StringDistance = StringDistance.Checked;
            try {
                settings.StringDistanceMinDist = Int32.Parse(StringDistanceMinDistance.Text);
            } catch (Exception) {
                settings.StringDistanceMinDist = 0;
            }

            DataModel.Instance.CurrentWorkbook.PolicySettings = settings;
            DataModel.Instance.CurrentWorkbook.Workbook.Saved = false;

            Close();
        }

        /// <summary>
        /// Closes the Dialog when the Cancel Button is clicked
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
