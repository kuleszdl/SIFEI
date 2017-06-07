using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Core.Rules
{
    public class RegexCondition : Condition
    {
        #region Fields

        private string regexValue = "";
        private ConditionType type = ConditionType.Regex;
        private Rule d;
        #endregion

        RegexCondition()
        {

        }

               


        public void AddtoRule(Rule rule)
        {
            
        }

    }
}
