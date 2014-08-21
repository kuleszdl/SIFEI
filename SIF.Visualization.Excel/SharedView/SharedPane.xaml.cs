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
                datamodel.ViolatedCells.ToList().ForEach(vc => vc.SetVisibility(datamodel.SelectedTab));
            }
        }
    }
}
