namespace SIF.Visualization.Excel.Core.Rules
{
    public class RuleCells : BindableBase
    {
        private bool booleanValue;
        private double numericValue;
        private string target;
        private string textValue = "";
        private ValueType type = ValueType.BLANK;

        public RuleCells(string target)
        {
            this.target = target;
        }

        public RuleCells()
        {
        }

        public string Target
        {
            get { return target; }
            set { SetProperty(ref target, value); }
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
                bool parsedBooleanValue;
                double parsedDoubleValue;

                if (bool.TryParse(value, out parsedBooleanValue))
                {
                    BooleanValue = parsedBooleanValue;
                    Type = ValueType.BOOLEAN;
                }
                else if (double.TryParse(value, out parsedDoubleValue))
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
    }
}