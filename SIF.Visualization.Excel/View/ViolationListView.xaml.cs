using SIF.Visualization.Excel.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace SIF.Visualization.Excel.View {
    /// <summary>
    /// Interaktionslogik für ViolationListView.xaml
    /// </summary>
    public partial class ViolationListView : UserControl {


        public ViolationListView() {
            InitializeComponent();
            DataContextChanged += ViolationsView_DataContextChanged;
        }

        private void ViolationsView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e) {

            if (DataContext == null) return;

            var visibleViolationsBinding = new Binding() {
                Source = DataModel.Instance.CurrentWorkbook.VisibleViolations,
                Mode = BindingMode.OneWay
            };

            ViolationListBox.SetBinding(ItemsControl.ItemsSourceProperty, visibleViolationsBinding);
        }

        private void ViolationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var items = e.AddedItems;
            if (items.Count > 0) {
                Violation vio = (Violation) items[0];
                CellManager.Instance.SelectCell(vio.Location);
                DataModel.Instance.CurrentWorkbook.NotifyUnreadViolationsChanged();
                ViolationListBox.ScrollIntoView(items[0]);
                e.Handled = true;
            }
        }

        private void Ignore_Click(object sender, RoutedEventArgs e) {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = ViolationState.IGNORE;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
            DataModel.Instance.CurrentWorkbook.NotifyUnreadViolationsChanged();
            e.Handled = true;
        }

        private void Later_Click(object sender, RoutedEventArgs e) {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = ViolationState.LATER;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
            DataModel.Instance.CurrentWorkbook.NotifyUnreadViolationsChanged();
            e.Handled = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e) {
            WorkbookModel wb = DataModel.Instance.CurrentWorkbook;
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            Cell cell = wb.GetCell(violation.Location);
            cell.Violations.Remove(violation);
            wb.Violations.Remove(violation);
            violation = null;
            wb.RecalculateViewModel();
            wb.NotifyUnreadViolationsChanged();
            e.Handled = true;
        }

        private void Reset_Click(object sender, RoutedEventArgs e) {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = ViolationState.OPEN;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
            DataModel.Instance.CurrentWorkbook.NotifyUnreadViolationsChanged();
            e.Handled = true;
        }
    }

}
