using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Implementations
{
    class AbilityRepository : IAbilityRepository
    {
        AppDBContext _context;
        public AbilityRepository(AppDBContext context)
        {
            _context = context;
        }

        public List<Ability> GetAllAbilities()
        {
            return _context.Ability.ToList();
        }

        public Ability PostAbility(Ability ability)
        {
            _context.Ability.Add(ability);
            _context.SaveChanges();

            return ability;
        }
    }
}
