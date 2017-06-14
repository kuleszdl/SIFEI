using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIF.Visualization.Excel.Core.Rules
{
    public class Condition : BindableBase
    {
        #region Fields
        public enum ConditionType
        {
            NONE,
            Regex,
            CharacterCount
        }
        private ConditionType type = ConditionType.NONE;
        private int characterCount = 0;
        private string regex = "";
        private Rule rule;

        public ConditionType Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        public int CharacterCount
        {
            get { return characterCount;  }
            set { SetProperty(ref characterCount, value);  }
        }

        public string Regex
        {
            get { return regex; }
            set { SetProperty(ref regex, value);  }
        }

        public string Value
        {
            get
            {
                switch (type)
                {
                    case ConditionType.Regex: return regex.ToString();
                    case ConditionType.CharacterCount: return characterCount.ToString();
                    default: return "";
                }
            }
            set
            {
                int parsedIntValue;
                if (Int32.TryParse(value, out parsedIntValue))
                {
                    CharacterCount = parsedIntValue;
                    Type = ConditionType.CharacterCount;
                }
                else
                {
                    Regex = value;
                    Type = ConditionType.Regex;
                }
                NotifyPropertyChanged();
            }
        }
        #endregion

        public void AddRegexCondition (ConditionType type, string value) {

        }
    }
}
