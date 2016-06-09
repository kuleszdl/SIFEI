using SIF.Visualization.Excel.Core;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace SIF.Visualization.Excel.ViolationsView
{
    /// <summary>
    /// Interaktionslogik für ViolationsView.xaml
    /// </summary>
    public partial class ViolationsView : UserControl
    {

        internal ListCollectionView ViolationsPane
        {
            get;
            private set;
        }

        public ViolationsView()
        {
            InitializeComponent();

            DataContextChanged += ViolationsView_DataContextChanged;
        }

        private void ViolationsView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (DataContext == null) return;

            ViolationsPane = new ListCollectionView((DataContext as WorkbookModel).Violations);
            ViolationsPane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
            ViolationsPane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));

            ViolationList.ItemsSource = ViolationsPane;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                ViolationList.ScrollIntoView(e.AddedItems[0]);
                DataModel.Instance.CurrentWorkbook.UnreadViolationCount = (from vi in DataModel.Instance.CurrentWorkbook.Violations where vi.IsRead == false select vi).Count();
            }
            e.Handled = true;
        }

        private void Ignore_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = ViolationType.IGNORE;
        }

        private void Later_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = ViolationType.LATER;
        }
    }

}
