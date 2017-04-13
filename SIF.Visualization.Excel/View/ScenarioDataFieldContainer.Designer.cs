namespace SIF.Visualization.Excel.View {

    partial class ScenarioDataFieldContainer {
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
            this.scenarioDataFieldHost = new System.Windows.Forms.Integration.ElementHost();
            this.scenarioDataField = new ScenarioDataField();
            this.SuspendLayout();
            // 
            // createScenarioDataFieldHost
            // 
            this.scenarioDataFieldHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scenarioDataFieldHost.Name = "scenarioDataFieldHost";
            this.scenarioDataFieldHost.TabIndex = 0;
            this.scenarioDataFieldHost.Text = "elementHost";
            this.scenarioDataFieldHost.Child = this.scenarioDataField;
            // 
            // CreateScenarioDataFieldContainer
            //
            this.Controls.Add(this.scenarioDataFieldHost);
            this.Name = "ScenarioDataFieldContainer";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost scenarioDataFieldHost;
        private ScenarioDataField scenarioDataField;
    }
}
