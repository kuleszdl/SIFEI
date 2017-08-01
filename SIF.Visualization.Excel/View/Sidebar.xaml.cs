using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.View
{
    /// <summary>
    ///     Interaktionslogik für Sidebar.xaml
    /// </summary>
    public partial class Sidebar : UserControl
    {
        public Sidebar()
        {
            InitializeComponent();
            // @Link http://stackoverflow.com/questions/11859821/rendering-issue-with-wpf-controls-inside-elementhost
            Loaded += delegate
            {
                var source = PresentationSource.FromVisual(this);
                var hwndTarget = source.CompositionTarget as HwndTarget;

                if (hwndTarget != null) hwndTarget.RenderMode = RenderMode.SoftwareOnly;
            };
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((FrameworkElement) e.Source).GetType() == typeof(TabControl))
            {
                var tabs = e.AddedItems;
                if (tabs.Count > 0)
                {
                    var tabcontrol = (TabControl) sender;
                    DataModel.Instance.CurrentWorkbook.SelectedTabIndex = tabcontrol.SelectedIndex;
                    e.Handled = true;
                }
            }
        }
    }
}