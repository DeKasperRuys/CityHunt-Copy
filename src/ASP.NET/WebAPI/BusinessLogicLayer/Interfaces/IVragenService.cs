using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IVragenService
    {
        List<Vragen> GetAlleVragen();
        Vragen PostVraag(Vragen vraag);
    }
}
