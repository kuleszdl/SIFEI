using SIF.Visualization.Excel.ScenarioCore;
using System;
using System.Collections.Generic;
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
    /// Interaktionslogik für CreateScenarioDataField.xaml
    /// </summary>
    public partial class CreateScenarioDataField : UserControl
    {
        public CreateScenarioDataField()
        {
            InitializeComponent();

            this.DataContextChanged += CreateScenarioDataField_DataContextChanged;
        }

        private void CreateScenarioDataField_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // remove binding 
            BindingOperations.ClearAllBindings(this.DataTextBox);

            if (this.DataContext != null && this.DataContext is CellData)
            {
                var myCellData = this.DataContext as CellData;
                
                //set text binding

                var textBinding = new Binding()
                {
                    Source = myCellData,
                    Path = new PropertyPath("Content"),
                    Mode = BindingMode.OneWayToSource,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };
                textBinding.ValidationRules.Add(new NumberValidationRule());

                this.DataTextBox.SetBinding(TextBox.TextProperty, textBinding);

                //set icon
                if (this.DataContext is InputCellData)
                {
                    this.DataIcon.Source = this.Resources["InputCellIcon"] as ImageSource;
                }
                else if (this.DataContext is IntermediateCellData)
                {
                    this.DataIcon.Source = this.Resources["IntermediateCellIcon"] as ImageSource;
                }
                else if (this.DataContext is ResultCellData)
                {
                    this.DataIcon.Source = this.Resources["OutputCellIcon"] as ImageSource;
                }
                else
                {
                    this.DataIcon.Source = this.Resources["InputCellIcon"] as ImageSource;
                }
            }
        }

        private void ContextMenu_Open(object sender, RoutedEventArgs e)
        {
            
            this.DataIcon.ContextMenu.PlacementTarget = this.DataIcon;
            this.DataIcon.ContextMenu.Placement = System.Windows.Controls.Primitives.PlacementMode.Bottom;
            this.DataIcon.ContextMenu.IsOpen = true;
        }

        private void ContextMenu_Close(object sender, RoutedEventArgs e)
        {
            this.DataIcon.ContextMenu.IsOpen = false;
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
            this.DataTextBox.Focus();
        }

        /// <summary>
        /// Set the focus to this data text box
        /// </summary>
        public void SetFocus()
        {
            this.DataTextBox.Focus();
        }

        /// <summary>
        /// Register a scenario data fild as next control to get the focus
        /// </summary>
        /// <param name="nextField"></param>
        public void RegisterNextFocusField(CreateScenarioDataField nextField)
        {
            this.FocusToNext += new EventHandler(nextField.SetFocus);
        }

        #endregion

    }
}
