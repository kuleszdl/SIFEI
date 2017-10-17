using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel
{
    /// <summary>
    ///     Interaktionslogik für CellErrorInfo.xaml
    ///     This handles how the images in the sidepane get visualized
    /// </summary>
    public partial class CellErrorInfo : UserControl
    {
        private bool gotClicked;

        public CellErrorInfo()
        {
            InitializeComponent();
            // @Link http://stackoverflow.com/questions/11859821/rendering-issue-with-wpf-controls-inside-elementhost
            Loaded += delegate
            {
                var source = PresentationSource.FromVisual(this);
                var hwndTarget = source.CompositionTarget as HwndTarget;

                if (hwndTarget != null) hwndTarget.RenderMode = RenderMode.SoftwareOnly;
            };
        }

        /// <summary>
        ///     Occurs when the Mouse enters the Contextmenue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void Layout_OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            if (gotClicked) return;
            gotClicked = true;
            var controlTemplate = SifContextMenu.Template;
            //Grid grid1 = (Grid) controlTemplate.FindName("ExtraInfo", SifContextMenu);
            //grid1.Visibility = Visibility.Visible;
        }


        /// <summary>
        ///     Neccessary so the Layoutonmouse Enter only occurs once after the Contextmenue being opened.
        ///     Contextmenu Opening could not be used since it needs the menu to be initialized already and then starts to redo it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameworkElement_OnContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            gotClicked = false;
        }

        /// <summary>
        ///     In case the selection of the Listbox gets changed that "Befund" gets shown in the detailed view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViolationListBox_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var controlTemplate = SifContextMenu.Template;
            var grid = (Grid) controlTemplate.FindName("ExtraInfo", SifContextMenu);
            var vio = (sender as ListBox).SelectedItem as Violation;
            if (vio != null)
            {
                vio.IsSelected = true;
                grid.Visibility = Visibility.Visible;
                var cell = grid.DataContext as Cell;
                cell.SelectedViolation = vio;
            }
            else
            {
                grid.Visibility = Visibility.Collapsed;
            }
            e.Handled = true;
        }
    }
}