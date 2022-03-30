using System.Linq;
using Beyond.Data;
using Beyond.Models.Crew;
using Beyond.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Beyond.Controllers
{
    public class CrewController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEnumNames _enumNames;

        public CrewController(ApplicationDbContext context, IEnumNames enumNames)
        {
            _context = context;
            _enumNames = enumNames;
        }

        [Route("/Crew")]
        [Authorize]
        public IActionResult Crew()
        {
            var crew = _context
                .Pilots
                .Select(x => new CrewViewModel()
                {
                    Description = x.Description,
                    Name = x.Name,
                    Rank = _enumNames.UiRankDecorator(x.Rank.ToString()),
                    Url = x.ImgPath
                }).ToList();
            return View(crew);
        }
    }
}
