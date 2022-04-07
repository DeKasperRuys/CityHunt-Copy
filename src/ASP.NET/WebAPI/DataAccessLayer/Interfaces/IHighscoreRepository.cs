using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IHighscoreRepository
    {
        public List<Highscore> GetAllHighscores();

        IQueryable<Highscore> GetHighscoreByScoreId(int id);

        public Highscore PostHighscoreByScoreId(Highscore highscore, int id);

    }
}
