using System.Windows;
using System.Windows.Controls;

namespace MCS_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
                
        }

        private void ShowNoClientSelectedView()
        {
            NoClientSelectedView.Visibility = Visibility.Visible;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
        }

        private void ShowSelectedClientView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Visible;
            AddNewClientView.Visibility = Visibility.Collapsed;
        }

        private void ShowAddNewClientView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Visible;
        }

        private void AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            ShowAddNewClientView();
        }

        private void CancelAddNewClient_Click(object sender, RoutedEventArgs e)
        {
            ShowNoClientSelectedView();
        }
    }
}
