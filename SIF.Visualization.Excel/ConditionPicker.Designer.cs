namespace SIF.Visualization.Excel
{
    partial class ConditionPicker
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
            this.ConditionNameLabel = new System.Windows.Forms.Label();
            this.ConditionNameTextBox = new System.Windows.Forms.TextBox();
            this.ConditionComponentsPanel = new System.Windows.Forms.Panel();
            this.ConditionFirstPanel = new System.Windows.Forms.Panel();
            this.Choose2CommaButton = new System.Windows.Forms.Button();
            this.Choose1CommaButton = new System.Windows.Forms.Button();
            this.ChooseOnlyNumbersButton = new System.Windows.Forms.Button();
            this.ChooseEmptyButton = new System.Windows.Forms.Button();
            this.ConditionFirstPanelLabel = new System.Windows.Forms.Label();
            this.ChooseRegexButton = new System.Windows.Forms.Button();
            this.ChooseCharacterCountButton = new System.Windows.Forms.Button();
            this.ConditionSecondPanel = new System.Windows.Forms.Panel();
            this.ConditionSecondPanelLabel = new System.Windows.Forms.Label();
            this.RegexTextBox = new System.Windows.Forms.TextBox();
            this.CharacterCountTextBox = new System.Windows.Forms.TextBox();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.ConditionComponentsPanel.SuspendLayout();
            this.ConditionFirstPanel.SuspendLayout();
            this.ConditionSecondPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // ConditionNameLabel
            // 
            this.ConditionNameLabel.AutoSize = true;
            this.ConditionNameLabel.Location = new System.Drawing.Point(18, 16);
            this.ConditionNameLabel.Name = "ConditionNameLabel";
            this.ConditionNameLabel.Size = new System.Drawing.Size(115, 13);
            this.ConditionNameLabel.TabIndex = 0;
            this.ConditionNameLabel.Text = "Name of the Condition:";
            // 
            // ConditionNameTextBox
            // 
            this.ConditionNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConditionNameTextBox.Location = new System.Drawing.Point(139, 13);
            this.ConditionNameTextBox.Name = "ConditionNameTextBox";
            this.ConditionNameTextBox.Size = new System.Drawing.Size(367, 20);
            this.ConditionNameTextBox.TabIndex = 1;
            // 
            // ConditionComponentsPanel
            // 
            this.ConditionComponentsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConditionComponentsPanel.Controls.Add(this.ConditionFirstPanel);
            this.ConditionComponentsPanel.Controls.Add(this.ConditionSecondPanel);
            this.ConditionComponentsPanel.Location = new System.Drawing.Point(11, 49);
            this.ConditionComponentsPanel.Name = "ConditionComponentsPanel";
            this.ConditionComponentsPanel.Padding = new System.Windows.Forms.Padding(10);
            this.ConditionComponentsPanel.Size = new System.Drawing.Size(496, 263);
            this.ConditionComponentsPanel.TabIndex = 2;
            // 
            // ConditionFirstPanel
            // 
            this.ConditionFirstPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConditionFirstPanel.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.ConditionFirstPanel.Controls.Add(this.Choose2CommaButton);
            this.ConditionFirstPanel.Controls.Add(this.Choose1CommaButton);
            this.ConditionFirstPanel.Controls.Add(this.ChooseOnlyNumbersButton);
            this.ConditionFirstPanel.Controls.Add(this.ChooseEmptyButton);
            this.ConditionFirstPanel.Controls.Add(this.ConditionFirstPanelLabel);
            this.ConditionFirstPanel.Controls.Add(this.ChooseRegexButton);
            this.ConditionFirstPanel.Controls.Add(this.ChooseCharacterCountButton);
            this.ConditionFirstPanel.Location = new System.Drawing.Point(13, 13);
            this.ConditionFirstPanel.Name = "ConditionFirstPanel";
            this.ConditionFirstPanel.Padding = new System.Windows.Forms.Padding(9);
            this.ConditionFirstPanel.Size = new System.Drawing.Size(470, 125);
            this.ConditionFirstPanel.TabIndex = 1;
            // 
            // Choose2CommaButton
            // 
            this.Choose2CommaButton.Location = new System.Drawing.Point(318, 76);
            this.Choose2CommaButton.Name = "Choose2CommaButton";
            this.Choose2CommaButton.Size = new System.Drawing.Size(140, 30);
            this.Choose2CommaButton.TabIndex = 6;
            this.Choose2CommaButton.Text = "2 Comma";
            this.Choose2CommaButton.UseVisualStyleBackColor = true;
            this.Choose2CommaButton.Click += new System.EventHandler(this.Choose2CommaButton_Click);
            // 
            // Choose1CommaButton
            // 
            this.Choose1CommaButton.Location = new System.Drawing.Point(165, 76);
            this.Choose1CommaButton.Name = "Choose1CommaButton";
            this.Choose1CommaButton.Size = new System.Drawing.Size(140, 30);
            this.Choose1CommaButton.TabIndex = 5;
            this.Choose1CommaButton.Text = "1 Comma";
            this.Choose1CommaButton.UseVisualStyleBackColor = true;
            this.Choose1CommaButton.Click += new System.EventHandler(this.Choose1CommaButton_Click);
            // 
            // ChooseOnlyNumbersButton
            // 
            this.ChooseOnlyNumbersButton.Location = new System.Drawing.Point(15, 76);
            this.ChooseOnlyNumbersButton.Name = "ChooseOnlyNumbersButton";
            this.ChooseOnlyNumbersButton.Size = new System.Drawing.Size(140, 30);
            this.ChooseOnlyNumbersButton.TabIndex = 4;
            this.ChooseOnlyNumbersButton.Text = "OnlyNumbers";
            this.ChooseOnlyNumbersButton.UseVisualStyleBackColor = true;
            this.ChooseOnlyNumbersButton.Click += new System.EventHandler(this.ChooseOnlyNumbers_Click);
            // 
            // ChooseEmptyButton
            // 
            this.ChooseEmptyButton.Location = new System.Drawing.Point(165, 32);
            this.ChooseEmptyButton.Name = "ChooseEmptyButton";
            this.ChooseEmptyButton.Size = new System.Drawing.Size(140, 30);
            this.ChooseEmptyButton.TabIndex = 3;
            this.ChooseEmptyButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_ConditionPicker_Empty;
            this.ChooseEmptyButton.UseVisualStyleBackColor = true;
            this.ChooseEmptyButton.Click += new System.EventHandler(this.ChooseEmptyButton_Click);
            // 
            // ConditionFirstPanelLabel
            // 
            this.ConditionFirstPanelLabel.AutoSize = true;
            this.ConditionFirstPanelLabel.Location = new System.Drawing.Point(12, 9);
            this.ConditionFirstPanelLabel.Name = "ConditionFirstPanelLabel";
            this.ConditionFirstPanelLabel.Size = new System.Drawing.Size(220, 13);
            this.ConditionFirstPanelLabel.TabIndex = 1;
            this.ConditionFirstPanelLabel.Text = "Please choose the type of the new condition.";
            // 
            // ChooseRegexButton
            // 
            this.ChooseRegexButton.Location = new System.Drawing.Point(12, 32);
            this.ChooseRegexButton.Margin = new System.Windows.Forms.Padding(10);
            this.ChooseRegexButton.Name = "ChooseRegexButton";
            this.ChooseRegexButton.Size = new System.Drawing.Size(140, 30);
            this.ChooseRegexButton.TabIndex = 0;
            this.ChooseRegexButton.Text = "Regex";
            this.ChooseRegexButton.UseVisualStyleBackColor = true;
            this.ChooseRegexButton.Click += new System.EventHandler(this.ChooseRegex_Click);
            // 
            // ChooseCharacterCountButton
            // 
            this.ChooseCharacterCountButton.Location = new System.Drawing.Point(318, 32);
            this.ChooseCharacterCountButton.Margin = new System.Windows.Forms.Padding(10);
            this.ChooseCharacterCountButton.Name = "ChooseCharacterCountButton";
            this.ChooseCharacterCountButton.Size = new System.Drawing.Size(140, 30);
            this.ChooseCharacterCountButton.TabIndex = 2;
            this.ChooseCharacterCountButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_Condition_CharacterCount;
            this.ChooseCharacterCountButton.UseVisualStyleBackColor = true;
            this.ChooseCharacterCountButton.Click += new System.EventHandler(this.ChooseCharacterCountButton_Click);
            // 
            // ConditionSecondPanel
            // 
            this.ConditionSecondPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConditionSecondPanel.Controls.Add(this.ConditionSecondPanelLabel);
            this.ConditionSecondPanel.Controls.Add(this.RegexTextBox);
            this.ConditionSecondPanel.Controls.Add(this.CharacterCountTextBox);
            this.ConditionSecondPanel.Location = new System.Drawing.Point(13, 144);
            this.ConditionSecondPanel.Name = "ConditionSecondPanel";
            this.ConditionSecondPanel.Size = new System.Drawing.Size(470, 101);
            this.ConditionSecondPanel.TabIndex = 3;
            // 
            // ConditionSecondPanelLabel
            // 
            this.ConditionSecondPanelLabel.AutoEllipsis = true;
            this.ConditionSecondPanelLabel.AutoSize = true;
            this.ConditionSecondPanelLabel.Location = new System.Drawing.Point(9, 9);
            this.ConditionSecondPanelLabel.Name = "ConditionSecondPanelLabel";
            this.ConditionSecondPanelLabel.Size = new System.Drawing.Size(489, 13);
            this.ConditionSecondPanelLabel.TabIndex = 0;
            this.ConditionSecondPanelLabel.Text = "Bitte geben sie eine Zahl ein. Diese Zahl legt die maximal erlaubte Anzahl von Ze" +
    "ichen einer Zelle fest.";
            // 
            // RegexTextBox
            // 
            this.RegexTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RegexTextBox.Location = new System.Drawing.Point(12, 68);
            this.RegexTextBox.Name = "RegexTextBox";
            this.RegexTextBox.Size = new System.Drawing.Size(446, 20);
            this.RegexTextBox.TabIndex = 0;
            this.RegexTextBox.Visible = false;
            // 
            // CharacterCountTextBox
            // 
            this.CharacterCountTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CharacterCountTextBox.Location = new System.Drawing.Point(12, 68);
            this.CharacterCountTextBox.Name = "CharacterCountTextBox";
            this.CharacterCountTextBox.Size = new System.Drawing.Size(446, 20);
            this.CharacterCountTextBox.TabIndex = 2;
            this.CharacterCountTextBox.Visible = false;
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.AutoSize = true;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(431, 347);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_Cancel;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmButton.AutoSize = true;
            this.ConfirmButton.Location = new System.Drawing.Point(330, 347);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(95, 23);
            this.ConfirmButton.TabIndex = 4;
            this.ConfirmButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_ConditionPicker_Confirm;
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // ConditionPicker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(519, 377);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ConditionComponentsPanel);
            this.Controls.Add(this.ConditionNameTextBox);
            this.Controls.Add(this.ConditionNameLabel);
            this.Name = "ConditionPicker";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bedingung wählen";
            this.TopMost = true;
            this.ConditionComponentsPanel.ResumeLayout(false);
            this.ConditionFirstPanel.ResumeLayout(false);
            this.ConditionFirstPanel.PerformLayout();
            this.ConditionSecondPanel.ResumeLayout(false);
            this.ConditionSecondPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label ConditionNameLabel;
        private System.Windows.Forms.TextBox ConditionNameTextBox;
        private System.Windows.Forms.Panel ConditionComponentsPanel;
        private System.Windows.Forms.Panel ConditionSecondPanel;
        private System.Windows.Forms.Button ChooseCharacterCountButton;
        private System.Windows.Forms.Panel ConditionFirstPanel;
        private System.Windows.Forms.Button ChooseRegexButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ConfirmButton;
        private System.Windows.Forms.TextBox RegexTextBox;
        private System.Windows.Forms.Label ConditionFirstPanelLabel;
        private System.Windows.Forms.TextBox CharacterCountTextBox;
        private System.Windows.Forms.Label ConditionSecondPanelLabel;
        private System.Windows.Forms.Button ChooseEmptyButton;
        private System.Windows.Forms.Button Choose2CommaButton;
        private System.Windows.Forms.Button Choose1CommaButton;
        private System.Windows.Forms.Button ChooseOnlyNumbersButton;
    }
}