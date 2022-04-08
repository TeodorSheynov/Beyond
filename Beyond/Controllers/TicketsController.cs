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
        private readonly ITakeViewModels _takeViewModels;
        private readonly ITakeEntityById _takeEntityById;
        private readonly ICreateAndSaveEntity _createAndSaveEntity;
        private readonly IDeleteAndSaveEntity _deleteAndSaveEntity;

        public TicketsController(ITakeViewModels takeViewModels, 
            ITakeEntityById takeEntityById, 
            ICreateAndSaveEntity createAndSaveEntity,
            IDeleteAndSaveEntity deleteAndSaveEntity)
        {
            _takeViewModels = takeViewModels;
            _takeEntityById = takeEntityById;
            _createAndSaveEntity = createAndSaveEntity;
            _deleteAndSaveEntity = deleteAndSaveEntity;
        }

        [Authorize]
        public IActionResult All()
        {
            var tickets = _takeViewModels.TicketsOrNull();
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
            var tickets= _takeViewModels.TicketsOrNull();
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
               
                var myTickets = _takeViewModels.MyTicketOrNull();
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
                _createAndSaveEntity.Ticket(user,vehicle);
                ViewData["id"] = id;
                return RedirectToAction("MyTickets");
            }
            catch (Exception)
            {
                ViewData["Message"] = "Something went wrong.";
                return View("Error");
            }
        }
        public IActionResult Delete(string id)
        {
            try
            { 
                _deleteAndSaveEntity.Ticket(id);
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
