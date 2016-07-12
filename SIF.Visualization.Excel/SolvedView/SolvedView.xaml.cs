using SIF.Visualization.Excel.Core;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SIF.Visualization.Excel.SolvedView
{
    /// <summary>
    /// Interaktionslogik für SolvedView.xaml
    /// </summary>
    public partial class SolvedView : UserControl
    {
        internal ListCollectionView SolvedViolationsPane
        {
            get;
            private set;
        }

        public SolvedView()
        {
            InitializeComponent();
            DataContextChanged += SolvedView_DataContextChanged;
        }


        private void SolvedView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (DataContext == null) return;

            SolvedViolationsPane = new ListCollectionView((DataContext as WorkbookModel).SolvedViolations);
            SolvedViolationsPane.SortDescriptions.Add(new SortDescription("SolvedTime", ListSortDirection.Descending));

            SolvedList.ItemsSource = SolvedViolationsPane;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                SolvedList.ScrollIntoView(e.AddedItems[0]);
            }
            e.Handled = true;
        }
    }
}
