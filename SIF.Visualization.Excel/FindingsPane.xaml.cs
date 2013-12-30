using SIF.Visualization.Excel.Cells;
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

namespace SIF.Visualization.Excel
{
    /// <summary>
    /// Interaktionslogik für FindingsPane.xaml
    /// </summary>
    public partial class FindingsPane : UserControl
    {
        internal ListCollectionView FindingsView
        {
            get;
            private set;
        }

        public FindingsPane()
        {
            InitializeComponent();

            this.DataContextChanged += FindingsPane_DataContextChanged;
        }

        private void FindingsPane_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null) return;

            this.FindingsView = new ListCollectionView((this.DataContext as WorkbookModel).Findings);
            this.FindingsView.SortDescriptions.Add(new SortDescription("Severity", ListSortDirection.Descending));

            this.FindingsList.ItemsSource = this.FindingsView;
        }

        private void Expander_Expanded(object sender, RoutedEventArgs e)
        {
            this.FindingsList.SelectedIndex = -1;

            //DependencyObject currentElement = (sender as Expander);

            //while (currentElement.GetType() != typeof(ListBoxItem))
            //{
            //    currentElement = VisualTreeHelper.GetParent(currentElement);
            //}

            //if (currentElement != null && currentElement.GetType() == typeof(ListBoxItem))
            //{
            //    (currentElement as ListBoxItem).IsSelected = false;
            //}
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems != null && e.AddedItems.Count > 0)
            {
                foreach (Violation violation in e.AddedItems)
                {
                    if (violation is SingleViolation)
                    {
                        (violation as SingleViolation).IsSelected = true;

                        // Scroll to that cell
                        violation.Cell.ScrollIntoView();
                    }
                }

                var list = this.FindingsList;

                // Unselect all others
                foreach (Finding finding in DataModel.Instance.CurrentWorkbook.Findings)
                {
                    try
                    {
                        var rootGrid = VisualTreeHelper.GetChild((list.ItemContainerGenerator.ContainerFromItem(finding) as ListBoxItem), 0) as Grid;
                        var innerExpander = (from UIElement p in rootGrid.Children
                                             where p.GetType() == typeof(Expander)
                                             select p).FirstOrDefault() as Expander;

                        var expanderContent = innerExpander.Content as Grid;

                        var listbox = VisualTreeHelper.GetChild(expanderContent, 0) as ListBox;
                        if (finding != (sender as ListBox).DataContext as Finding)
                        {
                            // This is another listbox, so clear it
                            listbox.SelectedItem = null;
                        }

                        var groupViolations = from p in finding.Violations
                                              where p is GroupViolation
                                              select p as GroupViolation;
                        foreach (var groupViolation in groupViolations)
                        {
                            if (groupViolation.SelectedViolation == e.AddedItems[0] as SingleViolation)
                            {
                                listbox.SelectedItem = null;
                            }
                        }

                        // Now get the group violations
                        if (finding != (sender as ListBox).DataContext as Finding || ((finding == (sender as ListBox).DataContext as Finding) && e.AddedItems[0] is SingleViolation))
                        {
                            foreach (var groupViolation in groupViolations)
                            {
                                if (groupViolation.SelectedViolation == e.AddedItems[0] as SingleViolation)
                                    continue;

                                var element = listbox.ItemContainerGenerator.ContainerFromItem(groupViolation);
                                if (element == null) continue;

                                var innerList = ((VisualTreeHelper.GetChild(element, 0) as Grid).Children[1] as Grid).Children[2] as ListBox;

                                innerList.SelectedItem = null;
                            }
                        }
                    }
                    catch { }
                }
            }
            if (e.RemovedItems != null && e.RemovedItems.Count > 0)
            {
                foreach (Violation violation in e.RemovedItems)
                {
                    if (violation is SingleViolation)
                        (violation as SingleViolation).IsSelected = false;
                }
            }
        }

        private void FalsePositive_Click(object sender, RoutedEventArgs e)
        {
            var violation = (sender as MenuItem).DataContext as SingleViolation;

            if (violation == null) return;

            violation.IsFalsePositive = !violation.IsFalsePositive;
        }
    }
}
