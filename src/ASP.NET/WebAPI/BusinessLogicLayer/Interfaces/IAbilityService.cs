using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IAbilityService
    {
        List<Ability> GetAllAbilities();
        Ability PostAbility(Ability ability);
    }
}
