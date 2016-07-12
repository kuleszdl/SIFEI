namespace SIF.Visualization.Excel.ScenarioView
{
    partial class ScenarioDetailPaneContainer
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.ScenarioDetailPaneHost = new System.Windows.Forms.Integration.ElementHost();
            this.scenarioDetailPane1 = new SIF.Visualization.Excel.ScenarioView.ScenarioDetailPane();
            this.SuspendLayout();
            // 
            // ScenarioDetailPaneHost
            // 
            this.ScenarioDetailPaneHost.AutoSize = true;
            this.ScenarioDetailPaneHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ScenarioDetailPaneHost.Location = new System.Drawing.Point(0, 0);
            this.ScenarioDetailPaneHost.Name = "ScenarioDetailPaneHost";
            this.ScenarioDetailPaneHost.Size = new System.Drawing.Size(238, 596);
            this.ScenarioDetailPaneHost.TabIndex = 0;
            this.ScenarioDetailPaneHost.Text = "ScenarioDetailPaneHost";
            this.ScenarioDetailPaneHost.Child = this.scenarioDetailPane1;
            // 
            // ScenarioDetailPaneContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ScenarioDetailPaneHost);
            this.Name = "ScenarioDetailPaneContainer";
            this.Size = new System.Drawing.Size(238, 596);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost ScenarioDetailPaneHost;
        private ScenarioDetailPane scenarioDetailPane1;

    }
}
