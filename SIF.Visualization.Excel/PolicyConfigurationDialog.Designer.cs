using SIF.Visualization.Excel.Properties;

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
            this.NCIF_M = new System.Windows.Forms.RadioButton();
            this.RD_M = new System.Windows.Forms.RadioButton();
            this.FC_M = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.NCIF_A = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.RD_A = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.FC_A = new System.Windows.Forms.RadioButton();
            this.CB_FormulaComplexity = new System.Windows.Forms.CheckBox();
            this.CB_ReadingDirection = new System.Windows.Forms.CheckBox();
            this.CB_NoConstantsInFormulas = new System.Windows.Forms.CheckBox();
            this.CB_MultipleSameRef = new System.Windows.Forms.CheckBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.MSR_A = new System.Windows.Forms.RadioButton();
            this.MSR_M = new System.Windows.Forms.RadioButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.NCC_A = new System.Windows.Forms.RadioButton();
            this.NCC_M = new System.Windows.Forms.RadioButton();
            this.CB_NonConsideredConstants = new System.Windows.Forms.CheckBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.RTN_A = new System.Windows.Forms.RadioButton();
            this.RTN_M = new System.Windows.Forms.RadioButton();
            this.CB_RefToNull = new System.Windows.Forms.CheckBox();
            this.panel7 = new System.Windows.Forms.Panel();
            this.OAO_A = new System.Windows.Forms.RadioButton();
            this.OAO_M = new System.Windows.Forms.RadioButton();
            this.CB_OneAmongOthers = new System.Windows.Forms.CheckBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.SD_A = new System.Windows.Forms.RadioButton();
            this.SD_M = new System.Windows.Forms.RadioButton();
            this.CB_StringDistance = new System.Windows.Forms.CheckBox();
            this.lb_MaxDistance = new System.Windows.Forms.Label();
            this.SD_Amount = new System.Windows.Forms.TextBox();
            this.cb_Ask_Thousands = new System.Windows.Forms.CheckBox();
            this.panel9 = new System.Windows.Forms.Panel();
            this.EIC_A = new System.Windows.Forms.RadioButton();
            this.EIC_M = new System.Windows.Forms.RadioButton();
            this.CB_ErrorInCells = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel8.SuspendLayout();
            this.panel9.SuspendLayout();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            this.Button_OK.Location = new System.Drawing.Point(245, 312);
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.Size = new System.Drawing.Size(98, 23);
            this.Button_OK.TabIndex = 9;
            this.Button_OK.Text = Resources.tl_PolicyConfiguration_Button_Ok;
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            this.Button_Cancel.Location = new System.Drawing.Point(352, 312);
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.Size = new System.Drawing.Size(75, 23);
            this.Button_Cancel.TabIndex = 10;
            this.Button_Cancel.Text = Resources.tl_PolicyConfiguration_Button_Cancel;
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // NCIF_M
            // 
            this.NCIF_M.AutoSize = true;
            this.NCIF_M.Enabled = false;
            this.NCIF_M.Location = new System.Drawing.Point(60, 3);
            this.NCIF_M.Name = "NCIF_M";
            this.NCIF_M.Size = new System.Drawing.Size(60, 17);
            this.NCIF_M.TabIndex = 12;
            this.NCIF_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.NCIF_M.UseVisualStyleBackColor = true;
            // 
            // RD_M
            // 
            this.RD_M.AutoSize = true;
            this.RD_M.Enabled = false;
            this.RD_M.Location = new System.Drawing.Point(60, 5);
            this.RD_M.Name = "RD_M";
            this.RD_M.Size = new System.Drawing.Size(60, 17);
            this.RD_M.TabIndex = 14;
            this.RD_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.RD_M.UseVisualStyleBackColor = true;
            // 
            // FC_M
            // 
            this.FC_M.AutoSize = true;
            this.FC_M.Enabled = false;
            this.FC_M.Location = new System.Drawing.Point(59, 3);
            this.FC_M.Name = "FC_M";
            this.FC_M.Size = new System.Drawing.Size(60, 17);
            this.FC_M.TabIndex = 16;
            this.FC_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.FC_M.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.NCIF_A);
            this.panel1.Controls.Add(this.NCIF_M);
            this.panel1.Location = new System.Drawing.Point(31, 29);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(161, 26);
            this.panel1.TabIndex = 17;
            // 
            // NCIF_A
            // 
            this.NCIF_A.AutoSize = true;
            this.NCIF_A.Checked = true;
            this.NCIF_A.Enabled = false;
            this.NCIF_A.Location = new System.Drawing.Point(1, 3);
            this.NCIF_A.Name = "NCIF_A";
            this.NCIF_A.Size = new System.Drawing.Size(58, 17);
            this.NCIF_A.TabIndex = 11;
            this.NCIF_A.TabStop = true;
            this.NCIF_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.NCIF_A.UseVisualStyleBackColor = true;
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
            this.RD_A.Checked = true;
            this.RD_A.Enabled = false;
            this.RD_A.Location = new System.Drawing.Point(1, 3);
            this.RD_A.Name = "RD_A";
            this.RD_A.Size = new System.Drawing.Size(58, 17);
            this.RD_A.TabIndex = 13;
            this.RD_A.TabStop = true;
            this.RD_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
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
            this.FC_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.FC_A.UseVisualStyleBackColor = true;
            // 
            // CB_FormulaComplexity
            // 
            this.CB_FormulaComplexity.AutoSize = true;
            this.CB_FormulaComplexity.Location = new System.Drawing.Point(12, 104);
            this.CB_FormulaComplexity.Name = "CB_FormulaComplexity";
            this.CB_FormulaComplexity.Size = new System.Drawing.Size(116, 17);
            this.CB_FormulaComplexity.TabIndex = 6;
            this.CB_FormulaComplexity.Text = Resources.tl_PolicyConfiguration_Rule_FormulaComplexity;
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
            this.CB_ReadingDirection.Text = Resources.tl_PolicyConfiguration_Rule_ReadingDirection;
            this.CB_ReadingDirection.UseVisualStyleBackColor = true;
            this.CB_ReadingDirection.CheckedChanged += new System.EventHandler(this.CB_ReadingDirection_CheckedChanged);
            // 
            // CB_NoConstantsInFormulas
            // 
            this.CB_NoConstantsInFormulas.AutoSize = true;
            this.CB_NoConstantsInFormulas.Location = new System.Drawing.Point(12, 12);
            this.CB_NoConstantsInFormulas.Name = "CB_NoConstantsInFormulas";
            this.CB_NoConstantsInFormulas.Size = new System.Drawing.Size(146, 17);
            this.CB_NoConstantsInFormulas.TabIndex = 0;
            this.CB_NoConstantsInFormulas.Text = Resources.tl_PolicyConfiguration_No_Constants;
            this.CB_NoConstantsInFormulas.UseVisualStyleBackColor = true;
            this.CB_NoConstantsInFormulas.CheckedChanged += new System.EventHandler(this.CB_Constraints_CheckedChanged);
            // 
            // CB_MultipleSameRef
            // 
            this.CB_MultipleSameRef.AllowDrop = true;
            this.CB_MultipleSameRef.AutoSize = true;
            this.CB_MultipleSameRef.Location = new System.Drawing.Point(245, 12);
            this.CB_MultipleSameRef.Name = "CB_MultipleSameRef";
            this.CB_MultipleSameRef.Size = new System.Drawing.Size(166, 17);
            this.CB_MultipleSameRef.TabIndex = 20;
            this.CB_MultipleSameRef.Text = Resources.tl_PolicyConfiguration_Rule_SameMultipleRefs;
            this.CB_MultipleSameRef.UseVisualStyleBackColor = true;
            this.CB_MultipleSameRef.CheckedChanged += new System.EventHandler(this.CB_MultipleSameRef_CheckedChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.MSR_A);
            this.panel4.Controls.Add(this.MSR_M);
            this.panel4.Location = new System.Drawing.Point(267, 29);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(160, 23);
            this.panel4.TabIndex = 21;
            // 
            // MSR_A
            // 
            this.MSR_A.AutoSize = true;
            this.MSR_A.Checked = true;
            this.MSR_A.Enabled = false;
            this.MSR_A.Location = new System.Drawing.Point(0, 3);
            this.MSR_A.Name = "MSR_A";
            this.MSR_A.Size = new System.Drawing.Size(58, 17);
            this.MSR_A.TabIndex = 15;
            this.MSR_A.TabStop = true;
            this.MSR_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.MSR_A.UseVisualStyleBackColor = true;
            // 
            // MSR_M
            // 
            this.MSR_M.AutoSize = true;
            this.MSR_M.Enabled = false;
            this.MSR_M.Location = new System.Drawing.Point(64, 3);
            this.MSR_M.Name = "MSR_M";
            this.MSR_M.Size = new System.Drawing.Size(60, 17);
            this.MSR_M.TabIndex = 16;
            this.MSR_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.MSR_M.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.NCC_A);
            this.panel5.Controls.Add(this.NCC_M);
            this.panel5.Location = new System.Drawing.Point(267, 79);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(160, 23);
            this.panel5.TabIndex = 23;
            // 
            // NCC_A
            // 
            this.NCC_A.AutoSize = true;
            this.NCC_A.Checked = true;
            this.NCC_A.Enabled = false;
            this.NCC_A.Location = new System.Drawing.Point(0, 3);
            this.NCC_A.Name = "NCC_A";
            this.NCC_A.Size = new System.Drawing.Size(58, 17);
            this.NCC_A.TabIndex = 15;
            this.NCC_A.TabStop = true;
            this.NCC_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.NCC_A.UseVisualStyleBackColor = true;
            // 
            // NCC_M
            // 
            this.NCC_M.AutoSize = true;
            this.NCC_M.Enabled = false;
            this.NCC_M.Location = new System.Drawing.Point(64, 3);
            this.NCC_M.Name = "NCC_M";
            this.NCC_M.Size = new System.Drawing.Size(60, 17);
            this.NCC_M.TabIndex = 16;
            this.NCC_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.NCC_M.UseVisualStyleBackColor = true;
            // 
            // CB_NonConsideredConstants
            // 
            this.CB_NonConsideredConstants.AutoSize = true;
            this.CB_NonConsideredConstants.Location = new System.Drawing.Point(245, 61);
            this.CB_NonConsideredConstants.Name = "CB_NonConsideredConstants";
            this.CB_NonConsideredConstants.Size = new System.Drawing.Size(150, 17);
            this.CB_NonConsideredConstants.TabIndex = 22;
            this.CB_NonConsideredConstants.Text = Resources.tl_PolicyConfiguration_Rule_NonConsideredConstants;
            this.CB_NonConsideredConstants.UseVisualStyleBackColor = true;
            this.CB_NonConsideredConstants.CheckedChanged += new System.EventHandler(this.CB_NonConsideredConstants_CheckedChanged);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.RTN_A);
            this.panel6.Controls.Add(this.RTN_M);
            this.panel6.Location = new System.Drawing.Point(267, 122);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(160, 23);
            this.panel6.TabIndex = 25;
            // 
            // RTN_A
            // 
            this.RTN_A.AutoSize = true;
            this.RTN_A.Checked = true;
            this.RTN_A.Enabled = false;
            this.RTN_A.Location = new System.Drawing.Point(0, 3);
            this.RTN_A.Name = "RTN_A";
            this.RTN_A.Size = new System.Drawing.Size(58, 17);
            this.RTN_A.TabIndex = 15;
            this.RTN_A.TabStop = true;
            this.RTN_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.RTN_A.UseVisualStyleBackColor = true;
            // 
            // RTN_M
            // 
            this.RTN_M.AutoSize = true;
            this.RTN_M.Enabled = false;
            this.RTN_M.Location = new System.Drawing.Point(62, 3);
            this.RTN_M.Name = "RTN_M";
            this.RTN_M.Size = new System.Drawing.Size(60, 17);
            this.RTN_M.TabIndex = 16;
            this.RTN_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.RTN_M.UseVisualStyleBackColor = true;
            // 
            // CB_RefToNull
            // 
            this.CB_RefToNull.AutoSize = true;
            this.CB_RefToNull.Location = new System.Drawing.Point(245, 108);
            this.CB_RefToNull.Name = "CB_RefToNull";
            this.CB_RefToNull.Size = new System.Drawing.Size(146, 17);
            this.CB_RefToNull.TabIndex = 24;
            this.CB_RefToNull.Text = Resources.tl_PolicyConfiguration_Rule_ReferencesToBlankCells;
            this.CB_RefToNull.UseVisualStyleBackColor = true;
            this.CB_RefToNull.CheckedChanged += new System.EventHandler(this.CB_RefToNull_CheckedChanged);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.OAO_A);
            this.panel7.Controls.Add(this.OAO_M);
            this.panel7.Location = new System.Drawing.Point(32, 164);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(160, 23);
            this.panel7.TabIndex = 27;
            // 
            // OAO_A
            // 
            this.OAO_A.AutoSize = true;
            this.OAO_A.Checked = true;
            this.OAO_A.Enabled = false;
            this.OAO_A.Location = new System.Drawing.Point(0, 3);
            this.OAO_A.Name = "OAO_A";
            this.OAO_A.Size = new System.Drawing.Size(58, 17);
            this.OAO_A.TabIndex = 15;
            this.OAO_A.TabStop = true;
            this.OAO_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.OAO_A.UseVisualStyleBackColor = true;
            // 
            // OAO_M
            // 
            this.OAO_M.AutoSize = true;
            this.OAO_M.Enabled = false;
            this.OAO_M.Location = new System.Drawing.Point(59, 3);
            this.OAO_M.Name = "OAO_M";
            this.OAO_M.Size = new System.Drawing.Size(60, 17);
            this.OAO_M.TabIndex = 16;
            this.OAO_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.OAO_M.UseVisualStyleBackColor = true;
            // 
            // CB_OneAmongOthers
            // 
            this.CB_OneAmongOthers.AutoSize = true;
            this.CB_OneAmongOthers.Location = new System.Drawing.Point(14, 150);
            this.CB_OneAmongOthers.Name = "CB_OneAmongOthers";
            this.CB_OneAmongOthers.Size = new System.Drawing.Size(113, 17);
            this.CB_OneAmongOthers.TabIndex = 26;
            this.CB_OneAmongOthers.Text = Resources.tl_PolicyConfiguration_Rule_OneAmongOthers;
            this.CB_OneAmongOthers.UseVisualStyleBackColor = true;
            this.CB_OneAmongOthers.CheckedChanged += new System.EventHandler(this.CB_OneAmongOthers_CheckedChanged);
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.SD_A);
            this.panel8.Controls.Add(this.SD_M);
            this.panel8.Location = new System.Drawing.Point(267, 182);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(160, 23);
            this.panel8.TabIndex = 29;
            // 
            // SD_A
            // 
            this.SD_A.AutoSize = true;
            this.SD_A.Checked = true;
            this.SD_A.Enabled = false;
            this.SD_A.Location = new System.Drawing.Point(3, 3);
            this.SD_A.Name = "SD_A";
            this.SD_A.Size = new System.Drawing.Size(58, 17);
            this.SD_A.TabIndex = 15;
            this.SD_A.TabStop = true;
            this.SD_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.SD_A.UseVisualStyleBackColor = true;
            // 
            // SD_M
            // 
            this.SD_M.AutoSize = true;
            this.SD_M.Enabled = false;
            this.SD_M.Location = new System.Drawing.Point(62, 3);
            this.SD_M.Name = "SD_M";
            this.SD_M.Size = new System.Drawing.Size(60, 17);
            this.SD_M.TabIndex = 16;
            this.SD_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.SD_M.UseVisualStyleBackColor = true;
            // 
            // CB_StringDistance
            // 
            this.CB_StringDistance.AutoSize = true;
            this.CB_StringDistance.Location = new System.Drawing.Point(245, 159);
            this.CB_StringDistance.Name = "CB_StringDistance";
            this.CB_StringDistance.Size = new System.Drawing.Size(98, 17);
            this.CB_StringDistance.TabIndex = 28;
            this.CB_StringDistance.Text = Resources.tl_PolicyConfiguration_Rule_StringDistance;
            this.CB_StringDistance.UseVisualStyleBackColor = true;
            this.CB_StringDistance.CheckedChanged += new System.EventHandler(this.CB_StringDistance_CheckedChanged);
            // 
            // lb_MaxDistance
            // 
            this.lb_MaxDistance.AutoSize = true;
            this.lb_MaxDistance.Location = new System.Drawing.Point(264, 216);
            this.lb_MaxDistance.Name = "lb_MaxDistance";
            this.lb_MaxDistance.Size = new System.Drawing.Size(73, 13);
            this.lb_MaxDistance.TabIndex = 30;
            this.lb_MaxDistance.Text = Resources.tl_PolicyConfiguration_Rule_StringDistanceExtra;
            // 
            // SD_Amount
            // 
            this.SD_Amount.Location = new System.Drawing.Point(377, 213);
            this.SD_Amount.Name = "SD_Amount";
            this.SD_Amount.Size = new System.Drawing.Size(50, 20);
            this.SD_Amount.TabIndex = 31;
            // 
            // cb_Ask_Thousands
            // 
            this.cb_Ask_Thousands.AutoSize = true;
            this.cb_Ask_Thousands.Location = new System.Drawing.Point(14, 264);
            this.cb_Ask_Thousands.Name = "cb_Ask_Thousands";
            this.cb_Ask_Thousands.Size = new System.Drawing.Size(137, 30);
            this.cb_Ask_Thousands.TabIndex = 32;
            this.cb_Ask_Thousands.Text = Resources.tl_PolicyConfiguration_Label_ThousandSeparators;
            this.cb_Ask_Thousands.UseVisualStyleBackColor = true;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.EIC_A);
            this.panel9.Controls.Add(this.EIC_M);
            this.panel9.Location = new System.Drawing.Point(32, 216);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(160, 23);
            this.panel9.TabIndex = 29;
            // 
            // EIC_A
            // 
            this.EIC_A.AutoSize = true;
            this.EIC_A.Checked = true;
            this.EIC_A.Enabled = false;
            this.EIC_A.Location = new System.Drawing.Point(0, 3);
            this.EIC_A.Name = "EIC_A";
            this.EIC_A.Size = new System.Drawing.Size(58, 17);
            this.EIC_A.TabIndex = 15;
            this.EIC_A.TabStop = true;
            this.EIC_A.Text = Resources.tl_PolicyConfiguration_Label_Always;
            this.EIC_A.UseVisualStyleBackColor = true;
            // 
            // EIC_M
            // 
            this.EIC_M.AutoSize = true;
            this.EIC_M.Enabled = false;
            this.EIC_M.Location = new System.Drawing.Point(59, 3);
            this.EIC_M.Name = "EIC_M";
            this.EIC_M.Size = new System.Drawing.Size(60, 17);
            this.EIC_M.TabIndex = 16;
            this.EIC_M.Text = Resources.tl_PolicyConfiguration_Label_Manual;
            this.EIC_M.UseVisualStyleBackColor = true;
            // 
            // CB_ErrorInCells
            // 
            this.CB_ErrorInCells.AutoSize = true;
            this.CB_ErrorInCells.Location = new System.Drawing.Point(14, 202);
            this.CB_ErrorInCells.Name = "CB_ErrorInCells";
            this.CB_ErrorInCells.Size = new System.Drawing.Size(102, 17);
            this.CB_ErrorInCells.TabIndex = 28;
            this.CB_ErrorInCells.Text = Resources.tl_PolicyConfiguration_Rule_CellsWithErrors;
            this.CB_ErrorInCells.UseVisualStyleBackColor = true;
            this.CB_ErrorInCells.CheckedChanged += new System.EventHandler(this.CB_ErrorInCells_CheckedChanged);
            // 
            // PolicyConfigurationDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(472, 363);
            this.Controls.Add(this.panel9);
            this.Controls.Add(this.CB_ErrorInCells);
            this.Controls.Add(this.cb_Ask_Thousands);
            this.Controls.Add(this.SD_Amount);
            this.Controls.Add(this.lb_MaxDistance);
            this.Controls.Add(this.panel8);
            this.Controls.Add(this.CB_StringDistance);
            this.Controls.Add(this.panel7);
            this.Controls.Add(this.CB_OneAmongOthers);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.CB_RefToNull);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.CB_NonConsideredConstants);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.CB_MultipleSameRef);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.CB_FormulaComplexity);
            this.Controls.Add(this.CB_ReadingDirection);
            this.Controls.Add(this.CB_NoConstantsInFormulas);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PolicyConfigurationDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = Resources.tl_PolicyConfiguration_WindowTitle;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox CB_NoConstantsInFormulas;
        private System.Windows.Forms.CheckBox CB_ReadingDirection;
        private System.Windows.Forms.CheckBox CB_FormulaComplexity;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.RadioButton NCIF_A;
        private System.Windows.Forms.RadioButton NCIF_M;
        private System.Windows.Forms.RadioButton RD_A;
        private System.Windows.Forms.RadioButton RD_M;
        private System.Windows.Forms.RadioButton FC_A;
        private System.Windows.Forms.RadioButton FC_M;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox CB_MultipleSameRef;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.RadioButton MSR_A;
        private System.Windows.Forms.RadioButton MSR_M;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.RadioButton NCC_A;
        private System.Windows.Forms.RadioButton NCC_M;
        private System.Windows.Forms.CheckBox CB_NonConsideredConstants;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.RadioButton RTN_A;
        private System.Windows.Forms.RadioButton RTN_M;
        private System.Windows.Forms.CheckBox CB_RefToNull;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.RadioButton OAO_A;
        private System.Windows.Forms.RadioButton OAO_M;
        private System.Windows.Forms.CheckBox CB_OneAmongOthers;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.RadioButton SD_A;
        private System.Windows.Forms.RadioButton SD_M;
        private System.Windows.Forms.CheckBox CB_StringDistance;
        private System.Windows.Forms.Label lb_MaxDistance;
        private System.Windows.Forms.TextBox SD_Amount;
        private System.Windows.Forms.CheckBox cb_Ask_Thousands;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.RadioButton EIC_A;
        private System.Windows.Forms.RadioButton EIC_M;
        private System.Windows.Forms.CheckBox CB_ErrorInCells;
    }
}