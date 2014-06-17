namespace SIF.Visualization.Excel.SharedView
{
    partial class SharedPaneContainer
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
            this.sharedPaneHost = new System.Windows.Forms.Integration.ElementHost();
            this.sharedPane1 = new SIF.Visualization.Excel.SharedView.SharedPane();
            this.SuspendLayout();
            // 
            // sharedPaneHost
            // 
            this.sharedPaneHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sharedPaneHost.Location = new System.Drawing.Point(0, 0);
            this.sharedPaneHost.Name = "sharedPaneHost";
            this.sharedPaneHost.Size = new System.Drawing.Size(300, 150);
            this.sharedPaneHost.TabIndex = 0;
            this.sharedPaneHost.Text = "elementHost1";
            this.sharedPaneHost.Child = this.sharedPane1;
            // 
            // SharedPaneContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sharedPaneHost);
            this.Name = "SharedPaneContainer";
            this.Size = new System.Drawing.Size(300, 150);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost sharedPaneHost;
        private SharedPane sharedPane1;

    }
}
