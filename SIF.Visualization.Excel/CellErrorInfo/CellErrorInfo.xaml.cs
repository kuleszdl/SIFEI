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

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            this.PopupMenu.IsOpen = false;
            (this.DataContext as Violation).ViolationState = Violation.ViolationType.IGNORE;
        }
    }
}
