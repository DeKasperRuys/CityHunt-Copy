using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Vragen3Controller : Controller
    {
        private IVragenService _vragenService;

        public Vragen3Controller(IVragenService vragenService)
        {
            _vragenService = vragenService;
        }

        [HttpGet]
        public List<Vragen> GetAllVragen()
        {
            return _vragenService.GetAlleVragen();
        }

        [HttpPost]
        public ActionResult<Vragen> AddVraag([FromBody] Vragen vraag)
        {
            _vragenService.PostVraag(vraag);

            return Created("", vraag);
        }

        // Delete vraag?
    }
}