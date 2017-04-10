namespace SIF.Visualization.Excel.Core.Scenarios
{
    public class ScenarioData : BindableBase
    {
        private string target;

        public string Target {
            get { return target; }
            set { SetProperty(ref target, value); }
        }

        public ScenarioData() {}

        public ScenarioData(string target) {
            this.target = target;
        }
    }
}
