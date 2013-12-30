using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.ScenarioCore.StaticScenarios
{
    public class ReadingDirection : StaticScenarioRule
    {
        Boolean mustBeLeftToRightReadable;
        Boolean mustBeTopToDownReadable;
        
        //Future work: ignored cells

        #region Properties

        /// <summary>
        /// Gets or sets the mustBeLeftToRightReadable of the current rule.
        /// </summary>
        public Boolean MustBeLeftToRightReadable
        {
            get 
            {
                if (mustBeLeftToRightReadable == null) this.mustBeLeftToRightReadable = false;
                return this.mustBeLeftToRightReadable; 
            }
            set { this.SetProperty(ref this.mustBeLeftToRightReadable, value); }
        }

        /// <summary>
        /// Gets or sets the mustBeTopToDownReadable of the current rule.
        /// </summary>
        public Boolean MustBeTopToDownReadable
        {
            get
            {
                if (mustBeTopToDownReadable == null) this.mustBeTopToDownReadable = false;
                return this.mustBeTopToDownReadable;
            }
            set { this.SetProperty(ref this.mustBeTopToDownReadable, value); }
        }

        #endregion
    }
}
