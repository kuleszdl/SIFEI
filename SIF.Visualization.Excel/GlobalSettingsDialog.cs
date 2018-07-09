using System;
using System.Windows.Forms;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel
{
    /// <summary>
    ///     A Dialog in which the global settings can be deinfed
    /// </summary>
    public partial class GlobalSettingsDialog : Form
    {
        /// <summary>
        ///     Instanciates a new Dialog in which the global settings can be changed
        /// </summary>
        public GlobalSettingsDialog()
        {
            InitializeComponent();
            sifUrlTextbox.Text = Settings.Default.SifServerUrl;

            FormBorderStyle = FormBorderStyle.FixedDialog;
            ShowDialog();
        }
        
        public String getSifUrlTextBox()
        {
            return sifUrlTextbox.Text;
        }

        private void GlobalSettingsDialog_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        ///     Click Handler of the Ok Button
        ///     Checks if the settings put by the user are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OK_Click(object sender, EventArgs e)
        {
            Settings.Default.SifServerUrl = sifUrlTextbox.Text;
            Settings.Default.Save();
           
           
            Close();
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void sifUrlTextbox_TextChanged(object sender, EventArgs e)
        {
        }
    }
}