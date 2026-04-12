using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MCS_MyTravel.Models;
using MCS_MyTravel.Repositories;
using MCS_MyTravel.Services;
using MCS_MyTravel.ViewModel;

namespace MCS_MyTravel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel viewModel;

        private BookingServices bookingServices = new BookingServices();

        public MainWindow(MainViewModel vm)
        {
            InitializeComponent();
            viewModel = vm;
            DataContext = viewModel;
        }
        private Client? SelectedClient => ClientList.SelectedItem as Client;

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
            PaymentsPanel.Visibility = Visibility.Collapsed;

        }

        private void ShowSelectedClientView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Visible;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Collapsed;
            PaymentsPanel.Visibility = Visibility.Collapsed;

            EditButton.IsEnabled = true;
        }

        private void ShowAddNewClientView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Visible;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Collapsed;
            PaymentsPanel.Visibility = Visibility.Collapsed;
        }
        private void ShowBookingView()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Visible;
            DocumentChoiceView.Visibility = Visibility.Collapsed;
            PaymentsPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowDocumentChoiceState()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Visible;
            PaymentsPanel.Visibility = Visibility.Collapsed;
        }

        private void ShowPaymentState()
        {
            NoClientSelectedView.Visibility = Visibility.Collapsed;
            SelectedClientView.Visibility = Visibility.Collapsed;
            AddNewClientView.Visibility = Visibility.Collapsed;
            BookingPanel.Visibility = Visibility.Collapsed;
            DocumentChoiceView.Visibility = Visibility.Collapsed;
            PaymentsPanel.Visibility = Visibility.Visible;
        }

        // this is just for viewing the state of adding a new client
        private void AddNewClient_Click(object sender, RoutedEventArgs e)
        {
            ShowAddNewClientView();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await viewModel.LoadClientsAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error loading clients", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

            viewModel.CurrentClient = new Client
            {
                Id = SelectedClient.Id,
                FullName = SelectedClient.FullName,
                BirthDate = SelectedClient.BirthDate,
                Phone = SelectedClient.Phone,
                PassportId = SelectedClient.PassportId,
                Notes = SelectedClient.Notes
            };

            ClientNameHeader.Text = SelectedClient.FullName;

            SetEditingState(false);
        }

        // activating forms to be able to be EDITED on EXISTING CLIENT
        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {

            if (SelectedClient == null)
            {
                return;
            }

            SetEditingState(true);
        }

        private void CancelAddNewClient_Click(Object sender, RoutedEventArgs e)
        {
            SetEditingState(false);
        }

        // Creating a new client from base / updating existing client
        private async void SaveNewClient_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                await viewModel.SaveClientAsync();
                MessageBox.Show("Client saved successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            ShowBookingView();
        }

        private void ArrangeBooking(Object sender, RoutedEventArgs e)
        {
            // first we check for the entered info/data for the client here
            // if everything is as itshould be. ask if he wants to continue to booking state
            ShowBookingView();
        }

        private void SaveBookingButton_Click(object sender, RoutedEventArgs e)
        {

            if ( SelectedClient == null )
            {
                return;
            }

            viewModel.CurrentBooking.ClientId = SelectedClient.Id;
            viewModel.CurrentBooking.StartDate = BookingStartDatePicker.SelectedDate ?? DateTime.MinValue;
            viewModel.CurrentBooking.EndDate = BookingEndDatePicker.SelectedDate ?? DateTime.MinValue;
            viewModel.CurrentBooking.Destination = BookingPlaceTextBox.Text;
            viewModel.CurrentBooking.Notes = BookingNotesTextBox.Text;

            bookingServices.AddBooking(viewModel.CurrentBooking);

            ShowPaymentState();
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
            SaveChangesButton.IsEnabled = isEditing;
        }

        private void AddPassengerButton_Click(Object sender, RoutedEventArgs e)
        {
            viewModel.CurrentBooking.Passengers.Add(new Passenger());
        }

        private void RemovePassengerButton_Click(object sender, RoutedEventArgs e)
        {
            FrameworkElement? element = sender as FrameworkElement;
            Passenger? passenger = element?.DataContext as Passenger;

            if (passenger == null)
                return;

            viewModel.CurrentBooking.Passengers.Remove(passenger);
        }

        private void SavePaymentButton_Click(object sender, EventArgs e)
        {
            ShowDocumentChoiceState();
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

        private void PriceCheckBox_Changed(object sender, RoutedEventArgs e)
        {
            if (TravelPriceBox == null) return;

            TravelPriceBox.IsEnabled = IncludeTravelCheckBox.IsChecked == true;
            InsurancePriceBox.IsEnabled = IncludeInsuranceCheckBox.IsChecked == true;
            TaxesPriceBox.IsEnabled = IncludeTaxesCheckBox.IsChecked == true;

            RecalculateTotal();
        }

        private void PriceField_Changed(object sender, TextChangedEventArgs e)
        {
            RecalculateTotal();
        }

        private void RecalculateTotal()
        {
            // Controls not loaded yet, skip
            if (HotelPriceBox == null || CalculatedTotalText == null)
                return;

            decimal hotel = decimal.TryParse(HotelPriceBox.Text, out var h) ? h : 0;
            decimal travel = (IncludeTravelCheckBox.IsChecked == true && decimal.TryParse(TravelPriceBox.Text, out var t)) ? t : 0;
            decimal insurance = (IncludeInsuranceCheckBox.IsChecked == true && decimal.TryParse(InsurancePriceBox.Text, out var i)) ? i : 0;
            decimal taxes = (IncludeTaxesCheckBox.IsChecked == true && decimal.TryParse(TaxesPriceBox.Text, out var tx)) ? tx : 0;

            decimal total = hotel + travel + insurance + taxes;

            CalculatedTotalText.Text = total.ToString("F2");
        }

        private void UpdatePaymentSummary(decimal total)
        {
            decimal paid = viewModel.CurrentBooking.Payments
                .Sum(p => p.Amount);

            decimal remaining = total - paid;
            decimal percent = total > 0 ? (paid / total * 100) : 0;

            SummaryPaidText.Text = paid.ToString("F2");
            SummaryRemainingText.Text = remaining.ToString("F2");
            SummaryPercentText.Text = $"{percent:F0}%";
        }

        private void AddPaymentButton_Click(object sender, RoutedEventArgs e)
        {
            if (!decimal.TryParse(PaymentAmountBox.Text, out decimal amount) || amount <= 0)
            {
                MessageBox.Show("Please enter a valid payment amount.", "Validation");
                return;
            }

            Payment payment = new Payment
            {
                Amount = amount,
                PaymentDate = PaymentDatePicker.SelectedDate ?? DateTime.Today,
                Notes = PaymentNotesBox.Text
            };

            viewModel.CurrentBooking.Payments.Add(payment);

            // Clear the entry fields
            PaymentAmountBox.Text = "";
            PaymentDatePicker.SelectedDate = null;
            PaymentNotesBox.Text = "";

            RecalculateTotal();
        }

        private void AddPaymentFromClientView_Click(object sender, RoutedEventArgs e)
        {
            ShowPaymentState();
        }
    }
}