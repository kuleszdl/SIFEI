using SIF.Visualization.Excel.Properties;
using System.Threading.Tasks;
using System.Windows;

namespace SIF.Visualization.Excel.Helper {
    /// <summary>
    /// Informs the User when a Scan is done. 
    /// </summary>
    public static class ScanHelper {
        /// <summary>
        /// Handles stuff on the UI when a Scan failed (Error Message, Enabeling Scan Button etc.)
        /// </summary>
        /// <param name="extraInformation"> The extra Information that should get passed to the user in an error</param>
        public static void ScanUnsuccessful(string extraInformation) {
            Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_unsuccessful;
            Globals.Ribbons.Ribbon.scanButton.Enabled = true;
            Globals.Ribbons.Ribbon.scanButton.Label = Resources.tl_Ribbon_AreaScan_ScanButton;
            MessageBox.Show(Resources.tl_Scan_unsuccessfulMessage + "\n" + extraInformation, Resources.tl_Scan_unsuccessful, MessageBoxButton.OK, MessageBoxImage.Error);
            StatusbarControlBack(20000);
        }

        /// <summary>
        /// Handles stuff on the UI when a Scan failed (Error Message, Enabeling Scan Button etc.)
        /// </summary>
        public static void ScanUnsuccessful() {
            Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_unsuccessful;
            Globals.Ribbons.Ribbon.scanButton.Enabled = true;
            Globals.Ribbons.Ribbon.scanButton.Label = Resources.tl_Ribbon_AreaScan_ScanButton;
            MessageBox.Show(Resources.tl_Scan_unsuccessfulMessage, Resources.tl_Scan_unsuccessful, MessageBoxButton.OK, MessageBoxImage.Error);
            StatusbarControlBack(20000);
        }

        /// <summary>
        /// Handles UI when Scan succeeded  (Enabeling Scan Button, giving Excel control back over the status bar etc.)
        /// </summary>
        public static void ScanSuccessful() {
            Globals.Ribbons.Ribbon.scanButton.Enabled = true;
            Globals.Ribbons.Ribbon.scanButton.Label = Resources.tl_Ribbon_AreaScan_ScanButton;
            Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_successful;
            StatusbarControlBack(20000);
        }


        /// <summary>
        /// Gives the control over the statusb ar back to Excel after i miliseconds
        /// </summary>
        /// <param name="i"></param>
        /// <returns> </returns>
        public static async Task StatusbarControlBack(int i) {
            await Task.Delay(i);
            Globals.ThisAddIn.Application.StatusBar = false;
        }

        /// <summary>
        /// Gives the control over the statusbar back to Excel after 10 sec
        /// </summary>
        /// <returns></returns>
        public static async Task StatusbarControlBack() {
            await Task.Delay(10000);
            Globals.ThisAddIn.Application.StatusBar = false;
        }

    }

}
