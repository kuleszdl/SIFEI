using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Networking;
using System.Linq;
using Microsoft.Office.Tools;
using System.Collections.Generic;
using System;
using SIF.Visualization.Excel.ScenarioView;
using SIF.Visualization.Excel.SharedView;
using SIF.Visualization.Excel.ViolationsView;

namespace SIF.Visualization.Excel
{
    public partial class ThisAddIn
    {
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

        private void ThisAddIn_Startup(object sender, EventArgs e)
        {
            InspectionEngine.Instance.Start();

            Globals.ThisAddIn.Application.WorkbookActivate += Application_WorkbookActivate;
        }

        private void ThisAddIn_Shutdown(object sender, EventArgs e)
        {
            InspectionEngine.Instance.Stop();
            Properties.Settings.Default.Save();
        }

        #region Multiple Worksheet Management

        private void Application_WorkbookActivate(Microsoft.Office.Interop.Excel.Workbook Wb)
        {
            // This method is called whenever a workbook comes to the front
            // Does not necessarily need to be a workbook that is persisted on the disk
            var workbook = DataModel.Instance.WorkbookModels.Where(p => ReferenceEquals(p.Workbook, Wb)).FirstOrDefault();
            if (workbook == null)
            {
                workbook = new WorkbookModel(Wb);
                DataModel.Instance.WorkbookModels.Add(workbook);

                DataModel.Instance.CurrentWorkbook = workbook;
                DataModel.Instance.CurrentWorkbook.LoadExtraInformation();

                /// create shared pane
                var sharedPaneContainer = new SharedPaneContainer();
                var sharedPane = CustomTaskPanes.Add(sharedPaneContainer, "Inspection");
                sharedPaneContainer.VisibleChanged += SharedPaneContainer_VisibleChanged;

                sharedPaneContainer.SharedPane.DataContext = workbook;
                sharedPane.Width = 340;
                TaskPanes.Add(new Tuple<WorkbookModel, string>(workbook, "shared Pane"), sharedPane);

                // create findings pane
                var violationViewContainer = new ViolationsViewContainer();
                var taskPane = CustomTaskPanes.Add(violationViewContainer, "Violations");
                violationViewContainer.VisibleChanged += FindingsPaneContainer_VisibleChanged;

                violationViewContainer.ViolationsView.DataContext = workbook;
                TaskPanes.Add(new Tuple<WorkbookModel, string>(workbook, "Violations"), taskPane);

                //create scenario detail pane
                var scenarioDetailPainContainer = new ScenarioDetailPaneContainer();
                var scenarioDetailPane = CustomTaskPanes.Add(scenarioDetailPainContainer, "Scenario");
                scenarioDetailPane.Width = 260;
                scenarioDetailPainContainer.VisibleChanged += ScenarioDetailPaneContainer_VisibleChanged;

                TaskPanes.Add(new Tuple<WorkbookModel, string>(workbook, "Scenario Details"), scenarioDetailPane);

                //add selection changed event handler for ribbon
                Wb.Application.SheetSelectionChange += DataModel.Instance.WorkbookSelectionChangedEventHandler;
                workbook.CellDefinitionChange += DataModel.Instance.CellDefinitionChangedEventHandler;

            }

            DataModel.Instance.CurrentWorkbook = workbook;
        }

        private void SharedPaneContainer_VisibleChanged(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void FindingsPaneContainer_VisibleChanged(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void ScenarioPaneContainer_VisibleChanged(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void ScenarioDetailPaneContainer_VisibleChanged(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void DefineCellsPaneContainer_VisibleChanged(object sender, EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        #endregion

        #region Von VSTO generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InternalStartup()
        {
            Startup += new EventHandler(ThisAddIn_Startup);
            Shutdown += new EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
