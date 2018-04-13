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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PolicyConfigurationDialog));
            this.Button_OK = new System.Windows.Forms.Button();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.FormulaComplexity = new System.Windows.Forms.CheckBox();
            this.ReadingDirection = new System.Windows.Forms.CheckBox();
            this.NoConstantsInFormulas = new System.Windows.Forms.CheckBox();
            this.MultipleSameRef = new System.Windows.Forms.CheckBox();
            this.NonConsideredConstants = new System.Windows.Forms.CheckBox();
            this.RefToNull = new System.Windows.Forms.CheckBox();
            this.OneAmongOthers = new System.Windows.Forms.CheckBox();
            this.StringDistance = new System.Windows.Forms.CheckBox();
            this.StringDistanceMinDistanceLabel = new System.Windows.Forms.Label();
            this.StringDistanceMinDistance = new System.Windows.Forms.TextBox();
            this.ErrorInCells = new System.Windows.Forms.CheckBox();
            this.FormulaComplexityMaxNesting = new System.Windows.Forms.TextBox();
            this.FormulaComplexityMaxNestingLabel = new System.Windows.Forms.Label();
            this.FormulaComplexityMaxOperations = new System.Windows.Forms.TextBox();
            this.FormulaComplexityMaxOperationsLabel = new System.Windows.Forms.Label();
            this.OneAmongOthersLengthLabel = new System.Windows.Forms.Label();
            this.OneAmongOthersLength = new System.Windows.Forms.TextBox();
            this.ReadingDirectionLeftRight = new System.Windows.Forms.CheckBox();
            this.ReadingDirectionTopBottom = new System.Windows.Forms.CheckBox();
            this.OneAmongOthersStyleHorizontal = new System.Windows.Forms.RadioButton();
            this.OneAmongOthersStyleVertical = new System.Windows.Forms.RadioButton();
            this.OneAmongOthersStyleBoth = new System.Windows.Forms.RadioButton();
            this.help_ErrorInCells = new System.Windows.Forms.Button();
            this.help_FormulaComplexity = new System.Windows.Forms.Button();
            this.help_MultipleSameRef = new System.Windows.Forms.Button();
            this.help_NoConstantsInFormulas = new System.Windows.Forms.Button();
            this.help_NonConsideredConstants = new System.Windows.Forms.Button();
            this.help_OneAmongOthers = new System.Windows.Forms.Button();
            this.help_ReadingDirection = new System.Windows.Forms.Button();
            this.help_RefToNull = new System.Windows.Forms.Button();
            this.help_StringDistance = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Button_OK
            // 
            resources.ApplyResources(this.Button_OK, "Button_OK");
            this.Button_OK.Name = "Button_OK";
            this.Button_OK.UseVisualStyleBackColor = true;
            this.Button_OK.Click += new System.EventHandler(this.Button_OK_Click);
            // 
            // Button_Cancel
            // 
            resources.ApplyResources(this.Button_Cancel, "Button_Cancel");
            this.Button_Cancel.Name = "Button_Cancel";
            this.Button_Cancel.UseVisualStyleBackColor = true;
            this.Button_Cancel.Click += new System.EventHandler(this.Button_Cancel_Click);
            // 
            // FormulaComplexity
            // 
            resources.ApplyResources(this.FormulaComplexity, "FormulaComplexity");
            this.FormulaComplexity.Name = "FormulaComplexity";
            this.FormulaComplexity.UseVisualStyleBackColor = true;
            this.FormulaComplexity.CheckedChanged += new System.EventHandler(this.CB_FormulaComplexity_CheckedChanged);
            // 
            // ReadingDirection
            // 
            resources.ApplyResources(this.ReadingDirection, "ReadingDirection");
            this.ReadingDirection.Name = "ReadingDirection";
            this.ReadingDirection.UseVisualStyleBackColor = true;
            this.ReadingDirection.CheckedChanged += new System.EventHandler(this.CB_ReadingDirection_CheckedChanged);
            // 
            // NoConstantsInFormulas
            // 
            resources.ApplyResources(this.NoConstantsInFormulas, "NoConstantsInFormulas");
            this.NoConstantsInFormulas.Name = "NoConstantsInFormulas";
            this.NoConstantsInFormulas.UseVisualStyleBackColor = true;
            this.NoConstantsInFormulas.CheckedChanged += new System.EventHandler(this.CB_Constraints_CheckedChanged);
            // 
            // MultipleSameRef
            // 
            resources.ApplyResources(this.MultipleSameRef, "MultipleSameRef");
            this.MultipleSameRef.Name = "MultipleSameRef";
            this.MultipleSameRef.UseVisualStyleBackColor = true;
            this.MultipleSameRef.CheckedChanged += new System.EventHandler(this.CB_MultipleSameRef_CheckedChanged);
            // 
            // NonConsideredConstants
            // 
            resources.ApplyResources(this.NonConsideredConstants, "NonConsideredConstants");
            this.NonConsideredConstants.Name = "NonConsideredConstants";
            this.NonConsideredConstants.UseVisualStyleBackColor = true;
            this.NonConsideredConstants.CheckedChanged += new System.EventHandler(this.CB_NonConsideredConstants_CheckedChanged);
            // 
            // RefToNull
            // 
            resources.ApplyResources(this.RefToNull, "RefToNull");
            this.RefToNull.Name = "RefToNull";
            this.RefToNull.UseVisualStyleBackColor = true;
            this.RefToNull.CheckedChanged += new System.EventHandler(this.CB_RefToNull_CheckedChanged);
            // 
            // OneAmongOthers
            // 
            resources.ApplyResources(this.OneAmongOthers, "OneAmongOthers");
            this.OneAmongOthers.Name = "OneAmongOthers";
            this.OneAmongOthers.UseVisualStyleBackColor = true;
            this.OneAmongOthers.CheckedChanged += new System.EventHandler(this.CB_OneAmongOthers_CheckedChanged);
            // 
            // StringDistance
            // 
            resources.ApplyResources(this.StringDistance, "StringDistance");
            this.StringDistance.Name = "StringDistance";
            this.StringDistance.UseVisualStyleBackColor = true;
            this.StringDistance.CheckedChanged += new System.EventHandler(this.CB_StringDistance_CheckedChanged);
            // 
            // StringDistanceMinDistanceLabel
            // 
            resources.ApplyResources(this.StringDistanceMinDistanceLabel, "StringDistanceMinDistanceLabel");
            this.StringDistanceMinDistanceLabel.Name = "StringDistanceMinDistanceLabel";
            // 
            // StringDistanceMinDistance
            // 
            this.StringDistanceMinDistance.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.StringDistanceMinDistance, "StringDistanceMinDistance");
            this.StringDistanceMinDistance.Name = "StringDistanceMinDistance";
            // 
            // ErrorInCells
            // 
            resources.ApplyResources(this.ErrorInCells, "ErrorInCells");
            this.ErrorInCells.Name = "ErrorInCells";
            this.ErrorInCells.UseVisualStyleBackColor = true;
            this.ErrorInCells.CheckedChanged += new System.EventHandler(this.CB_ErrorInCells_CheckedChanged);
            // 
            // FormulaComplexityMaxNesting
            // 
            this.FormulaComplexityMaxNesting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.FormulaComplexityMaxNesting, "FormulaComplexityMaxNesting");
            this.FormulaComplexityMaxNesting.Name = "FormulaComplexityMaxNesting";
            // 
            // FormulaComplexityMaxNestingLabel
            // 
            resources.ApplyResources(this.FormulaComplexityMaxNestingLabel, "FormulaComplexityMaxNestingLabel");
            this.FormulaComplexityMaxNestingLabel.Name = "FormulaComplexityMaxNestingLabel";
            this.FormulaComplexityMaxNestingLabel.Click += new System.EventHandler(this.label1_Click);
            // 
            // FormulaComplexityMaxOperations
            // 
            this.FormulaComplexityMaxOperations.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.FormulaComplexityMaxOperations, "FormulaComplexityMaxOperations");
            this.FormulaComplexityMaxOperations.Name = "FormulaComplexityMaxOperations";
            this.FormulaComplexityMaxOperations.TabStop = false;
            this.FormulaComplexityMaxOperations.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // FormulaComplexityMaxOperationsLabel
            // 
            resources.ApplyResources(this.FormulaComplexityMaxOperationsLabel, "FormulaComplexityMaxOperationsLabel");
            this.FormulaComplexityMaxOperationsLabel.Name = "FormulaComplexityMaxOperationsLabel";
            this.FormulaComplexityMaxOperationsLabel.Click += new System.EventHandler(this.label2_Click);
            // 
            // OneAmongOthersLengthLabel
            // 
            resources.ApplyResources(this.OneAmongOthersLengthLabel, "OneAmongOthersLengthLabel");
            this.OneAmongOthersLengthLabel.Name = "OneAmongOthersLengthLabel";
            // 
            // OneAmongOthersLength
            // 
            this.OneAmongOthersLength.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.OneAmongOthersLength, "OneAmongOthersLength");
            this.OneAmongOthersLength.Name = "OneAmongOthersLength";
            // 
            // ReadingDirectionLeftRight
            // 
            resources.ApplyResources(this.ReadingDirectionLeftRight, "ReadingDirectionLeftRight");
            this.ReadingDirectionLeftRight.Name = "ReadingDirectionLeftRight";
            this.ReadingDirectionLeftRight.UseVisualStyleBackColor = true;
            // 
            // ReadingDirectionTopBottom
            // 
            resources.ApplyResources(this.ReadingDirectionTopBottom, "ReadingDirectionTopBottom");
            this.ReadingDirectionTopBottom.Name = "ReadingDirectionTopBottom";
            this.ReadingDirectionTopBottom.UseVisualStyleBackColor = true;
            // 
            // OneAmongOthersStyleHorizontal
            // 
            resources.ApplyResources(this.OneAmongOthersStyleHorizontal, "OneAmongOthersStyleHorizontal");
            this.OneAmongOthersStyleHorizontal.Name = "OneAmongOthersStyleHorizontal";
            this.OneAmongOthersStyleHorizontal.TabStop = true;
            this.OneAmongOthersStyleHorizontal.UseVisualStyleBackColor = true;
            // 
            // OneAmongOthersStyleVertical
            // 
            resources.ApplyResources(this.OneAmongOthersStyleVertical, "OneAmongOthersStyleVertical");
            this.OneAmongOthersStyleVertical.Name = "OneAmongOthersStyleVertical";
            this.OneAmongOthersStyleVertical.TabStop = true;
            this.OneAmongOthersStyleVertical.UseVisualStyleBackColor = true;
            // 
            // OneAmongOthersStyleBoth
            // 
            resources.ApplyResources(this.OneAmongOthersStyleBoth, "OneAmongOthersStyleBoth");
            this.OneAmongOthersStyleBoth.Name = "OneAmongOthersStyleBoth";
            this.OneAmongOthersStyleBoth.TabStop = true;
            this.OneAmongOthersStyleBoth.UseVisualStyleBackColor = true;
            // 
            // hilfe
            // 
            this.help_ErrorInCells.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_ErrorInCells, "hilfe");
            this.help_ErrorInCells.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_ErrorInCells.Name = "hilfe";
            this.help_ErrorInCells.UseVisualStyleBackColor = false;
            this.help_ErrorInCells.Click += new System.EventHandler(this.hilfe_Click);
            // 
            // button1
            // 
            this.help_FormulaComplexity.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_FormulaComplexity, "button1");
            this.help_FormulaComplexity.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_FormulaComplexity.Name = "button1";
            this.help_FormulaComplexity.UseVisualStyleBackColor = false;
            this.help_FormulaComplexity.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.help_MultipleSameRef.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_MultipleSameRef, "button2");
            this.help_MultipleSameRef.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_MultipleSameRef.Name = "button2";
            this.help_MultipleSameRef.UseVisualStyleBackColor = false;
            // 
            // button3
            // 
            this.help_NoConstantsInFormulas.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_NoConstantsInFormulas, "button3");
            this.help_NoConstantsInFormulas.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_NoConstantsInFormulas.Name = "button3";
            this.help_NoConstantsInFormulas.UseVisualStyleBackColor = false;
            // 
            // button4
            // 
            this.help_NonConsideredConstants.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_NonConsideredConstants, "button4");
            this.help_NonConsideredConstants.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_NonConsideredConstants.Name = "button4";
            this.help_NonConsideredConstants.UseVisualStyleBackColor = false;
            // 
            // button5
            // 
            this.help_OneAmongOthers.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_OneAmongOthers, "button5");
            this.help_OneAmongOthers.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_OneAmongOthers.Name = "button5";
            this.help_OneAmongOthers.UseVisualStyleBackColor = false;
            // 
            // button6
            // 
            this.help_ReadingDirection.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_ReadingDirection, "button6");
            this.help_ReadingDirection.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_ReadingDirection.Name = "button6";
            this.help_ReadingDirection.UseVisualStyleBackColor = false;
            // 
            // button7
            // 
            this.help_RefToNull.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_RefToNull, "button7");
            this.help_RefToNull.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_RefToNull.Name = "button7";
            this.help_RefToNull.UseVisualStyleBackColor = false;
            // 
            // button8
            // 
            this.help_StringDistance.BackColor = System.Drawing.SystemColors.ActiveCaption;
            resources.ApplyResources(this.help_StringDistance, "button8");
            this.help_StringDistance.Cursor = System.Windows.Forms.Cursors.Default;
            this.help_StringDistance.Name = "button8";
            this.help_StringDistance.UseVisualStyleBackColor = false;
            // 
            // PolicyConfigurationDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.help_StringDistance);
            this.Controls.Add(this.help_RefToNull);
            this.Controls.Add(this.help_ReadingDirection);
            this.Controls.Add(this.help_OneAmongOthers);
            this.Controls.Add(this.help_NonConsideredConstants);
            this.Controls.Add(this.help_NoConstantsInFormulas);
            this.Controls.Add(this.help_MultipleSameRef);
            this.Controls.Add(this.help_FormulaComplexity);
            this.Controls.Add(this.help_ErrorInCells);
            this.Controls.Add(this.OneAmongOthersStyleBoth);
            this.Controls.Add(this.OneAmongOthersStyleVertical);
            this.Controls.Add(this.OneAmongOthersStyleHorizontal);
            this.Controls.Add(this.ReadingDirectionTopBottom);
            this.Controls.Add(this.ReadingDirectionLeftRight);
            this.Controls.Add(this.OneAmongOthersLength);
            this.Controls.Add(this.OneAmongOthersLengthLabel);
            this.Controls.Add(this.FormulaComplexityMaxOperations);
            this.Controls.Add(this.FormulaComplexityMaxOperationsLabel);
            this.Controls.Add(this.FormulaComplexityMaxNesting);
            this.Controls.Add(this.FormulaComplexityMaxNestingLabel);
            this.Controls.Add(this.ErrorInCells);
            this.Controls.Add(this.StringDistanceMinDistance);
            this.Controls.Add(this.StringDistanceMinDistanceLabel);
            this.Controls.Add(this.StringDistance);
            this.Controls.Add(this.OneAmongOthers);
            this.Controls.Add(this.RefToNull);
            this.Controls.Add(this.NonConsideredConstants);
            this.Controls.Add(this.MultipleSameRef);
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.FormulaComplexity);
            this.Controls.Add(this.ReadingDirection);
            this.Controls.Add(this.NoConstantsInFormulas);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "PolicyConfigurationDialog";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox NoConstantsInFormulas;
        private System.Windows.Forms.CheckBox ReadingDirection;
        private System.Windows.Forms.CheckBox FormulaComplexity;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.CheckBox MultipleSameRef;
        private System.Windows.Forms.CheckBox NonConsideredConstants;
        private System.Windows.Forms.CheckBox RefToNull;
        private System.Windows.Forms.CheckBox OneAmongOthers;
        private System.Windows.Forms.CheckBox StringDistance;
        private System.Windows.Forms.Label StringDistanceMinDistanceLabel;
        private System.Windows.Forms.TextBox StringDistanceMinDistance;
        private System.Windows.Forms.CheckBox ErrorInCells;
        private System.Windows.Forms.TextBox FormulaComplexityMaxNesting;
        private System.Windows.Forms.Label FormulaComplexityMaxNestingLabel;
        private System.Windows.Forms.TextBox FormulaComplexityMaxOperations;
        private System.Windows.Forms.Label FormulaComplexityMaxOperationsLabel;
        private System.Windows.Forms.Label OneAmongOthersLengthLabel;
        private System.Windows.Forms.TextBox OneAmongOthersLength;
        private System.Windows.Forms.CheckBox ReadingDirectionLeftRight;
        private System.Windows.Forms.CheckBox ReadingDirectionTopBottom;
        private System.Windows.Forms.RadioButton OneAmongOthersStyleHorizontal;
        private System.Windows.Forms.RadioButton OneAmongOthersStyleVertical;
        private System.Windows.Forms.RadioButton OneAmongOthersStyleBoth;
        private System.Windows.Forms.Button help_ErrorInCells;
        private System.Windows.Forms.Button help_FormulaComplexity;
        private System.Windows.Forms.Button help_MultipleSameRef;
        private System.Windows.Forms.Button help_NoConstantsInFormulas;
        private System.Windows.Forms.Button help_NonConsideredConstants;
        private System.Windows.Forms.Button help_OneAmongOthers;
        private System.Windows.Forms.Button help_ReadingDirection;
        private System.Windows.Forms.Button help_RefToNull;
        private System.Windows.Forms.Button help_StringDistance;
    }
}