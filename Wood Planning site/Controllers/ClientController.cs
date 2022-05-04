using Microsoft.AspNetCore.Mvc;
using WoodPlanning.Models;
//step 33.6
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WoodPlanning.Controllers
{
    //step 5.1
    public class ClientController : Controller
    {
        private IClientRepository _clientRepository;
        private UserManager<Client> userManager;
        public ClientController(IClientRepository clientRepository,UserManager<Client> _userManager)
        {
            //dependency injection
            _clientRepository = clientRepository;
            userManager = _userManager;
        }
        public IActionResult Details(string id)
        {
            var model = _clientRepository.GetClientById(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
        
        public IActionResult Index()
        {
            if (User.Identity.Name == "Admin")
            {
                IndexViewModel model = new IndexViewModel();
                model.Clients = _clientRepository.InitializeData();
                return View(model);
            }
            var userid = userManager.GetUserId(User);
            var client = _clientRepository.GetClientById(userid);
            return View("indClient",client);
        }
        //step 5.3
        public IActionResult Create(Client client)
        {
            //validation attributes
            if (ModelState.IsValid)
            {
                _clientRepository.AddClient(client);
            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Update(string id)
        {
            Client client = _clientRepository.GetClientById(id);
            return View(client);
        }

        [HttpPost]
        public IActionResult Update(Client obj, string id)
        {
            obj.Id = id;
            _clientRepository.UpdateClient(obj);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(string id)
        {
            _clientRepository.DeleteClient(id);
            return RedirectToAction("Index");
        }
    }
}
