using Microsoft.Office.Core;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.ScenarioCore;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Forms;
using Binding = System.Windows.Data.Binding;
using CustomTaskPane = Microsoft.Office.Tools.CustomTaskPane;
using MessageBox = System.Windows.MessageBox;
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

        private void EditScenarioButton_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            if (DataContext is WorkbookModel)
            {
                var workbook = (DataContext as WorkbookModel);

                // get scenario detail pane
                var scenarioDetailPane =
                    Globals.ThisAddIn.TaskPanes[new Tuple<WorkbookModel, string>(workbook, "Scenario Details")];


                if (scenarioDetailPane.Control is ScenarioDetailPaneContainer)
                {
                    //open scenario detail pane

                    //scenarioDetailPane.DockPosition = (MsoCTPDockPosition) MsoCTPDockPosition.msoCTPDockPositionFloating;
                    //scenarioDetailPane.Height = 500;
                    scenarioDetailPane.Control.AutoSize = true;
                    scenarioDetailPane.Control.AutoSizeMode = AutoSizeMode.GrowOnly;
                    scenarioDetailPane.Width = 300;
                    scenarioDetailPane.Visible = true;
                    scenarioDetailPane.VisibleChanged += new EventHandler((sender1, e) => PlayMusicEvent(sender, e, scenarioDetailPane));
                }
            }
        }

        private void PlayMusicEvent(object sender, EventArgs eventArgs, CustomTaskPane scenarioDetailPane)
        {
            scenarioDetailPane.Control.Visible = scenarioDetailPane.Visible;
        }


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

        private void DeleteScenarioButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ScenariosList.SelectedItem;

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
