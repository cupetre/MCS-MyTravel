using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;
using MCS_MyTravel.Repositories;

namespace MCS_MyTravel.Services
{
    public class ClientServices : IClientServices
    {
        private readonly IClientRepo _clientRepo;

        public ClientServices(IClientRepo clientRepo)
        {
            _clientRepo = clientRepo;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            ValidateClient(client);

            bool passportExists = await _clientRepo.ExistsByPassportIdAsync(client.PassportId);

            if ( passportExists )
            {
                throw new InvalidOperationException("A client already exists.");
            }

            await _clientRepo.AddClientAsync(client);

            return client;
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _clientRepo.GetAllClientsAsync();
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            return await _clientRepo.GetByIdAsync(id);
        }

        public async Task RemoveClientASync(int id)
        {
            var existingClient = await _clientRepo.GetByIdAsync(id);

            if ( existingClient == null )
            {
                throw new InvalidOperationException("Client not found");
            }

            await _clientRepo.DeleteClientAsync(existingClient);
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            ValidateClient(client);

            //if all good
            var existingClient = await _clientRepo.GetByIdAsync(client.Id);

            if (existingClient == null)
            {
                throw new KeyNotFoundException("non-existant");
            }

            //else
            bool passportExists = await _clientRepo.ExistsByPassportIdAsync(client.PassportId);

            if (passportExists)
            {
                throw new InvalidOperationException("already exists");
            }

            existingClient.FullName = client.FullName.Trim();
            existingClient.PassportId = client.PassportId.Trim();
            existingClient.BirthDate = client.BirthDate;
            existingClient.Phone = client.Phone.Trim();
            existingClient.Notes = client.Notes.Trim();

            await _clientRepo.UpdateClientAsync(existingClient);
            return existingClient;

        }

        private static void ValidateClient(Client client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            if (string.IsNullOrWhiteSpace(client.FullName))
                throw new InvalidOperationException("Full name is required.");

            if (string.IsNullOrWhiteSpace(client.PassportId))
                throw new InvalidOperationException("Passport ID is required.");
        }
    }
}
