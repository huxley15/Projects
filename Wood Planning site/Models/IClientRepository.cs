using System.Collections.Generic;

namespace WoodPlanning.Models
{
    //step 2.1
    public interface IClientRepository
    {
        List<Client> Clients { get; set; }
        IEnumerable<Client> InitializeData();
        Client GetClientById(string id);

        //step 2.3
        void AddClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(string id);
    }
}
