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
            this.components = new System.ComponentModel.Container();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ConfirmButton = new System.Windows.Forms.Button();
            this.ConditionLabel = new System.Windows.Forms.Label();
            this.RuleNameLabel = new System.Windows.Forms.Label();
            this.RuleNameTextBox = new System.Windows.Forms.TextBox();
            this.NewConditionButton = new System.Windows.Forms.Button();
            this.ConditionPanel = new System.Windows.Forms.Panel();
            this.RuleAreaLabel = new System.Windows.Forms.Label();
            this.CellAreaBox = new System.Windows.Forms.TextBox();
            this.ChooseCellButton = new System.Windows.Forms.Button();
            this.DescriptionLabel = new System.Windows.Forms.Label();
            this.RuleDescriptionTextBox = new System.Windows.Forms.RichTextBox();
            this.ToolTipName = new System.Windows.Forms.ToolTip(this.components);
            this.TooltipLabelName = new System.Windows.Forms.Label();
            this.ToolTipLabelCellArea = new System.Windows.Forms.Label();
            this.ToolTipLabelDescription = new System.Windows.Forms.Label();
            this.TooltipLabelCondition = new System.Windows.Forms.Label();
            this.ToolTipCellArea = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTipDescription = new System.Windows.Forms.ToolTip(this.components);
            this.ToolTipCondition = new System.Windows.Forms.ToolTip(this.components);
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
            // ConditionLabel
            // 
            this.ConditionLabel.AutoSize = true;
            this.ConditionLabel.Location = new System.Drawing.Point(12, 125);
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
            this.NewConditionButton.Image = global::SIF.Visualization.Excel.Properties.Resources.plus;
            this.NewConditionButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.ConditionPanel.Location = new System.Drawing.Point(125, 112);
            this.ConditionPanel.Name = "ConditionPanel";
            this.ConditionPanel.Padding = new System.Windows.Forms.Padding(10);
            this.ConditionPanel.Size = new System.Drawing.Size(550, 380);
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
            this.DescriptionLabel.Location = new System.Drawing.Point(12, 70);
            this.DescriptionLabel.Name = "DescriptionLabel";
            this.DescriptionLabel.Size = new System.Drawing.Size(88, 13);
            this.DescriptionLabel.TabIndex = 17;
            this.DescriptionLabel.Text = "Rule Description:";
            // 
            // RuleDescriptionTextBox
            // 
            this.RuleDescriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RuleDescriptionTextBox.Location = new System.Drawing.Point(132, 67);
            this.RuleDescriptionTextBox.Name = "RuleDescriptionTextBox";
            this.RuleDescriptionTextBox.Size = new System.Drawing.Size(541, 39);
            this.RuleDescriptionTextBox.TabIndex = 18;
            this.RuleDescriptionTextBox.Text = "";
            // 
            // ToolTipName
            // 
            this.ToolTipName.AutomaticDelay = 0;
            this.ToolTipName.AutoPopDelay = 5000;
            this.ToolTipName.InitialDelay = 0;
            this.ToolTipName.IsBalloon = true;
            this.ToolTipName.ReshowDelay = 100;
            // 
            // TooltipLabelName
            // 
            this.TooltipLabelName.AutoSize = true;
            this.TooltipLabelName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TooltipLabelName.Location = new System.Drawing.Point(50, 16);
            this.TooltipLabelName.Name = "TooltipLabelName";
            this.TooltipLabelName.Size = new System.Drawing.Size(14, 13);
            this.TooltipLabelName.TabIndex = 19;
            this.TooltipLabelName.Text = "?";
            this.ToolTipName.SetToolTip(this.TooltipLabelName, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_Name);
            // 
            // ToolTipLabelCellArea
            // 
            this.ToolTipLabelCellArea.AutoSize = true;
            this.ToolTipLabelCellArea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolTipLabelCellArea.Location = new System.Drawing.Point(63, 48);
            this.ToolTipLabelCellArea.Name = "ToolTipLabelCellArea";
            this.ToolTipLabelCellArea.Size = new System.Drawing.Size(14, 13);
            this.ToolTipLabelCellArea.TabIndex = 20;
            this.ToolTipLabelCellArea.Text = "?";
            this.ToolTipCellArea.SetToolTip(this.ToolTipLabelCellArea, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_CellArea);
            // 
            // ToolTipLabelDescription
            // 
            this.ToolTipLabelDescription.AutoSize = true;
            this.ToolTipLabelDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ToolTipLabelDescription.Location = new System.Drawing.Point(106, 70);
            this.ToolTipLabelDescription.Name = "ToolTipLabelDescription";
            this.ToolTipLabelDescription.Size = new System.Drawing.Size(14, 13);
            this.ToolTipLabelDescription.TabIndex = 21;
            this.ToolTipLabelDescription.Text = "?";
            this.ToolTipDescription.SetToolTip(this.ToolTipLabelDescription, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_Description);
            // 
            // TooltipLabelCondition
            // 
            this.TooltipLabelCondition.AutoSize = true;
            this.TooltipLabelCondition.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TooltipLabelCondition.Location = new System.Drawing.Point(83, 125);
            this.TooltipLabelCondition.Name = "TooltipLabelCondition";
            this.TooltipLabelCondition.Size = new System.Drawing.Size(14, 13);
            this.TooltipLabelCondition.TabIndex = 22;
            this.TooltipLabelCondition.Text = "?";
            this.ToolTipCondition.SetToolTip(this.TooltipLabelCondition, global::SIF.Visualization.Excel.Properties.Resources.tl_RuleEditor_ToolTip_Condition);
            // 
            // ToolTipCellArea
            // 
            this.ToolTipCellArea.AutomaticDelay = 0;
            this.ToolTipCellArea.AutoPopDelay = 5000;
            this.ToolTipCellArea.InitialDelay = 0;
            this.ToolTipCellArea.IsBalloon = true;
            this.ToolTipCellArea.ReshowDelay = 100;
            // 
            // ToolTipDescription
            // 
            this.ToolTipDescription.AutomaticDelay = 0;
            this.ToolTipDescription.AutoPopDelay = 5000;
            this.ToolTipDescription.InitialDelay = 0;
            this.ToolTipDescription.IsBalloon = true;
            this.ToolTipDescription.ReshowDelay = 100;
            // 
            // ToolTipCondition
            // 
            this.ToolTipCondition.AutomaticDelay = 0;
            this.ToolTipCondition.AutoPopDelay = 5000;
            this.ToolTipCondition.InitialDelay = 0;
            this.ToolTipCondition.IsBalloon = true;
            this.ToolTipCondition.ReshowDelay = 100;
            // 
            // RuleEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(685, 533);
            this.Controls.Add(this.TooltipLabelCondition);
            this.Controls.Add(this.ToolTipLabelDescription);
            this.Controls.Add(this.ToolTipLabelCellArea);
            this.Controls.Add(this.TooltipLabelName);
            this.Controls.Add(this.RuleDescriptionTextBox);
            this.Controls.Add(this.DescriptionLabel);
            this.Controls.Add(this.ChooseCellButton);
            this.Controls.Add(this.CellAreaBox);
            this.Controls.Add(this.RuleAreaLabel);
            this.Controls.Add(this.RuleNameTextBox);
            this.Controls.Add(this.RuleNameLabel);
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
        private System.Windows.Forms.Label ConditionLabel;
        private System.Windows.Forms.Label RuleNameLabel;
        private System.Windows.Forms.TextBox RuleNameTextBox;
        private System.Windows.Forms.Button NewConditionButton;
        private System.Windows.Forms.Panel ConditionPanel;
        private System.Windows.Forms.Label RuleAreaLabel;
        private System.Windows.Forms.TextBox CellAreaBox;
        private System.Windows.Forms.Button ChooseCellButton;
        private System.Windows.Forms.Label DescriptionLabel;
        private System.Windows.Forms.RichTextBox RuleDescriptionTextBox;
        private System.Windows.Forms.ToolTip ToolTipName;
        private System.Windows.Forms.Label TooltipLabelName;
        private System.Windows.Forms.Label ToolTipLabelCellArea;
        private System.Windows.Forms.Label ToolTipLabelDescription;
        private System.Windows.Forms.Label TooltipLabelCondition;
        private System.Windows.Forms.ToolTip ToolTipCellArea;
        private System.Windows.Forms.ToolTip ToolTipDescription;
        private System.Windows.Forms.ToolTip ToolTipCondition;



        
    }
}