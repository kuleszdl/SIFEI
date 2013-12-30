using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore;
using SIF.Visualization.Excel.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIF.Visualization.Excel.ScenarioView
{
    /// <summary>
    /// Interaktionslogik für DefineCellsPane.xaml
    /// </summary>
    public partial class DefineCellsPane : UserControl
    {
        public DefineCellsPane()
        {
            InitializeComponent();
            
            this.DataContextChanged += DefineCellsPane_DataContextChanged;
        }

        private void DefineCellsPane_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null || !(this.DataContext is WorkbookModel)) return;

            var myWorkbookModel = this.DataContext as WorkbookModel;
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

            this.CellDefinitionsList.SetBinding(ListBox.ItemsSourceProperty, defineCellsBinding);
        }

        private void CellDefinitionsList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ListBox).SelectedItem as Core.Cell;

            if (selectedItem != null)
            {
                //synchronize selection
                CellManager.Instance.SelectCell(Core.DataModel.Instance.CurrentWorkbook, selectedItem.Location);
            }

        }

        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            var wb = Core.DataModel.Instance.CurrentWorkbook;
            var selectedItems = new List<object>();
            foreach (var item in this.CellDefinitionsList.SelectedItems)
            {
                selectedItems.Add(item);
            }

            foreach (var selectedItem in selectedItems)
            {
                if (selectedItem is Cells.InputCell)
                {
                    var cellList = new List<Cell>();
                    cellList.Add(selectedItem as Cell);
                    wb.DefineInputCell(cellList, WorkbookModel.CellDefinitionOption.Undefine);
                }
                else if (selectedItem is Cells.IntermediateCell)
                {
                    var cellList = new List<Cell>();
                    cellList.Add(selectedItem as Cell);
                    wb.DefineIntermediateCell(cellList, WorkbookModel.CellDefinitionOption.Undefine);
                }
                else if (selectedItem is Cells.OutputCell)
                {
                    var cellList = new List<Cell>();
                    cellList.Add(selectedItem as Cell);
                    wb.DefineOutputCell(cellList, WorkbookModel.CellDefinitionOption.Undefine);
                }
            }
        }

        
    }
}
