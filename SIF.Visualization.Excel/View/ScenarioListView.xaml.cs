using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Scenarios;

namespace SIF.Visualization.Excel.View
{
    /// <summary>
    ///     Interaktionslogik für ScenarioListView.xaml
    /// </summary>
    public partial class ScenarioListView : UserControl
    {
        #region Properties

        internal ListCollectionView ScenariosView { get; private set; }

        #endregion

        #region Methods

        /// <summary>
        ///     Initializes a new Scenario Pane including some Binding for the searchbox
        /// </summary>
        public ScenarioListView()
        {
            InitializeComponent();
            DataContextChanged += ScenarioListView_DataContextChanged;
        }

        private void ScenarioListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
                return;

            var binding = new Binding
            {
                Source = DataModel.Instance.CurrentWorkbook.Scenarios,
                Mode = BindingMode.OneWay
            };

            ScenarioListBox.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        #endregion

        #region Click Methods

        /// <summary>
        ///     Checks on which scenario the edit button got clicked to then open the detailed view of that scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void EditScenarioButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            var model = DataContext as WorkbookModel;
            if (model != null)
            {
                var workbook = model;
                var window = new Window
                {
                    // @TODO: use resources
                    Title = "Scenario Detail Dialog",
                    Content = new ScenarioDetailDialog(),
                    SizeToContent = SizeToContent.WidthAndHeight,
                    ResizeMode = ResizeMode.NoResize
                };

                window.ShowDialog();
            }
        }

        /// <summary>
        ///     Checks on which scenario the delete button got clicked to then delete the corresponding scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void DeleteScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ScenarioListBox.SelectedItem;

            if (selectedItem != null)
                (ScenariosView.SourceCollection as ObservableCollection<Scenario>).Remove(selectedItem as Scenario);

            //delete scenario
        }

        #endregion
    }
}