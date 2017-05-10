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
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.FilterConditionLabel = new System.Windows.Forms.Label();
            this.FilterRadioButtonAND = new System.Windows.Forms.RadioButton();
            this.FilterRadioButtonOR = new System.Windows.Forms.RadioButton();
            this.FilterRadioButtonNONE = new System.Windows.Forms.RadioButton();
            this.ConditionLabel = new System.Windows.Forms.Label();
            this.RuleNameLabel = new System.Windows.Forms.Label();
            this.RuleNameTextBox = new System.Windows.Forms.TextBox();
            this.NewConditionButton = new System.Windows.Forms.Button();
            this.ConditionFirstComboBox = new System.Windows.Forms.ComboBox();
            this.ConditionRegexTextBox = new System.Windows.Forms.TextBox();
            this.ChooseAreaCheckbox = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(377, 309);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmButton.Location = new System.Drawing.Point(296, 309);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(75, 23);
            this.ConfirmButton.TabIndex = 1;
            this.ConfirmButton.Text = "Create Rule";
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // FilterConditionLabel
            // 
            this.FilterConditionLabel.AutoSize = true;
            this.FilterConditionLabel.Location = new System.Drawing.Point(12, 55);
            this.FilterConditionLabel.Name = "FilterConditionLabel";
            this.FilterConditionLabel.Size = new System.Drawing.Size(69, 13);
            this.FilterConditionLabel.TabIndex = 2;
            this.FilterConditionLabel.Text = "Filter criterion";
            // 
            // FilterRadioButtonAND
            // 
            this.FilterRadioButtonAND.AutoSize = true;
            this.FilterRadioButtonAND.Location = new System.Drawing.Point(132, 51);
            this.FilterRadioButtonAND.Name = "FilterRadioButtonAND";
            this.FilterRadioButtonAND.Size = new System.Drawing.Size(146, 17);
            this.FilterRadioButtonAND.TabIndex = 3;
            this.FilterRadioButtonAND.TabStop = true;
            this.FilterRadioButtonAND.Text = "All conditions are satisfied";
            this.FilterRadioButtonAND.UseVisualStyleBackColor = true;
            // 
            // FilterRadioButtonOR
            // 
            this.FilterRadioButtonOR.AutoSize = true;
            this.FilterRadioButtonOR.Location = new System.Drawing.Point(132, 74);
            this.FilterRadioButtonOR.Name = "FilterRadioButtonOR";
            this.FilterRadioButtonOR.Size = new System.Drawing.Size(184, 17);
            this.FilterRadioButtonOR.TabIndex = 4;
            this.FilterRadioButtonOR.TabStop = true;
            this.FilterRadioButtonOR.Text = "(At least) one condition is satisfied";
            this.FilterRadioButtonOR.UseVisualStyleBackColor = true;
            // 
            // FilterRadioButtonNONE
            // 
            this.FilterRadioButtonNONE.AutoSize = true;
            this.FilterRadioButtonNONE.Location = new System.Drawing.Point(132, 97);
            this.FilterRadioButtonNONE.Name = "FilterRadioButtonNONE";
            this.FilterRadioButtonNONE.Size = new System.Drawing.Size(137, 17);
            this.FilterRadioButtonNONE.TabIndex = 5;
            this.FilterRadioButtonNONE.TabStop = true;
            this.FilterRadioButtonNONE.Text = "No Condition is satisfied";
            this.FilterRadioButtonNONE.UseVisualStyleBackColor = true;
            // 
            // ConditionLabel
            // 
            this.ConditionLabel.AutoSize = true;
            this.ConditionLabel.Location = new System.Drawing.Point(12, 133);
            this.ConditionLabel.Name = "ConditionLabel";
            this.ConditionLabel.Size = new System.Drawing.Size(65, 13);
            this.ConditionLabel.TabIndex = 6;
            this.ConditionLabel.Text = "Condition(s):";
            // 
            // RuleNameLabel
            // 
            this.RuleNameLabel.AutoSize = true;
            this.RuleNameLabel.Location = new System.Drawing.Point(12, 13);
            this.RuleNameLabel.Name = "RuleNameLabel";
            this.RuleNameLabel.Size = new System.Drawing.Size(84, 13);
            this.RuleNameLabel.TabIndex = 7;
            this.RuleNameLabel.Text = "Name (optional):";
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
            this.NewConditionButton.Text = "New Condition";
            this.NewConditionButton.UseVisualStyleBackColor = true;
            this.NewConditionButton.Click += new System.EventHandler(this.NewConditionButton_Click);
            // 
            // ConditionFirstComboBox
            // 
            this.ConditionFirstComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ConditionFirstComboBox.FormattingEnabled = true;
            this.ConditionFirstComboBox.ImeMode = System.Windows.Forms.ImeMode.Disable;
            this.ConditionFirstComboBox.Location = new System.Drawing.Point(132, 163);
            this.ConditionFirstComboBox.Name = "ConditionFirstComboBox";
            this.ConditionFirstComboBox.Size = new System.Drawing.Size(121, 21);
            this.ConditionFirstComboBox.TabIndex = 10;
            this.ConditionFirstComboBox.Items.Add("Regex");
            this.ConditionFirstComboBox.Items.Add("Character Count");
            // 
            // ConditionRegexTextBox
            // 
            this.ConditionRegexTextBox.AllowDrop = true;
            this.ConditionRegexTextBox.Location = new System.Drawing.Point(260, 164);
            this.ConditionRegexTextBox.Name = "ConditionRegexTextBox";
            this.ConditionRegexTextBox.Size = new System.Drawing.Size(100, 20);
            this.ConditionRegexTextBox.TabIndex = 11;
            this.ConditionRegexTextBox.Text = "Regex String";
            this.ConditionRegexTextBox.Visible = false;
            // 
            // ChooseAreaCheckbox
            // 
            this.ChooseAreaCheckbox.AutoSize = true;
            this.ChooseAreaCheckbox.Location = new System.Drawing.Point(15, 309);
            this.ChooseAreaCheckbox.Name = "ChooseAreaCheckbox";
            this.ChooseAreaCheckbox.Size = new System.Drawing.Size(117, 17);
            this.ChooseAreaCheckbox.TabIndex = 12;
            this.ChooseAreaCheckbox.Text = "Apply Rule on Area";
            this.ChooseAreaCheckbox.UseVisualStyleBackColor = true;
            // 
            // RuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.CancelButton;
            this.ClientSize = new System.Drawing.Size(464, 344);
            this.Controls.Add(this.ChooseAreaCheckbox);
            this.Controls.Add(this.ConditionRegexTextBox);
            this.Controls.Add(this.ConditionFirstComboBox);
            this.Controls.Add(this.NewConditionButton);
            this.Controls.Add(this.RuleNameTextBox);
            this.Controls.Add(this.RuleNameLabel);
            this.Controls.Add(this.ConditionLabel);
            this.Controls.Add(this.FilterRadioButtonNONE);
            this.Controls.Add(this.FilterRadioButtonOR);
            this.Controls.Add(this.FilterRadioButtonAND);
            this.Controls.Add(this.FilterConditionLabel);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.CancelButton);
            this.Name = "RuleEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Create new Rule";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.Label FilterConditionLabel;
        private System.Windows.Forms.RadioButton FilterRadioButtonAND;
        private System.Windows.Forms.RadioButton FilterRadioButtonOR;
        private System.Windows.Forms.RadioButton FilterRadioButtonNONE;
        private System.Windows.Forms.Label ConditionLabel;
        private System.Windows.Forms.Label RuleNameLabel;
        private System.Windows.Forms.TextBox RuleNameTextBox;
        private System.Windows.Forms.Button NewConditionButton;
        private System.Windows.Forms.ComboBox ConditionFirstComboBox;
        private System.Windows.Forms.TextBox ConditionRegexTextBox;
        private System.Windows.Forms.CheckBox ChooseAreaCheckbox;
    }
}