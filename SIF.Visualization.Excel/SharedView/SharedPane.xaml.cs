using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
                datamodel.SelectedTab = (SharedTabs)tabcontrol.SelectedIndex;
                datamodel.Violations.ToList().ForEach(vi => vi.IsSelected = false);
                datamodel.IgnoredViolations.ToList().ForEach(vi => vi.IsSelected = false);
                datamodel.LaterViolations.ToList().ForEach(vi => vi.IsSelected = false);
                datamodel.SolvedViolations.ToList().ForEach(vi => vi.IsSelected = false);

                switch ((SharedTabs)tabcontrol.SelectedIndex)
                {
                    case SharedTabs.Violations:
                        datamodel.Violations.ToList().ForEach(vi => vi.IsVisible = true);
                        datamodel.IgnoredViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.LaterViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.SolvedViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.Violations.ToList().ForEach(vi => vi.CreateControls());
                        break;
                    case SharedTabs.Ignore:
                        datamodel.Violations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.IgnoredViolations.ToList().ForEach(vi => vi.IsVisible = true);
                        datamodel.LaterViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.SolvedViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.IgnoredViolations.ToList().ForEach(vi => vi.CreateControls());
                        break;
                    case SharedTabs.Later:
                        datamodel.Violations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.IgnoredViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.LaterViolations.ToList().ForEach(vi => vi.IsVisible = true);
                        datamodel.SolvedViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.LaterViolations.ToList().ForEach(vi => vi.CreateControls());
                        break;
                    case SharedTabs.Solved:
                        datamodel.Violations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.IgnoredViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.LaterViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.SolvedViolations.ToList().ForEach(vi => vi.IsVisible = true);
                        datamodel.SolvedViolations.ToList().ForEach(vi => vi.CreateControls());
                        break;
                    default:
                        datamodel.Violations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.IgnoredViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.LaterViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        datamodel.SolvedViolations.ToList().ForEach(vi => vi.IsVisible = false);
                        break;
                }
            }
        }
    }
}
