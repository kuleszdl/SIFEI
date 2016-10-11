using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace SIF.Visualization.Excel
{

    /// <summary>
    /// Interaktionslogik für CellErrorInfo.xaml
    /// This handles how the images in the sidepane get visualized
    /// </summary>
    public partial class CellErrorInfo : UserControl
    {

        private bool gotClicked=  false;
        public CellErrorInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Occurs when the Mouse enters the Contextmenue
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="mouseEventArgs"></param>
        private void Layout_OnMouseEnter(object sender, MouseEventArgs mouseEventArgs)
        {
            if (gotClicked) return;
            gotClicked = true;
            var controlTemplate = ContextMenu.Template;
            Grid grid1 = (Grid) controlTemplate.FindName("ExtraInfo", ContextMenu);
            grid1.Visibility = Visibility.Visible;
            ListBox listbox = (ListBox) controlTemplate.FindName("ViolationList", ContextMenu);
            if (listbox.SelectedItem != null)
            {
                grid1.DataContext = listbox.SelectedItem;
            }
            else
            {
                grid1.DataContext = listbox.Items[0];
            }
        }

    
        /// <summary>
        /// Neccessary so the Layoutonmouse Enter only occurs once after the Contextmenue being opened. 
        /// Contextmenu Opening could not be used since it needs the menu to be initialized already and then starts to redo it
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrameworkElement_OnContextMenuClosing(object sender, ContextMenuEventArgs e)
        {
            gotClicked = false;
        }

        /// <summary>
        /// In case the selection of the Listbox gets changed that "Befund" gets shown in the detailed view
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ViolationList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var controlTemplate = ContextMenu.Template;
            Grid grid1 = (Grid)controlTemplate.FindName("ExtraInfo", ContextMenu);
            grid1.Visibility = Visibility.Visible;
            ListBox listbox = (ListBox)controlTemplate.FindName("ViolationList", ContextMenu);
            grid1.DataContext = listbox.SelectedItem;
        }
    }
}
