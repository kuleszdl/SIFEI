using SIF.Visualization.Excel.Core;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace SIF.Visualization.Excel.View
{
    /// <summary>
    /// Interaktionslogik für ScenarioCellList.xaml
    /// </summary>
    public partial class ScenarioCellList : UserControl
    {
        /// <summary>
        /// Puts a Textox over defined scenariocells so the user can input the desired values in them
        /// </summary>
        public ScenarioCellList()
        {
            InitializeComponent();
            
            DataContextChanged += ScenarioCellList_DataContextChanged;
        }

        private void ScenarioCellList_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null || !(DataContext is WorkbookModel)) return;

            var myWorkbookModel = DataContext as WorkbookModel;
            var binding = new Binding()
            {
                Source = myWorkbookModel.ScenarioCells,
                Mode = BindingMode.OneWay
            };

            ScenarioCellListBox.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var cell = (Cell) button.DataContext;
            cell.ScenarioCellType = ScenarioCellType.NONE;
            DataModel.Instance.CurrentWorkbook.RecalculateViewModel();
            e.Handled = true;
        }

        private void CellDefinitionsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            var items = e.AddedItems;
            if (items.Count > 0) {
                Cell cell = (Cell) items[0];
                CellManager.Instance.SelectCell(cell.Location);
            }
            e.Handled = true;
        }
        
    }
}
