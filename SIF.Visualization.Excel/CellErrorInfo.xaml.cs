using Microsoft.Office.Tools;
using SIF.Visualization.Excel.Core;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SIF.Visualization.Excel
{
    /// <summary>
    /// Interaktionslogik für CellErrorInfo.xaml
    /// </summary>
    public partial class CellErrorInfo : UserControl
    {
        public CellErrorInfo()
        {
            InitializeComponent();
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var findingsPane = ((Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Findings")] as CustomTaskPane).Control as FindingsPaneContainer).FindingsPane;

            // Show the findings pane
            (Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "Findings")] as CustomTaskPane).Visible = true;

            var list = findingsPane.FindingsList;
            var listBoxItemRootGrid = VisualTreeHelper.GetChild((list.ItemContainerGenerator.ContainerFromItem((this.DataContext as SingleViolation).Finding) as ListBoxItem), 0) as Grid;

            var expander = (from UIElement p in listBoxItemRootGrid.Children
                            where p.GetType() == typeof(Expander)
                            select p).FirstOrDefault() as Expander;

            expander.IsExpanded = true;

            if ((this.DataContext as SingleViolation).Finding.Violations.Contains(this.DataContext as Violation))
            {
                // This is a single violation
                var expanderContent = expander.Content as Grid;

                var listbox = VisualTreeHelper.GetChild(expanderContent, 0) as ListBox;
                listbox.SelectedItem = this.DataContext;
            }
            else
            {
                // This is an element of a group violation
                var groupViolation = (from p in (this.DataContext as SingleViolation).Finding.Violations
                                      where p is GroupViolation
                                      where (p as GroupViolation).Violations.Contains(this.DataContext as SingleViolation)
                                      select p as GroupViolation).FirstOrDefault();

                var expanderContent = expander.Content as Grid;

                var listbox = VisualTreeHelper.GetChild(expanderContent, 0) as ListBox;
                listbox.SelectedItem = groupViolation;

                groupViolation.SelectedViolation = this.DataContext as SingleViolation;
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.PopupMenu.IsOpen = false;
            (this.DataContext as SingleViolation).IsFalsePositive = true;
        }
    }
}
