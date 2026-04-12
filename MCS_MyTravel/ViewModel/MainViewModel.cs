using System.Collections.ObjectModel;
using MCS_MyTravel.Models;
using MCS_MyTravel.Services;

namespace MCS_MyTravel.ViewModel
{
    public class MainViewModel
    {
        private readonly IClientServices _clientServices;

        public ObservableCollection<Client> Clients { get; set; } = new();
        public Client CurrentClient { get; set; } = new Client();

        public Booking CurrentBooking { get; set; }
        public ObservableCollection<Booking> Bookings { get; set; }

        public MainViewModel(IClientServices clientServices)
        {
            _clientServices = clientServices;

            CurrentClient = new Client();
            CurrentBooking = new Booking();
            Bookings = new ObservableCollection<Booking>();
        }

        public async Task LoadClientsAsync()
        {
            var clients = await _clientServices.GetAllClientsAsync();

            Clients.Clear();

            foreach (var client in clients)
            {
                Clients.Add(client);
            }
        }

        public async Task SaveClientAsync()
        {
            if ( CurrentClient.Id == 0 )
            {
                await _clientServices.CreateClientAsync( CurrentClient );
            } else
            {
                await _clientServices.UpdateClientAsync( CurrentClient );
            }

            await LoadClientsAsync();
            CurrentClient = new Client();
        }
    }
}
