using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.Core;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SIF.Visualization.Excel.ScenarioCore;

namespace SIF.Visualization.Excel.ScenarioView
{
    /// <summary>
    /// Interaktionslogik für DefineCellsPane.xaml
    /// </summary>
    public partial class DefineCellsPane : UserControl
    {
        /// <summary>
        /// Puts a Textox over defined scenariocells so the user can input the desired values in them
        /// </summary>
        public DefineCellsPane()
        {
            InitializeComponent();
            
            DataContextChanged += DefineCellsPane_DataContextChanged;
        }

        private void DefineCellsPane_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null || !(DataContext is WorkbookModel)) return;

            var myWorkbookModel = DataContext as WorkbookModel;
            var defineCellsCollection = new CompositeCollection();

            #region collection containers
            var inputCellsCContoiner = new CollectionContainer()
            {
                Collection = myWorkbookModel.InputCells
            };
            defineCellsCollection.Add(inputCellsCContoiner);

            var intermediateCellsCContainer = new CollectionContainer()
            {
                Collection = myWorkbookModel.IntermediateCells
            };
            defineCellsCollection.Add(intermediateCellsCContainer);

            var outputCellsCContainer = new CollectionContainer()
            {
                Collection = myWorkbookModel.OutputCells
            };
            defineCellsCollection.Add(outputCellsCContainer);

            #endregion

            var defineCellsBinding = new Binding()
            {
                Source = defineCellsCollection,
                Mode = BindingMode.OneWay
            };

            CellDefinitionsList.SetBinding(ItemsControl.ItemsSourceProperty, defineCellsBinding);
        }

        private void CellDefinitionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ListBox).SelectedItem as Cell;

            if (selectedItem != null)
            {
                //synchronize selection
                CellManager.Instance.SelectCell(DataModel.Instance.CurrentWorkbook, selectedItem.Location);
            }

        }

        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            var wb = DataModel.Instance.CurrentWorkbook;
            var selectedItems = new List<object>();
            foreach (var item in CellDefinitionsList.SelectedItems)
            {
                selectedItems.Add(item);
            }

            foreach (var selectedItem in selectedItems)
            {
                if (selectedItem is InputCell)
                {
                    var cellList = new List<Cell>();
                    cellList.Add(selectedItem as Cell);
                    wb.DefineInputCell(cellList, WorkbookModel.CellDefinitionOption.Undefine);
                }
                else if (selectedItem is IntermediateCell)
                {
                    var cellList = new List<Cell>();
                    cellList.Add(selectedItem as Cell);
                    wb.DefineIntermediateCell(cellList, WorkbookModel.CellDefinitionOption.Undefine);
                }
                else if (selectedItem is OutputCell)
                {
                    var cellList = new List<Cell>();
                    cellList.Add(selectedItem as Cell);
                    wb.DefineOutputCell(cellList, WorkbookModel.CellDefinitionOption.Undefine);
                }
            }
        }

        
    }
}
