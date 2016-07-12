using System;
using System.Windows;
using System.Windows.Forms;

namespace SIF.Visualization.Excel.ScenarioView
{
    public partial class ScenarioDetailPaneContainer : UserControl
    {
        public ScenarioDetailPane ScenarioDetailPane
        {
            get
            {
                if (ScenarioDetailPaneHost != null && ScenarioDetailPaneHost.Child != null)
                    return ScenarioDetailPaneHost.Child as ScenarioDetailPane;
                else return null;
            }
        }

        public ScenarioDetailPaneContainer()
        {
            InitializeComponent();
            VisibleChanged += ScenarioDetailPaneContainer_VisibleChanged;
            
        }

        void ScenarioDetailPaneContainer_VisibleChanged(object sender, EventArgs e)
        {
            
                if (Visible)
                {
                    ScenarioDetailPane.Visibility = Visibility.Visible;
                }
                else
                {
                    ScenarioDetailPane.Visibility = Visibility.Collapsed;
                }
           


        }
    }
}
