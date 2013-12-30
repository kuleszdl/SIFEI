namespace SIF.Visualization.Excel.ScenarioView
{
    partial class DefineCellsPaneContainer
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
            this.DefineCellsPaneHost = new System.Windows.Forms.Integration.ElementHost();
            this.defineCellsPane1 = new SIF.Visualization.Excel.ScenarioView.DefineCellsPane();
            this.SuspendLayout();
            // 
            // DefineCellsPaneHost
            // 
            this.DefineCellsPaneHost.AutoSize = true;
            this.DefineCellsPaneHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DefineCellsPaneHost.Location = new System.Drawing.Point(0, 0);
            this.DefineCellsPaneHost.Name = "DefineCellsPaneHost";
            this.DefineCellsPaneHost.Size = new System.Drawing.Size(150, 150);
            this.DefineCellsPaneHost.TabIndex = 0;
            this.DefineCellsPaneHost.Text = "elementHost1";
            this.DefineCellsPaneHost.Child = this.defineCellsPane1;
            // 
            // DefineCellsPaneContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DefineCellsPaneHost);
            this.Name = "DefineCellsPaneContainer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost DefineCellsPaneHost;
        private DefineCellsPane defineCellsPane1;

    }
}
