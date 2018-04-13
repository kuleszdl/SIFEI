using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using SIF.Visualization.Excel.Core.Scenarios;

namespace SIF.Visualization.Excel.View
{
    /// <summary>
    ///     Interaktionslogik für CreateScenarioDataField.xaml
    /// </summary>
    public partial class ScenarioDataField : UserControl
    {
        public ScenarioDataField()
        {
            InitializeComponent();

            DataContextChanged += ScenarioDataField_DataContextChanged;
        }

        private void ScenarioDataField_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // remove binding 
            BindingOperations.ClearAllBindings(DataTextBox);

            if (DataContext != null && DataContext is ScenarioData)
            {
                var scenarioData = DataContext as ScenarioData;

                //set text binding

                var textBinding = new Binding
                {
                    Source = scenarioData,
                    Path = new PropertyPath("Value"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                DataTextBox.SetBinding(TextBox.TextProperty, textBinding);

                //set icon
                if (DataContext is InputData) DataIcon.Source = Resources["InputCellIcon"] as ImageSource;
                else if (DataContext is InvariantData)
                    DataIcon.Source = Resources["IntermediateCellIcon"] as ImageSource;
                else if (DataContext is ConditionData) DataIcon.Source = Resources["OutputCellIcon"] as ImageSource;
            }
        }

        private void ContextMenu_Open(object sender, RoutedEventArgs e)
        {
            DataIcon.ContextMenu.PlacementTarget = DataIcon;
            DataIcon.ContextMenu.Placement = PlacementMode.Bottom;
            DataIcon.ContextMenu.IsOpen = true;
        }

        private void ContextMenu_Close(object sender, RoutedEventArgs e)
        {
            DataIcon.ContextMenu.IsOpen = false;
        }

        private void OnKeyDownHandler(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Tab)
            {
                OnFocusToNext(EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.Key == Key.Enter)
            {
                OnFocusToNext(EventArgs.Empty);
                e.Handled = true;
            }
        }

        #region event handling for focus change

        /// <summary>
        ///     This event will be raised if the focus of the data text box should be gone to the next
        /// </summary>
        private event EventHandler FocusToNext;

        protected virtual void OnFocusToNext(EventArgs e)
        {
            if (FocusToNext != null) FocusToNext(this, e);
        }

        /// <summary>
        ///     Set the focus to this data text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFocus(object sender, EventArgs e)
        {
            DataTextBox.Focus();
        }

        /// <summary>
        ///     Set the focus to this data text box
        /// </summary>
        public void SetFocus()
        {
            DataTextBox.Focus();
        }

        /// <summary>
        ///     Register a scenario data fild as next control to get the focus
        /// </summary>
        /// <param name="nextField"></param>
        public void RegisterNextFocusField(ScenarioDataField nextField)
        {
            FocusToNext += nextField.SetFocus;
        }

        #endregion
    }
}