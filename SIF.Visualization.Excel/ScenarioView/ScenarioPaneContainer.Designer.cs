namespace SIF.Visualization.Excel.ScenarioView
{
    partial class ScenarioPaneContainer
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
            this.scenarioPaneHost = new System.Windows.Forms.Integration.ElementHost();
            this.scenarioPane1 = new SIF.Visualization.Excel.ScenarioView.ScenarioPane();
            this.SuspendLayout();
            // 
            // scenarioPaneHost
            // 
            this.scenarioPaneHost.AutoSize = true;
            this.scenarioPaneHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scenarioPaneHost.Location = new System.Drawing.Point(0, 0);
            this.scenarioPaneHost.Name = "scenarioPaneHost";
            this.scenarioPaneHost.Size = new System.Drawing.Size(212, 658);
            this.scenarioPaneHost.TabIndex = 0;
            this.scenarioPaneHost.Text = "scenarioPaneHost";
            this.scenarioPaneHost.Child = this.scenarioPane1;
            // 
            // ScenarioPaneContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.scenarioPaneHost);
            this.Name = "ScenarioPaneContainer";
            this.Size = new System.Drawing.Size(212, 658);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost scenarioPaneHost;
        private ScenarioPane scenarioPane1;
    }
}
