namespace SIF.Visualization.Excel.View
{
    partial class SidebarContainer
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
            this.SidebarHost = new System.Windows.Forms.Integration.ElementHost();
            this.Sidebar1 = new SIF.Visualization.Excel.View.Sidebar();
            this.SuspendLayout();
            // 
            // SidebarHost
            // 
            this.SidebarHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SidebarHost.Name = "SidebarHost";
            this.SidebarHost.TabIndex = 0;
            this.SidebarHost.Child = this.Sidebar1;
            // 
            // SidebarContainer
            // 
            this.Controls.Add(this.SidebarHost);
            this.Name = "SidebarContainer";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost SidebarHost;
        private Sidebar Sidebar1;

    }
}
