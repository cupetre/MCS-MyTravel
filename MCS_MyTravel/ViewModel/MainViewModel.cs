using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using MCS_MyTravel.Models;
using MCS_MyTravel.Services;

namespace MCS_MyTravel.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IClientServices _clientServices;
        private readonly IBookingServices _bookingServices;

         // ------------------
        public Booking _currentBooking { get; set; } = new();
        public ObservableCollection<Booking> Bookings { get; set; } = new();

        // -------------------
        public ObservableCollection<Client> Clients { get; set; } = new();
        public Client _currentClient { get; set; } = new();

        // -------------------
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
            IBookingServices bookingServices
            )
        {
            _clientServices = clientServices;
            _bookingServices = bookingServices;

            CurrentClient = new Client();

            CurrentBooking = new Booking
            {
                Passengers = new ObservableCollection<Passenger>(),
                Payments = new ObservableCollection<Payment>()
            };
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

                if (CurrentBooking.Id == 0)
                    await _bookingServices.CreateBookingAsync(CurrentBooking);
                else
                    await _bookingServices.UpdateBookingAsync(CurrentBooking);

                await LoadBookingsForClientAsync();

                // Reset to a fresh booking but keep passengers/payments initialized
                CurrentBooking = new Booking
                {
                    Passengers = new ObservableCollection<Passenger>(),
                    Payments = new ObservableCollection<Payment>()
                };
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

        protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    }
}
