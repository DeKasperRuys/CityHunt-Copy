using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Ability3Controller : ControllerBase
    {
        private IAbilityRepository _abilityRepository;
        public Ability3Controller(IAbilityRepository abilityRepository)
        {
            _abilityRepository = abilityRepository;
        }

        // GET: api/Ability3
        [HttpGet]
        public List<Ability> GetAllAbilities()
        {
            return _abilityRepository.GetAllAbilities();
        }

        // POST: api/Ability3
        [HttpPost]
        public ActionResult<Ability> PostAbility([FromBody] Ability ability)
        {
            return _abilityRepository.PostAbility(ability);
        }

        // delete ability?

    }
}
