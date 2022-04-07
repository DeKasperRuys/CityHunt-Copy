using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Web.Mvc;

namespace DataAccessLayer.Implementations
{
    class VragenRepository : IVragenRepository
    {
        AppDBContext _context;

        public VragenRepository(AppDBContext context)
        {
            _context = context;
        }
        public List<Vragen> GetAlleVragen()
        {
            return _context.Vragen.ToList();
        }

        public Vragen PostVraag(Vragen vraag)
        {

            _context.Vragen.Add(vraag);
            _context.SaveChanges();

            return vraag;
        }
    }
}
