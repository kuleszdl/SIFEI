using Microsoft.Office.Core;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore;
using SIF.Visualization.Excel.ScenarioView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Interaktionslogik für ScenarioPane.xaml
    /// </summary>
    public partial class ScenarioPane : UserControl
    {
        #region Fields

        private string filterString;

        #endregion

        #region Properties

        internal ListCollectionView ScenariosView
        {
            get;
            private set;
        }

        public string FilterString
        {
            get
            {
                if (filterString == null) filterString = String.Empty;
                return this.filterString;
            }
            set
            {
                this.filterString = value;
                if (this.ScenariosView != null) this.ScenariosView.Refresh();
            }
        }

        #endregion

        #region Methods
        public ScenarioPane()
        {
            InitializeComponent();

            this.DataContextChanged += ScenarioPane_DataContextChanged;

            var searchBoxBinding = new Binding()
            {
                Source = this,
                Path = new PropertyPath("FilterString"),
                Mode = BindingMode.OneWayToSource,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            this.searchBox.SetBinding(TextBox.TextProperty, searchBoxBinding);
        }

        #region Event Handling Methods

        private void ScenarioPane_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (this.DataContext == null || !(this.DataContext is WorkbookModel)) return;
            var workbook = this.DataContext as WorkbookModel;

            //update bindings
            #region binding definitions

            var countScenariosBinding = new Binding()
            {
                Source = workbook,
                Path = new PropertyPath("Scenarios.Count"),
                Mode = BindingMode.OneWay
            };

            #endregion

            #region set bindings

            //scenario list
            this.ScenariosView = new ListCollectionView((this.DataContext as WorkbookModel).Scenarios);
            this.ScenariosView.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            this.ScenariosView.Filter = SceanrioFilter;

            this.ScenariosList.ItemsSource = this.ScenariosView;

            // count scenarios
            this.countTextBlock.SetBinding(TextBlock.TextProperty, countScenariosBinding);
            #endregion

            //deselect the items.
            this.ScenariosList.SelectedIndex = -1;

        }

        #endregion

        #region Click Methods
        private void ScenarioDesc_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (this.DataContext is WorkbookModel)
            {
                var workbook = (this.DataContext as WorkbookModel);

                // get scenario detail pane
                var scenarioDetailPane = Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(workbook, "Scenario Details")];

                if (scenarioDetailPane.Control is ScenarioDetailPaneContainer)
                {
                    //open scenario detail pane
                    scenarioDetailPane.Visible = true;

                }
            }
        }

        private void ScenariosList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext is WorkbookModel)
            {
                var workbook = (this.DataContext as WorkbookModel);

                // get scenario detail pane
                if (Globals.ThisAddIn.TaskPanes.ContainsKey(new Tuple<WorkbookModel, string>(workbook, "Scenario Details")))
                {
                    var scenarioDetailPane = Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(workbook, "Scenario Details")];

                    if (scenarioDetailPane.Control is ScenarioDetailPaneContainer)
                    {
                        // set new data context
                        var selectedItem = ScenariosList.SelectedItem as Scenario;
                        var scenarioDetailPaneContainer = scenarioDetailPane.Control as ScenarioDetailPaneContainer;
                        scenarioDetailPaneContainer.ScenarioDetailPane.DataContext = selectedItem;

                        //open scenario detail pane
                        //scenarioDetailPane.Visible = true;
                    }
                }
            }
        }

        private void DeleteScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = this.ScenariosList.SelectedItem;

            if (selectedItem == null || !(selectedItem is Scenario)) return;

            string messageText = SIF.Visualization.Excel.Properties.Resources.tl_ScenarioPane_DeleteConfirmQuestion +  "?:" + "\n" + (selectedItem as Scenario).Title;
            MessageBoxResult result = MessageBox.Show(messageText, SIF.Visualization.Excel.Properties.Resources.tl_ScenarioPane_DeleteConfirmQuestionTitle, MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return;
            

            #region if the selected scenario ist opend in the detail pane close it
            // get scenario detail pane
            var scenarioDetailPane = Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>((this.DataContext as WorkbookModel), "Scenario Details")];
            var scenarioDetailPaneContainer = scenarioDetailPane.Control as ScenarioDetailPaneContainer;
            if ((selectedItem as Scenario).Equals(scenarioDetailPaneContainer.ScenarioDetailPane.DataContext as Scenario))
            {
                // close detail pane
                scenarioDetailPane.Visible = false;

                //delete data context
                scenarioDetailPaneContainer.ScenarioDetailPane.DataContext = null;
            }
            #endregion

            //delete scenario
            (this.ScenariosView.SourceCollection as ObservableCollection<Scenario>).Remove(selectedItem as Scenario);


        }

        #endregion

        #region Filter Methods

        private bool SceanrioFilter(object item)
        {
            var secnario = item as Scenario;

            return secnario.Title.IndexOf(FilterString, StringComparison.OrdinalIgnoreCase) >= 0;
        }

        #endregion

        #endregion




    }
}
