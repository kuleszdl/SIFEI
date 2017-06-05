namespace SIF.Visualization.Excel.View
{
    partial class RuleDataFieldContainer
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ruleDataFieldHost = new System.Windows.Forms.Integration.ElementHost();
            this.ruleDataField = new RuleDataField();
            this.SuspendLayout();
            // 
            // createScenarioDataFieldHost
            // 
            this.ruleDataFieldHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ruleDataFieldHost.Name = "scenarioDataFieldHost";
            this.ruleDataFieldHost.TabIndex = 0;
            this.ruleDataFieldHost.Text = "elementHost";
            this.ruleDataFieldHost.Child = this.ruleDataField;
            // 
            // CreateScenarioDataFieldContainer
            //
            this.Controls.Add(this.ruleDataFieldHost);
            this.Name = "RuleDataFieldContainer";
            this.ResumeLayout(false);
            this.components = new System.ComponentModel.Container();
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Text = "RuleDataFieldContainer";
        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ruleDataFieldHost;
        private RuleDataField ruleDataField;
    }
}