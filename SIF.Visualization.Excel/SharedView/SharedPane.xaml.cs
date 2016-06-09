using SIF.Visualization.Excel.Core;
using System.Linq;
using System.Windows.Controls;

namespace SIF.Visualization.Excel.SharedView
{
    /// <summary>
    /// Interaktionslogik für SharedPane.xaml
    /// </summary>
    public partial class SharedPane : UserControl
    {

        public SharedPane()
        {
            InitializeComponent();
            tabcontrol.SelectionChanged += TabControl_SelectionChanged;
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                var datamodel = DataModel.Instance.CurrentWorkbook;
                switch (datamodel.SelectedTab)
                {
                    case SharedTabs.Open:
                        datamodel.Violations.ToList().ForEach(vi => vi.IsSelected = false);
                        break;
                    case SharedTabs.Later:
                        datamodel.LaterViolations.ToList().ForEach(vi => vi.IsSelected = false);
                        break;
                    case SharedTabs.Ignore:
                        datamodel.IgnoredViolations.ToList().ForEach(vi => vi.IsSelected = false);
                        break;
                    case SharedTabs.Archive:
                        datamodel.SolvedViolations.ToList().ForEach(vi => vi.IsSelected = false);
                        break;
                }
                datamodel.SelectedTab = (SharedTabs)tabcontrol.SelectedIndex;
                
                // Update label, since tab is switched now
                switch (datamodel.SelectedTab)
                {
                    case SharedTabs.Open:
                        datamodel.SelectedTabLabel = Properties.Resources.tl_SharedPane_Open;
                        break;
                    case SharedTabs.Later:
                        datamodel.SelectedTabLabel = Properties.Resources.tl_SharedPane_Later;
                        break;
                    case SharedTabs.Ignore:
                        datamodel.SelectedTabLabel = Properties.Resources.tl_SharedPane_Ignored;
                        break;
                    case SharedTabs.Archive:
                        datamodel.SelectedTabLabel = Properties.Resources.tl_SharedPane_Archived;
                        break;
                    case SharedTabs.Cells:
                        datamodel.SelectedTabLabel = Properties.Resources.tl_SharedPane_Cells;
                        break;
                    case SharedTabs.Scenarios:
                        datamodel.SelectedTabLabel = Properties.Resources.tl_SharedPane_Scenarios;
                        break;
                }

            }
        }
    }
}
