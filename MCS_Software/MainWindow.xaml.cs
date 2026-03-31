using System;
using System.Collections.Generic;
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

            List<Client> clients = new List<Client>
            {
                new Client { Id = 1, FullName = "James", PassportID = "P123", Date = DateTime.Now, Phone = "123", Notes = "" },
                new Client { Id = 2, FullName = "BaconBoy" , PassportID = "P456", Date = DateTime.Now, Phone = "456", Notes = "" },
                new Client { Id = 2, FullName = "Bachata" , PassportID = "P456", Date = DateTime.Now, Phone = "456", Notes = "" }
            };

            ClientList.ItemsSource = clients;
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

        private void ClientList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ShowSelectedClientView();

            Client selectedClient = ClientList.SelectedItem as Client;

            if (selectedClient == null)
            {
                return;
            }

            FullNameBox.Text = selectedClient.FullName;
            PhoneBox.Text = selectedClient.Phone;
            DateBirthBox.SelectedDate = selectedClient.Date;
            PassportBox.Text = selectedClient.PassportID;
            NotesBox.Text = selectedClient.Notes;
        }
    }
}