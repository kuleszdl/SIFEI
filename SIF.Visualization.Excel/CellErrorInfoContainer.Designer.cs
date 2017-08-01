
namespace SIF.Visualization.Excel {

    partial class CellErrorInfoContainer {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.ElementHost = new System.Windows.Forms.Integration.ElementHost();
            this.SuspendLayout();
            //
            // elementHost1
            // 
            this.ElementHost.SuspendLayout();
            this.ElementHost.Name = "ElementHost";
            this.ElementHost.AutoSize = true;
            this.ElementHost.TabIndex = 0;
            this.ElementHost.Child = null;
            // 
            // CellErrorInfoContainer
            //
            this.Controls.Add(this.ElementHost);
            this.Name = "CellErrorInfoContainer";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
