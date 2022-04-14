using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using Beyond.Data;
using Beyond.Models;
using Beyond.Data.Models;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


namespace Beyond.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ITakeModels _takeModels;
        private readonly ITakeEntityById _takeEntityById;
        private readonly ICreateEntity _createEntity;
        private readonly IDeleteEntity _deleteEntity;

        public TicketsController(ITakeModels takeModels, 
            ITakeEntityById takeEntityById, 
            ICreateEntity createEntity,
            IDeleteEntity deleteEntity)
        {
            _takeModels = takeModels;
            _takeEntityById = takeEntityById;
            _createEntity = createEntity;
            _deleteEntity = deleteEntity;
        }

        [Authorize]
        public IActionResult All()
        {
            var tickets = _takeModels.TicketsOrNull();
            switch (tickets)
            {
                case null:
                    ViewData["Message"] = "There are no tickets.";
                    return View("Error");
                default:
                    return View(tickets);
            }
        }
        [Authorize]
        public IActionResult Search(string name)
        {
            var tickets= _takeModels.TicketsOrNull();
            switch (tickets)
            {
                case null:
                    ViewData["Message"] = "There are no tickets.";
                    return View("Error");
                default:
                {
                    var searchedTickets = tickets.Where(d => d.Name == name);
                    return View("All",searchedTickets);
                }
            }
        }

        [Authorize]
        public IActionResult MyTickets()
        {
           
            try
            {
               
                var myTickets = _takeModels.MyTicketsOrNull();
                switch (myTickets)
                {
                    case null:
                        ViewData["Message"] = "You haven't bought ticket yet.";
                        return View("Error");
                    default:
                        return View(myTickets);
                }
            }
            catch (Exception)
            {
                ViewData["Message"] = "Something went wrong.";
                return View("Error");
            }
        }

        [Authorize]
        public IActionResult Buy(string id)
        {
            var userId= User.FindFirstValue(ClaimTypes.NameIdentifier);
            try
            {
                var user = _takeEntityById.User(userId);
                var vehicle = _takeEntityById.Vehicle(id);
                _createEntity.Ticket(user,vehicle);
                ViewData["id"] = id;
                return RedirectToAction("MyTickets");
            }
            catch (Exception)
            {
                ViewData["Message"] = "Something went wrong.";
                return View("Error");
            }
        }
        [Authorize]
        public IActionResult Delete(string id)
        {
            try
            { 
                _deleteEntity.Ticket(id);
                return RedirectToAction("MyTickets");
            }
            catch (Exception)
            {
                ViewData["Message"] = "Something went wrong while deleting the record.";
                return View("Error");
            }
        }
    }
}
