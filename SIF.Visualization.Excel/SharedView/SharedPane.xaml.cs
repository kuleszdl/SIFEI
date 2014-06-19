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
        public enum SharedPaneTabIndex
        {
            /// <summary>
            /// Register Violations
            /// </summary>
            Violations,
            /// <summary>
            /// Register FalsePositive
            /// </summary>
            FalsePositive,
            /// <summary>
            /// Register Later
            /// </summary>
            Later,
            /// <summary>
            /// Register Solved
            /// </summary>
            Solved,
            /// <summary>
            /// Register Cells
            /// </summary>
            Cells,
            /// <summary>
            /// Register Scenarios
            /// </summary>
            Scenarios,
        }

        public SharedPane()
        {
            InitializeComponent();
            tabcontrol.SelectionChanged += TabControl_SelectionChanged;
        }

        public void changeTabTo(SharedPaneTabIndex tabIndex)
        {
            tabcontrol.SelectedIndex = (int)tabIndex;
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.Source is TabControl)
            {
                SharedPaneTabIndex index = (SharedPaneTabIndex)tabcontrol.SelectedIndex;
                DataModel.Instance.CurrentWorkbook.Violations.ToList().ForEach(vi => vi.IsSelected = false);
                DataModel.Instance.CurrentWorkbook.FalsePositives.ToList().ForEach(vi => vi.IsSelected = false);
                DataModel.Instance.CurrentWorkbook.LaterViolations.ToList().ForEach(vi => vi.IsSelected = false);
                DataModel.Instance.CurrentWorkbook.SolvedViolations.ToList().ForEach(vi => vi.IsSelected = false);
            }
        }
    }
}
