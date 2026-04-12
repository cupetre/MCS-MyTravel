using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;

namespace MCS_MyTravel.Repositories
{
    public interface IClientRepo
    {
        Task<List<Client>> GetAllClientsAsync();
        Task<Client?> GetByIdAsync(int id);
        Task AddClientAsync (Client client);
        Task DeleteClientAsync (Client client);
        Task UpdateClientAsync (Client client);
        Task<bool> ExistsByPassportIdAsync(string passportId, int? excludeClientId = null);
    }
}
