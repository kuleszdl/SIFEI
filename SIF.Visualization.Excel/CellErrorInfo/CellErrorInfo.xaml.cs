using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SIF.Visualization.Excel
{
    /// <summary>
    /// Interaktionslogik für CellErrorInfo.xaml
    /// This handles how the images in the sidepane get visualized
    /// </summary>
    public partial class CellErrorInfo : UserControl
    {
        public CellErrorInfo()
        {
            InitializeComponent();
            
        }

        void Violations_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            
        }

        private void Layout_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            
            Grid grid = sender as Grid;

            if (grid != null)
            {
                /*   ContextMenu contextMenu = grid.ContextMenu;
                   contextMenu.PlacementTarget = grid;
                   contextMenu.IsOpen = true;*/
                //grid.ContextMenu.IsOpen = true;
                //grid.ContextMenu.PlacementTarget = grid;
                // grid.ContextMenu.
                ContextMenuMulti.PlacementTarget = grid;
                ContextMenuMulti.IsOpen = true;

            }
        }
        private void Layout_OnMouseDown2(object sender, MouseButtonEventArgs e)
        {
            
            Grid grid = sender as Grid;

            if (grid != null)
            {
                /*   ContextMenu contextMenu = grid.ContextMenu;
                   contextMenu.PlacementTarget = grid;
                   contextMenu.IsOpen = true;*/
                //grid.ContextMenu.IsOpen = true;
                //grid.ContextMenu.PlacementTarget = grid;
                // grid.ContextMenu.
                ContextMenu2.PlacementTarget = grid;
                ContextMenu2.IsOpen = true;

            }
        }
    }
}
