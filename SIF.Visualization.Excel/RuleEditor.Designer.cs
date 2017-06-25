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
            this.ConditionPanel = new System.Windows.Forms.Panel();
            this.RuleAreaLabel = new System.Windows.Forms.Label();
            this.CellAreaBox = new System.Windows.Forms.TextBox();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.ChooseCellButton = new System.Windows.Forms.Button();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.RuleDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.ConditionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // CancelButton
            // 
            this.CancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CancelButton.AutoSize = true;
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(598, 498);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 0;
            this.CancelButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_Cancel;
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ConfirmButton
            // 
            this.ConfirmButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfirmButton.AutoSize = true;
            this.ConfirmButton.Location = new System.Drawing.Point(496, 498);
            this.ConfirmButton.Name = "ConfirmButton";
            this.ConfirmButton.Size = new System.Drawing.Size(96, 23);
            this.ConfirmButton.TabIndex = 1;
            this.ConfirmButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_Confirm;
            this.ConfirmButton.UseVisualStyleBackColor = true;
            this.ConfirmButton.Click += new System.EventHandler(this.ConfirmButton_Click);
            // 
            // FilterConditionLabel
            // 
            this.FilterConditionLabel.AutoSize = true;
            this.FilterConditionLabel.Location = new System.Drawing.Point(12, 69);
            this.FilterConditionLabel.Name = "FilterConditionLabel";
            this.FilterConditionLabel.Size = new System.Drawing.Size(72, 13);
            this.FilterConditionLabel.TabIndex = 2;
            this.FilterConditionLabel.Text = "Trigger when:";
            // 
            // FilterRadioButtonAND
            // 
            this.FilterRadioButtonAND.AutoSize = true;
            this.FilterRadioButtonAND.Location = new System.Drawing.Point(132, 67);
            this.FilterRadioButtonAND.Name = "FilterRadioButtonAND";
            this.FilterRadioButtonAND.Size = new System.Drawing.Size(146, 17);
            this.FilterRadioButtonAND.TabIndex = 3;
            this.FilterRadioButtonAND.TabStop = true;
            this.FilterRadioButtonAND.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_FilterConditionAND;
            this.FilterRadioButtonAND.UseVisualStyleBackColor = true;
            // 
            // FilterRadioButtonOR
            // 
            this.FilterRadioButtonOR.AutoSize = true;
            this.FilterRadioButtonOR.Location = new System.Drawing.Point(132, 90);
            this.FilterRadioButtonOR.Name = "FilterRadioButtonOR";
            this.FilterRadioButtonOR.Size = new System.Drawing.Size(184, 17);
            this.FilterRadioButtonOR.TabIndex = 4;
            this.FilterRadioButtonOR.TabStop = true;
            this.FilterRadioButtonOR.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_FilterConditionOR;
            this.FilterRadioButtonOR.UseVisualStyleBackColor = true;
            // 
            // FilterRadioButtonNONE
            // 
            this.FilterRadioButtonNONE.AutoSize = true;
            this.FilterRadioButtonNONE.Location = new System.Drawing.Point(132, 113);
            this.FilterRadioButtonNONE.Name = "FilterRadioButtonNONE";
            this.FilterRadioButtonNONE.Size = new System.Drawing.Size(137, 17);
            this.FilterRadioButtonNONE.TabIndex = 5;
            this.FilterRadioButtonNONE.TabStop = true;
            this.FilterRadioButtonNONE.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_FilterConditionNONE;
            this.FilterRadioButtonNONE.UseVisualStyleBackColor = true;
            // 
            // ConditionLabel
            // 
            this.ConditionLabel.AutoSize = true;
            this.ConditionLabel.Location = new System.Drawing.Point(12, 200);
            this.ConditionLabel.Name = "ConditionLabel";
            this.ConditionLabel.Size = new System.Drawing.Size(65, 13);
            this.ConditionLabel.TabIndex = 6;
            this.ConditionLabel.Text = "Condition(s):";
            // 
            // RuleNameLabel
            // 
            this.RuleNameLabel.AutoSize = true;
            this.RuleNameLabel.Location = new System.Drawing.Point(12, 16);
            this.RuleNameLabel.Name = "RuleNameLabel";
            this.RuleNameLabel.Size = new System.Drawing.Size(38, 13);
            this.RuleNameLabel.TabIndex = 7;
            this.RuleNameLabel.Text = "Name:";
            // 
            // RuleNameTextBox
            // 
            this.RuleNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RuleNameTextBox.Location = new System.Drawing.Point(132, 13);
            this.RuleNameTextBox.Name = "RuleNameTextBox";
            this.RuleNameTextBox.Size = new System.Drawing.Size(541, 20);
            this.RuleNameTextBox.TabIndex = 8;
            // 
            // NewConditionButton
            // 
            this.NewConditionButton.Location = new System.Drawing.Point(13, 13);
            this.NewConditionButton.Name = "NewConditionButton";
            this.NewConditionButton.Size = new System.Drawing.Size(105, 23);
            this.NewConditionButton.TabIndex = 9;
            this.NewConditionButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_NewCondition;
            this.NewConditionButton.UseVisualStyleBackColor = true;
            this.NewConditionButton.Click += new System.EventHandler(this.NewConditionButton_Click);
            // 
            // ConditionPanel
            // 
            this.ConditionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ConditionPanel.AutoSize = true;
            this.ConditionPanel.Controls.Add(this.NewConditionButton);
            this.ConditionPanel.Location = new System.Drawing.Point(132, 182);
            this.ConditionPanel.Name = "ConditionPanel";
            this.ConditionPanel.Padding = new System.Windows.Forms.Padding(10);
            this.ConditionPanel.Size = new System.Drawing.Size(544, 310);
            this.ConditionPanel.TabIndex = 13;
            // 
            // RuleAreaLabel
            // 
            this.RuleAreaLabel.AutoSize = true;
            this.RuleAreaLabel.Location = new System.Drawing.Point(12, 48);
            this.RuleAreaLabel.Name = "RuleAreaLabel";
            this.RuleAreaLabel.Size = new System.Drawing.Size(52, 13);
            this.RuleAreaLabel.TabIndex = 14;
            this.RuleAreaLabel.Text = "Cell Area:";
            // 
            // CellAreaBox
            // 
            this.CellAreaBox.Location = new System.Drawing.Point(132, 41);
            this.CellAreaBox.Name = "CellAreaBox";
            this.CellAreaBox.Size = new System.Drawing.Size(379, 20);
            this.CellAreaBox.TabIndex = 15;
            // 
            // ChooseCellButton
            // 
            this.ChooseCellButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChooseCellButton.AutoSize = true;
            this.ChooseCellButton.Location = new System.Drawing.Point(517, 38);
            this.ChooseCellButton.Name = "ChooseCellButton";
            this.ChooseCellButton.Size = new System.Drawing.Size(156, 23);
            this.ChooseCellButton.TabIndex = 16;
            this.ChooseCellButton.Text = global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_CellPicker;
            this.ChooseCellButton.UseVisualStyleBackColor = true;
            this.ChooseCellButton.Click += new System.EventHandler(this.ChooseCellButton_Click);
            // 
            // DescriptionLabel
            // 
            this.DescriptionLabel.AutoSize = true;
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 149);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(88, 13);
            this.DescriptionLabel.TabIndex = 17;
            this.DescriptionLabel.Text = "Rule Description:";
            // 
            // RuleDescriptionTextBox
            // 
            this.RuleDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RuleDescriptionTextBox.Location = new System.Drawing.Point(132, 137);
            this.RuleDescriptionTextBox.Name = "RuleDescriptionTextBox";
            this.RuleDescriptionTextBox.Size = new System.Drawing.Size(541, 39);
            this.RuleDescriptionTextBox.TabIndex = 18;
            this.RuleDescriptionTextBox.Text = "";
            // 
            // RuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(685, 533);
            this.Controls.Add(this.RuleDescriptionTextBox);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.ChooseCellButton);
            this.Controls.Add(this.CellAreaBox);
            this.Controls.Add(this.RuleAreaLabel);
            this.Controls.Add(this.RuleNameTextBox);
            this.Controls.Add(this.RuleNameLabel);
            this.Controls.Add(this.FilterRadioButtonNONE);
            this.Controls.Add(this.FilterRadioButtonOR);
            this.Controls.Add(this.FilterRadioButtonAND);
            this.Controls.Add(this.FilterConditionLabel);
            this.Controls.Add(this.ConfirmButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.ConditionPanel);
            this.Controls.Add(this.ConditionLabel);
            this.Name = "RuleEditor";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rule Editor";
            this.TopMost = true;
            this.ConditionPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Panel ConditionPanel;
        private System.Windows.Forms.Label RuleAreaLabel;
        private System.Windows.Forms.TextBox CellAreaBox;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button ChooseCellButton;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.RichTextBox RuleDescriptionTextBox;



        
    }
}