using System;
using System.Windows;

namespace SIF.Visualization.Excel
{
    public partial class CustomInputDialog : Window
    {
        public CustomInputDialog(string question, string title = "Input", string defaultAnswer = "")
        {
            InitializeComponent();
            Title = title;
            lblQuestion.Content = question;
            txtAnswer.Text = defaultAnswer;
        }

        private void btnDialogOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            txtAnswer.SelectAll();
            txtAnswer.Focus();
        }

        public string Answer
        {
            get { return txtAnswer.Text; }
        }
    }
}
