using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Core.Rules
{
    public class Condition 
    {
        #region Fields
        public enum ConditionType
        {
            Regex,
            CharacterCount
        }
        private ConditionType conditionType;
        private string regexValue;
        private Rule rule;

        #endregion

        public string RegexValue
        {
            get;
            set;
        }

    }
}
