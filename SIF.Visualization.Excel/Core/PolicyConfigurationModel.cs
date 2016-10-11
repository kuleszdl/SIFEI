using System;
using System.Xml.Linq;

namespace SIF.Visualization.Excel.Core
{
    public class PolicyConfigurationModel
    {
        #region Fields
        #region XML Names

        private String name_formulaComplexity = "fc",
            name_formulaComplexityAutomatic = "fc_a",
            name_noConstantsInFormulas = "ncif",
            name_noConstantsInFormulasAutomatic = "ncif_a",
            name_readingDirection = "rd",
            name_readingDirectionAutomatic = "rd_a",
            name_multipleSameRef = "msr",
            name_multipleSameRefAutomatic = "msr_a",
            name_nonConsideredConstants = "ncc",
            name_nonConsideredConstantsAutomatic = "ncc_a",
            name_refToNull = "rtn",
            name_refToNullAutomatic = "rtn_a",
            name_oneAmongOthers = "oao",
            name_oneAmongOthersAutomatic = "oao_a",
            name_stringDistance = "sd",
            name_stringDistanceAutomatic = "sd_a",
            name_stringDistanceMaxDist = "sd_maxDist",
            name_errorInCells = "eic",
            name_errorInCellsAutomatic = "eic_a";
        #endregion
        #region Settings

        private Boolean formulaComplexity = false,
            formulaComplexityAutomatic = true,
            noConstantsInFormulas = false,
            noConstantsInFormulasAutomatic = true,
            readingDirection = false,
            readingDirectionAutomatic = true,
            multipleSameRef = false,
            multipleSameRefAutomatic = true,
            nonConsideredConstants = false,
            nonConsideredConstantsAutomatic = true,
            refToNull = false,
            refToNullAutomatic = true,
            oneAmongOthers = false,
            oneAmongOthersAutomatic = true,
            stringDistance = false,
            stringDistanceAutomatic = true,
            errorInCells = false,
            errorInCellsAutomatic = true;

        private int stringDistanceMaxDist = 1;

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

        /// <summary>
        /// Gets or sets weather Formula Complexity should be checked  in automatic scans
        /// </summary>
        public Boolean FormulaComplexityAutomatic
        {
            get { return formulaComplexityAutomatic; }
            set { formulaComplexityAutomatic = value; }
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
        /// Gets or sets weather No Constants in Formulas should be checked in automatic scans
        /// </summary>
        public Boolean NoConstantsInFormulasAutomatic
        {
            get { return noConstantsInFormulasAutomatic; }
            set { noConstantsInFormulasAutomatic = value; }
        }

        /// <summary>
        /// Gets or sets weather reading direction should be checked 
        /// </summary>
        public Boolean ReadingDirection
        {
            get { return readingDirection; }
            set { readingDirection = value; }
        }

        /// <summary>
        /// Gets or sets weather reading directions  should be checked in automatic scans
        /// </summary>
        public Boolean ReadingDirectionAutomatic
        {
            get { return readingDirectionAutomatic; }
            set { readingDirectionAutomatic = value; }
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
        /// Gets or sets weather multiple same references should be checked in automatic scans
        /// </summary>
        public Boolean MultipleSameRefAutomatic
        {
            get { return multipleSameRefAutomatic; }
            set { multipleSameRefAutomatic = value; }
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
        /// Gets or sets weather non considered constants should be checked in automatic scans
        /// </summary>
        public Boolean NonConsideredConstantsAutomatic
        {
            get { return nonConsideredConstantsAutomatic; }
            set { nonConsideredConstantsAutomatic = value; }
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
        /// Gets or sets weather references to null should be checked in automatic scans
        /// </summary>
        public Boolean RefToNullAutomatic
        {
            get { return refToNullAutomatic; }
            set { refToNullAutomatic = value; }
        }

        /// <summary>
        /// Gets or sets weather the one among others rule should be checked
        /// </summary>
        public Boolean OneAmongOthers
        {
            get { return oneAmongOthers; }
            set { oneAmongOthers = value; }
        }

        /// <summary>
        /// Gets or sets weather the one among others rule should be checked in automatic scans
        /// </summary>
        public Boolean OneAmongOthersAutomatic
        {
            get { return oneAmongOthersAutomatic; }
            set { oneAmongOthersAutomatic = value; }
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
        /// /// <summary>
        /// Gets or sets weather the string distance should be checked in automatic scans
        /// </summary>
        /// </summary>
        public Boolean StringDistanceAutomatic
        {
            get { return stringDistanceAutomatic; }
            set { stringDistanceAutomatic = value; }
        }

        /// <summary>
        /// Gets or sets for how big the String Distance should be
        /// </summary>
        public int StringDistanceMaxDist
        {
            get { return stringDistanceMaxDist; }
            set { stringDistanceMaxDist = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public Boolean ErrorInCellsAutomatic
        {
            get { return errorInCellsAutomatic; }
            set { errorInCellsAutomatic = value; }
        }
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
            return noConstantsInFormulasAutomatic || readingDirectionAutomatic || formulaComplexityAutomatic
                || multipleSameRefAutomatic || nonConsideredConstantsAutomatic || refToNullAutomatic
                || oneAmongOthersAutomatic || stringDistanceAutomatic || errorInCellsAutomatic;
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

        public void loadXML(XElement settingsRoot)
        {
            formulaComplexity = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_formulaComplexity)).Value);
            formulaComplexityAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_formulaComplexityAutomatic)).Value);
            noConstantsInFormulas = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_noConstantsInFormulas)).Value);
            noConstantsInFormulasAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_noConstantsInFormulasAutomatic)).Value);
            readingDirection = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_readingDirection)).Value);
            readingDirectionAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_readingDirectionAutomatic)).Value);
            multipleSameRef = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_multipleSameRef)).Value);
            multipleSameRefAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_multipleSameRefAutomatic)).Value);
            nonConsideredConstants = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_nonConsideredConstants)).Value);
            nonConsideredConstantsAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_nonConsideredConstantsAutomatic)).Value);
            refToNull = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_refToNull)).Value);
            refToNullAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_refToNullAutomatic)).Value);
            oneAmongOthers = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_oneAmongOthers)).Value);
            oneAmongOthersAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_oneAmongOthersAutomatic)).Value);
            stringDistance = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_stringDistance)).Value);
            stringDistanceAutomatic = Convert.ToBoolean(settingsRoot.Element(XName.Get(name_stringDistanceAutomatic)).Value);
            stringDistanceMaxDist = Convert.ToInt32(settingsRoot.Element(XName.Get(name_stringDistanceMaxDist)).Value);
            var eic = settingsRoot.Element(XName.Get(name_errorInCells));
            if (eic != null)
            {
                errorInCells = Convert.ToBoolean(eic.Value);
            }
            var eica = settingsRoot.Element(XName.Get(name_errorInCellsAutomatic));
            if (eica != null)
            {
                errorInCellsAutomatic = Convert.ToBoolean(eica.Value);
            }
        }

        public void saveXML(XElement settingsRoot)
        {
            XElement xformulaComplexity = new XElement(name_formulaComplexity);
            xformulaComplexity.Value = formulaComplexity.ToString();
            XElement xformulaComplexityAutomatic = new XElement(name_formulaComplexityAutomatic);
            xformulaComplexityAutomatic.Value = formulaComplexityAutomatic.ToString();
            settingsRoot.Add(xformulaComplexity);
            settingsRoot.Add(xformulaComplexityAutomatic);

            XElement xnoConstantsInFormulas = new XElement(name_noConstantsInFormulas);
            xnoConstantsInFormulas.Value = noConstantsInFormulas.ToString();
            XElement xnoConstantsInFormulasAutomatic = new XElement(name_noConstantsInFormulasAutomatic);
            xnoConstantsInFormulasAutomatic.Value = noConstantsInFormulasAutomatic.ToString();
            settingsRoot.Add(xnoConstantsInFormulas);
            settingsRoot.Add(xnoConstantsInFormulasAutomatic);

            XElement xreadingDirection = new XElement(name_readingDirection);
            xreadingDirection.Value = readingDirection.ToString();
            XElement xreadingDirectionAutomatic = new XElement(name_readingDirectionAutomatic);
            xreadingDirectionAutomatic.Value = readingDirectionAutomatic.ToString();
            settingsRoot.Add(xreadingDirection);
            settingsRoot.Add(xreadingDirectionAutomatic);

            XElement xmultipleSameRef = new XElement(name_multipleSameRef);
            xmultipleSameRef.Value = multipleSameRef.ToString();
            XElement xmultipleSameRefAutomatic = new XElement(name_multipleSameRefAutomatic);
            xmultipleSameRefAutomatic.Value = multipleSameRefAutomatic.ToString();
            settingsRoot.Add(xmultipleSameRef);
            settingsRoot.Add(xmultipleSameRefAutomatic);

            XElement xnonConsideredConstants = new XElement(name_nonConsideredConstants);
            xnonConsideredConstants.Value = nonConsideredConstants.ToString();
            XElement xnonConsideredConstantsAutomatic = new XElement(name_nonConsideredConstantsAutomatic);
            xnonConsideredConstantsAutomatic.Value = nonConsideredConstantsAutomatic.ToString();
            settingsRoot.Add(xnonConsideredConstants);
            settingsRoot.Add(xnonConsideredConstantsAutomatic);

            XElement xrefToNull = new XElement(name_refToNull);
            xrefToNull.Value = refToNull.ToString();
            XElement xrefToNullAutomatic = new XElement(name_refToNullAutomatic);
            xrefToNullAutomatic.Value = refToNullAutomatic.ToString();
            settingsRoot.Add(xrefToNull);
            settingsRoot.Add(xrefToNullAutomatic);

            XElement xoneAmongOthers = new XElement(name_oneAmongOthers);
            xoneAmongOthers.Value = oneAmongOthers.ToString();
            XElement xoneAmongOthersAutomatic = new XElement(name_oneAmongOthersAutomatic);
            xoneAmongOthersAutomatic.Value = oneAmongOthersAutomatic.ToString();
            settingsRoot.Add(xoneAmongOthers);
            settingsRoot.Add(xoneAmongOthersAutomatic);

            XElement xstringDistance = new XElement(name_stringDistance);
            xstringDistance.Value = stringDistance.ToString();
            XElement xstringDistanceAutomatic = new XElement(name_stringDistanceAutomatic);
            xstringDistanceAutomatic.Value = stringDistanceAutomatic.ToString();
            XElement xstringDistanceMaxDist = new XElement(name_stringDistanceMaxDist);
            xstringDistanceMaxDist.Value = stringDistanceMaxDist.ToString();
            settingsRoot.Add(xstringDistance);
            settingsRoot.Add(xstringDistanceAutomatic);
            settingsRoot.Add(xstringDistanceMaxDist);

            XElement xeic = new XElement(name_errorInCells);
            xeic.Value = errorInCells.ToString();
            XElement xeica = new XElement(name_errorInCellsAutomatic);
            xeica.Value = errorInCellsAutomatic.ToString();
            settingsRoot.Add(xeic);
            settingsRoot.Add(xeica);
        }
    }
}
