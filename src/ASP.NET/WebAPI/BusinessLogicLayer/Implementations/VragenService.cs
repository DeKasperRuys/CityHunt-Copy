using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementations
{
    public class VragenService : IVragenService
    {
        private IVragenRepository _vragenRepository;

        public VragenService(IVragenRepository vragenRepository)
        {
            _vragenRepository = vragenRepository;
        }
        public List<Vragen> GetAlleVragen()
        {
            return _vragenRepository.GetAlleVragen();
        }

        public Vragen PostVraag(Vragen vraag)
        {
            return _vragenRepository.PostVraag(vraag);
        }
    }
}
