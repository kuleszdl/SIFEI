using SIF.Visualization.Excel.Cells;
using SIF.Visualization.Excel.ScenarioCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace SIF.Visualization.Excel.ScenarioView
{
    /// <summary>
    /// Interaktionslogik für ScenarioDetailPane.xaml
    /// </summary>
    public partial class ScenarioDetailPane : UserControl
    {
        #region Properties
        internal CompositeCollection ScenarioDataCollection
        {
            get;
            private set;
        }

        #endregion


        #region Methods

        public ScenarioDetailPane()
        {
            InitializeComponent();

            this.DataContextChanged += ScenarioDetailPane_DataContextChanged;

        }

        #region Event Handling Methods

        private void ScenarioDetailPane_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null || !(this.DataContext is Scenario)) return;

            var myScenario = this.DataContext as Scenario;

            //update bindings
            #region binding definitions
            var titleBinding = new Binding()
            {
                Source = myScenario,
                Path = new PropertyPath("Title"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var authorBinding = new Binding()
            {
                Source = myScenario,
                Path = new PropertyPath("Author"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var creationDateBinding = new Binding()
            {
                Source = myScenario,
                Path = new PropertyPath("CrationDate"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var ratingBinding = new Binding()
            {
                Source = myScenario,
                Path = new PropertyPath("Rating"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            var descriptionBinding = new Binding()
            {
                Source = myScenario,
                Path = new PropertyPath("Description"),
                Mode = BindingMode.TwoWay,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };

            #endregion

            #region set bindings
            //task pane title
            this.PaneTitle.SetBinding(TextBlock.TextProperty, titleBinding);

            //general
            #region general
            //title
            this.TitleTextBox.SetBinding(TextBox.TextProperty, titleBinding);

            //author
            this.AuthorTextbox.SetBinding(TextBox.TextProperty, authorBinding);

            //creation date
            this.CreateDatePicker.SetBinding(DatePicker.SelectedDateProperty, creationDateBinding);

            //rating
            this.RatingTextBox.SetBinding(TextBox.TextProperty, ratingBinding);

            #endregion

            //description
            this.DescriptionTextBox.SetBinding(TextBox.TextProperty, descriptionBinding);
            
            // input, intermediate and result cells data
            ScenarioDataCollection = new CompositeCollection();

            #region collection container
            var inputDataCContainer = new CollectionContainer()
            {
                Collection = (this.DataContext as Scenario).Inputs
            };
            ScenarioDataCollection.Add(inputDataCContainer);

            var intermediateCContainer = new CollectionContainer()
            {
                Collection = (this.DataContext as Scenario).Intermediates
            };
            ScenarioDataCollection.Add(intermediateCContainer);

            var resultDataCContainer = new CollectionContainer()
            {
                Collection = (this.DataContext as Scenario).Results
            };
            ScenarioDataCollection.Add(resultDataCContainer);
            #endregion

            /*this.ScenarioDataView = new ListCollectionView(scenarioDataCollection);
            this.ScenarioDataView.SortDescriptions.Add(new SortDescription("Location", ListSortDirection.Ascending));*/
            this.ScenarioDataListBox.ItemsSource = ScenarioDataCollection;

            #endregion
        }


        #endregion

        #region Click Methods
        
        /// <summary>
        /// Remove the selected item.
        /// Get the observable collection via the composite collection and the collection containers and remove the selected item.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DeleteDataButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.ScenarioDataListBox.SelectedItem;

            if (selectedItem == null) return;

            foreach (var cont in this.ScenarioDataCollection)
            {
                //remove input cell data
                if ((selectedItem is InputCellData) 
                    && (cont is CollectionContainer) 
                    && (cont as CollectionContainer).Collection is ObservableCollection<InputCellData>)
                {
                    ((cont as CollectionContainer).Collection as ObservableCollection<InputCellData>).Remove(selectedItem as InputCellData);
                }

                //remove intermediate cell data
                if ((selectedItem is IntermediateCellData) 
                    && (cont is CollectionContainer) 
                    && (cont as CollectionContainer).Collection is ObservableCollection<IntermediateCellData>)
                {
                    ((cont as CollectionContainer).Collection as ObservableCollection<IntermediateCellData>).Remove(selectedItem as IntermediateCellData);
                }

                //remove result cell data
                if ((selectedItem is ResultCellData)
                    && (cont is CollectionContainer) 
                    && (cont as CollectionContainer).Collection is ObservableCollection<ResultCellData>)
                {
                    ((cont as CollectionContainer).Collection as ObservableCollection<ResultCellData>).Remove(selectedItem as ResultCellData);
                }
            }
        }

        #endregion

        private void ScenarioDataListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (sender as ListBox).SelectedItem as CellData;

            if (selectedItem != null)
            {
                //synchronize selection
                CellManager.Instance.SelectCell(Core.DataModel.Instance.CurrentWorkbook, selectedItem.Location); 
            }

        }

        #endregion
    }
}
