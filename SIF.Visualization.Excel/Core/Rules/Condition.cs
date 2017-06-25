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
        private string conditionValue;
        private string conditionName;

        public ConditionType Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }
        public string Name
        {
            get { return conditionName; }
            set { SetProperty(ref conditionName, value); }
        }

        public string Value
        {
            get
            {
                switch (type)
                {
                    case ConditionType.Regex: return conditionValue;
                    case ConditionType.CharacterCount: return conditionValue.ToString();
                    default: return "";
                }
            }
            set
            {
                SetProperty(ref conditionValue, value);
                NotifyPropertyChanged();
            }
        }
        #endregion

    }
}
