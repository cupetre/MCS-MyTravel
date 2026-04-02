using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        private BookingServices bookingServices = new BookingServices();

        private ClientServices clientServices = new ClientServices();

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

        private void ShowNoClientSelectedView()
        {
            NoClientSelectedView.Visibility = Visibility.Visible;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Collapsed;

        }

        private void ShowSelectedClientView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Visible;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Collapsed;

            EditButton.IsEnabled = true;
        }

        private void ShowAddNewClientView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Visible;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Collapsed;
        }
        private void ShowBookingView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Visible;
            DocumentChoiceView.Visibility = Visibility.Collapsed;
        }

        private void ShowDocumentChoiceState()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Visible;
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

            if (SelectedClient == null)
            {
                ShowNoClientSelectedView();
                return;
            }

            ShowSelectedClientView();

            FullNameBox.Text = SelectedClient.FullName;
            PhoneBox.Text = SelectedClient.Phone;
            DateBirthBox.SelectedDate = SelectedClient.Date;
            PassportBox.Text = SelectedClient.PassportID;
            NotesBox.Text = SelectedClient.Notes;

            ClientNameHeader.Text = SelectedClient.FullName;

            SetEditingState(false);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedClient == null)
                return;

            clientServices.UpdateClient(
                SelectedClient,
                FullNameBox.Text,
                PassportBox.Text,
                DateBirthBox.SelectedDate ?? DateTime.MinValue,
                PhoneBox.Text,
                NotesBox.Text
            );

            ClientNameHeader.Text = SelectedClient.FullName;

            SetEditingState(false);
        }

        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {

            if ( SelectedClient == null)
            {
                return;
            }

            SetEditingState(true);
        }
        private void SaveNewClient_Click(object sender, RoutedEventArgs e)
        {
            // Basic validation
            if (string.IsNullOrWhiteSpace(NewFullName.Text))
            {
                MessageBox.Show("Please enter a full name.", "Validation");
                return;
            }

            Client newClient = new Client
            {
                Id = viewModel.Clients.Count + 1,
                FullName = NewFullName.Text,
                Phone = NewPhone.Text,
                PassportID = NewPassport.Text,
                Date = NewDateBirth.SelectedDate ?? DateTime.MinValue,
                Notes = NewNotes.Text
            };

            viewModel.Clients.Add(newClient);
            clientServices.AddClient(newClient);

            // Clear the form
            NewFullName.Text = "";
            NewPhone.Text = "";
            NewPassport.Text = "";
            NewDateBirth.SelectedDate = null;
            NewNotes.Text = "";

            ClientList.SelectedItem = newClient;
            ShowBookingView();
        }

        private void ArrangeBooking (Object sender, RoutedEventArgs e)
        {
            // first we check for the entered info/data for the client here
            // if everything is as itshould be. ask if he wants to continue to booking state
            ShowBookingView();
        }


        private void SaveBookingButton_Click(object sender, RoutedEventArgs e)
        {
            viewModel.CurrentBooking.ClientId = SelectedClient.Id;
            viewModel.CurrentBooking.StartDate = BookingStartDatePicker.SelectedDate ?? DateTime.MinValue;
            viewModel.CurrentBooking.EndDate = BookingEndDatePicker.SelectedDate ?? DateTime.MinValue;
            viewModel.CurrentBooking.Destination = BookingPlaceTextBox.Text;
            viewModel.CurrentBooking.Notes = BookingNotesTextBox.Text;

            bookingServices.AddBooking(viewModel.CurrentBooking);

            ShowDocumentChoiceState();
        }

        private void CancelBookingButton_Click(object sender, RoutedEventArgs e)
        {
            BookingStartDatePicker.SelectedDate = null;
            BookingEndDatePicker.SelectedDate = null;
            BookingPlaceTextBox.Text = "";
            BookingTotalPriceTextBox.Text = "";
            BookingNotesTextBox.Text = "";

            viewModel.CurrentBooking.Passengers.Clear();

            if (SelectedClient != null)
                ShowSelectedClientView();
            else
                ShowNoClientSelectedView();
        }

        private void SetEditingState(bool isEditing)
        {
            FullNameBox.IsEnabled = isEditing;
            PhoneBox.IsEnabled = isEditing;
            DateBirthBox.IsEnabled = isEditing;
            PassportBox.IsEnabled = isEditing;

            EditButton.IsEnabled = !isEditing;
            SaveButton.IsEnabled = isEditing;
        }

        private void AddPassengerButton_Click(Object sender, RoutedEventArgs e)
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

        private void AgreementCard_Click(object sender, MouseButtonEventArgs e)
        {
            // ShowAgreementForm(); — hook this up later
        }

        private void VoucherCard_Click(object sender, MouseButtonEventArgs e)
        {
            // ShowVoucherForm(); — hook this up later
        }

        private void InvoiceCard_Click(object sender, MouseButtonEventArgs e)
        {
            // ShowInvoiceForm(); — hook this up later
        }
    }
}