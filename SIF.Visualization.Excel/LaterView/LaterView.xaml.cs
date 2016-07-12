using SIF.Visualization.Excel.Core;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace SIF.Visualization.Excel.LaterView
{
    /// <summary>
    /// Interaktionslogik für LaterView.xaml
    /// </summary>
    public partial class LaterView : UserControl
    {

        internal ListCollectionView LaterViolationsPane
        {
            get;
            private set;
        }

        public LaterView()
        {
            InitializeComponent();
            DataContextChanged += LaterView_DataContextChanged;
        }


        private void LaterView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (DataContext == null) return;

            LaterViolationsPane = new ListCollectionView((DataContext as WorkbookModel).LaterViolations);
            LaterViolationsPane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
            LaterViolationsPane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));

            LaterList.ItemsSource = LaterViolationsPane;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                LaterList.ScrollIntoView(e.AddedItems[0]);
            }
            e.Handled = true;
        }

        private void Now_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = ViolationType.OPEN;
        }
    }
}
