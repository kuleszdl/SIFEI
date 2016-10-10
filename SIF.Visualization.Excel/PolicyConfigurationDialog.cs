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
    public partial class PolicyConfigurationDialog : Form
    {       
        /// <summary>
        /// An instance of a dialog in which the policies to check can be defined
        /// </summary>
        public PolicyConfigurationDialog()
        {
            InitializeComponent();

            postInitialize();

            FormBorderStyle = FormBorderStyle.FixedDialog;
            PolicyConfigurationModel settings = DataModel.Instance.CurrentWorkbook.PolicySettings;
            SetNoConstants(settings);
            SetReadingDir(settings);
            SetFormuCompl(settings);
            SetMultSamRef(settings);
            SetNonConCon(settings);
            SetRefToNull(settings);
            SetOneAmongOth(settings);
            SetStringDist(settings);
            SetErrorinCell(settings);
           
            cb_Ask_Thousands.Checked = !Settings.Default.SifUseThousandsSeparator;


            ShowDialog();
        }

        #region Methods to set Settings

        /// <summary>
        /// Sets the setting for Error in Cells
        /// </summary>
        /// <param name="settings"></param>
        private void SetErrorinCell(PolicyConfigurationModel settings)
        {
            if (settings.ErrorInCells)
            {
                CB_ErrorInCells.Checked = true;
                EIC_A.Enabled = true;
                EIC_M.Enabled = true;

                if (!settings.OneAmongOthersAutomatic)
                {
                    EIC_M.Checked = true;
                }
                else
                {
                    EIC_A.Checked = true;
                }
            }
        }

        /// <summary>
        /// Sets the Setting for the string distance
        /// </summary>
        /// <param name="settings"></param>
        private void SetStringDist(PolicyConfigurationModel settings)
        {
            if (settings.StringDistance)
            {
                CB_StringDistance.Checked = true;
                SD_A.Enabled = true;
                SD_M.Enabled = true;

                if (!settings.StringDistanceAutomatic)
                {
                    SD_M.Checked = true;
                }
                else
                {
                    SD_A.Checked = true;
                }
                SD_Amount.Text = settings.StringDistanceMaxDist.ToString();
            }
            else
            {
                SD_Amount.Enabled = false;
            }
        }


        /// <summary>
        /// Sets settings for the one among others rule 
        /// </summary>
        /// <param name="settings"></param>
        private void SetOneAmongOth(PolicyConfigurationModel settings)
        {
            if (settings.OneAmongOthers)
            {
                CB_OneAmongOthers.Checked = true;
                OAO_A.Enabled = true;
                OAO_M.Enabled = true;

                if (!settings.OneAmongOthersAutomatic)
                {
                    OAO_M.Checked = true;
                }
                else
                {
                    OAO_A.Checked = true;
                }
            }
        }

        /// <summary>
        /// Sets the Settings for the reference to null in a cell
        /// </summary>
        /// <param name="settings"></param>
        private void SetRefToNull(PolicyConfigurationModel settings)
        {
            if (settings.RefToNull)
            {
                CB_RefToNull.Checked = true;
                RTN_A.Enabled = true;
                RTN_M.Enabled = true;

                if (!settings.RefToNullAutomatic)
                {
                    RTN_M.Checked = true;
                }
                else
                {
                    RTN_A.Checked = true;
                }
            }
        }

        /// <summary>
        /// Sets Settings for non consider constants
        /// </summary>
        /// <param name="settings"></param>
        private void SetNonConCon(PolicyConfigurationModel settings)
        {
            if (settings.NonConsideredConstants)
            {
                CB_NonConsideredConstants.Checked = true;
                NCC_A.Enabled = true;
                NCC_M.Enabled = true;

                if (!settings.NonConsideredConstantsAutomatic)
                {
                    NCC_M.Checked = true;
                }
                else
                {
                    NCC_A.Checked = true;
                }
            }
        }

        /// <summary>
        /// Sets the Settings for Multiple same References in a formula
        /// </summary>
        /// <param name="settings"></param>
        private void SetMultSamRef(PolicyConfigurationModel settings)
        {
            if (settings.MultipleSameRef)
            {
                CB_MultipleSameRef.Checked = true;
                MSR_A.Enabled = true;
                MSR_M.Enabled = true;

                if (!settings.MultipleSameRefAutomatic)
                {
                    MSR_M.Checked = true;
                }
                else
                {
                    MSR_A.Checked = true;
                }
            }
        }


        /// <summary>
        /// Sets the Settings for Formula Complexity 
        /// </summary>
        /// <param name="settings"></param>
        private void SetFormuCompl(PolicyConfigurationModel settings)
        {
            if (settings.FormulaComplexity)
            {
                CB_FormulaComplexity.Checked = true;
                FC_A.Enabled = true;
                FC_M.Enabled = true;

                if (!settings.FormulaComplexityAutomatic)
                {
                    FC_M.Checked = true;
                }
                else
                {
                    FC_A.Checked = true;
                }
            }
        }

        /// <summary>
        /// Sets the settings for No Constants in Formulas
        /// </summary>
        /// <param name="settings"></param>
        private void SetNoConstants(PolicyConfigurationModel settings)
        {
            if (settings.NoConstantsInFormulas)                                   // if the Constraints box was previously selected and is stored in the settings
            {
                CB_NoConstantsInFormulas.Checked = true;                                  // display the Constraints box as checked
                NCIF_A.Enabled = true;                                   // and enable the Always and Manual radio buttons
                NCIF_M.Enabled = true;

                if (settings.NoConstantsInFormulasAutomatic)                     // The frequency is set to Always by default. If it is changed to false (aka Manual)
                {
                    NCIF_A.Checked = true;                               // display the frequency as Manual 
                }
                else
                {
                    NCIF_M.Checked = true;                               // otherwise, keep the defaul setting of Always
                }
            }
        }

        

        /// <summary>
        /// Sets the settings for the Reading Direction
        /// </summary>
        /// <param name="settings"></param>
        private void SetReadingDir(PolicyConfigurationModel settings)
        {
            if (settings.ReadingDirection)
            {
                CB_ReadingDirection.Checked = true;
                RD_A.Enabled = true;
                RD_M.Enabled = true;

                if (!settings.ReadingDirectionAutomatic)
                {
                    RD_M.Checked = true;
                }
                else
                {
                    RD_A.Checked = true;
                }
            }
        }

#endregion

        /// <summary>
        /// Update Labels and other UI elements from translation via Resources after ecerything is initialized
        /// </summary>
        private void postInitialize()
        {
            Text = Resources.tl_PolicyConfiguration_WindowTitle; // Window title
            Button_Cancel.Text = Resources.tl_PolicyConfiguration_Button_Cancel;
            Button_OK.Text = Resources.tl_PolicyConfiguration_Button_Ok;
            CB_ErrorInCells.Text = Resources.tl_PolicyConfiguration_Rule_CellsWithErrors;
            CB_FormulaComplexity.Text = Resources.tl_PolicyConfiguration_Rule_FormulaComplexity;
            CB_MultipleSameRef.Text = Resources.tl_PolicyConfiguration_Rule_SameMultipleRefs;
            CB_NoConstantsInFormulas.Text = Resources.tl_PolicyConfiguration_Rule_Constants;
            CB_NonConsideredConstants.Text = Resources.tl_PolicyConfiguration_Rule_NonConsideredConstants;
            CB_OneAmongOthers.Text = Resources.tl_PolicyConfiguration_Rule_OneAmongOthers;
            CB_ReadingDirection.Text = Resources.tl_PolicyConfiguration_Rule_ReadingDirection;
            CB_RefToNull.Text = Resources.tl_PolicyConfiguration_Rule_ReferencesToBlankCells;
            CB_StringDistance.Text = Resources.tl_PolicyConfiguration_Rule_StringDistance;
            lb_MaxDistance.Text = Resources.tl_PolicyConfiguration_Rule_StringDistanceExtra;
            cb_Ask_Thousands.Text = Resources.tl_PolicyConfiguration_Label_ThousandSeparators;

            // Labels always / manual
            String textAlways = Resources.tl_PolicyConfiguration_Label_Always;
            String textManual = Resources.tl_PolicyConfiguration_Label_Manual;
            
            NCIF_A.Text = textAlways;
            NCIF_M.Text = textManual;
            RD_A.Text = textAlways;
            RD_M.Text = textManual;
            FC_A.Text = textAlways;
            FC_M.Text = textManual;
            MSR_A.Text = textAlways;
            MSR_M.Text = textManual;
            NCC_A.Text = textAlways;
            NCC_M.Text = textManual;
            RTN_A.Text = textAlways;
            RTN_M.Text = textManual;
            OAO_A.Text = textAlways;
            OAO_M.Text = textManual;
            EIC_A.Text = textAlways;
            EIC_M.Text = textManual;
            SD_A.Text = textAlways;
            SD_M.Text = textManual;
        }

        /// <summary>
        /// Eventhandler for when the ok Button is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OK_Click(object sender, EventArgs e)
        {
            bool error = false;
            PolicyConfigurationModel settings = DataModel.Instance.CurrentWorkbook.PolicySettings;

            #region Parsing w/ possible errors
            if (SD_Amount.Enabled)
            {
                int stringDstMax = 0;
                try
                {
                    stringDstMax = Convert.ToInt32(SD_Amount.Text);
                    if (stringDstMax < 1)
                    {
                        error = true;
                    }
                }
                catch (FormatException)
                {
                    error = true;
                }
                if (error)
                {
                    SD_Amount.BackColor = Color.LightPink;
                }
                else
                {
                    settings.StringDistanceMaxDist = stringDstMax;
                }
            }
           
            #endregion

            if (error) // abort the closing and further parsing of members
                return;

            settings.NoConstantsInFormulas = CB_NoConstantsInFormulas.Checked;
            settings.NoConstantsInFormulasAutomatic = NCIF_A.Checked;

            settings.ReadingDirection = CB_ReadingDirection.Checked;
            settings.ReadingDirectionAutomatic = RD_A.Checked;

            settings.FormulaComplexity = CB_FormulaComplexity.Checked;
            settings.FormulaComplexityAutomatic = FC_A.Checked;

            settings.MultipleSameRef = CB_MultipleSameRef.Checked;
            settings.MultipleSameRefAutomatic = MSR_A.Checked;

            settings.NonConsideredConstants = CB_NonConsideredConstants.Checked;
            settings.NonConsideredConstantsAutomatic = NCC_A.Checked;

            settings.RefToNull = CB_RefToNull.Checked;
            settings.RefToNullAutomatic = RTN_A.Checked;

            settings.OneAmongOthers = CB_OneAmongOthers.Checked;
            settings.OneAmongOthersAutomatic = OAO_A.Checked;

            settings.StringDistance = CB_StringDistance.Checked;
            settings.StringDistanceAutomatic = SD_A.Checked;

            settings.ErrorInCells = CB_ErrorInCells.Checked;
            settings.ErrorInCellsAutomatic = EIC_A.Checked;

            Settings.Default.SifUseThousandsSeparator = !cb_Ask_Thousands.Checked;

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

        /// <summary>
        /// Handler when Checkbox of the No Constants in Formulars  is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_Constraints_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_NoConstantsInFormulas.Checked)                                         // If the Constraints box is checked
            {
                NCIF_A.Enabled = true;                                   // enable the frequency radio buttons
                NCIF_M.Enabled = true;
            }
            else                                                                // If the Constraints box is unchecked
            {
                NCIF_A.Enabled = false;                                  // disable the frequency radio buttons                              
                NCIF_M.Enabled = false;
            }
        }

        /// <summary>
        /// Handler when Checkbox of the reading direction  is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_ReadingDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_ReadingDirection.Checked)
            {
                RD_A.Enabled = true;
                RD_M.Enabled = true;
            }
            else
            {
                RD_A.Enabled = false;
                RD_M.Enabled = false;
            }
        }

        /// <summary>
        /// Handler when Checkbox of the Formula Complexity  is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_FormulaComplexity_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_FormulaComplexity.Checked)
            {
                FC_A.Enabled = true;
                FC_M.Enabled = true;

            }
            else
            {
                FC_A.Enabled = false;
                FC_M.Enabled = false;
            }
        }

        /// <summary>
        /// Handler when Checkbox of the multiple same reference  is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CB_MultipleSameRef_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_MultipleSameRef.Checked)
            {
                MSR_A.Enabled = true;
                MSR_M.Enabled = true;
            }
            else
            {
                MSR_A.Enabled = false;
                MSR_M.Enabled = false;
            }
        }

        private void CB_NonConsideredConstants_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_NonConsideredConstants.Checked)
            {
                NCC_A.Enabled = true;
                NCC_M.Enabled = true;
            }
            else
            {
                NCC_A.Enabled = false;
                NCC_M.Enabled = false;
            }
        }

        private void CB_RefToNull_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_RefToNull.Checked)
            {
                RTN_A.Enabled = true;
                RTN_M.Enabled = true;
            }
            else
            {
                RTN_A.Enabled = false;
                RTN_M.Enabled = false;
            }
        }

        private void CB_OneAmongOthers_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_OneAmongOthers.Checked)
            {
                OAO_A.Enabled = true;
                OAO_M.Enabled = true;
            }
            else
            {
                OAO_A.Enabled = false;
                OAO_M.Enabled = false;
            }
        }
        private void CB_ErrorInCells_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_ErrorInCells.Checked)
            {
                EIC_A.Enabled = true;
                EIC_M.Enabled = true;
            }
            else
            {
                EIC_A.Enabled = false;
                EIC_M.Enabled = false;
            }
        }

        private void CB_StringDistance_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_StringDistance.Checked)
            {
                SD_A.Enabled = true;
                SD_M.Enabled = true;
                SD_Amount.Enabled = true;
                SD_Amount.Text = DataModel.Instance.CurrentWorkbook.PolicySettings.StringDistanceMaxDist.ToString();

            }
            else
            {
                SD_A.Enabled = false;
                SD_M.Enabled = false;
                SD_Amount.Enabled = false;
            }
        }
    }
}
