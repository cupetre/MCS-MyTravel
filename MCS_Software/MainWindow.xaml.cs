using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using MCS_Software.Models;
using MCS_Software.Services;
using MCS_Software.ViewModel;

namespace MCS_Software
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;

        private BookingServices bookingServices;

        private ClientServices clientServices; 

        public MainWindow()
        {
            InitializeComponent();

            List<Client> clients = new List<Client>
            {
                new Client { Id = 1, FullName = "James", PassportID = "P123", Date = DateTime.Now, Phone = "123", Notes = "" },
                new Client { Id = 2, FullName = "BaconBoy" , PassportID = "P456", Date = DateTime.Now, Phone = "456", Notes = "" },
                new Client { Id = 2, FullName = "Bachata" , PassportID = "P456", Date = DateTime.Now, Phone = "456", Notes = "" }
            };

            List<Booking> bookings = new List<Booking>
            {
                new Booking { Id = 1, ClientId = 1, StartDate = DateTime.Now, EndDate = DateTime.Now , Destination = "Malta", TotalPrice = 100 , Passengers = { }, Status = "Active" , Notes = "" },
                new Booking { Id = 2, ClientId = 2, StartDate = DateTime.Now, EndDate = DateTime.Now , Destination = "Malta", TotalPrice = 100 , Passengers = { }, Status = "Active" , Notes = "" },
                new Booking { Id = 3, ClientId = 3, StartDate = DateTime.Now, EndDate = DateTime.Now , Destination = "Malta", TotalPrice = 100 , Passengers = { }, Status = "Active" , Notes = "" }
            };

            ClientList.ItemsSource = clients;

            viewModel = new MainViewModel();
            DataContext = viewModel;

        }

        private Client SelectedClient
        {
            get { return ClientList.SelectedItem as Client; }
        }

        /* private Booking SelectedBooking
        {
            get { return BookingList.SelectedItem as Booking; }
        } */

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

        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {

            if ( SelectedClient == null)
            {
                return;
            }

            setEditingState(true);
        }
        private void SaveNewClient_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClient == null)
            {
                return;
            }

            Client newClient = new Client
            {
                FullName = FullNameBox.Text,
                Phone = PhoneBox.Text,
                PassportID = PassportBox.Text,
                Date = DateBirthBox.SelectedDate ?? DateTime.MinValue
            };

            clientServices.AddClient(newClient);
            ClientList.SelectedItem = newClient;
            SetBookingState();
        }

        private void ArrangeBooking (Object sender, RoutedEventArgs e)
        {
            // first we check for the entered info/data for the client here
            // if everything is as itshould be. ask if he wants to continue to booking state
            SetBookingState();
        }

        private void SetBookingState()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Visible;
        }

        private void SaveBookingButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClient == null)
                return;

            Booking booking = new Booking
            {
                ClientId = SelectedClient.Id,
                StartDate = BookingStartDatePicker.SelectedDate ?? DateTime.MinValue,
                EndDate = BookingEndDatePicker.SelectedDate ?? DateTime.MinValue,
                Destination = BookingPlaceTextBox.Text,
                Notes = BookingNotesTextBox.Text
            };

            bookingServices.AddBooking(booking);
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

        private void AddPassengerButton(Object sender, RoutedEventArgs e)
        {
            viewModel.CurrentBooking.Passengers.Add(new Passenger());
        }

        private void RemovePassengerButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement element = sender as FrameworkElement;
            Passenger passenger = element?.DataContext as Passenger;

            if (passenger == null)
                return;

            viewModel.CurrentBooking.Passengers.Remove(passenger);
        }
    }
}