using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Implementations
{
    public class HighscoreService : IHighscoreService
    {
        private IHighscoreRepository _highscoreRepository;
        public HighscoreService(IHighscoreRepository highscoreRepository)
        {
            _highscoreRepository = highscoreRepository;
        }
        public List<Highscore> GetAllHighscores()
        {
            return _highscoreRepository.GetAllHighscores();
        }

        public IQueryable<Highscore> GetHighscoreByScoreId(int id)
        {
            return _highscoreRepository.GetHighscoreByScoreId(id);
        }

        public Highscore PostHighscore(Highscore highscore, int id)
        {
            if (highscore.CurrentPoints < 0 || highscore.TotalPoints < 0) throw new ArgumentOutOfRangeException("Points kunnen niet lager dan 0 zijn.");

            return _highscoreRepository.PostHighscoreByScoreId(highscore, id);
        }

    }
}
