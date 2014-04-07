using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Networking;
using System.Windows.Forms;
using System.Linq;
using Microsoft.Office.Tools;
using System.Collections.Generic;
using System;
using SIF.Visualization.Excel.ScenarioCore;
using Microsoft.Office.Interop.Excel;
using SIF.Visualization.Excel.ScenarioView;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.SharedView;

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
                if (this.taskPanes == null) this.taskPanes = new Dictionary<Tuple<WorkbookModel, string>, CustomTaskPane>();
                return this.taskPanes;
            }
        }

        #endregion

        private void ThisAddIn_Startup(object sender, System.EventArgs e)
        {
            InspectionEngine.Instance.Start();
            
            Globals.ThisAddIn.Application.WorkbookActivate += Application_WorkbookActivate;
        }

        private void ThisAddIn_Shutdown(object sender, System.EventArgs e)
        {
            InspectionEngine.Instance.Stop();
        }

        #region Multiple Worksheet Management

        private void Application_WorkbookActivate(Microsoft.Office.Interop.Excel.Workbook Wb)
        {
            // This method is called whenever a workbook comes to the front
            // Does not necessarily need to be a workbook that is persisted on the disk
            var workbook = DataModel.Instance.WorkbookModels.Where(p => object.ReferenceEquals(p.Workbook, Wb)).FirstOrDefault();
            if (workbook == null)
            {
                workbook = new WorkbookModel(Wb);
                DataModel.Instance.WorkbookModels.Add(workbook);

                DataModel.Instance.CurrentWorkbook = workbook;

                /// create shared pane
                var sharedPaneContainer = new SharedPaneContainer();
                var sharedPane = this.CustomTaskPanes.Add(sharedPaneContainer, "Inspection");
                sharedPaneContainer.VisibleChanged += SharedPaneContainer_VisibleChanged;

                sharedPaneContainer.SharedPane.DataContext = workbook;
                this.TaskPanes.Add(new Tuple<WorkbookModel, string>(workbook, "shared Pane"), sharedPane);

                // create findings pane
                var findingsPaneContainer = new FindingsPaneContainer();
                var taskPane = this.CustomTaskPanes.Add(findingsPaneContainer, "Findings");
                findingsPaneContainer.VisibleChanged += FindingsPaneContainer_VisibleChanged;

                findingsPaneContainer.FindingsPane.DataContext = workbook;
                this.TaskPanes.Add(new Tuple<WorkbookModel, string>(workbook, "Findings"), taskPane);

                //create scenario detail pane
                var scenarioDetailPainContainer = new ScenarioDetailPaneContainer();
                var scenarioDetailPane = this.CustomTaskPanes.Add(scenarioDetailPainContainer, "Scenario");
                scenarioDetailPane.Width = 260;
                scenarioDetailPainContainer.VisibleChanged += ScenarioDetailPaneContainer_VisibleChanged;

                this.TaskPanes.Add(new Tuple<WorkbookModel,string>(workbook, "Scenario Details"), scenarioDetailPane);

                //add selection changed event handler for ribbon
                Wb.Application.SheetSelectionChange += DataModel.Instance.WorkbookSelectionChangedEventHandler;
                workbook.CellDefinitionChange += DataModel.Instance.CellDefinitionChangedEventHandler;

            }

            DataModel.Instance.CurrentWorkbook = workbook;
        }

        private void SharedPaneContainer_VisibleChanged(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void FindingsPaneContainer_VisibleChanged(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void ScenarioPaneContainer_VisibleChanged(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void ScenarioDetailPaneContainer_VisibleChanged(object sender, System.EventArgs e)
        {
            //throw new System.NotImplementedException();
        }

        private void DefineCellsPaneContainer_VisibleChanged(object sender, System.EventArgs e)
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
            this.Startup += new System.EventHandler(ThisAddIn_Startup);
            this.Shutdown += new System.EventHandler(ThisAddIn_Shutdown);
        }

        #endregion
    }
}
