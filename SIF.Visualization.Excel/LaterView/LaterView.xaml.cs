using Microsoft.Office.Tools;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ViolationsView;
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
            this.DataContextChanged += LaterView_DataContextChanged;
        }


        private void LaterView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (this.DataContext == null) return;

            this.LaterViolationsPane = new ListCollectionView((this.DataContext as WorkbookModel).LaterViolations);
            this.LaterViolationsPane.SortDescriptions.Add(new SortDescription("FirstOccurrence", ListSortDirection.Descending));
            this.LaterViolationsPane.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));

            this.LaterList.ItemsSource = this.LaterViolationsPane;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                this.LaterList.ScrollIntoView(e.AddedItems[0]);
            }
        }

        private void Now_Click(object sender, RoutedEventArgs e)
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
