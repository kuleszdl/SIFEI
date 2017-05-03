namespace SIF.Visualization.Excel
{
    partial class RuleEditor
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
            this.CancelButton = new System.Windows.Forms.Button();
            this.TestButton = new System.Windows.Forms.Button();
            this.FilterConditionLabel = new System.Windows.Forms.Label();
            this.FilterRadioButtonAND = new System.Windows.Forms.RadioButton();
            this.FilterRadioButtonOR = new System.Windows.Forms.RadioButton();
            this.FilterRadioButtonNONE = new System.Windows.Forms.RadioButton();
            this.ConditionLabel = new System.Windows.Forms.Label();
            this.RuleNameLabel = new System.Windows.Forms.Label();
            this.RuleNameTextBox = new System.Windows.Forms.TextBox();
            this.NewConditionButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Location = new System.Drawing.Point(341, 242);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // TestButton
            // 
            this.TestButton.Location = new System.Drawing.Point(260, 242);
            this.TestButton.Name = "TestButton";
            this.TestButton.Size = new System.Drawing.Size(75, 23);
            this.TestButton.TabIndex = 1;
            this.TestButton.Text = "Test";
            this.TestButton.UseVisualStyleBackColor = true;
            this.TestButton.Click += new System.EventHandler(this.TestButton_Click);
            // 
            // FilterConditionLabel
            // 
            this.FilterConditionLabel.AutoSize = true;
            this.FilterConditionLabel.Location = new System.Drawing.Point(12, 55);
            this.FilterConditionLabel.Name = "FilterConditionLabel";
            this.FilterConditionLabel.Size = new System.Drawing.Size(69, 13);
            this.FilterConditionLabel.TabIndex = 2;
            this.FilterConditionLabel.Text = "Filterkriterien:";
            // 
            // FilterRadioButtonAND
            // 
            this.FilterRadioButtonAND.AutoSize = true;
            this.FilterRadioButtonAND.Location = new System.Drawing.Point(132, 51);
            this.FilterRadioButtonAND.Name = "FilterRadioButtonAND";
            this.FilterRadioButtonAND.Size = new System.Drawing.Size(197, 17);
            this.FilterRadioButtonAND.TabIndex = 3;
            this.FilterRadioButtonAND.TabStop = true;
            this.FilterRadioButtonAND.Text = "Alle Bedingungen müssen erfüllt sein";
            this.FilterRadioButtonAND.UseVisualStyleBackColor = true;
            // 
            // FilterRadioButtonOR
            // 
            this.FilterRadioButtonOR.AutoSize = true;
            this.FilterRadioButtonOR.Location = new System.Drawing.Point(132, 74);
            this.FilterRadioButtonOR.Name = "FilterRadioButtonOR";
            this.FilterRadioButtonOR.Size = new System.Drawing.Size(207, 17);
            this.FilterRadioButtonOR.TabIndex = 4;
            this.FilterRadioButtonOR.TabStop = true;
            this.FilterRadioButtonOR.Text = "Eine der Bedingungen muss erfüllt sein";
            this.FilterRadioButtonOR.UseVisualStyleBackColor = true;
            // 
            // FilterRadioButtonNONE
            // 
            this.FilterRadioButtonNONE.AutoSize = true;
            this.FilterRadioButtonNONE.Location = new System.Drawing.Point(132, 97);
            this.FilterRadioButtonNONE.Name = "FilterRadioButtonNONE";
            this.FilterRadioButtonNONE.Size = new System.Drawing.Size(177, 17);
            this.FilterRadioButtonNONE.TabIndex = 5;
            this.FilterRadioButtonNONE.TabStop = true;
            this.FilterRadioButtonNONE.Text = "Keine der Bedingungen ist erfüllt";
            this.FilterRadioButtonNONE.UseVisualStyleBackColor = true;
            // 
            // ConditionLabel
            // 
            this.ConditionLabel.AutoSize = true;
            this.ConditionLabel.Location = new System.Drawing.Point(12, 133);
            this.ConditionLabel.Name = "ConditionLabel";
            this.ConditionLabel.Size = new System.Drawing.Size(73, 13);
            this.ConditionLabel.TabIndex = 6;
            this.ConditionLabel.Text = "Bedingungen:";
            // 
            // RuleNameLabel
            // 
            this.RuleNameLabel.AutoSize = true;
            this.RuleNameLabel.Location = new System.Drawing.Point(12, 13);
            this.RuleNameLabel.Name = "RuleNameLabel";
            this.RuleNameLabel.Size = new System.Drawing.Size(110, 13);
            this.RuleNameLabel.TabIndex = 7;
            this.RuleNameLabel.Text = "Regelname (optional):";
            // 
            // RuleNameTextBox
            // 
            this.RuleNameTextBox.Location = new System.Drawing.Point(132, 13);
            this.RuleNameTextBox.Name = "RuleNameTextBox";
            this.RuleNameTextBox.Size = new System.Drawing.Size(284, 20);
            this.RuleNameTextBox.TabIndex = 8;
            // 
            // NewConditionButton
            // 
            this.NewConditionButton.Location = new System.Drawing.Point(132, 133);
            this.NewConditionButton.Name = "NewConditionButton";
            this.NewConditionButton.Size = new System.Drawing.Size(105, 23);
            this.NewConditionButton.TabIndex = 9;
            this.NewConditionButton.Text = "Neue Bedingung";
            this.NewConditionButton.UseVisualStyleBackColor = true;
            // 
            // RuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(428, 277);
            this.Controls.Add(this.NewConditionButton);
            this.Controls.Add(this.RuleNameTextBox);
            this.Controls.Add(this.RuleNameLabel);
            this.Controls.Add(this.ConditionLabel);
            this.Controls.Add(this.FilterRadioButtonNONE);
            this.Controls.Add(this.FilterRadioButtonOR);
            this.Controls.Add(this.FilterRadioButtonAND);
            this.Controls.Add(this.FilterConditionLabel);
            this.Controls.Add(this.TestButton);
            this.Controls.Add(this.CancelButton);
            this.Name = "RuleEditor";
            this.Text = "RuleEditor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button TestButton;
        private System.Windows.Forms.Label FilterConditionLabel;
        private System.Windows.Forms.RadioButton FilterRadioButtonAND;
        private System.Windows.Forms.RadioButton FilterRadioButtonOR;
        private System.Windows.Forms.RadioButton FilterRadioButtonNONE;
        private System.Windows.Forms.Label ConditionLabel;
        private System.Windows.Forms.Label RuleNameLabel;
        private System.Windows.Forms.TextBox RuleNameTextBox;
        private System.Windows.Forms.Button NewConditionButton;
    }
}