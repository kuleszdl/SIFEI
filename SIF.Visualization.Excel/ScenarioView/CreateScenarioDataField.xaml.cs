using SIF.Visualization.Excel.ScenarioCore;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace SIF.Visualization.Excel.ScenarioView
{
    /// <summary>
    /// Interaktionslogik für CreateScenarioDataField.xaml
    /// </summary>
    public partial class CreateScenarioDataField : UserControl
    {
        public CreateScenarioDataField()
        {
            InitializeComponent();

            DataContextChanged += CreateScenarioDataField_DataContextChanged;
        }

        private void CreateScenarioDataField_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // remove binding 
            BindingOperations.ClearAllBindings(DataTextBox);

            if (DataContext != null && DataContext is CellData)
            {
                var myCellData = DataContext as CellData;
                
                //set text binding

                var textBinding = new Binding()
                {
                    Source = myCellData,
                    Path = new PropertyPath("Content"),
                    Mode = BindingMode.OneWayToSource,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                textBinding.ValidationRules.Add(new NumberValidationRule());

                DataTextBox.SetBinding(TextBox.TextProperty, textBinding);

                //set icon
                if (DataContext is InputCellData)
                {
                    DataIcon.Source = Resources["InputCellIcon"] as ImageSource;
                }
                else if (DataContext is IntermediateCellData)
                {
                    DataIcon.Source = Resources["IntermediateCellIcon"] as ImageSource;
                }
                else if (DataContext is ResultCellData)
                {
                    DataIcon.Source = Resources["OutputCellIcon"] as ImageSource;
                }
                else
                {
                    DataIcon.Source = Resources["InputCellIcon"] as ImageSource;
                }
            }
        }

        private void ContextMenu_Open(object sender, RoutedEventArgs e)
        {
            
            DataIcon.ContextMenu.PlacementTarget = DataIcon;
            DataIcon.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
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
        /// This event will be raised if the focus of the data text box should be gone to the next
        /// </summary>
        private event EventHandler FocusToNext;

        protected virtual void OnFocusToNext(EventArgs e)
        {
            if (FocusToNext != null)
            {
                FocusToNext(this, e);
            }
        }

        /// <summary>
        /// Set the focus to this data text box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetFocus(object sender, EventArgs e)
        {
            DataTextBox.Focus();
        }

        /// <summary>
        /// Set the focus to this data text box
        /// </summary>
        public void SetFocus()
        {
            DataTextBox.Focus();
        }

        /// <summary>
        /// Register a scenario data fild as next control to get the focus
        /// </summary>
        /// <param name="nextField"></param>
        public void RegisterNextFocusField(CreateScenarioDataField nextField)
        {
            FocusToNext += new EventHandler(nextField.SetFocus);
        }

        #endregion

    }
}
