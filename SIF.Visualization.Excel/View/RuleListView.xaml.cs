using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using SIF.Visualization.Excel.Core;
using SIF.Visualization.Excel.Core.Rules;
using SIF.Visualization.Excel.View.CustomRules;

namespace SIF.Visualization.Excel.View
{
    /// <summary>
    ///     Interaction logic for RuleListView.xaml
    /// </summary>
    public partial class RuleListView : UserControl
    {
        public RuleListView()
        {
            InitializeComponent();
            DataContextChanged += RuleListView_DataContextChanged;
        }

        internal ListCollectionView RuleView { get; private set; }

        private void RuleListView_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (DataContext == null)
                return;
            var binding = new Binding
            {
                Source = DataModel.Instance.CurrentWorkbook.Rules,
                Mode = BindingMode.OneWay
            };
            RuleListBox.SetBinding(ItemsControl.ItemsSourceProperty, binding);
        }

        private void SidebarDeleteRuleButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var grid = button.Parent as Grid;
            var rule = grid.DataContext as Rule;
            try
            {
                DataModel.Instance.CurrentWorkbook.Rules.Remove(rule);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }

        private void SidebarEditRuleButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var grid = button.Parent as Grid;
            var rule = grid.DataContext as Rule;
            try
            {
                RuleEditor.Instance.Open(rule);
            }
            catch (Exception f)
            {
                MessageBox.Show(f.ToString());
            }
        }
    }
}