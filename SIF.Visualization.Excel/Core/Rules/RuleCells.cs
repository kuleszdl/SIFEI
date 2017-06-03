using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIF.Visualization.Excel.Core.Rules
{
    public class RuleData : BindableBase
    {
        private ValueType type = ValueType.BLANK;
        private string textValue = "";
        private double numericValue = 0;
        private bool booleanValue = false;
        private string target;

        public string Target {
            get {
                return target;
            }
            set {
                SetProperty(ref target,value);
            }
        }

        public ValueType Type
        {
            get { return type; }
            set { SetProperty(ref type, value); }
        }

        public double NumericValue
        {
            get { return numericValue; }
            set { SetProperty(ref numericValue, value); }
        }

        public string TextValue
        {
            get { return textValue; }
            set { SetProperty(ref textValue, value); }
        }

        public bool BooleanValue
        {
            get { return booleanValue; }
            set { SetProperty(ref booleanValue, value); }
        }

        public string Value
        {
            get
            {
                switch (type)
                {
                    case ValueType.BOOLEAN: return booleanValue.ToString();
                    case ValueType.TEXT: return textValue;
                    case ValueType.NUMERIC: return numericValue.ToString();
                    default: return "";
                }
            }
            set
            {
                Boolean parsedBooleanValue;
                Double parsedDoubleValue;

                if (Boolean.TryParse(value, out parsedBooleanValue))
                {
                    BooleanValue = parsedBooleanValue;
                    Type = ValueType.BOOLEAN;
                }
                else if (Double.TryParse(value, out parsedDoubleValue))
                {
                    NumericValue = parsedDoubleValue;
                    Type = ValueType.NUMERIC;
                }
                else
                {
                    TextValue = value;
                    Type = ValueType.TEXT;
                }
                NotifyPropertyChanged();
            }
        }

        public RuleData(string target) {
            this.target = target;
        }

        public RuleData() {}


    }
}
