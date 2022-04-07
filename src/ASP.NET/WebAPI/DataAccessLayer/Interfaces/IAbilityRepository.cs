using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IAbilityRepository
    {
        public List<Ability> GetAllAbilities();
        Ability PostAbility(Ability ability);



    }
}
