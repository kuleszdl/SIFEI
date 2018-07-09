﻿namespace SIF.Visualization.Excel
{
    partial class Ribbon : Microsoft.Office.Tools.Ribbon.RibbonBase
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        public Ribbon() : base(Globals.Factory.GetRibbonFactory())
        {
            InitializeComponent();
            InitializeComponentOverride();
        }

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">"true", wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls "false".</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// Method for manually overriding stuff (especially labels for translation) created by the designer
        /// </summary>
        private void InitializeComponentOverride()
        {
            // inspectionTab
            this.inspectionTab.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_Title;

            // Action area
            this.testGroup.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScan_Title;
            this.automaticScanCheckBox.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScan_AutomaticScansCheckbox;
            this.automaticScanCheckBox.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScan_AutomaticScansCheckboxTooltip;
            this.policyConfigurationDialog.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScan_PolicyConfigurationButton;
            this.policyConfigurationDialog.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScan_PolicyConfigurationButtonTooltip;
            this.scanButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScan_ScanButton;
            this.scanButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScan_ScanButtonTooltip;

            // View area
            this.viewGroup.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaView_Title;
            this.SidebarButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaView_PaneButton;
            this.SidebarButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaView_PaneButtonTooltip;
            this.clearButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaView_ResetButton;
            this.clearButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaView_ResetButtonTooltip;

            // Scenario area
            this.scenarioGroup.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScenario_Title;
            this.CreateNewScenarioButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScenario_NewButton;
            this.CreateNewScenarioButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScenario_NewButtonTooltip;
            this.cancelScenarioButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScenario_CancelButton;
            this.cancelScenarioButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScenario_CancelButtonTolltip;
            this.submitScenarioButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScenario_SaveButton;
            this.submitScenarioButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaScenario_SaveButtonTooltip;

            // Cell definition area
            this.defineGroup.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaDefine_Title;
            this.inputCellToggleButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaDefine_Inputcell;
            this.inputCellToggleButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaDefine_InputcellTooltip;
            this.intermediateCellToggleButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaDefine_Intermediatecell;
            this.intermediateCellToggleButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaDefine_IntermediatecellTooltip;
            this.resultCellToggleButton.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaDefine_Resultcell;
            this.resultCellToggleButton.ScreenTip = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaDefine_ResultcellTooltip;

            // Miscellaneous area
            this.miscellaneousGroup.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaMiscellaneous_Title;
            this.globalSettingsDialog.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaMiscellaneous_GlobalSettingsButton;
            System.Windows.Forms.ToolTip tooltip = new System.Windows.Forms.ToolTip();
            tooltip.AutoPopDelay = 5000;
            tooltip.InitialDelay = 1000;
            tooltip.ReshowDelay = 1;
            tooltip.ShowAlways = true;
            

            this.CB_SanityControls.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaMiscellaneous_ShowSanity;
            
            // Rule Area
            this.RuleEditGroup.Label = SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaRule_Title;
            

        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für Designerunterstützung -
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.inspectionTab = this.Factory.CreateRibbonTab();
            this.testGroup = this.Factory.CreateRibbonGroup();
            this.automaticScanCheckBox = this.Factory.CreateRibbonCheckBox();
            this.viewGroup = this.Factory.CreateRibbonGroup();
            this.scenarioGroup = this.Factory.CreateRibbonGroup();
            this.defineGroup = this.Factory.CreateRibbonGroup();
            this.sanityGroup = this.Factory.CreateRibbonGroup();
            this.sanityWarnCheckbox = this.Factory.CreateRibbonCheckBox();
            this.miscellaneousGroup = this.Factory.CreateRibbonGroup();
            this.CB_SanityControls = this.Factory.CreateRibbonCheckBox();
            this.RuleEditGroup = this.Factory.CreateRibbonGroup();
            this.group1 = this.Factory.CreateRibbonGroup();
            this.scanButton = this.Factory.CreateRibbonButton();
            this.policyConfigurationDialog = this.Factory.CreateRibbonButton();
            this.btnLoadFile1 = this.Factory.CreateRibbonButton();
            this.btnLoadFile2 = this.Factory.CreateRibbonButton();
            this.SidebarButton = this.Factory.CreateRibbonButton();
            this.clearButton = this.Factory.CreateRibbonButton();
            this.CreateNewScenarioButton = this.Factory.CreateRibbonButton();
            this.submitScenarioButton = this.Factory.CreateRibbonButton();
            this.cancelScenarioButton = this.Factory.CreateRibbonButton();
            this.inputCellToggleButton = this.Factory.CreateRibbonToggleButton();
            this.intermediateCellToggleButton = this.Factory.CreateRibbonToggleButton();
            this.resultCellToggleButton = this.Factory.CreateRibbonToggleButton();
            this.sanityValueCellToggleButton = this.Factory.CreateRibbonToggleButton();
            this.sanityConstraintCellToggleButton = this.Factory.CreateRibbonToggleButton();
            this.sanityExplanationCellToggleButton = this.Factory.CreateRibbonToggleButton();
            this.sanityCheckingCellToggleButton = this.Factory.CreateRibbonToggleButton();
            this.button1 = this.Factory.CreateRibbonButton();
            this.globalSettingsDialog = this.Factory.CreateRibbonButton();
            this.ruleEditorButton = this.Factory.CreateRibbonButton();
            this.button3 = this.Factory.CreateRibbonButton();
            this.button2 = this.Factory.CreateRibbonButton();
            this.serverStartenButton = this.Factory.CreateRibbonButton();
            this.inspectionTab.SuspendLayout();
            this.testGroup.SuspendLayout();
            this.viewGroup.SuspendLayout();
            this.scenarioGroup.SuspendLayout();
            this.defineGroup.SuspendLayout();
            this.sanityGroup.SuspendLayout();
            this.miscellaneousGroup.SuspendLayout();
            this.RuleEditGroup.SuspendLayout();
            this.group1.SuspendLayout();
            this.SuspendLayout();
            // 
            // inspectionTab
            // 
            this.inspectionTab.ControlId.ControlIdType = Microsoft.Office.Tools.Ribbon.RibbonControlIdType.Office;
            this.inspectionTab.Groups.Add(this.testGroup);
            this.inspectionTab.Groups.Add(this.viewGroup);
            this.inspectionTab.Groups.Add(this.scenarioGroup);
            this.inspectionTab.Groups.Add(this.defineGroup);
            this.inspectionTab.Groups.Add(this.sanityGroup);
            this.inspectionTab.Groups.Add(this.miscellaneousGroup);
            this.inspectionTab.Groups.Add(this.RuleEditGroup);
            this.inspectionTab.Groups.Add(this.group1);
            this.inspectionTab.Label = "INSPECTION";
            this.inspectionTab.Name = "inspectionTab";
            // 
            // testGroup
            // 
            this.testGroup.Items.Add(this.scanButton);
            this.testGroup.Items.Add(this.automaticScanCheckBox);
            this.testGroup.Items.Add(this.policyConfigurationDialog);
            this.testGroup.Items.Add(this.btnLoadFile1);
            this.testGroup.Items.Add(this.btnLoadFile2);
            this.testGroup.Label = "Test";
            this.testGroup.Name = "testGroup";
            // 
            // automaticScanCheckBox
            // 
            this.automaticScanCheckBox.Label = "Automatic scans";
            this.automaticScanCheckBox.Name = "automaticScanCheckBox";
            this.automaticScanCheckBox.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.automaticScanCheckBox_Click);
            // 
            // viewGroup
            // 
            this.viewGroup.Items.Add(this.SidebarButton);
            this.viewGroup.Items.Add(this.clearButton);
            this.viewGroup.Label = "Ansicht";
            this.viewGroup.Name = "viewGroup";
            // 
            // scenarioGroup
            // 
            this.scenarioGroup.Items.Add(this.CreateNewScenarioButton);
            this.scenarioGroup.Items.Add(this.submitScenarioButton);
            this.scenarioGroup.Items.Add(this.cancelScenarioButton);
            this.scenarioGroup.Label = "Scenario";
            this.scenarioGroup.Name = "scenarioGroup";
            // 
            // defineGroup
            // 
            this.defineGroup.Items.Add(this.inputCellToggleButton);
            this.defineGroup.Items.Add(this.intermediateCellToggleButton);
            this.defineGroup.Items.Add(this.resultCellToggleButton);
            this.defineGroup.Label = "Define Scenario Cells";
            this.defineGroup.Name = "defineGroup";
            // 
            // sanityGroup
            // 
            this.sanityGroup.Items.Add(this.sanityValueCellToggleButton);
            this.sanityGroup.Items.Add(this.sanityConstraintCellToggleButton);
            this.sanityGroup.Items.Add(this.sanityExplanationCellToggleButton);
            this.sanityGroup.Items.Add(this.sanityCheckingCellToggleButton);
            this.sanityGroup.Items.Add(this.sanityWarnCheckbox);
            this.sanityGroup.Items.Add(this.button1);
            this.sanityGroup.Label = "Plausibilität";
            this.sanityGroup.Name = "sanityGroup";
            this.sanityGroup.Visible = false;
            // 
            // sanityWarnCheckbox
            // 
            this.sanityWarnCheckbox.Checked = true;
            this.sanityWarnCheckbox.Label = "Warnings";
            this.sanityWarnCheckbox.Name = "sanityWarnCheckbox";
            this.sanityWarnCheckbox.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.warnings_Click);
            // 
            // miscellaneousGroup
            // 
            this.miscellaneousGroup.Items.Add(this.globalSettingsDialog);
            this.miscellaneousGroup.Items.Add(this.CB_SanityControls);
            this.miscellaneousGroup.Items.Add(this.serverStartenButton);
            this.miscellaneousGroup.Label = "Miscellaneous";
            this.miscellaneousGroup.Name = "miscellaneousGroup";
            // 
            // CB_SanityControls
            // 
            this.CB_SanityControls.Label = "Show sanity controls";
            this.CB_SanityControls.Name = "CB_SanityControls";
            this.CB_SanityControls.Visible = false;
            this.CB_SanityControls.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.CB_SanityControls_Click);
            // 
            // RuleEditGroup
            // 
            this.RuleEditGroup.Items.Add(this.ruleEditorButton);
            this.RuleEditGroup.Items.Add(this.button3);
            this.RuleEditGroup.Label = "Regel Editor";
            this.RuleEditGroup.Name = "RuleEditGroup";
            // 
            // group1
            // 
            this.group1.Items.Add(this.button2);
            this.group1.Label = "User Guid";
            this.group1.Name = "group1";
            // 
            // scanButton
            // 
            this.scanButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.scanButton.Description = "Scans the current workbook with all scenarios.";
            this.scanButton.Label = "Scan";
            this.scanButton.Name = "scanButton";
            this.scanButton.OfficeImageId = "Synchronize";
            this.scanButton.ScreenTip = "Scans the current workbook.";
            this.scanButton.ShowImage = true;
            this.scanButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.scanButton_Click);
            // 
            // policyConfigurationDialog
            // 
            this.policyConfigurationDialog.Image = global::SIF.Visualization.Excel.Properties.Resources.input_clear;
            this.policyConfigurationDialog.Label = "Policy Configuration";
            this.policyConfigurationDialog.Name = "policyConfigurationDialog";
            this.policyConfigurationDialog.ShowImage = true;
            this.policyConfigurationDialog.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click);
            // 
            // btnLoadFile1
            // 
            this.btnLoadFile1.Label = "";
            this.btnLoadFile1.Name = "btnLoadFile1";
            // 
            // btnLoadFile2
            // 
            this.btnLoadFile2.Label = "";
            this.btnLoadFile2.Name = "btnLoadFile2";
            // 
            // SidebarButton
            // 
            this.SidebarButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.SidebarButton.Description = "Opens the inspections pane.";
            this.SidebarButton.Image = global::SIF.Visualization.Excel.Properties.Resources.inspectionpane;
            this.SidebarButton.Label = "Inspection Pane";
            this.SidebarButton.Name = "SidebarButton";
            this.SidebarButton.ScreenTip = "Opens a pane with the cell definitions, scenario overview and findings.";
            this.SidebarButton.ShowImage = true;
            this.SidebarButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SidebarButton_Click);
            // 
            // clearButton
            // 
            this.clearButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.clearButton.Description = "Resets the document to the state before the test execution.";
            this.clearButton.Label = "Reset document";
            this.clearButton.Name = "clearButton";
            this.clearButton.OfficeImageId = "ClearRow";
            this.clearButton.ScreenTip = "Resets the document to the state before the test execution.";
            this.clearButton.ShowImage = true;
            this.clearButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.clearButton_Click);
            // 
            // CreateNewScenarioButton
            // 
            this.CreateNewScenarioButton.ControlSize = Microsoft.Office.Core.RibbonControlSize.RibbonControlSizeLarge;
            this.CreateNewScenarioButton.Description = "Creates a new scenario";
            this.CreateNewScenarioButton.Label = "New";
            this.CreateNewScenarioButton.Name = "CreateNewScenarioButton";
            this.CreateNewScenarioButton.OfficeImageId = "OutlookTaskToday";
            this.CreateNewScenarioButton.ScreenTip = "Creates a new scenario";
            this.CreateNewScenarioButton.ShowImage = true;
            this.CreateNewScenarioButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.NewScenarioButton_Click);
            // 
            // submitScenarioButton
            // 
            this.submitScenarioButton.Description = "Submits scenario cration.";
            this.submitScenarioButton.Label = "Save";
            this.submitScenarioButton.Name = "submitScenarioButton";
            this.submitScenarioButton.OfficeImageId = "OutlookTaskToday";
            this.submitScenarioButton.ScreenTip = "Save this scenario";
            this.submitScenarioButton.ShowImage = true;
            this.submitScenarioButton.Visible = false;
            this.submitScenarioButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.SubmitScenarioButton_Click);
            // 
            // cancelScenarioButton
            // 
            this.cancelScenarioButton.Description = "Cancel the scenario creation.";
            this.cancelScenarioButton.Label = "Cancel";
            this.cancelScenarioButton.Name = "cancelScenarioButton";
            this.cancelScenarioButton.OfficeImageId = "CancelEditing";
            this.cancelScenarioButton.ScreenTip = "Discard this scenario";
            this.cancelScenarioButton.ShowImage = true;
            this.cancelScenarioButton.Visible = false;
            this.cancelScenarioButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.cancelScenarioButton_Click);
            // 
            // inputCellToggleButton
            // 
            this.inputCellToggleButton.Description = "Defines as Input Cell.";
            this.inputCellToggleButton.Image = global::SIF.Visualization.Excel.Properties.Resources.input_clear;
            this.inputCellToggleButton.Label = "Input Cell";
            this.inputCellToggleButton.Name = "inputCellToggleButton";
            this.inputCellToggleButton.ScreenTip = "Defines a Input Cell.";
            this.inputCellToggleButton.ShowImage = true;
            this.inputCellToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DefineInputCell_Click);
            // 
            // intermediateCellToggleButton
            // 
            this.intermediateCellToggleButton.Description = "Defines as Intermediate Cell.";
            this.intermediateCellToggleButton.Image = global::SIF.Visualization.Excel.Properties.Resources.intermediate_clear;
            this.intermediateCellToggleButton.Label = "Intermediate Cell";
            this.intermediateCellToggleButton.Name = "intermediateCellToggleButton";
            this.intermediateCellToggleButton.ScreenTip = "Defines a Intermediate Cell.";
            this.intermediateCellToggleButton.ShowImage = true;
            this.intermediateCellToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DefineIntermediateCell_Click);
            // 
            // resultCellToggleButton
            // 
            this.resultCellToggleButton.Description = "Defines as Result Cell.";
            this.resultCellToggleButton.Image = global::SIF.Visualization.Excel.Properties.Resources.output_clear;
            this.resultCellToggleButton.Label = "Result Cell";
            this.resultCellToggleButton.Name = "resultCellToggleButton";
            this.resultCellToggleButton.ScreenTip = "Defines a Result Cell.";
            this.resultCellToggleButton.ShowImage = true;
            this.resultCellToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DefineResultCell_Click);
            // 
            // sanityValueCellToggleButton
            // 
            this.sanityValueCellToggleButton.Label = "Values";
            this.sanityValueCellToggleButton.Name = "sanityValueCellToggleButton";
            this.sanityValueCellToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DefineSanityValueCell_Click);
            // 
            // sanityConstraintCellToggleButton
            // 
            this.sanityConstraintCellToggleButton.Label = "Restriction";
            this.sanityConstraintCellToggleButton.Name = "sanityConstraintCellToggleButton";
            this.sanityConstraintCellToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DefineSanityConstraintCell_Click);
            // 
            // sanityExplanationCellToggleButton
            // 
            this.sanityExplanationCellToggleButton.Label = "Explanation";
            this.sanityExplanationCellToggleButton.Name = "sanityExplanationCellToggleButton";
            this.sanityExplanationCellToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DefineSanityExplanationCell_Click);
            // 
            // sanityCheckingCellToggleButton
            // 
            this.sanityCheckingCellToggleButton.Label = "To be checked";
            this.sanityCheckingCellToggleButton.Name = "sanityCheckingCellToggleButton";
            this.sanityCheckingCellToggleButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.DefineSanityCheckingCell_Click);
            // 
            // button1
            // 
            this.button1.Label = "ToggleAutoScan";
            this.button1.Name = "button1";
            this.button1.Visible = false;
            this.button1.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button1_Click_1);
            // 
            // globalSettingsDialog
            // 
            this.globalSettingsDialog.Image = global::SIF.Visualization.Excel.Properties.Resources.input_clear;
            this.globalSettingsDialog.Label = "Global Settings";
            this.globalSettingsDialog.Name = "globalSettingsDialog";
            this.globalSettingsDialog.ShowImage = true;
            this.globalSettingsDialog.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.globalSettings_Click);
            // 
            // ruleEditorButton
            // 
            this.ruleEditorButton.Label = global::SIF.Visualization.Excel.Properties.Resources.tl_Ribbon_AreaRule_RuleEditor;
            this.ruleEditorButton.Name = "ruleEditorButton";
            this.ruleEditorButton.ShowImage = true;
            this.ruleEditorButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.RuleEdit_Click);
            // 
            // button3
            // 
            this.button3.Image = global::SIF.Visualization.Excel.Properties.Resources.Download;
            this.button3.Label = "button3";
            this.button3.Name = "button3";
            this.button3.ShowLabel = false;
            // 
            // button2
            // 
            this.button2.Label = "Benutzeranleitung";
            this.button2.Name = "button2";
            this.button2.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.button2_Click_1);
            // 
            // serverStartenButton
            // 
            this.serverStartenButton.Image = global::SIF.Visualization.Excel.Properties.Resources.input_clear;
            this.serverStartenButton.Label = "Server starten";
            this.serverStartenButton.Name = "serverStartenButton";
            this.serverStartenButton.ShowImage = true;
            this.serverStartenButton.Click += new Microsoft.Office.Tools.Ribbon.RibbonControlEventHandler(this.serverStartenButton_Click);
            // 
            // Ribbon
            // 
            this.Name = "Ribbon";
            this.RibbonType = "Microsoft.Excel.Workbook";
            this.Tabs.Add(this.inspectionTab);
            this.Load += new Microsoft.Office.Tools.Ribbon.RibbonUIEventHandler(this.Ribbon_Load);
            this.inspectionTab.ResumeLayout(false);
            this.inspectionTab.PerformLayout();
            this.testGroup.ResumeLayout(false);
            this.testGroup.PerformLayout();
            this.viewGroup.ResumeLayout(false);
            this.viewGroup.PerformLayout();
            this.scenarioGroup.ResumeLayout(false);
            this.scenarioGroup.PerformLayout();
            this.defineGroup.ResumeLayout(false);
            this.defineGroup.PerformLayout();
            this.sanityGroup.ResumeLayout(false);
            this.sanityGroup.PerformLayout();
            this.miscellaneousGroup.ResumeLayout(false);
            this.miscellaneousGroup.PerformLayout();
            this.RuleEditGroup.ResumeLayout(false);
            this.RuleEditGroup.PerformLayout();
            this.group1.ResumeLayout(false);
            this.group1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        internal Microsoft.Office.Tools.Ribbon.RibbonTab inspectionTab;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup testGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton scanButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup viewGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton clearButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup scenarioGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup defineGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton inputCellToggleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton intermediateCellToggleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton resultCellToggleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton submitScenarioButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton cancelScenarioButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton SidebarButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton CreateNewScenarioButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup sanityGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton sanityValueCellToggleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton sanityConstraintCellToggleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton sanityExplanationCellToggleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox sanityWarnCheckbox;
        internal Microsoft.Office.Tools.Ribbon.RibbonToggleButton sanityCheckingCellToggleButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox automaticScanCheckBox;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLoadFile1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton btnLoadFile2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton policyConfigurationDialog;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup miscellaneousGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton globalSettingsDialog;
        internal Microsoft.Office.Tools.Ribbon.RibbonCheckBox CB_SanityControls;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup RuleEditGroup;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton ruleEditorButton;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button1;
        internal Microsoft.Office.Tools.Ribbon.RibbonGroup group1;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button2;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton button3;
        internal Microsoft.Office.Tools.Ribbon.RibbonButton serverStartenButton;
    }

    partial class ThisRibbonCollection
    {
        internal Ribbon Ribbon
        {
            get { return this.GetRibbon<Ribbon>(); }
        }
    }
}
