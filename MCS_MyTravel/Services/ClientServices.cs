using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCS_MyTravel.Models;

namespace MCS_Travel.Services
{
    public class ClientServices
    {
    
    private ObservableCollection<Client> clients = new ObservableCollection<Client>();

    public ObservableCollection<Client> GetClients()
    {
        return clients;
    }

        public void AddClient(Client client)
    {
        clients.Add(client);
    }

    public void UpdateClient(Client client, string fullName, string passportId, DateTime birthDate, string phone, string notes)
    {
        client.FullName = fullName;
        client.PassportId = passportId;
        client.BirthDate = birthDate;
        client.Phone = phone;
        client.Notes = notes;
    }
    }
}
