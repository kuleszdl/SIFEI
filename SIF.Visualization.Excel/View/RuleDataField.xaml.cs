using System;
using System.Collections.Generic;
using SIF.Visualization.Excel.Core.Rules;
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

namespace SIF.Visualization.Excel.View
{
    /// <summary>
    /// Interaktionslogik für RuleDataField.xaml
    /// </summary>
    public partial class RuleDataField : UserControl
    {
        public RuleDataField()
        {
            InitializeComponent();

            DataContextChanged += RuleDataField_DataContextChanged;
        }

        private void RuleDataField_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            BindingOperations.ClearAllBindings(DataTextBox);

            if (DataContext != null && DataContext is RuleCells)
            {
                var ruleData = DataContext as RuleCells;

                //textbinding
                var textBinding = new Binding()
                {
                    Source = ruleData,
                    Path = new PropertyPath("RuleValue"),
                    Mode = BindingMode.TwoWay,
                    UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged
                };

                DataTextBox.SetBinding(TextBox.TextProperty, textBinding);

                //set icon TODO

            }
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

        #region Focus change
        private event EventHandler FocusToNext;
        protected virtual void OnFocusToNext(EventArgs e)
        {
            if (FocusToNext != null)
            {
                FocusToNext(this, e);
            }
        }

        private void SetFocus(object sender, EventArgs e)
        {
            DataTextBox.Focus();
        }

        public void SetFocus()
        {
            DataTextBox.Focus();
        }

        ///// <summary>
        ///// Register a rule data field as next control to get the focus
        ///// </summary>
        ///// <param name="nextField"></param>
        public void RegisterNextFocusField(RuleDataField nextField)
        {
            FocusToNext += new EventHandler(nextField.SetFocus);
        }

        #endregion

        
    }
}
