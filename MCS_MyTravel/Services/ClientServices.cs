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

        public async Task RemoveClientAsync(int id)
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
            ValidateClient(client); // validira toj so go PULLNAVME OD FRONT DALI E OKEj

            //if all good
            var existingClient = await _clientRepo.GetByIdAsync(client.Id);

            if (existingClient == null)
            {
                throw new KeyNotFoundException("non-existant");
            }

            //else
            bool passportExists = await _clientRepo.ExistsByPassportIdAsync(client.PassportId, client.Id);

            if (passportExists)
            {
                throw new InvalidOperationException("already exists");
            }

            existingClient.FullName = client.FullName.Trim();
            existingClient.PassportId = client.PassportId.Trim();
            existingClient.BirthDate = client.BirthDate;
            //klavame prasalnici vo slucaj da bida ostaeni prazni pohsot ne se rrequired
            existingClient.Phone = client.Phone?.Trim();
            existingClient.Notes = client.Notes?.Trim();

            await _clientRepo.UpdateClientAsync(existingClient);
            return existingClient;
        }

        private static string? ValidateClient(Client client)
        {
            if (string.IsNullOrWhiteSpace(client.FullName))
                return "Full name is required.";

            if (string.IsNullOrWhiteSpace(client.PassportId))
                return "Passport ID is required.";

            return null;
        }
    }
}
