using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel
{
    /// <summary>
    /// A Dialog in which the global settings can be deinfed
    /// </summary>
    public partial class GlobalSettingsDialog : Form
    {
        private readonly string _debugFile = Settings.Default.FrameworkPath + @"\debug";

        /// <summary>
        /// Instanciates a new Dialog in which the global settings can be changed
        /// </summary>
        public GlobalSettingsDialog()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedDialog;

            if (Settings.Default.SifStartup) CB_SifStartup.Checked = true;
            
            if(!Settings.Default.SifStartup) CB_SifStartup.Checked = false;
            
            if (File.Exists(_debugFile)) CB_SifDebugMode.Checked = true;
            
            if (!File.Exists(_debugFile)) CB_SifDebugMode.Checked = false;
            
            if (string.IsNullOrEmpty(Settings.Default.FrameworkPath)) 
                TB_SifPath.Text = "C:\\Spreadsheet Inspection Framework";
            
            if (!string.IsNullOrEmpty(Settings.Default.FrameworkPath))
                TB_SifPath.Text = Settings.Default.FrameworkPath;
           
            if (string.IsNullOrEmpty(Settings.Default.SifVersion) ||
                Settings.Default.SifVersion.Equals("Java"))
                // Trigger actions by CB_SifVersion_SelectedIndexChanged for Java
                CB_SifVersion.SelectedIndex = 0;
            
            if (Settings.Default.SifVersion.Equals("Mono") ||
                Settings.Default.SifVersion.Equals(".NET"))
                // Trigger actions by CB_SifVersion_SelectedIndexChanged for .NET or Mono
                CB_SifVersion.SelectedIndex = 1;
            

            TB_DotnetVersion.Text = Environment.Version.ToString();

            ShowDialog();
        }

        private void GlobalSettingsDialog_Load(object sender, EventArgs e)
        {
        }

        private void CB_SifDebugMode_CheckedChanged(object sender, EventArgs e)
        {
            // TODO Erst bei OK drücken neue Einstellung verarbeiten
        }

        private void CB_SifVersion_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (CB_SifVersion.SelectedIndex == 0)
            {
                LoadJavaSettings();
            }

            else
            {
                LoadNetMonoVersion();
            }
        }

        /// <summary>
        /// Loads the settings if .Net or Mono were selected
        /// </summary>
        private void LoadNetMonoVersion()
        {
            if (!T_Framework.Enabled)
                {
                   T_Framework.Enabled = true;
                }
                TB_MonoPath.Text = Settings.Default.MonoFrameworkPath;
                if (string.IsNullOrEmpty(Settings.Default.SifVersion)
                    || Settings.Default.SifVersion.Equals(".NET"))
                {
                    // Trigger actions by RB_DotNet_CheckedChanged
                    RB_DotNet.Checked = true;
                   }
                if (Settings.Default.SifVersion.Equals("Mono"))
                {
                    // Trigger actions by RB_Mono_Checked
                    RB_Mono.Checked = true;
                   }


                if (!T_Communication.Enabled)
                {
                   
                    T_Communication.Enabled = true;
                }
                // Only display socket options when standard communication between dotnet and java is selected
                if (string.IsNullOrEmpty(Settings.Default.CommunicationMethod)
                        || Settings.Default.CommunicationMethod.Equals("standard"))
                    {
                        if (!T_Socket.Enabled)
                        {
                         T_Socket.Enabled = true;

                        }
                        if (Settings.Default.SifOptions.Equals("socket"))
                        {
                            RB_SocketOld.Checked = true;
                        }
                        if (Settings.Default.SifOptions.Equals("socketNew"))
                        {
                            RB_SocketNew.Checked = true;
                        }
                        // Trigger actions by RB_NetSocket_Checked
                        RB_JavaNetSocket.Checked = true;
                    }
                 
               
                if ((!string.IsNullOrEmpty(Settings.Default.CommunicationMethod))
                    && (!Settings.Default.CommunicationMethod.Equals("standard")))
                {
                    if (T_Socket.Enabled)
                    {
                       T_Socket.Enabled = false;
                        // TODO other methods not implemented yet
                    }
                }
            
        }

        /// <summary>
        /// Loads the settings when the Java verion got selected
        /// </summary>
        private void LoadJavaSettings()
        {
            if (Settings.Default.SifOptions.Equals("socket"))
            {
                RB_SocketOld.Checked = true;
            }
            if (Settings.Default.SifOptions.Equals("socketNew"))
            {
                RB_SocketNew.Checked = true;
            }
            if ((File.Exists(TB_SifPath.Text + @"\sif.jar"))
                && ExecutableExistsOnPath("java.exe"))
            {
                Button_OK.Enabled = true;
            }
            if ((!File.Exists(Settings.Default.FrameworkPath + @"\sif.jar"))
                || !ExecutableExistsOnPath("java.exe"))
            {
                Button_OK.Enabled = false;
            }
            if (T_Framework.Enabled)
            {
                T_Framework.Enabled = false;
            }
            if (T_Communication.Enabled)
            {
                T_Communication.Enabled = false;
            }
            if (!T_Socket.Enabled)
            {
                T_Socket.Enabled = true;
                if (!Settings.Default.SifOptions.Equals("socketNew"))
                {
                    RB_SocketOld.Checked = true;
                }
                else
                {
                    RB_SocketNew.Checked = true;
                }
            }
        }

        /// <summary>
        /// Click Handler of the Ok Button
        /// Checks if the settings put by the user are valid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_OK_Click(object sender, EventArgs e)
        {
            if (CB_SifStartup.Checked)
            {
                Settings.Default.SifStartup = true;
                Settings.Default.Save();
        }
            if (!CB_SifStartup.Checked)
            {
                Settings.Default.SifStartup = false;
                Settings.Default.Save();
            }

            try
            {
                if (CB_SifDebugMode.Checked)
                {
                    FileStream debugStream = new FileStream(_debugFile, FileMode.Create);
                    debugStream.WriteByte(1);
                    debugStream.Close();
                    }
                if (!CB_SifDebugMode.Checked)
                {
                    // TODO disconnect from SIF and delete debug file as SIF has a lock on this file for obvious reasons
                    File.Delete(_debugFile);
                    }
            }
            catch (Exception)
            {
                MessageBox.Show(
                    Resources.DebugReadWriteErrorPre_MessageBoxText +
                    _debugFile + Resources.DebugReadWriteErrorSuf_MessageBoxText,
                    Resources.DebugReadWriteError_MessageBoxTitle);
            }

            Settings.Default.FrameworkPath = TB_SifPath.Text;
            Settings.Default.Save();
            
            if (CB_SifVersion.SelectedIndex == 0)
            {
                LoadJava();
            }
            if (CB_SifVersion.SelectedIndex == 1)
            {
                LoadMonoNet();
            }

            Close();
        }

        /// <summary>
        /// Loading if Mono or .Net got selectesd
        /// </summary>
        private void LoadMonoNet()
        {
            if (RB_Mono.Checked)
            {
                Settings.Default.SifVersion = "Mono";
                Settings.Default.Save();
                Settings.Default.MonoFrameworkPath = ExecutableExistsOnPath("mono.exe")
                    ? string.Empty
                    : TB_MonoPath.Text;
                Settings.Default.Save();
            }

            if (RB_DotNet.Checked)
            {
                Settings.Default.SifVersion = ".NET";
                Settings.Default.Save();
            }
            if (RB_JavaNetSocket.Checked)
            {
                Settings.Default.CommunicationMethod = "standard";
                Settings.Default.Save();
                if (RB_SocketOld.Checked)
                {
                    Settings.Default.SifOptions = "socket";
                    Settings.Default.Save();
                }
                if (RB_SocketNew.Checked)
                {
                    Settings.Default.SifOptions = "socketNew";
                    Settings.Default.Save();
                }
            }
            if (RB_NetPipe.Checked)
            {
                Settings.Default.CommunicationMethod = "pipe";
                Settings.Default.Save();
                Settings.Default.SifOptions = string.Empty;
                Settings.Default.Save();
            }
            if (RB_DLLAccess.Checked)
            {
                Settings.Default.CommunicationMethod = "dll";
                Settings.Default.Save();
                Settings.Default.SifOptions = string.Empty;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Loading if Java was selected
        /// </summary>
        private void LoadJava()
        {
            Settings.Default.SifVersion = "Java";
            Settings.Default.Save();
            Settings.Default.CommunicationMethod = "standard";
            Settings.Default.Save();

            if (RB_SocketOld.Checked)
            {
                Settings.Default.SifOptions = "socket";
                Settings.Default.Save();
            }
            if (RB_SocketNew.Checked)
            {
                Settings.Default.SifOptions = "socketNew";
                Settings.Default.Save();
            }
        }


        private void RB_DotNet_CheckedChanged(object sender, EventArgs e)
        {
            GB_MonoPathSelection.Enabled = false;
            if (File.Exists(TB_SifPath.Text + @"\sif.exe")
                && IKVMDirectoryAndAssembliesExistOnPath())
            {
                Button_OK.Enabled = true;
            }
            else
            {
                Button_OK.Enabled = false;
            }
        }

        private void tabSocket_Click(object sender, EventArgs e)
        {
        }

        private void tabRuntime_Click(object sender, EventArgs e)
        {
        }

        private void T_Framework_Click(object sender, EventArgs e)
        {
        }

        private void RB_SocketOld_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void RB_JavaNetSocket_CheckedChanged(object sender, EventArgs e)
        {
            if ((string.IsNullOrEmpty(Settings.Default.CommunicationMethod))
                || (Settings.Default.CommunicationMethod.Equals("standard")))
            {
                if ((string.IsNullOrEmpty(Settings.Default.SifOptions))
                    || Settings.Default.SifOptions.Equals("socket"))
                {
                    RB_SocketOld.Checked = true;
                }
                else
                {
                    RB_SocketNew.Checked = true;
                }
            }
        }

        private void GB_DotNetFrameworkSelection_Enter(object sender, EventArgs e)
        {
        }

        private void CB_SifStartup_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void LBL_SifVersion_Click(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {
        }

        private void RB_NetPipe_CheckedChanged(object sender, EventArgs e)
        {
            // TODO not implemented yet
        }

        private void RB_DLLAccess_CheckedChanged(object sender, EventArgs e)
        {
            //TODO not implemented yet
        }

        private void RB_SocketNew_CheckedChanged(object sender, EventArgs e)
        {
        }

        private void TB_SifPath_TextChanged(object sender, EventArgs e)
        {
        }

        private void GB_MonoPathSelection_Enter(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Buitton to browase for the mono instance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_MonoBrowse_Click(object sender, EventArgs e)
        {
            Button_OK.Enabled = false;
            DialogResult monoResult = FBD_MonoDirectorySelect.ShowDialog();
            if (monoResult == DialogResult.OK)
            {
                string monoFilePath = FBD_MonoDirectorySelect.SelectedPath;
                TB_MonoPath.Text = monoFilePath;
            }
        }

        /// <summary>
        /// Handler for the Checkbox if Mono shoudld be used
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RB_Mono_CheckedChanged(object sender, EventArgs e)
        {
            GB_MonoPathSelection.Enabled = true;
            if ((ExecutableExistsOnPath("mono.exe")
                 || File.Exists(TB_MonoPath.Text + @"\mono.exe"))
                && File.Exists(TB_SifPath.Text + @"\sif.exe")
                && IKVMDirectoryAndAssembliesExistOnPath())
            {
                Button_OK.Enabled = true;
            }
            else
            {
                Button_OK.Enabled = false;
            }
        }

        /// <summary>
        /// Checks weather an executable file exists in the specified path
        /// </summary>
        /// <param name="executableFileName"></param>
        /// <returns></returns>
        public static bool ExecutableExistsOnPath(string executableFileName)
        {
            var pathEntries = Environment.GetEnvironmentVariable(("PATH"));
            foreach (var pathPrefix in pathEntries.Split(';'))
            {
                var executablePath = Path.Combine(pathPrefix, executableFileName);
                if (File.Exists(executablePath))
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IKVMDirectoryAndAssembliesExistOnPath()
        {
            string[] assemblyList =
            {
                @"IKVM.Runtime",
                @"IKVM.Runtime.JNI",
                @"IKVM.OpenJDK.Beans",
                @"IKVM.OpenJDK.Charsets",
                @"IKVM.OpenJDK.Cldrdata",
                @"IKVM.OpenJDK.Corba",
                @"IKVM.OpenJDK.Core",
                @"IKVM.OpenJDK.Jdbc",
                @"IKVM.OpenJDK.Localedata",
                @"IKVM.OpenJDK.Management",
                @"IKVM.OpenJDK.Media",
                @"IKVM.OpenJDK.Misc",
                @"IKVM.OpenJDK.Naming",
                @"IKVM.OpenJDK.Nashorn",
                @"IKVM.OpenJDK.Remoting",
                @"IKVM.OpenJDK.Security",
                @"IKVM.OpenJDK.SwingAWT",
                @"IKVM.OpenJDK.Text",
                @"IKVM.OpenJDK.Tools",
                @"IKVM.OpenJDK.Util",
                @"IKVM.OpenJDK.XML.API",
                @"IKVM.OpenJDK.XML.Bind",
                @"IKVM.OpenJDK.XML.Crypto",
                @"IKVM.OpenJDK.XML.Parse",
                @"IKVM.OpenJDK.XML.Transform",
                @"IKVM.OpenJDK.XML.WebServices",
                @"IKVM.OpenJDK.XML.XPath"
            };

            foreach (string assemblyName in assemblyList)
            {
                if (!GacUtil.IsAssemblyInGAC(assemblyName))
                {
                    return false;
                }
            }
            return true;
        }

        private void Button_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void B_SifBrowse_Click(object sender, EventArgs e)
        {
            Button_OK.Enabled = false;
            DialogResult sifResult = FBD_SifDirectorySelect.ShowDialog();
            if (sifResult == DialogResult.OK)
            {
                string sifFilePath = FBD_SifDirectorySelect.SelectedPath;
                TB_SifPath.Text = sifFilePath;
            }
        }

        /// <summary>
        /// Verifies weather there is SIF located on the specified path
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void B_VerifySifPath_Click(object sender, EventArgs e)
        {
            if (CB_SifVersion.SelectedIndex == 0)
            {
                if ((File.Exists(TB_SifPath.Text + @"\sif.jar"))
                    && ExecutableExistsOnPath("java.exe"))
                {
                    Button_OK.Enabled = true;
                }
                else
                {
                    Button_OK.Enabled = false;
                    MessageBox.Show(
                        Resources.NoJreSifInstallationError_MessageBoxText,
                        Resources.NoJreSifInstallationError_MessageBoxTitle);
                }
            }
            else
            {
                if ((File.Exists(TB_SifPath.Text + @"\sif.exe"))
                    && IKVMDirectoryAndAssembliesExistOnPath())
                {
                    if (RB_Mono.Checked
                        && (ExecutableExistsOnPath("mono.exe")
                            || File.Exists(TB_MonoPath + @"\mono.exe")))
                    {
                        Button_OK.Enabled = true;
                    }
                    else
                    {
                        Button_OK.Enabled = true;
                    }
                }
                else
                {
                    Button_OK.Enabled = false;
                    MessageBox.Show(
                        Resources.NoMonoSifInstallaionError_MessageBoxText,
                        Resources.NoMonoSifInstallationError_MessageBoxTitle);
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void FBD_SifDirectorySelect_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}

/**
 * From http://stackoverflow.com/questions/19456547/how-to-programmatically-determine-if-net-assembly-is-installed-in-gac
 */

public static class GacUtil
{
    [DllImport("fusion.dll")]
    private static extern IntPtr CreateAssemblyCache(
        out IAssemblyCache ppAsmCache,
        int reserved);

    [ComImport]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    [Guid("e707dcde-d1cd-11d2-bab9-00c04f8eceae")]
    private interface IAssemblyCache
    {
        int Dummy1();

        [PreserveSig()]
        IntPtr QueryAssemblyInfo(
            int flags,
            [MarshalAs(UnmanagedType.LPWStr)] string assemblyName,
            ref AssemblyInfo assemblyInfo);

        int Dummy2();
        int Dummy3();
        int Dummy4();
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct AssemblyInfo
    {
        public int cbAssemblyInfo;
        public int assemblyFlags;
        public long assemblySizeInKB;

        [MarshalAs(UnmanagedType.LPWStr)] public string currentAssemblyPath;

        public int cchBuf;
    }

    public static bool IsAssemblyInGAC(string assemblyName)
    {
        var assembyInfo = new AssemblyInfo {cchBuf = 512};
        assembyInfo.currentAssemblyPath = new string('\0', assembyInfo.cchBuf);

        IAssemblyCache assemblyCache;

        var hr = CreateAssemblyCache(out assemblyCache, 0);

        if (hr == IntPtr.Zero)
        {
            hr = assemblyCache.QueryAssemblyInfo(
                1,
                assemblyName,
                ref assembyInfo);

            if (hr != IntPtr.Zero)
            {
                return false;
            }

            return true;
        }

        Marshal.ThrowExceptionForHR(hr.ToInt32());
        return false;
    }
}