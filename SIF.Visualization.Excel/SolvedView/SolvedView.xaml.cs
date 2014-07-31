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
            this.DataContextChanged += SolvedView_DataContextChanged;
        }


        private void SolvedView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

            if (this.DataContext == null) return;

            this.SolvedViolationsPane = new ListCollectionView((this.DataContext as WorkbookModel).SolvedViolations);
            this.SolvedViolationsPane.SortDescriptions.Add(new SortDescription("SolvedTime", ListSortDirection.Descending));

            this.SolvedList.ItemsSource = this.SolvedViolationsPane;
        }


        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                this.SolvedList.ScrollIntoView(e.AddedItems[0]);
            }
            e.Handled = true;
        }
    }
}
