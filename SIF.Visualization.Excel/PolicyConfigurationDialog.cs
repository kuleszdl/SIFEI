using System.Diagnostics;
using System.IO;
using SIF.Visualization.Excel.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel
{
    public partial class PolicyConfigurationDialog : Form
    {
        private readonly String debugFile = Settings.Default.FrameworkPath + @"\debug";

        public PolicyConfigurationDialog()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            PolicyConfigurationModel settings = DataModel.Instance.CurrentWorkbook.PolicySettings;

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

           
            cb_Ask_Thousands.Checked = !Settings.Default.SifUseThousandsSeparator;


            this.ShowDialog();
        }

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

            this.Close();

        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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
