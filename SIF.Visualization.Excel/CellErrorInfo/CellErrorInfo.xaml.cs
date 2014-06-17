using Microsoft.Office.Tools;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.SharedView;
using SIF.Visualization.Excel.ViolationsView;
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
            var sharedPane = ((Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "shared Pane")] as CustomTaskPane).Control as SharedPaneContainer).SharedPane;

            // Show the violations pane
            Violation violation = this.DataContext as Violation;
            switch (violation.ViolationState)
            {
                case Violation.ViolationType.FALSEPOSITIVE:
                    sharedPane.changeTabTo(SharedPane.SharedPaneTabIndex.FalsePositive);
                    break;
                case Violation.ViolationType.LATER:
                    sharedPane.changeTabTo(SharedPane.SharedPaneTabIndex.Later);
                    break;
                case Violation.ViolationType.NEW:
                    sharedPane.changeTabTo(SharedPane.SharedPaneTabIndex.Violations);
                    break;
                case Violation.ViolationType.SOLVED:
                    sharedPane.changeTabTo(SharedPane.SharedPaneTabIndex.Solved);
                    break;
            }
            (Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(DataModel.Instance.CurrentWorkbook, "shared Pane")] as CustomTaskPane).Visible = true;

            // Select the item
            (this.DataContext as Violation).IsSelected = true;
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.PopupMenu.IsOpen = false;
            (this.DataContext as SingleViolation).ViolationState = Violation.ViolationType.FALSEPOSITIVE;
        }
    }
}
