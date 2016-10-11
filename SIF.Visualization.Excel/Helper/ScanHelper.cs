using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.Helper
{
    /// <summary>
    /// Informs the User when a Scan is done. 
    /// </summary>
    public static class  ScanHelper
    {
       /// <summary>
       /// Handles stuff on the UI when a Scan failed (Error Message, Enabeling Scan Button etc.)
       /// </summary>
       /// <param name="extraInformation"> The extra Information that should get passed to the user in an error</param>
       public static void ScanUnsuccessful(string extraInformation)
       {
           Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_unsuccessful;
           Globals.Ribbons.Ribbon.scanButton.Enabled = true;
           Globals.Ribbons.Ribbon.scanButton.Label =
           Resources.tl_Ribbon_AreaScan_ScanButton;
           MessageBox.Show(Resources.tl_Scan_unsuccessfulMessage + "\n" + extraInformation, Resources.tl_Scan_unsuccessful,
                       MessageBoxButton.OK, MessageBoxImage.Error);
           StatusbarControlBack(20000);
       }

       /// <summary>
       /// Handles stuff on the UI when a Scan failed (Error Message, Enabeling Scan Button etc.)
       /// </summary>
       public static void ScanUnsuccessful()
       {
           Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_unsuccessful;
           Globals.Ribbons.Ribbon.scanButton.Enabled = true;
           Globals.Ribbons.Ribbon.scanButton.Label =
           Resources.tl_Ribbon_AreaScan_ScanButton;
           MessageBox.Show(Resources.tl_Scan_unsuccessfulMessage, Resources.tl_Scan_unsuccessful,
                       MessageBoxButton.OK, MessageBoxImage.Error);
           StatusbarControlBack(20000);
       }

       /// <summary>
       /// Handles UI when Scan succeeded  (Enabeling Scan Button, giving Excel control back over the status bar etc.)
       /// </summary>
       public static void ScanSuccessful()
       {
           Globals.Ribbons.Ribbon.scanButton.Enabled = true;
           Globals.Ribbons.Ribbon.scanButton.Label =
               Resources.tl_Ribbon_AreaScan_ScanButton;
           Globals.ThisAddIn.Application.StatusBar = Resources.tl_Scan_successful;

          StatusbarControlBack(20000);
       }


       /// <summary>
       /// Gives the control over the statusb ar back to Excel after i miliseconds
       /// </summary>
       /// <param name="i"></param>
       /// <returns> </returns>
       public static async Task StatusbarControlBack(int i)
       {
           await Task.Delay(i);
           Globals.ThisAddIn.Application.StatusBar = false;
       }

       /// <summary>
       /// Gives the control over the statusbar back to Excel after 10 sec
       /// </summary>
       /// <returns></returns>
       public static async Task StatusbarControlBack()
       {
           await Task.Delay(10000);
           Globals.ThisAddIn.Application.StatusBar = false;
       }

        /// <summary>
        /// Handles saving the document before a scan
        /// </summary>
        /// <returns></returns>
        public static bool SaveBefore(InspectionType inspectionType)
        {
            try
            {
                if (DataModel.Instance.CurrentWorkbook.Workbook.Path.Length <= 0)
                {
                    //If there already exists a file with the same name in the same location save this file appended by the current minute. Not 100% save
                    // but for the first few files its ok
                    if (
                        DataModel.Instance.WorkbookModels.Any(
                            workbookModel =>
                                workbookModel.Workbook.Name.Equals(DataModel.Instance.CurrentWorkbook.Workbook.Name +
                                                                   "." + XlFileFormat.xlWorkbookDefault)))
                    {
                        DataModel.Instance.CurrentWorkbook.Workbook.SaveAs(
                            DataModel.Instance.CurrentWorkbook.Workbook.Name + DateTime.Now.Minute
                            + ". " + XlFileFormat.xlWorkbookDefault);
                        }
                    else
                    {
                        DataModel.Instance.CurrentWorkbook.Workbook.SaveAs();
                    }
                    DataModel.Instance.CurrentWorkbook.Inspect(inspectionType);
                    return true;
                }
                else
                {
                    // Inspect the current workbook
                    DataModel.Instance.CurrentWorkbook.Inspect(inspectionType);
                    return true;
                }
            }
                // COMException gets raised: 1. when User cancles save process
                //  2. User starts a scan while a cell with formulas is opened
                // 3. A File with the same name is already saved at this location
                // Please add future additional reasons since this COMException seems to happen for a couple of reasons. Even with Same HResult the reasons may differ
            catch (COMException ex)
            {
                //File is not saved
                if (DataModel.Instance.CurrentWorkbook.Workbook.Path.Length <= 0)
                {
                    ScanUnsuccessful(Resources.tl_Scan_needssave);
                }
                // Cell is getting eddited
                else
                {
                    ScanUnsuccessful(Resources.tl_stopEdit);
                }
                return false;
            }
        }


    }




}
