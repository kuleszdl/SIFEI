using Microsoft.Office.Core;
using SIF.Visualization.Excel.Core;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using Binding = System.Windows.Data.Binding;
using Button = System.Windows.Forms.Button;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;
using MessageBox = System.Windows.MessageBox;
using Scenario = SIF.Visualization.Excel.ScenarioCore.Scenario;
using TextBox = System.Windows.Controls.TextBox;
using UserControl = System.Windows.Controls.UserControl;

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

        internal ListCollectionView ScenariosView { get; private set; }

        /// <summary>
        /// Gets or sets the string to filter for in the search box
        /// </summary>
        public string FilterString
        {
            get
            {
                if (filterString == null) filterString = String.Empty;
                return filterString;
            }
            set
            {
                filterString = value;
                if (ScenariosView != null) ScenariosView.Refresh();
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a new Scenario Pane including some Binding for the searchbox
        /// </summary>
        public ScenarioPane()
        {
            InitializeComponent();

            DataContextChanged += ScenarioPane_DataContextChanged;


            var searchBoxBinding = new Binding()
            {
                Source = this,
                Path = new PropertyPath("FilterString"),
                Mode = BindingMode.OneWayToSource,
                UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
            };
            searchBox.SetBinding(TextBox.TextProperty, searchBoxBinding);
        }

        #region Event Handling Methods

        private void ScenarioPane_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null || !(DataContext is WorkbookModel)) return;
            var workbook = DataContext as WorkbookModel;

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
            ScenariosView = new ListCollectionView((DataContext as WorkbookModel).Scenarios);
            ScenariosView.SortDescriptions.Add(new SortDescription("Title", ListSortDirection.Ascending));
            ScenariosView.Filter = SceanrioFilter;

            ScenariosList.ItemsSource = ScenariosView;

            // count scenarios
            countTextBlock.SetBinding(TextBlock.TextProperty, countScenariosBinding);

            #endregion

            //deselect the items.
            ScenariosList.SelectedIndex = -1;
        }

        #endregion

        #region Click Methods
        
        
        /// <summary>
        /// Checks on which scenario the edit button got clicked to then open the detailed view of that scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void EditScenarioButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            var model = DataContext as WorkbookModel;
            if (model != null)
            {
                var workbook = model;
                // get scenario detail pane
                var scenarioDetailPane =
                    Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(workbook, "Scenario Details")];

                if (scenarioDetailPane.Control is ScenarioDetailPaneContainer)
                {
                    var selectedItem = (sender as FrameworkElement).Tag;
                    ScenariosList.SelectedItem = selectedItem;
                    //open scenario detail pane
                    scenarioDetailPane.DockPosition = (MsoCTPDockPosition) MsoCTPDockPosition.msoCTPDockPositionFloating;
                    scenarioDetailPane.Height = 500;
                    scenarioDetailPane.Control.AutoSize = true;
                    scenarioDetailPane.Control.AutoSizeMode = AutoSizeMode.GrowOnly;
                    scenarioDetailPane.Width = 500;
                    scenarioDetailPane.Visible = true;
                    scenarioDetailPane.VisibleChanged += new EventHandler((sender1, e) => ChangeVisibility(sender, e, scenarioDetailPane));
                }
            }
        }

        /// <summary>
        /// Changes the Visibility of the scenario detail pane
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        /// <param name="scenarioDetailPane"></param>
        private void ChangeVisibility(object sender, EventArgs eventArgs, CustomTaskPane scenarioDetailPane)
        {
            scenarioDetailPane.Control.Visible = scenarioDetailPane.Visible;
        }

        /// <summary>
        /// Nico: Believe this is depreciated now. Since there was no comment before on what this method really does can't be sure though.
        /// So for now i am leaving it in. Sorry 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ScenariosList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext is WorkbookModel)
            {
                var workbook = (DataContext as WorkbookModel);

                // get scenario detail pane
                if (
                    Globals.ThisAddIn.TaskPanes.ContainsKey(new Tuple<WorkbookModel, string>(workbook,
                        "Scenario Details")))
                {
                    var scenarioDetailPane =
                        Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(workbook, "Scenario Details")];

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

        /// <summary>
        /// Checks on which scenario the delete button got clicked to then delete the corresponding scenario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="routedEventArgs"></param>
        private void DeleteScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = (sender as FrameworkElement).Tag;
            ScenariosList.SelectedItem = selectedItem;
            

            if (selectedItem == null || !(selectedItem is Scenario)) return;

            string messageText = Properties.Resources.tl_ScenarioPane_DeleteConfirmQuestion +
                                 "?:" + "\n" + (selectedItem as Scenario).Title;
            MessageBoxResult result = MessageBox.Show(messageText,
                Properties.Resources.tl_ScenarioPane_DeleteConfirmQuestionTitle,
                MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return;

            #region if the selected scenario ist opend in the detail pane close it

            // get scenario detail pane
            var scenarioDetailPane =
                Globals.ThisAddIn.TaskPanes[
                    new Tuple<WorkbookModel, string>((DataContext as WorkbookModel), "Scenario Details")];
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
            (ScenariosView.SourceCollection as ObservableCollection<Scenario>).Remove(selectedItem as Scenario);
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
