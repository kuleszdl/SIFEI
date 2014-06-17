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

namespace SIF.Visualization.Excel.FalsePositiveView
{
    /// <summary>
    /// Interaktionslogik für UserControl1.xaml
    /// </summary>
    public partial class FalsePositiveView : UserControl
    {
        internal ListCollectionView FalsePositivePane
        {
            get;
            private set;
        }

        public FalsePositiveView()
        {
            InitializeComponent();
            this.DataContextChanged += FalsePositiveView_DataContextChanged;
            this.FalsePositiveList.SelectionChanged += this.ListBox_SelectionChanged;
        }


        private void FalsePositiveView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (this.DataContext == null) return;

            this.FalsePositivePane = new ListCollectionView((this.DataContext as WorkbookModel).FalsePositives);
            this.FalsePositivePane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
            this.FalsePositivePane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));

            this.FalsePositiveList.ItemsSource = this.FalsePositivePane;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                this.FalsePositiveList.ScrollIntoView(e.AddedItems[0]);
            }
        }

        private void FalsePositive_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.ViolationState = Violation.ViolationType.NEW;
        }

        private void Visible_Click(object sender, RoutedEventArgs e)
        {
            Grid grid = ((Grid)((TextBlock)(sender as Hyperlink).Parent).Parent);
            Violation violation = (grid.DataContext as Violation);
            violation.IsVisible = !violation.IsVisible;
        }
    }
}
