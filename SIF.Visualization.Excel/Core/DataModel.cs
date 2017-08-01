using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Office.Interop.Excel;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    ///     This is the global data model class.
    /// </summary>
    public class DataModel : BindableBase
    {
        #region Singleton

        private static volatile DataModel instance;
        private static readonly object syncRoot = new object();

        private DataModel()
        {
        }

        public static DataModel Instance
        {
            get
            {
                if (instance == null)
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DataModel();
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
        ///     Gets or sets the current workbook.
        /// </summary>
        public WorkbookModel CurrentWorkbook
        {
            get { return currentWorkbook; }
            set { SetProperty(ref currentWorkbook, value); }
        }

        /// <summary>
        ///     Gets the worksheet models.
        /// </summary>
        public ObservableCollection<WorkbookModel> WorkbookModels
        {
            get
            {
                if (workbookModels == null) workbookModels = new ObservableCollection<WorkbookModel>();
                return workbookModels;
            }
        }

        public AppEvents_SheetSelectionChangeEventHandler WorkbookSelectionChangedEventHandler { get; set; }

        #endregion

        #region Operators

        /// <summary>
        ///     Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as DataModel;
            if ((object) other == null) return false;

            return CurrentWorkbook == other.CurrentWorkbook &&
                   WorkbookModels.SequenceEqual(other.WorkbookModels);
        }

        /// <summary>
        ///     Serves as a hash function for a particular type.
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        ///     Determines, whether two objects are equal.
        /// </summary>
        /// <param name="a">The first instance.</param>
        /// <param name="b">The second instance.</param>
        /// <returns>true, if the given instances are equal; otherwise, false.</returns>
        public static bool operator ==(DataModel a, DataModel b)
        {
            if (ReferenceEquals(a, b)) return true;
            if ((object) a == null || (object) b == null) return false;

            return a.Equals(b);
        }

        /// <summary>
        ///     Determines, whether two objects are inequal.
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