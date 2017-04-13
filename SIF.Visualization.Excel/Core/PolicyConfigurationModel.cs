using System;

namespace SIF.Visualization.Excel.Core
{
    public class PolicyConfigurationModel
    {
        #region Fields

        #region Settings
        private Boolean errorInCells = false;
        private Boolean formulaComplexity = false;
        private Boolean multipleSameRef = false;
        private Boolean noConstantsInFormulas = false;
        private Boolean nonConsideredConstants = false;
        private Boolean oneAmongOthers = false;
        private Boolean readingDirection = false;
        private Boolean refToNull = false;
        private Boolean stringDistance = false;

        private Int32 formulaComplexity_maxDepth = 2;
        private Int32 formulaComplexity_maxOperations = 5;
        private String oneAmongOthers_style = "both";
        private Int32 oneAmongOthers_length = 3;
        private Boolean readingDirection_leftRight = true;
        private Boolean readingDirection_topBottom = true;
        private Int32 stringDistance_minDist = 2;
        #endregion

        #endregion

        #region Properties_Settings
        /// <summary>
        /// Gets or sets weather Formula Complexity should be checked 
        /// </summary>
        public Boolean FormulaComplexity
        {
            get { return formulaComplexity; }
            set { formulaComplexity = value; }
        }

        public Int32 FormulaComplexityMaxDepth {
            get { return formulaComplexity_maxDepth; }
            set { formulaComplexity_maxDepth = value; }
        }

        public Int32 FormulaComplexityMaxOperations {
            get { return formulaComplexity_maxOperations; }
            set { formulaComplexity_maxOperations = value; }
        }

        /// <summary>
        /// Gets or sets weather No Constants in Formulas should be checked
        /// </summary>
        public Boolean NoConstantsInFormulas
        {
            get { return noConstantsInFormulas; }
            set { noConstantsInFormulas = value; }
        }

        /// <summary>
        /// Gets or sets weather reading direction should be checked 
        /// </summary>
        public Boolean ReadingDirection
        {
            get { return readingDirection; }
            set { readingDirection = value; }
        }

        public Boolean ReadingDirectionLeftRight {
            get { return readingDirection_leftRight; }
            set { readingDirection_leftRight = value; }
        }

        public Boolean ReadingDirectionTopBottom {
            get { return readingDirection_topBottom; }
            set { readingDirection_topBottom = value; }
        }

        /// <summary>
        /// Gets or sets weather multiple same references should be checked
        /// </summary>
        public Boolean MultipleSameRef
        {
            get { return multipleSameRef; }
            set { multipleSameRef = value; }
        }

        /// <summary>
        /// Gets or sets weather non considered constants should be checked
        /// </summary>
        public Boolean NonConsideredConstants
        {
            get { return nonConsideredConstants; }
            set { nonConsideredConstants = value; }
        }

        /// <summary>
        /// Gets or sets weather references to null in Formulas should be checked
        /// </summary>
        public Boolean RefToNull
        {
            get { return refToNull; }
            set { refToNull = value; }
        }

        /// <summary>
        /// Gets or sets weather the one among others rule should be checked
        /// </summary>
        public Boolean OneAmongOthers
        {
            get { return oneAmongOthers; }
            set { oneAmongOthers = value; }
        }

        public Int32 OneAmongOthersLength {
            get { return oneAmongOthers_length; }
            set { oneAmongOthers_length = value; }
        }

        public String OneAmongOthersStyle {
            get { return oneAmongOthers_style; }
            set { oneAmongOthers_style = value; }
        }

        /// <summary>
        /// Gets or sets weather the string distance should be checked
        /// </summary>
        public Boolean StringDistance
        {
            get { return stringDistance; }
            set { stringDistance = value; }
        }


        /// <summary>
        /// Gets or sets for how big the String Distance should be
        /// </summary>
        public Int32 StringDistanceMinDist
        {
            get { return stringDistance_minDist; }
            set { stringDistance_minDist = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Boolean ErrorInCells
        {
            get { return errorInCells; }
            set { errorInCells = value; }
        }

        #endregion

        /// <summary>
        /// Checks weather any automatic scan criterias are enabled
        /// </summary>
        /// <returns></returns>
        public Boolean HasAutomaticScans()
        {
            return noConstantsInFormulas || readingDirection || formulaComplexity
                || multipleSameRef || nonConsideredConstants || refToNull
                || oneAmongOthers || stringDistance || errorInCells;
        }

        /// <summary>
        /// Checks weather any manual scan criterias are enabled
        /// </summary>
        /// <returns></returns>
        public Boolean hasManualScans()
        {
            return noConstantsInFormulas || readingDirection || formulaComplexity
                || multipleSameRef || nonConsideredConstants || refToNull
                || oneAmongOthers || stringDistance || errorInCells;
        }
    }
}
