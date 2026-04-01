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

        private Client SelectedClient
        {
            get { return ClientList.SelectedItem as Client; }
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

            EditButton.IsEnabled = true;
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
            EditButton.IsEnabled = true;

            ShowSelectedClientView();

            if (SelectedClient == null)
            {
                return;
            }

            FullNameBox.Text = SelectedClient.FullName;
            PhoneBox.Text = SelectedClient.Phone;
            DateBirthBox.SelectedDate = SelectedClient.Date;
            PassportBox.Text = SelectedClient.PassportID;
            NotesBox.Text = SelectedClient.Notes;

            ClientNameHeader.Text = SelectedClient.FullName;

            setEditingState(false);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if ( SelectedClient == null)
            {
                return;
            }

            SelectedClient.FullName = FullNameBox.Text;
            SelectedClient.Phone = PhoneBox.Text;
            SelectedClient.PassportID = PassportBox.Text;
            SelectedClient.Date = DateBirthBox.SelectedDate ?? DateTime.MinValue;

            ClientList.Items.Refresh();
            
            setEditingState(false);
        }

        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {

            if ( SelectedClient == null)
            {
                return;
            }

            setEditingState(true);
        }

        private void setEditingState(bool isEditing)
        {
            FullNameBox.IsEnabled = isEditing;
            PhoneBox.IsEnabled = isEditing;
            DateBirthBox.IsEnabled = isEditing;
            PassportBox.IsEnabled = isEditing;

            EditButton.IsEnabled = !isEditing;
            SaveButton.IsEnabled = isEditing;
        }
    }
}