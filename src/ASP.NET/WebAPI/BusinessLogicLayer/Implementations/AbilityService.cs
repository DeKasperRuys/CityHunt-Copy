using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementations
{
    public class AbilityService : IAbilityService
    {
        private IAbilityRepository _abilityRepository;
        public AbilityService(IAbilityRepository abilityRepository)
        {
            _abilityRepository = abilityRepository;
        }
        public List<Ability> GetAllAbilities()
        {
            return _abilityRepository.GetAllAbilities();
        }

        public Ability PostAbility(Ability ability)
        {
            if (ability.AbilityType <= 0 || ability.AbilityType >= 3) throw new ArgumentOutOfRangeException("Ability type kan enkel 1 of 2 zijn");

            return _abilityRepository.PostAbility(ability);
        }
    }
}
