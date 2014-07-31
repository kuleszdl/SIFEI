using SIF.Visualization.Excel.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
            this.DataContextChanged += FalsePositiveView_DataContextChanged;
            this.IgnoreList.SelectionChanged += this.ListBox_SelectionChanged;
        }


        private void FalsePositiveView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (this.DataContext == null) return;

            this.IgnorePane = new ListCollectionView((this.DataContext as WorkbookModel).IgnoredViolations);
            this.IgnorePane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
            this.IgnorePane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));

            this.IgnoreList.ItemsSource = this.IgnorePane;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                this.IgnoreList.ScrollIntoView(e.AddedItems[0]);
                e.Handled = true;
            }
        }

        private void Ignore_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = Violation.ViolationType.NEW;
        }
    }
}
