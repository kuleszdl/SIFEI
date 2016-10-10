using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Forms;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.ScenarioView
{
    class NumberValidationRule : ValidationRule
    {
        /// <summary>
        /// Matches anything, which does not consist only of digits with a leading sign and exponent at the end
        /// </summary>
        private static readonly Regex Nonnumeric = new Regex("\\s*[+-]?[^0-9,\\.\\s]+(e[^0-9\\s]*)?");

        private const NumberStyles ThousandStyles = NumberStyles.Float | NumberStyles.AllowThousands;
        private const NumberStyles NormalStyles = NumberStyles.Float;
        private Boolean ignoreLocal = false;


        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult result;
            string val = (string) value;

            #region check whether empty or non-numeric
            if (string.IsNullOrEmpty(val))
            {
                return new ValidationResult(true, string.Empty);
            }

            if (Nonnumeric.IsMatch(val))
            {
                return  new ValidationResult(true, string.Empty);
            }
            #endregion

            Boolean thousandsValid = false;
            try
            {
                Double.Parse(val, ThousandStyles);
                if (!(Settings.Default.SifUseThousandsSeparator || ignoreLocal))
                {
                    thousandsValid = true;
                    Double.Parse(val, NormalStyles);
                }
                result = new ValidationResult(true, string.Empty);
            }
            catch (FormatException e)
            {
                if (thousandsValid && !ignoreLocal)
                {
                    var messageBox = new NumberValidationMessageBox();
                    DialogResult userChoice = messageBox.ShowDialog();
                    switch (userChoice)
                    {
                        case (DialogResult.Yes) :
                            ignoreLocal = true;
                            break;
                        case (DialogResult.Ignore) :
                            Settings.Default.SifUseThousandsSeparator = true;
                            break;
                    }

                    // get the validation result without asking infinite times
                    bool old = ignoreLocal;
                    ignoreLocal = true;
                    result = Validate(value, cultureInfo);
                    ignoreLocal = old;
                } 
                else 
                {
                    result = new ValidationResult(false, e.Message);
                }
            }

            return result;
        }
    }
}
