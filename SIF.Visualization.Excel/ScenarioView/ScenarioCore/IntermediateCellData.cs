using SIF.Visualization.Excel.ScenarioCore.Visitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.ScenarioCore
{
    public class IntermediateCellData : CellData, IAcceptVisitor
    {
        #region Fields

        private double differenceUp = Properties.Settings.Default.StandartDifference;
        private double differenceDown = Properties.Settings.Default.StandartDifference;

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the up difference of this scenario.
        /// </summary>
        public double DifferenceUp
        {
            get { return this.differenceUp; }
            set { this.SetProperty(ref this.differenceUp, value); }
        }

        /// <summary>
        /// Gets or sets the down difference of this scenario.
        /// </summary>
        public double DifferenceDown
        {
            get { return this.differenceDown; }
            set { this.SetProperty(ref this.differenceDown, value); }
        }

        #endregion

        public IntermediateCellData()
        {
        }


        #region Accept Visitor
        public object Accept(IVisitor v)
        {
            return v.Visit(this);
        }
        #endregion
    }
}
