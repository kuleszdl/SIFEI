using Microsoft.Office.Interop.Excel;
using System;
using System.Diagnostics;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SIF.Visualization.Excel.Core {
    /// <summary>
    /// Models one Violation in the Workbook
    /// </summary>
    public class Violation : BindableBase {
        
        #region Fields
        private string id = "";
        private string description;
        private string location;
        private double severity;
        private Policy policy;
        private DateTime firstOccurrence;
        private DateTime solvedTime;
        private bool foundAgain = false;
        private bool isRead = false;
        private bool isSelected = false;
        private bool isCellSelected = false;
        private ViolationState state = ViolationState.OPEN;
        private Workbook workbook;
        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the description of this violation.
        /// </summary>
        public string Id {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        /// <summary>
        /// Gets or sets the description of this violation.
        /// </summary>
        public string Description {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        /// <summary>
        /// Gets or sets the location of this violation.
        /// </summary>
        public string Location {
            get { return location; }
            set { SetProperty(ref location, value); }
        }

        /// <summary>
        /// Gets or sets the severity of this violation.
        /// </summary>
        public double Severity {
            get { return severity; }
            set { SetProperty(ref severity, value); }
        }

        /// <summary>
        /// Gets or sets the first occurrence of this violation.
        /// </summary>
        public DateTime FirstOccurrence {
            get { return firstOccurrence; }
            set { SetProperty(ref firstOccurrence, value); }
        }

        /// <summary>
        /// Gets or sets the found again value of this violation.
        /// </summary>
        public bool FoundAgain {
            get { return foundAgain; }
            set { foundAgain = value; }
        }

        /// <summary>
        /// Gets or sets the Policy of this violation.
        /// </summary>
        public Policy Policy {
            get { return policy; }
            set { SetProperty(ref policy, value); }
        }

        /// <summary>
        /// Gets or sets a value that shows whether this violation has been read or not
        /// </summary>
        public bool IsRead {
            get { return isRead; }
            set { SetProperty(ref isRead, value); }
        }


        /// <summary>
        /// Gets or sets a value that indicates whether this violation is selected in the user interface.
        /// </summary>
        public bool IsSelected {
            get { return isSelected; }
            set {
                if (value) {
                    IsRead = true;
                }
                SetProperty(ref isSelected, value);
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether this violation is selected in the user interface.
        /// </summary>
        public bool IsCellSelected {
            get { return isCellSelected; }
            set {
                if (!value)
                    IsSelected = false;
                SetProperty(ref isCellSelected, value);
            }
        }

        /// <summary>
        /// Gets or sets the time when this violation has been solved
        /// </summary>
        public DateTime SolvedTime {
            get { return solvedTime; }
            set { SetProperty(ref solvedTime, value); }
        }

        /// <summary>
        /// Gets or sets the state of this violation.
        /// </summary>
        public ViolationState ViolationState {
            get { return state; }
            set { this.state = value; }
        }

        #endregion

        #region Operators

        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj) {
            Violation other = obj as Violation;
            if ((object) other != null)
                return id.Equals(other.Id);
            return false;
        }

        /// <summary>
        /// Serves as a hash function for a particular type. 
        /// </summary>
        /// <returns>A hash code for the current Object.</returns>
        public override int GetHashCode() {
            return id.GetHashCode();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Serialization Constructor of a violation
        /// </summary>
        public Violation() {}

        /// <summary>
        /// Constructor of a violation
        /// </summary>
        /// <param name="root">the root XML element</param>
        /// <param name="workbook">the current workbook</param>
        /// <param name="scanTime">the time when this violation has been occurred</param>
        /// <param name="policy">the Policy of this violation</param>
        public Violation(XElement root, Workbook workbook, DateTime scanTime, Policy policy) {
            this.workbook = workbook;
            this.firstOccurrence = scanTime;
            this.policy = policy;
            
            try {
                id = (string) root.Element(XName.Get("uid"));         
                description = (string) root.Element(XName.Get("description"));
                location = (string) root.Element(XName.Get("location"));
                severity = Double.Parse((string) root.Element(XName.Get("severity")));
                if (String.IsNullOrEmpty(id) || String.IsNullOrEmpty(description) || String.IsNullOrEmpty(location))
                    throw new Exception("Could not create violation: malformed or incomplete xml");
            } catch (Exception e) {
                Debug.WriteLine(e);
            }
        }
        #endregion

        #region Methods


        #endregion
    }
}
