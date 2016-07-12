using SIF.Visualization.Excel.ViolationsView;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// This is the global data model class.
    /// </summary>
    public class DataModel : BindableBase
    {
        #region Singleton

        private static volatile DataModel instance;
        private static object syncRoot = new Object();

        private DataModel() { }

        public static DataModel Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataModel();
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Fields

        private WorkbookModel currentWorkbook;
        private ObservableCollection<WorkbookModel> workbookModels;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current workbook.
        /// </summary>
        public WorkbookModel CurrentWorkbook
        {
            get
            {
                if (currentWorkbook == null)
                {
                    // This method is called whenever a workbook comes to the front
                    // Does not necessarily need to be a workbook that is persisted on the disk
                    var workbook = Instance.WorkbookModels.Where(p => ReferenceEquals(p.Workbook, Globals.ThisAddIn.Application.ActiveWorkbook)).FirstOrDefault();
                    if (workbook == null)
                    {
                        workbook = new WorkbookModel(Globals.ThisAddIn.Application.ActiveWorkbook);
                        Instance.WorkbookModels.Add(workbook);

                        Instance.CurrentWorkbook = workbook;

                        var violationsViewContainer = new ViolationsViewContainer();
                        var taskPane = Globals.ThisAddIn.CustomTaskPanes.Add(violationsViewContainer, "Violations");

                        violationsViewContainer.ViolationsView.DataContext = workbook;

                        Globals.ThisAddIn.TaskPanes.Add(new Tuple<WorkbookModel, string>(workbook, "Violations"), taskPane);
                    }

                    currentWorkbook = workbook;
                }
                return currentWorkbook;
            }
            set { SetProperty(ref currentWorkbook, value); }
        }

        /// <summary>
        /// Gets the worksheet models.
        /// </summary>
        public ObservableCollection<WorkbookModel> WorkbookModels
        {
            get
            {
                if (workbookModels == null) workbookModels = new ObservableCollection<WorkbookModel>();
                return workbookModels;
            }
        }

        public Microsoft.Office.Interop.Excel.AppEvents_SheetSelectionChangeEventHandler WorkbookSelectionChangedEventHandler
        {
            get;
            set;
        }

        public WorkbookModel.CellDefinitionChangeHandler CellDefinitionChangedEventHandler
        {
            get;
            set;
        }

        #endregion

        #region Operators

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            DataModel other = obj as DataModel;
            if ((object)other == null) return false;

            return CurrentWorkbook == other.CurrentWorkbook &&
                   WorkbookModels.SequenceEqual(other.WorkbookModels);
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Determines, whether two objects are equal.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>true, if the given instances are equal; otherwise, false.</returns>
        public static bool operator ==(DataModel a, DataModel b)
        {
            if (ReferenceEquals(a, b)) return true;
            if (((object)a == null) || ((object)b == null)) return false;

            return a.Equals(b);
        }

        /// <summary>
        /// Determines, whether two objects are inequal.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>true, if the given instances are inequal; otherwise, false.</returns>
        public static bool operator !=(DataModel a, DataModel b)
        {
            return !(a == b);
        }

        #endregion

       
    }
}
