using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

namespace Beyond.Controllers
{
    public class DestinationsController : Controller
    {
        public IActionResult Ticket()
        {
            return View();
        }
        //public IActionResult Ticket(string id)
        //{
        //    return Ok(id);
        //}

        [Authorize]
        [Route("/Destinations")]
        public IActionResult Destinations() => View();
    }
}
