using System;
using System.Collections.Generic;
using Beyond.Data;
using Beyond.Models.Destination;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;

namespace Beyond.Controllers
{
    public class DestinationsController : Controller
    {
        private readonly ITakeModels _takeViewModels;
        private readonly IMemoryCache _memoryCache;
        public DestinationsController(ITakeModels takeViewModels, IMemoryCache memoryCache)
        {
            _takeViewModels = takeViewModels;
            _memoryCache = memoryCache;
        }
        [Authorize]
        [Route("/Destinations")]
        public IActionResult Destinations()
        {
            const string destinationsCache = "DestinationCache";
            var destinations = this._memoryCache.Get<List<DestinationViewModel>>(destinationsCache);
            if (destinations==null)
            {
                destinations = _takeViewModels.DestinationsOrNull();
                var cacheOptions = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(TimeSpan.FromMinutes(15));

                this._memoryCache.Set(destinationsCache, destinations,cacheOptions);
            }
            
            switch (destinations)
            {
                case null:
                    ViewData["Message"] = "There are no destinations.";
                    return View("Error");
                default:
                    return View(destinations);
            }
        }
    }
}
