using SIF.Visualization.Excel.ViolationsView;
using System;
using System.IO;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    /// <summary>
    /// Instance of an inspection
    /// </summary>
    public class InspectionJob : BindableBase
    {
        #region Fields

        private WorkbookModel workbook;
        private string spreadsheetPath;
        private XDocument policyXML;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the workbook that needs to be inspected.
        /// </summary>
        public WorkbookModel Workbook
        {
            get { return workbook; }
            set { SetProperty(ref workbook, value); }
        }

        /// <summary>
        /// Gets or sets the path of the spreadsheet that is under evaluation.
        /// </summary>
        public string SpreadsheetPath
        {
            get { return spreadsheetPath; }
            set { SetProperty(ref spreadsheetPath, value); }
        }

        /// <summary>
        /// Gets or sets the policy of the spreadsheet that is under evaluation.
        /// </summary>
        public XDocument PolicyXML
        {
            get { return policyXML; }
            set { SetProperty(ref policyXML, value); }
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
            var other = obj as InspectionJob;
            if ((object)other == null) return false;

            return Workbook == other.Workbook &&
                   SpreadsheetPath == other.SpreadsheetPath &&
                   policyXML == other.policyXML;
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
        public static bool operator ==(InspectionJob a, InspectionJob b)
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
        public static bool operator !=(InspectionJob a, InspectionJob b)
        {
            return !(a == b);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Constructor to  create a new Inspection Job
        /// </summary>
        /// <param name="workbook"> Workbook that should be inspected</param>
        /// <param name="spreadsheetPath">Path where the Sheet is saved</param>
        /// <param name="policyXML"> The XML where it is defined which rules should be checked</param>
        public InspectionJob(WorkbookModel workbook, string spreadsheetPath, XDocument policyXML)
        {
            this.workbook = workbook;
            this.spreadsheetPath = spreadsheetPath;
            this.policyXML = policyXML;
        }

        /// <summary>
        /// Deletes the Workbook saved in the before specified Path
        /// </summary>
        public void DeleteWorkbookFile()
        {
            try
            {
                File.Delete(SpreadsheetPath);
            }
            catch (FileNotFoundException)
            {
                // ignore silently
            }
        }

        /// <summary>
        /// The scan has been successful, now handle the report to finalize the whole process
        /// </summary>
        public void Finalize(string report)
        {
            DeleteWorkbookFile();
            if (Workbook == null) return;
            // Execute on the right dispatcher
            (Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(Workbook, "Violations")].Control as ViolationsViewContainer).ViolationsView.Dispatcher.Invoke(() =>
            {
                Workbook.Load(report);
            });
        }

        #endregion
    }
}
