using MCS_MyTravel.Models;
using MCS_MyTravel.Services;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MCS_MyTravel.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IClientServices _clientServices;
        private readonly IBookingServices _bookingServices;
        private readonly IPaymentServices _paymentServices;

         // ------------------
        public Booking _currentBooking { get; set; } = new();
        public ObservableCollection<Booking> Bookings { get; set; } = new();

        // -------------------
        public ObservableCollection<Client> Clients { get; set; } = new();
        public Client _currentClient { get; set; } = new();

        // -------------------
        public ObservableCollection<Payment> Payments { get; set; } = new();
        public Payment _currentPayment { get; set; } = new();

        // --------------------

        public event PropertyChangedEventHandler? PropertyChanged;

        // -------------------

        private bool _isLoading;
        private string _errorMessage = string.Empty;

        public Client CurrentClient
        {
            get => _currentClient;
            set
            {
                _currentClient = value;
                OnPropertyChanged();
            }
        }

        public Booking CurrentBooking
        {
            get => _currentBooking;
            set 
            { 
                _currentBooking = value; 
                OnPropertyChanged(); 
            }
        }

        public Payment CurrentPayment
        {
            get => _currentPayment;
            set
            {
                _currentPayment = value;
                OnPropertyChanged();
            }
        }

        private decimal _paymentTotal;
        public decimal PaymentTotal
        {
            get => _paymentTotal;
            set
            {
                _paymentTotal = value;
                OnPropertyChanged();
            }
        }

        private decimal _paidSoFar;
        public decimal PaidSoFar
        {
            get => _paidSoFar;
            set
            {
                _paidSoFar = value;
                OnPropertyChanged();
            }
        }

        private decimal _remainingAmount;
        public decimal RemainingAmount
        {
            get => _remainingAmount;
            set
            {
                _remainingAmount = value;
                OnPropertyChanged();
            }
        }

        private decimal _paidPercentage;
        public decimal PaidPercentage
        {
            get => _paidPercentage;
            set
            {
                _paidPercentage = value;
                OnPropertyChanged();
            }
        }

        private string _paymentStatus = "Unpaid";
        public string PaymentStatus
        {
            get => _paymentStatus;
            set
            {
                _paymentStatus = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set { _isLoading = value; OnPropertyChanged(); }
        }
        public string ErrorMessage
        {
            get => _errorMessage;
            set { _errorMessage = value; OnPropertyChanged(); }
        }

        public MainViewModel(
            IClientServices clientServices,
            IBookingServices bookingServices,
            IPaymentServices paymentServices
            )
        {
            _clientServices = clientServices;
            _bookingServices = bookingServices;
            _paymentServices = paymentServices;

            CurrentClient = new Client();

            CurrentBooking = new Booking
            {
                Passengers = new ObservableCollection<Passenger>(),
                Payments = new ObservableCollection<Payment>()
            };

            CurrentPayment = new Payment();
        }
        // ── Client methods ───────────────────────────────────────────
        public async Task LoadClientsAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var clients = await _clientServices.GetAllClientsAsync();
                Clients.Clear();
                foreach (var client in clients)
                    Clients.Add(client);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load clients: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task SaveClientAsync()
        {
            if ( CurrentClient == null )
            {
                    throw new Exception("CurrentClient is not well selected ");
            }

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;
                MessageBox.Show($"CurrentClient ID = {CurrentClient.Id}, Name = {CurrentClient.FullName}");

                if (CurrentClient.Id == 0)
                    await _clientServices.CreateClientAsync(CurrentClient);
                else
                    throw new Exception("CurrentClient is not well selected, it has ID != 0, it thinks its an existing one ");

                await LoadClientsAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to save client: {ex.Message}";
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }
        public async Task UpdateClientAsync()
        {
            if (CurrentClient == null)
            {
                throw new Exception("CurrentClient is not well selected ");
            }

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;
                MessageBox.Show($"CurrentClient ID = {CurrentClient.Id}, Name = {CurrentClient.FullName}");
                
                if (CurrentClient.Id == 0)
                    throw new Exception("CurrentClient is not well selected, it has ID = 0, it thinks its a new one ");
                else
                    await _clientServices.UpdateClientAsync(CurrentClient);
                
                //update listing
                await LoadClientsAsync();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to update client: {ex.Message}";
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task DeleteClientAsync()
        {
            if (CurrentClient.Id <= 0)
                return;

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                await _clientServices.RemoveClientAsync(CurrentClient.Id);
                await LoadClientsAsync();
                CurrentClient = new Client();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to delete client: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        // ── Booking methods ──────────────────────────────────────────
        public async Task LoadBookingsForClientAsync()
        {
            if (CurrentClient.Id <= 0)
                return;

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                var bookings = await _bookingServices.GetBookingsByClientIdAsync(CurrentClient.Id);
                Bookings.Clear();
                foreach (var booking in bookings)
                    Bookings.Add(booking);
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to load bookings: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task SaveBookingAsync()
        {
            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                System.Diagnostics.Debug.WriteLine($"Client ID: {CurrentClient?.Id}, Name: {CurrentClient?.FullName}");
                CurrentBooking.FinalTotalPrice = CurrentBooking.TotalPrice;

                System.Diagnostics.Debug.WriteLine($" FinalTotalPrice is : {CurrentBooking.FinalTotalPrice}");

                if (CurrentBooking.Id == 0)
                    await _bookingServices.CreateBookingAsync(CurrentBooking);
                else
                    await _bookingServices.UpdateBookingAsync(CurrentBooking);

                await LoadBookingsForClientAsync();
                await LoadPaymentsForBookingAsync();

            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to save booking: {ex.ToString}";
                throw;
            }
            finally
            {
                IsLoading = false;
            }
        }

        public async Task DeleteBookingAsync()
        {
            if (CurrentBooking.Id <= 0)
                return;

            try
            {
                IsLoading = true;
                ErrorMessage = string.Empty;

                await _bookingServices.DeleteBookingAsync(CurrentBooking.Id);
                await LoadBookingsForClientAsync();

                CurrentBooking = new Booking
                {
                    Passengers = new ObservableCollection<Passenger>(),
                    Payments = new ObservableCollection<Payment>()
                };
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Failed to delete booking: {ex.Message}";
            }
            finally
            {
                IsLoading = false;
            }
        }

        // -- Payments Methods ---------------------------------------
        public async Task LoadPaymentsForBookingAsync()
        {
            if (CurrentBooking.Id <= 0)
                return;

            var payments = await _paymentServices.GetPaymentsByBookingIdAsync(CurrentBooking.Id);

            Payments.Clear();
            foreach (var payment in payments)
                Payments.Add(payment);
        }

        public async Task LoadPaymentsForSelectedClientAsync()
        {
            if (CurrentClient == null || CurrentClient.Id <= 0)
                throw new KeyNotFoundException("No selected client");

            var bookings = await _bookingServices.GetBookingsByClientIdAsync(CurrentClient.Id);

           CurrentBooking = bookings.FirstOrDefault();

            if (CurrentBooking == null)
            {
                Payments.Clear();
                Debug.WriteLine($"Client: {CurrentClient.FullName} has no booking.");
                RecalculatePaymentSummary();
                return;
            }

            Debug.WriteLine($"CurrentClient: {CurrentClient.FullName}");
            Debug.WriteLine($"BookingId: {CurrentBooking.Id}");
            Debug.WriteLine($"Bookings count: {bookings.Count}");

            var payments = await _paymentServices.GetPaymentsByBookingIdAsync(CurrentBooking.Id);

            Payments.Clear();

            foreach (var payment in payments)
            {
                Payments.Add(payment);
                Debug.WriteLine($"PaymentId: {payment.Id}, Amount: {payment.Amount}, Date: {payment.PaymentDate}");
            }

            RecalculatePaymentSummary();
        }

        public async Task SavePaymentAsync()
        {
            if (CurrentBooking == null || CurrentBooking.Id <= 0)
                throw new KeyNotFoundException("Current booking was not found");

            CurrentPayment.BookingId = CurrentBooking.Id;

            if (CurrentPayment.Id == 0)
                await _paymentServices.CreatePaymentAsync(CurrentPayment);
            else
                await _paymentServices.UpdatePaymentAsync(CurrentPayment);

            await LoadPaymentsForBookingAsync();

            CurrentPayment = new Payment();
        }

        public void RecalculatePaymentSummary()
        {
            decimal total = CurrentBooking?.FinalTotalPrice ?? 0;
            decimal paid = Payments.Sum(p => p.Amount);

            decimal remaining = total - paid;
            decimal percent = total > 0 ? paid / total * 100 : 0;

            if (remaining < 0)
                remaining = 0;

            if (percent > 100)
                percent = 100;

            PaymentTotal = total;
            PaidSoFar = paid;
            RemainingAmount = remaining;
            PaidPercentage = percent;

            if (total <= 0)
                PaymentStatus = "No price set";
            else if (paid >= total)
                PaymentStatus = "Fully paid";
            else if (paid > 0)
                PaymentStatus = "Partially paid";
            else
                PaymentStatus = "Unpaid";
        }

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
