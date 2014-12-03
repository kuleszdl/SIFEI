using System.Dynamic;
using SIF.Visualization.Excel.Networking;
using SIF.Visualization.Excel.Properties;
using SIF.Visualization.Excel.ViolationsView;
using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
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
            get { return this.workbook; }
            set { this.SetProperty(ref this.workbook, value); }
        }

        /// <summary>
        /// Gets or sets the path of the spreadsheet that is under evaluation.
        /// </summary>
        public string SpreadsheetPath
        {
            get { return this.spreadsheetPath; }
            set { this.SetProperty(ref this.spreadsheetPath, value); }
        }

        // <summary>
        /// Gets or sets the policy of the spreadsheet that is under evaluation.
        /// </summary>
        public XDocument PolicyXML
        {
            get { return this.policyXML; }
            set { this.SetProperty(ref this.policyXML, value); }
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

            return this.Workbook == other.Workbook &&
                   this.SpreadsheetPath == other.SpreadsheetPath &&
                   this.policyXML == other.policyXML;
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
            if (System.Object.ReferenceEquals(a, b)) return true;
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

        public InspectionJob(WorkbookModel workbook, string spreadsheetPath, XDocument policyXML)
        {
            this.workbook = workbook;
            this.spreadsheetPath = spreadsheetPath;
            this.policyXML = policyXML;
        }

        public void DeleteWorkbookFile()
        {
            try
            {
                File.Delete(this.SpreadsheetPath);
            }
            catch (FileNotFoundException)
            {
                // ignore silently
            }
        }

        /// <summary>
        /// The execution has been successful, now handle the report.
        /// </summary>
        public void Finalize(string report)
        {
            DeleteWorkbookFile();
            if (this.Workbook == null) return;
            // Execute on the right dispatcher
            (Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(this.Workbook, "Violations")].Control as ViolationsViewContainer).ViolationsView.Dispatcher.Invoke(() =>
            {
                this.Workbook.Load(report);
            });
        }

        #endregion
    }
}
