using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using Microsoft.Office.Tools;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.View;

namespace SIF.Visualization.Excel
{
    public partial class ThisAddIn
    {
        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            Globals.ThisAddIn.Application.WorkbookActivate += Application_WorkbookActivate;
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }

        #region Multiple Worksheet Management

        private void Application_WorkbookActivate(Workbook Wb)
        {
            // This method is called whenever a workbook comes to the front
            // Does not necessarily need to be a workbook that is persisted on the disk
            var workbook = DataModel.Instance.WorkbookModels.Where(p => ReferenceEquals(p.Workbook, Wb))
                .FirstOrDefault();
            if (workbook == null)
            {
                workbook = new WorkbookModel(Wb);
                DataModel.Instance.WorkbookModels.Add(workbook);

                DataModel.Instance.CurrentWorkbook = workbook;
                DataModel.Instance.CurrentWorkbook.LoadExtraInformation();

                /// create shared pane
                var SidebarContainer = new SidebarContainer();
                var Sidebar = CustomTaskPanes.Add(SidebarContainer, "Sidebar");

                SidebarContainer.Sidebar.DataContext = workbook;
                Sidebar.Width = 320;
                TaskPanes.Add(new Tuple<WorkbookModel, string>(workbook, "Sidebar"), Sidebar);

                //add selection changed event handler for ribbon
                Wb.Application.SheetSelectionChange += DataModel.Instance.WorkbookSelectionChangedEventHandler;
            }

            DataModel.Instance.CurrentWorkbook = workbook;
        }

        #endregion

        #region Von VSTO generierter Code

        /// <summary>
        ///     Erforderliche Methode für die Designerunterstützung.
        ///     Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InternalStartup()
        {
            Startup += ThisAddIn_Startup;
            Shutdown += ThisAddIn_Shutdown;
        }

        #endregion

        #region Properties

        private Dictionary<Tuple<WorkbookModel, string>, CustomTaskPane> taskPanes;

        internal Dictionary<Tuple<WorkbookModel, string>, CustomTaskPane> TaskPanes
        {
            get
            {
                if (taskPanes == null) taskPanes = new Dictionary<Tuple<WorkbookModel, string>, CustomTaskPane>();
                return taskPanes;
            }
        }

        #endregion
    }
}