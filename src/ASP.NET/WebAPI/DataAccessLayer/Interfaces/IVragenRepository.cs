using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IVragenRepository
    {
        List<Vragen> GetAlleVragen();
        Vragen PostVraag(Vragen vraag);
    }
}
