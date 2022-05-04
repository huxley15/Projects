using System.Collections.Generic;

namespace WoodPlanning.Models
{
    //step 3.1
    public class ClientRepository : IClientRepository
    {
        public List<Client> Clients { get; set; }

        public void AddClient(Client client)
        {
            Clients.Add(client);
        }

        public void DeleteClient(string id)
        {
            var client = Clients.Find(x => x.Id == id);
            Clients.Remove(client);
        }

        public Client GetClientById(string id)
        {
            if (id == null)
            {
                return null;
            }
            else
            {
                return Clients.Find(x=>x.Id == id);
            }
        }

        public IEnumerable<Client> InitializeData()
        {
            return Clients;
        }

        public void UpdateClient(Client client)
        {
            var DBclient = Clients.Find(x=> x.Id == client.Id);
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
        }
    }
}
