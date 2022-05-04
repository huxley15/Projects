using System.Collections.Generic;

namespace WoodPlanning.Models
{
    //step 5.2
    public class DBClientRepository : IClientRepository
    {
        public List<Client> Clients { get; set; }

        private ClientContext _clientContext;
        public DBClientRepository(ClientContext clientContext)
        {
            _clientContext = clientContext;
        }
        public void AddClient(Client client)
        {
            _clientContext.Clients.Add(client);
            _clientContext.SaveChanges();
        }

        public void DeleteClient(string id)
        {
            var client = _clientContext.Clients.Find(id);
            if (client != null)
            {
                _clientContext.Clients.Remove(client);
                _clientContext.SaveChanges();
            }
        }

        public Client GetClientById(string id)
        {
            return _clientContext.Clients.Find(id);
        }

        public IEnumerable<Client> InitializeData()
        {
            return _clientContext.Clients;
        }

        public void UpdateClient(Client client)
        {
            var DBclient = _clientContext.Clients.Find(client.Id);
            if (DBclient != null)
            {
                DBclient.Id = client.Id;
                DBclient.FirstName = client.FirstName;
                DBclient.LastName = client.LastName;
                DBclient.Email = client.Email;
                DBclient.PhoneNumber = client.PhoneNumber;
                DBclient.StreetAddress = client.StreetAddress;
                DBclient.City = client.City;
                DBclient.State = client.State;
                DBclient.PostalCode = client.PostalCode;
                DBclient.EmploymentStatus = client.EmploymentStatus;
                DBclient.MaritalStatus = client.MaritalStatus;
                DBclient.Dependents = client.Dependents;
                DBclient.Services = client.Services;
                DBclient.Comments = client.Comments;
            }
            _clientContext.SaveChanges();
        }
    }
}
