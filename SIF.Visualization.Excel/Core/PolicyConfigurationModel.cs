namespace SIF.Visualization.Excel.Core
{
    public class PolicyConfigurationModel
    {
        /// <summary>
        ///     Checks weather any automatic scan criterias are enabled
        /// </summary>
        /// <returns></returns>
        public bool HasAutomaticScans()
        {
            return NoConstantsInFormulas || ReadingDirection || FormulaComplexity
                   || MultipleSameRef || NonConsideredConstants || RefToNull
                   || OneAmongOthers || StringDistance || ErrorInCells;
        }

        /// <summary>
        ///     Checks weather any manual scan criterias are enabled
        /// </summary>
        /// <returns></returns>
        public bool hasManualScans()
        {
            return NoConstantsInFormulas || ReadingDirection || FormulaComplexity
                   || MultipleSameRef || NonConsideredConstants || RefToNull
                   || OneAmongOthers || StringDistance || ErrorInCells;
        }

        #region Fields

        #region Settings

        private int formulaComplexity_maxDepth = 2;
        private int formulaComplexity_maxOperations = 5;
        private string oneAmongOthers_style = "both";
        private int oneAmongOthers_length = 3;
        private bool readingDirection_leftRight = true;
        private bool readingDirection_topBottom = true;
        private int stringDistance_minDist = 2;

        public PolicyConfigurationModel()
        {
            ErrorInCells = false;
            StringDistance = false;
            OneAmongOthers = false;
            RefToNull = false;
            NonConsideredConstants = false;
            MultipleSameRef = false;
            ReadingDirection = false;
            NoConstantsInFormulas = false;
            FormulaComplexity = false;
        }

        #endregion

        #endregion

        #region Properties_Settings

        /// <summary>
        ///     Gets or sets weather Formula Complexity should be checked
        /// </summary>
        public bool FormulaComplexity { get; set; }

        public int FormulaComplexityMaxDepth
        {
            get { return formulaComplexity_maxDepth; }
            set { formulaComplexity_maxDepth = value; }
        }

        public int FormulaComplexityMaxOperations
        {
            get { return formulaComplexity_maxOperations; }
            set { formulaComplexity_maxOperations = value; }
        }

        /// <summary>
        ///     Gets or sets weather No Constants in Formulas should be checked
        /// </summary>
        public bool NoConstantsInFormulas { get; set; }

        /// <summary>
        ///     Gets or sets weather reading direction should be checked
        /// </summary>
        public bool ReadingDirection { get; set; }

        public bool ReadingDirectionLeftRight
        {
            get { return readingDirection_leftRight; }
            set { readingDirection_leftRight = value; }
        }

        public bool ReadingDirectionTopBottom
        {
            get { return readingDirection_topBottom; }
            set { readingDirection_topBottom = value; }
        }

        /// <summary>
        ///     Gets or sets weather multiple same references should be checked
        /// </summary>
        public bool MultipleSameRef { get; set; }

        /// <summary>
        ///     Gets or sets weather non considered constants should be checked
        /// </summary>
        public bool NonConsideredConstants { get; set; }

        /// <summary>
        ///     Gets or sets weather references to null in Formulas should be checked
        /// </summary>
        public bool RefToNull { get; set; }

        /// <summary>
        ///     Gets or sets weather the one among others rule should be checked
        /// </summary>
        public bool OneAmongOthers { get; set; }

        public int OneAmongOthersLength
        {
            get { return oneAmongOthers_length; }
            set { oneAmongOthers_length = value; }
        }

        public string OneAmongOthersStyle
        {
            get { return oneAmongOthers_style; }
            set { oneAmongOthers_style = value; }
        }

        /// <summary>
        ///     Gets or sets weather the string distance should be checked
        /// </summary>
        public bool StringDistance { get; set; }


        /// <summary>
        ///     Gets or sets for how big the String Distance should be
        /// </summary>
        public int StringDistanceMinDist
        {
            get { return stringDistance_minDist; }
            set { stringDistance_minDist = value; }
        }

        /// <summary>
        /// </summary>
        public bool ErrorInCells { get; set; }

        #endregion
    }
}