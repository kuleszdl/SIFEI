namespace SIF.Visualization.Excel
{
    partial class GlobalSettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GlobalSettingsDialog));
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.sifUrlLabel = new System.Windows.Forms.Label();
            this.sifUrlTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // Button_Cancel
            // 
            resources.ApplyResources(this.Button_Cancel, "Button_Cancel");
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Button_OK
            // 
            resources.ApplyResources(this.Button_OK, "Button_OK");
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // sifUrlLabel
            // 
            resources.ApplyResources(this.sifUrlLabel, "sifUrlLabel");
            this.sifUrlLabel.Name = "sifUrlLabel";
            // 
            // sifUrlTextbox
            // 
            this.sifUrlTextbox.CausesValidation = false;
            resources.ApplyResources(this.sifUrlTextbox, "sifUrlTextbox");
            this.sifUrlTextbox.Name = "sifUrlTextbox";
            this.sifUrlTextbox.TextChanged += new System.EventHandler(this.sifUrlTextbox_TextChanged);
            // 
            // GlobalSettingsDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.sifUrlTextbox);
            this.Controls.Add(this.sifUrlLabel);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalSettingsDialog";
            this.Load += new System.EventHandler(this.GlobalSettingsDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Label sifUrlLabel;
        private System.Windows.Forms.TextBox sifUrlTextbox;
    }
}