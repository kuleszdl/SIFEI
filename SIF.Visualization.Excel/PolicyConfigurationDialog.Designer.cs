namespace SIF.Visualization.Excel
{
    partial class PolicyConfigurationDialog
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
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Constraints_M = new System.Windows.Forms.RadioButton();
            this.RD_M = new System.Windows.Forms.RadioButton();
            this.FC_M = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Constraints_A = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RD_A = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FC_A = new System.Windows.Forms.RadioButton();
            this.CB_FormulaComplexity = new System.Windows.Forms.CheckBox();
            this.CB_ReadingDirection = new System.Windows.Forms.CheckBox();
            this.CB_Constraints = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(73, 159);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(75, 23);
            this.Button_OK.TabIndex = 9;
            this.Button_OK.Text = "OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(165, 159);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 10;
            this.Button_Cancel.Text = "Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // Constraints_M
            // 
            this.Constraints_M.AutoSize = true;
            this.Constraints_M.Enabled = false;
            this.Constraints_M.Location = new System.Drawing.Point(94, 3);
            this.Constraints_M.Name = "Constraints_M";
            this.Constraints_M.Size = new System.Drawing.Size(60, 17);
            this.Constraints_M.TabIndex = 12;
            this.Constraints_M.Text = "Manual";
            this.Constraints_M.UseVisualStyleBackColor = true;
            // 
            // RD_M
            // 
            this.RD_M.AutoSize = true;
            this.RD_M.Enabled = false;
            this.RD_M.Location = new System.Drawing.Point(94, 3);
            this.RD_M.Name = "RD_M";
            this.RD_M.Size = new System.Drawing.Size(60, 17);
            this.RD_M.TabIndex = 14;
            this.RD_M.Text = "Manual";
            this.RD_M.UseVisualStyleBackColor = true;
            // 
            // FC_M
            // 
            this.FC_M.AutoSize = true;
            this.FC_M.Enabled = false;
            this.FC_M.Location = new System.Drawing.Point(93, 3);
            this.FC_M.Name = "FC_M";
            this.FC_M.Size = new System.Drawing.Size(60, 17);
            this.FC_M.TabIndex = 16;
            this.FC_M.Text = "Manual";
            this.FC_M.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.Constraints_A);
            this.panel1.Controls.Add(this.Constraints_M);
            this.panel1.Location = new System.Drawing.Point(31, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 26);
            this.panel1.TabIndex = 17;
            // 
            // Constraints_A
            // 
            this.Constraints_A.AutoSize = true;
            this.Constraints_A.Checked = global::SIF.Visualization.Excel.Properties.Settings.Default.ConstraintsFrequency;
            this.Constraints_A.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SIF.Visualization.Excel.Properties.Settings.Default, "ConstraintsFrequency", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.Constraints_A.Enabled = false;
            this.Constraints_A.Location = new System.Drawing.Point(1, 3);
            this.Constraints_A.Name = "Constraints_A";
            this.Constraints_A.Size = new System.Drawing.Size(58, 17);
            this.Constraints_A.TabIndex = 11;
            this.Constraints_A.TabStop = true;
            this.Constraints_A.Text = "Always";
            this.Constraints_A.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.RD_A);
            this.panel2.Controls.Add(this.RD_M);
            this.panel2.Location = new System.Drawing.Point(31, 76);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(161, 22);
            this.panel2.TabIndex = 18;
            // 
            // RD_A
            // 
            this.RD_A.AutoSize = true;
            this.RD_A.Checked = global::SIF.Visualization.Excel.Properties.Settings.Default.ReadingDirectionFrequency;
            this.RD_A.DataBindings.Add(new System.Windows.Forms.Binding("Checked", global::SIF.Visualization.Excel.Properties.Settings.Default, "ReadingDirectionFrequency", true, System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
            this.RD_A.Enabled = false;
            this.RD_A.Location = new System.Drawing.Point(1, 3);
            this.RD_A.Name = "RD_A";
            this.RD_A.Size = new System.Drawing.Size(58, 17);
            this.RD_A.TabIndex = 13;
            this.RD_A.TabStop = true;
            this.RD_A.Text = "Always";
            this.RD_A.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.FC_A);
            this.panel3.Controls.Add(this.FC_M);
            this.panel3.Location = new System.Drawing.Point(32, 119);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(160, 23);
            this.panel3.TabIndex = 19;
            // 
            // FC_A
            // 
            this.FC_A.AutoSize = true;
            this.FC_A.Checked = true;
            this.FC_A.Enabled = false;
            this.FC_A.Location = new System.Drawing.Point(0, 3);
            this.FC_A.Name = "FC_A";
            this.FC_A.Size = new System.Drawing.Size(58, 17);
            this.FC_A.TabIndex = 15;
            this.FC_A.TabStop = true;
            this.FC_A.Text = "Always";
            this.FC_A.UseVisualStyleBackColor = true;
            // 
            // CB_FormulaComplexity
            // 
            this.CB_FormulaComplexity.AutoSize = true;
            this.CB_FormulaComplexity.Location = new System.Drawing.Point(12, 104);
            this.CB_FormulaComplexity.Name = "CB_FormulaComplexity";
            this.CB_FormulaComplexity.Size = new System.Drawing.Size(116, 17);
            this.CB_FormulaComplexity.TabIndex = 6;
            this.CB_FormulaComplexity.Text = "Formula Complexity";
            this.CB_FormulaComplexity.UseVisualStyleBackColor = true;
            this.CB_FormulaComplexity.CheckedChanged += new System.EventHandler(this.CB_FormulaComplexity_CheckedChanged);
            // 
            // CB_ReadingDirection
            // 
            this.CB_ReadingDirection.AutoSize = true;
            this.CB_ReadingDirection.Location = new System.Drawing.Point(12, 61);
            this.CB_ReadingDirection.Name = "CB_ReadingDirection";
            this.CB_ReadingDirection.Size = new System.Drawing.Size(111, 17);
            this.CB_ReadingDirection.TabIndex = 3;
            this.CB_ReadingDirection.Text = "Reading Direction";
            this.CB_ReadingDirection.UseVisualStyleBackColor = true;
            this.CB_ReadingDirection.CheckedChanged += new System.EventHandler(this.CB_ReadingDirection_CheckedChanged);
            // 
            // CB_Constraints
            // 
            this.CB_Constraints.AutoSize = true;
            this.CB_Constraints.Location = new System.Drawing.Point(12, 12);
            this.CB_Constraints.Name = "CB_Constraints";
            this.CB_Constraints.Size = new System.Drawing.Size(78, 17);
            this.CB_Constraints.TabIndex = 0;
            this.CB_Constraints.Text = "Constraints";
            this.CB_Constraints.UseVisualStyleBackColor = true;
            this.CB_Constraints.CheckedChanged += new System.EventHandler(this.CB_Constraints_CheckedChanged);
            // 
            // PolicyConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(249, 190);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.CB_FormulaComplexity);
            this.Controls.Add(this.CB_ReadingDirection);
            this.Controls.Add(this.CB_Constraints);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PolicyConfigurationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Policy Configuration";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();
            
        }

        #endregion

        private System.Windows.Forms.CheckBox CB_Constraints;
        private System.Windows.Forms.CheckBox CB_ReadingDirection;
        private System.Windows.Forms.CheckBox CB_FormulaComplexity;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.RadioButton Constraints_A;
        private System.Windows.Forms.RadioButton Constraints_M;
        private System.Windows.Forms.RadioButton RD_A;
        private System.Windows.Forms.RadioButton RD_M;
        private System.Windows.Forms.RadioButton FC_A;
        private System.Windows.Forms.RadioButton FC_M;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
    }
}