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

    }
}
