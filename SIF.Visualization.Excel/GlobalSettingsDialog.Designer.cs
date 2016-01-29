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
            this.TC_GlobalSettings = new System.Windows.Forms.TabControl();
            this.T_Runtime = new System.Windows.Forms.TabPage();
            this.TC_JavaDotNet = new System.Windows.Forms.TabControl();
            this.T_Framework = new System.Windows.Forms.TabPage();
            this.GB_DotNetFrameworkSelection = new System.Windows.Forms.GroupBox();
            this.L_DotnetVersion = new System.Windows.Forms.Label();
            this.TB_DotnetVersion = new System.Windows.Forms.TextBox();
            this.TB_MonoPath = new System.Windows.Forms.TextBox();
            this.B_MonoBrowse = new System.Windows.Forms.Button();
            this.RB_Mono = new System.Windows.Forms.RadioButton();
            this.RB_DotNet = new System.Windows.Forms.RadioButton();
            this.GB_MonoPathSelection = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.T_Communication = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.RB_DLLAccess = new System.Windows.Forms.RadioButton();
            this.RB_NetPipe = new System.Windows.Forms.RadioButton();
            this.RB_JavaNetSocket = new System.Windows.Forms.RadioButton();
            this.T_Socket = new System.Windows.Forms.TabPage();
            this.groupBoxJavaSelection = new System.Windows.Forms.GroupBox();
            this.RB_SocketNew = new System.Windows.Forms.RadioButton();
            this.RB_SocketOld = new System.Windows.Forms.RadioButton();
            this.CB_SifVersion = new System.Windows.Forms.ComboBox();
            this.LBL_SifVersion = new System.Windows.Forms.Label();
            this.CB_SifStartup = new System.Windows.Forms.CheckBox();
            this.GB_SifPath = new System.Windows.Forms.GroupBox();
            this.B_VerifySifPath = new System.Windows.Forms.Button();
            this.TB_SifPath = new System.Windows.Forms.TextBox();
            this.B_SifBrowse = new System.Windows.Forms.Button();
            this.T_Debug = new System.Windows.Forms.TabPage();
            this.CB_SifDebugMode = new System.Windows.Forms.CheckBox();
            this.Button_Cancel = new System.Windows.Forms.Button();
            this.Button_OK = new System.Windows.Forms.Button();
            this.FBD_SifDirectorySelect = new System.Windows.Forms.FolderBrowserDialog();
            this.FBD_MonoDirectorySelect = new System.Windows.Forms.FolderBrowserDialog();
            this.TC_GlobalSettings.SuspendLayout();
            this.T_Runtime.SuspendLayout();
            this.TC_JavaDotNet.SuspendLayout();
            this.T_Framework.SuspendLayout();
            this.GB_DotNetFrameworkSelection.SuspendLayout();
            this.T_Communication.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.T_Socket.SuspendLayout();
            this.groupBoxJavaSelection.SuspendLayout();
            this.GB_SifPath.SuspendLayout();
            this.T_Debug.SuspendLayout();
            this.SuspendLayout();
            // 
            // TC_GlobalSettings
            // 
            resources.ApplyResources(this.TC_GlobalSettings, "TC_GlobalSettings");
            this.TC_GlobalSettings.Controls.Add(this.T_Runtime);
            this.TC_GlobalSettings.Controls.Add(this.T_Debug);
            this.TC_GlobalSettings.Name = "TC_GlobalSettings";
            this.TC_GlobalSettings.SelectedIndex = 0;
            // 
            // T_Runtime
            // 
            this.T_Runtime.Controls.Add(this.TC_JavaDotNet);
            this.T_Runtime.Controls.Add(this.CB_SifVersion);
            this.T_Runtime.Controls.Add(this.LBL_SifVersion);
            this.T_Runtime.Controls.Add(this.CB_SifStartup);
            this.T_Runtime.Controls.Add(this.GB_SifPath);
            resources.ApplyResources(this.T_Runtime, "T_Runtime");
            this.T_Runtime.Name = "T_Runtime";
            this.T_Runtime.UseVisualStyleBackColor = true;
            this.T_Runtime.Click += new System.EventHandler(this.tabRuntime_Click);
            // 
            // TC_JavaDotNet
            // 
            this.TC_JavaDotNet.Controls.Add(this.T_Framework);
            this.TC_JavaDotNet.Controls.Add(this.T_Communication);
            this.TC_JavaDotNet.Controls.Add(this.T_Socket);
            resources.ApplyResources(this.TC_JavaDotNet, "TC_JavaDotNet");
            this.TC_JavaDotNet.Name = "TC_JavaDotNet";
            this.TC_JavaDotNet.SelectedIndex = 0;
            // 
            // T_Framework
            // 
            this.T_Framework.Controls.Add(this.GB_DotNetFrameworkSelection);
            resources.ApplyResources(this.T_Framework, "T_Framework");
            this.T_Framework.Name = "T_Framework";
            this.T_Framework.UseVisualStyleBackColor = true;
            this.T_Framework.Click += new System.EventHandler(this.T_Framework_Click);
            // 
            // GB_DotNetFrameworkSelection
            // 
            this.GB_DotNetFrameworkSelection.Controls.Add(this.L_DotnetVersion);
            this.GB_DotNetFrameworkSelection.Controls.Add(this.TB_DotnetVersion);
            this.GB_DotNetFrameworkSelection.Controls.Add(this.TB_MonoPath);
            this.GB_DotNetFrameworkSelection.Controls.Add(this.B_MonoBrowse);
            this.GB_DotNetFrameworkSelection.Controls.Add(this.RB_Mono);
            this.GB_DotNetFrameworkSelection.Controls.Add(this.RB_DotNet);
            this.GB_DotNetFrameworkSelection.Controls.Add(this.GB_MonoPathSelection);
            this.GB_DotNetFrameworkSelection.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.GB_DotNetFrameworkSelection, "GB_DotNetFrameworkSelection");
            this.GB_DotNetFrameworkSelection.Name = "GB_DotNetFrameworkSelection";
            this.GB_DotNetFrameworkSelection.TabStop = false;
            this.GB_DotNetFrameworkSelection.Enter += new System.EventHandler(this.GB_DotNetFrameworkSelection_Enter);
            // 
            // L_DotnetVersion
            // 
            resources.ApplyResources(this.L_DotnetVersion, "L_DotnetVersion");
            this.L_DotnetVersion.Name = "L_DotnetVersion";
            // 
            // TB_DotnetVersion
            // 
            resources.ApplyResources(this.TB_DotnetVersion, "TB_DotnetVersion");
            this.TB_DotnetVersion.Name = "TB_DotnetVersion";
            this.TB_DotnetVersion.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // TB_MonoPath
            // 
            resources.ApplyResources(this.TB_MonoPath, "TB_MonoPath");
            this.TB_MonoPath.Name = "TB_MonoPath";
            // 
            // B_MonoBrowse
            // 
            resources.ApplyResources(this.B_MonoBrowse, "B_MonoBrowse");
            this.B_MonoBrowse.Name = "B_MonoBrowse";
            this.B_MonoBrowse.UseVisualStyleBackColor = true;
            this.B_MonoBrowse.Click += new System.EventHandler(this.B_MonoBrowse_Click);
            // 
            // RB_Mono
            // 
            resources.ApplyResources(this.RB_Mono, "RB_Mono");
            this.RB_Mono.Name = "RB_Mono";
            this.RB_Mono.UseVisualStyleBackColor = true;
            this.RB_Mono.CheckedChanged += new System.EventHandler(this.RB_Mono_CheckedChanged);
            // 
            // RB_DotNet
            // 
            resources.ApplyResources(this.RB_DotNet, "RB_DotNet");
            this.RB_DotNet.Checked = true;
            this.RB_DotNet.Name = "RB_DotNet";
            this.RB_DotNet.TabStop = true;
            this.RB_DotNet.UseVisualStyleBackColor = true;
            this.RB_DotNet.CheckedChanged += new System.EventHandler(this.RB_DotNet_CheckedChanged);
            // 
            // GB_MonoPathSelection
            // 
            resources.ApplyResources(this.GB_MonoPathSelection, "GB_MonoPathSelection");
            this.GB_MonoPathSelection.Name = "GB_MonoPathSelection";
            this.GB_MonoPathSelection.TabStop = false;
            this.GB_MonoPathSelection.Enter += new System.EventHandler(this.GB_MonoPathSelection_Enter);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // T_Communication
            // 
            this.T_Communication.Controls.Add(this.groupBox1);
            resources.ApplyResources(this.T_Communication, "T_Communication");
            this.T_Communication.Name = "T_Communication";
            this.T_Communication.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.RB_DLLAccess);
            this.groupBox1.Controls.Add(this.RB_NetPipe);
            this.groupBox1.Controls.Add(this.RB_JavaNetSocket);
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // RB_DLLAccess
            // 
            resources.ApplyResources(this.RB_DLLAccess, "RB_DLLAccess");
            this.RB_DLLAccess.Name = "RB_DLLAccess";
            this.RB_DLLAccess.TabStop = true;
            this.RB_DLLAccess.UseVisualStyleBackColor = true;
            this.RB_DLLAccess.CheckedChanged += new System.EventHandler(this.RB_DLLAccess_CheckedChanged);
            // 
            // RB_NetPipe
            // 
            resources.ApplyResources(this.RB_NetPipe, "RB_NetPipe");
            this.RB_NetPipe.Name = "RB_NetPipe";
            this.RB_NetPipe.UseVisualStyleBackColor = true;
            this.RB_NetPipe.CheckedChanged += new System.EventHandler(this.RB_NetPipe_CheckedChanged);
            // 
            // RB_JavaNetSocket
            // 
            resources.ApplyResources(this.RB_JavaNetSocket, "RB_JavaNetSocket");
            this.RB_JavaNetSocket.Checked = true;
            this.RB_JavaNetSocket.Name = "RB_JavaNetSocket";
            this.RB_JavaNetSocket.TabStop = true;
            this.RB_JavaNetSocket.UseVisualStyleBackColor = true;
            this.RB_JavaNetSocket.CheckedChanged += new System.EventHandler(this.RB_JavaNetSocket_CheckedChanged);
            // 
            // T_Socket
            // 
            this.T_Socket.Controls.Add(this.groupBoxJavaSelection);
            resources.ApplyResources(this.T_Socket, "T_Socket");
            this.T_Socket.Name = "T_Socket";
            this.T_Socket.UseVisualStyleBackColor = true;
            this.T_Socket.Click += new System.EventHandler(this.tabSocket_Click);
            // 
            // groupBoxJavaSelection
            // 
            this.groupBoxJavaSelection.Controls.Add(this.RB_SocketNew);
            this.groupBoxJavaSelection.Controls.Add(this.RB_SocketOld);
            resources.ApplyResources(this.groupBoxJavaSelection, "groupBoxJavaSelection");
            this.groupBoxJavaSelection.Name = "groupBoxJavaSelection";
            this.groupBoxJavaSelection.TabStop = false;
            // 
            // RB_SocketNew
            // 
            resources.ApplyResources(this.RB_SocketNew, "RB_SocketNew");
            this.RB_SocketNew.Name = "RB_SocketNew";
            this.RB_SocketNew.UseVisualStyleBackColor = true;
            this.RB_SocketNew.CheckedChanged += new System.EventHandler(this.RB_SocketNew_CheckedChanged);
            // 
            // RB_SocketOld
            // 
            resources.ApplyResources(this.RB_SocketOld, "RB_SocketOld");
            this.RB_SocketOld.Checked = true;
            this.RB_SocketOld.Name = "RB_SocketOld";
            this.RB_SocketOld.TabStop = true;
            this.RB_SocketOld.UseVisualStyleBackColor = true;
            this.RB_SocketOld.CheckedChanged += new System.EventHandler(this.RB_SocketOld_CheckedChanged);
            // 
            // CB_SifVersion
            // 
            this.CB_SifVersion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.CB_SifVersion.FormattingEnabled = true;
            this.CB_SifVersion.Items.AddRange(new object[] {
            resources.GetString("CB_SifVersion.Items"),
            resources.GetString("CB_SifVersion.Items1")});
            resources.ApplyResources(this.CB_SifVersion, "CB_SifVersion");
            this.CB_SifVersion.Name = "CB_SifVersion";
            this.CB_SifVersion.SelectedIndexChanged += new System.EventHandler(this.CB_SifVersion_SelectedIndexChanged);
            // 
            // LBL_SifVersion
            // 
            resources.ApplyResources(this.LBL_SifVersion, "LBL_SifVersion");
            this.LBL_SifVersion.Name = "LBL_SifVersion";
            this.LBL_SifVersion.Click += new System.EventHandler(this.LBL_SifVersion_Click);
            // 
            // CB_SifStartup
            // 
            resources.ApplyResources(this.CB_SifStartup, "CB_SifStartup");
            this.CB_SifStartup.Checked = true;
            this.CB_SifStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_SifStartup.Name = "CB_SifStartup";
            this.CB_SifStartup.UseVisualStyleBackColor = true;
            this.CB_SifStartup.CheckedChanged += new System.EventHandler(this.CB_SifStartup_CheckedChanged);
            // 
            // GB_SifPath
            // 
            this.GB_SifPath.Controls.Add(this.B_VerifySifPath);
            this.GB_SifPath.Controls.Add(this.TB_SifPath);
            this.GB_SifPath.Controls.Add(this.B_SifBrowse);
            resources.ApplyResources(this.GB_SifPath, "GB_SifPath");
            this.GB_SifPath.Name = "GB_SifPath";
            this.GB_SifPath.TabStop = false;
            // 
            // B_VerifySifPath
            // 
            resources.ApplyResources(this.B_VerifySifPath, "B_VerifySifPath");
            this.B_VerifySifPath.Name = "B_VerifySifPath";
            this.B_VerifySifPath.UseVisualStyleBackColor = true;
            this.B_VerifySifPath.Click += new System.EventHandler(this.B_VerifySifPath_Click);
            // 
            // TB_SifPath
            // 
            resources.ApplyResources(this.TB_SifPath, "TB_SifPath");
            this.TB_SifPath.Name = "TB_SifPath";
            this.TB_SifPath.ReadOnly = true;
            this.TB_SifPath.TextChanged += new System.EventHandler(this.TB_SifPath_TextChanged);
            // 
            // B_SifBrowse
            // 
            resources.ApplyResources(this.B_SifBrowse, "B_SifBrowse");
            this.B_SifBrowse.Name = "B_SifBrowse";
            this.B_SifBrowse.UseVisualStyleBackColor = true;
            this.B_SifBrowse.Click += new System.EventHandler(this.B_SifBrowse_Click);
            // 
            // T_Debug
            // 
            this.T_Debug.Controls.Add(this.CB_SifDebugMode);
            resources.ApplyResources(this.T_Debug, "T_Debug");
            this.T_Debug.Name = "T_Debug";
            this.T_Debug.UseVisualStyleBackColor = true;
            // 
            // CB_SifDebugMode
            // 
            resources.ApplyResources(this.CB_SifDebugMode, "CB_SifDebugMode");
            this.CB_SifDebugMode.Checked = true;
            this.CB_SifDebugMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.CB_SifDebugMode.Name = "CB_SifDebugMode";
            this.CB_SifDebugMode.UseVisualStyleBackColor = true;
            this.CB_SifDebugMode.CheckedChanged += new System.EventHandler(this.CB_SifDebugMode_CheckedChanged);
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
            // FBD_SifDirectorySelect
            // 
            resources.ApplyResources(this.FBD_SifDirectorySelect, "FBD_SifDirectorySelect");
            this.FBD_SifDirectorySelect.RootFolder = System.Environment.SpecialFolder.MyComputer;
            this.FBD_SifDirectorySelect.ShowNewFolderButton = false;
            this.FBD_SifDirectorySelect.HelpRequest += new System.EventHandler(this.FBD_SifDirectorySelect_HelpRequest);
            // 
            // FBD_MonoDirectorySelect
            // 
            resources.ApplyResources(this.FBD_MonoDirectorySelect, "FBD_MonoDirectorySelect");
            this.FBD_MonoDirectorySelect.RootFolder = System.Environment.SpecialFolder.UserProfile;
            this.FBD_MonoDirectorySelect.ShowNewFolderButton = false;
            // 
            // GlobalSettingsDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Button_Cancel);
            this.Controls.Add(this.Button_OK);
            this.Controls.Add(this.TC_GlobalSettings);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GlobalSettingsDialog";
            this.Load += new System.EventHandler(this.GlobalSettingsDialog_Load);
            this.TC_GlobalSettings.ResumeLayout(false);
            this.T_Runtime.ResumeLayout(false);
            this.T_Runtime.PerformLayout();
            this.TC_JavaDotNet.ResumeLayout(false);
            this.T_Framework.ResumeLayout(false);
            this.GB_DotNetFrameworkSelection.ResumeLayout(false);
            this.GB_DotNetFrameworkSelection.PerformLayout();
            this.T_Communication.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.T_Socket.ResumeLayout(false);
            this.groupBoxJavaSelection.ResumeLayout(false);
            this.groupBoxJavaSelection.PerformLayout();
            this.GB_SifPath.ResumeLayout(false);
            this.GB_SifPath.PerformLayout();
            this.T_Debug.ResumeLayout(false);
            this.T_Debug.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TC_GlobalSettings;
        private System.Windows.Forms.TabPage T_Runtime;
        private System.Windows.Forms.TabPage T_Debug;
        private System.Windows.Forms.CheckBox CB_SifDebugMode;
        private System.Windows.Forms.Button Button_Cancel;
        private System.Windows.Forms.Button Button_OK;
        private System.Windows.Forms.CheckBox CB_SifStartup;
        private System.Windows.Forms.ComboBox CB_SifVersion;
        private System.Windows.Forms.Label LBL_SifVersion;
        private System.Windows.Forms.GroupBox groupBoxJavaSelection;
        private System.Windows.Forms.RadioButton RB_SocketNew;
        private System.Windows.Forms.RadioButton RB_SocketOld;
        private System.Windows.Forms.TabControl TC_JavaDotNet;
        private System.Windows.Forms.TabPage T_Socket;
        private System.Windows.Forms.TabPage T_Communication;
        private System.Windows.Forms.GroupBox GB_DotNetFrameworkSelection;
        private System.Windows.Forms.RadioButton RB_Mono;
        private System.Windows.Forms.RadioButton RB_DotNet;
        private System.Windows.Forms.TabPage T_Framework;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton RB_NetPipe;
        private System.Windows.Forms.RadioButton RB_JavaNetSocket;
        private System.Windows.Forms.RadioButton RB_DLLAccess;
        private System.Windows.Forms.FolderBrowserDialog FBD_SifDirectorySelect;
        private System.Windows.Forms.GroupBox GB_SifPath;
        private System.Windows.Forms.Button B_VerifySifPath;
        private System.Windows.Forms.TextBox TB_SifPath;
        private System.Windows.Forms.Button B_SifBrowse;
        private System.Windows.Forms.TextBox TB_MonoPath;
        private System.Windows.Forms.Button B_MonoBrowse;
        private System.Windows.Forms.GroupBox GB_MonoPathSelection;
        private System.Windows.Forms.FolderBrowserDialog FBD_MonoDirectorySelect;
        private System.Windows.Forms.TextBox TB_DotnetVersion;
        private System.Windows.Forms.Label L_DotnetVersion;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}