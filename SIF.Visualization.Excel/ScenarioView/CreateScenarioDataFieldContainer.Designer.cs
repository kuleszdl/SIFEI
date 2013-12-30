namespace SIF.Visualization.Excel.ScenarioView
{
    partial class CreateScenarioDataFieldContainer
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
            this.createScenarioDataFieldHost = new System.Windows.Forms.Integration.ElementHost();
            this.createScenarioDataField1 = new SIF.Visualization.Excel.ScenarioView.CreateScenarioDataField();
            this.SuspendLayout();
            // 
            // createScenarioDataFieldHost
            // 
            this.createScenarioDataFieldHost.AutoSize = true;
            this.createScenarioDataFieldHost.Dock = System.Windows.Forms.DockStyle.Fill;
            this.createScenarioDataFieldHost.Location = new System.Drawing.Point(0, 0);
            this.createScenarioDataFieldHost.Name = "createScenarioDataFieldHost";
            this.createScenarioDataFieldHost.Size = new System.Drawing.Size(150, 150);
            this.createScenarioDataFieldHost.TabIndex = 0;
            this.createScenarioDataFieldHost.Text = "elementHost1";
            this.createScenarioDataFieldHost.Child = this.createScenarioDataField1;
            // 
            // CreateScenarioDataFieldContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.createScenarioDataFieldHost);
            this.Name = "CreateScenarioDataFieldContainer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost createScenarioDataFieldHost;
        private CreateScenarioDataField createScenarioDataField1;
    }
}
