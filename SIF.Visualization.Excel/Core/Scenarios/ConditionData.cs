namespace SIF.Visualization.Excel.Core.Scenarios
{
    public class ConditionData : InputData
    {
        private OperatorType op = OperatorType.EQUALS;


        public OperatorType Operator {
            get { return op; }
            set { SetProperty(ref op, value); }
        }

        public ConditionData() { }

        public ConditionData(string target) : base(target) {}
    }
}
