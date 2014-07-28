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

namespace SIF.Visualization.Excel
{
    public partial class PolicyConfigurationDialog : Form
    {
        public PolicyConfigurationDialog()
        {
            InitializeComponent();

            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            
            if (Settings.Default.Constraints)                                   // if the Constraints box was previously selected and is stored in the settings
            {   
                CB_Constraints.Checked = true;                                  // display the Constraints box as checked
                Constraints_A.Enabled = true;                                   // and enable the Always and Manual radio buttons
                Constraints_M.Enabled = true;

                if (!Settings.Default.ConstraintsFrequency)                     // The frequency is set to Always by default. If it is changed to false (aka Manual)
                {
                    Constraints_M.Checked = true;                               // display the frequency as Manual 
                }
                else
                {
                    Constraints_A.Checked = true;                               // otherwise, keep the defaul setting of Always
                }
            }

            if (Settings.Default.ReadingDirection)
            {
                CB_ReadingDirection.Checked = true;
                RD_A.Enabled = true;
                RD_M.Enabled = true;

                if (!Settings.Default.ReadingDirectionFrequency)
                {
                    RD_M.Checked = true;
                }
                else
                {
                    RD_A.Checked = true;
                }
            }

            if (Settings.Default.FormulaComplexity)
            {
                CB_FormulaComplexity.Checked = true;
                FC_A.Enabled = true;
                FC_M.Enabled = true;

                if (!Settings.Default.FormulaComplexityFrequency)
                {
                    FC_M.Checked = true;
                }
                else
                {
                    FC_A.Checked = true;
                }
            }

                                          
            this.ShowDialog();
        }

        private void Button_OK_Click(object sender, EventArgs e)
        {
            //string message = "Please restart the program in order to apply the new settings.";
            //string caption = "Warning";
            //MessageBoxButtons button = MessageBoxButtons.OK;
            //MessageBoxIcon icon = MessageBoxIcon.Warning;
            //DialogResult result = MessageBox.Show(message, caption, button, icon);

            // When OK is clicked, store the selections as per the checkboxes and radio buttons into the settings

            Settings.Default.Constraints = CB_Constraints.Checked;                  
            Settings.Default.ReadingDirection = CB_ReadingDirection.Checked;
            Settings.Default.FormulaComplexity = CB_FormulaComplexity.Checked;
            Settings.Default.ConstraintsFrequency = Constraints_A.Checked;
            Settings.Default.ReadingDirectionFrequency = RD_A.Checked;
            Settings.Default.FormulaComplexityFrequency = FC_A.Checked;
         
            this.Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CB_Constraints_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_Constraints.Checked)                                         // If the Constraints box is checked
            {
                Constraints_A.Enabled = true;                                   // enable the frequency radio buttons
                Constraints_M.Enabled = true;
                Constraints_A.Checked = true;                                   // and check the Always frquency button by default
                Constraints_M.Checked = false;
                
            }
            else                                                                // If the Constraints box is unchecked
            {
                Constraints_A.Enabled = false;                                  // disable the frequency radio buttons                              
                Constraints_M.Enabled = false;                                  
                Constraints_A.Checked = true;                                   // and check the Always frequency button by default
                Constraints_M.Checked = false;      
              
            }
        }

        private void CB_ReadingDirection_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_ReadingDirection.Checked)
            {
                RD_A.Enabled = true;
                RD_M.Enabled = true;
                RD_A.Checked = true;
                RD_M.Checked = false;
            }
            else
            {
                RD_A.Enabled = false;
                RD_M.Enabled = false;
                RD_A.Checked = true;
                RD_M.Checked = false;
            }
        }

        private void CB_FormulaComplexity_CheckedChanged(object sender, EventArgs e)
        {
            if (CB_FormulaComplexity.Checked)
            {
                FC_A.Enabled = true;
                FC_M.Enabled = true;
                FC_A.Checked = true;
                FC_M.Checked = false;

            }
            else
            {
                FC_A.Enabled = false;
                FC_M.Enabled = false;
                FC_A.Checked = true;
                FC_M.Checked = false;
            }
        }
    }
}
