using SIF.Visualization.Excel.Core;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace SIF.Visualization.Excel.IgnoreView
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class IgnoreView : UserControl
    {
        internal ListCollectionView IgnorePane
        {
            get;
            private set;
        }

        public IgnoreView()
        {
            InitializeComponent();
            DataContextChanged += FalsePositiveView_DataContextChanged;
            IgnoreList.SelectionChanged += ListBox_SelectionChanged;
        }


        private void FalsePositiveView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (DataContext == null) return;

            IgnorePane = new ListCollectionView((DataContext as WorkbookModel).IgnoredViolations);
            IgnorePane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
            IgnorePane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));

            IgnoreList.ItemsSource = IgnorePane;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                IgnoreList.ScrollIntoView(e.AddedItems[0]);
            }
            e.Handled = true;
        }

        private void Ignore_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = ViolationType.OPEN;
        }
    }
}
