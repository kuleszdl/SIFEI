using System;

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
            get { return mustBeLeftToRightReadable; }
            set { SetProperty(ref mustBeLeftToRightReadable, value); }
        }

        /// <summary>
        /// Gets or sets the mustBeTopToDownReadable of the current rule.
        /// </summary>
        public Boolean MustBeTopToDownReadable
        {
            get { return mustBeTopToDownReadable; }
            set { SetProperty(ref mustBeTopToDownReadable, value); }
        }

        #endregion
    }
}
