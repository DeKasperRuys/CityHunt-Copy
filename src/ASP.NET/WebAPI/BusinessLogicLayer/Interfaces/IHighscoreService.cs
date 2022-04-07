using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IHighscoreService
    {
        public List<Highscore> GetAllHighscores();

        public IQueryable<Highscore> GetHighscoreByScoreId(int id);
        public Highscore PostHighscore(Highscore highscore, int id);
    }
}
