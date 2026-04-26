using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Data;
using MCS_MyTravel.Models;
using Microsoft.EntityFrameworkCore;

namespace MCS_MyTravel.Repositories
{
    public class ClientRepo : IClientRepo
    {
        private readonly AppDbContext _context;

        public ClientRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteClientAsync(Client client)
        {
            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Client>> GetAllClientsAsync()
        {
            return await _context.Clients
                .OrderBy(c => c.Id)
                .ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExistsByPassportIdAsync(string passportId, int? excludeClientId = null)
        {
            var query = _context.Clients
                .Where(c => c.PassportId == passportId);
            
            if ( excludeClientId != null)
            {
                query = query.Where(c => c.Id != excludeClientId.Value);
            }
            return await query.AnyAsync();
        }
    }
}
