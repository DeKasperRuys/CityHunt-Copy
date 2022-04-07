using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Implementations
{
    class HighscoreRepository : IHighscoreRepository
    {
        AppDBContext _context;
        public HighscoreRepository(AppDBContext context)
        {
            _context = context;
        }

        public List<Highscore> GetAllHighscores()
        {
            return _context.Highscore.ToList();
        }

        
        public IQueryable<Highscore> GetHighscoreByScoreId(int id)
        {
            IQueryable<Highscore> myPoints = _context.Highscore;

            myPoints = myPoints.Where(d => d.ScoreId == id);

            return myPoints;
        }
        

        public Highscore PostHighscoreByScoreId(Highscore highscore, int id)
        {
            //IQueryable<Highscore> score = _context.Highscore;

            //var score = _context.Highscore.FirstOrDefault(d => d.ScoreId == id);

            highscore.ScoreId = id;

            //if (highscore.CurrentPoints > score.TotalPoints) highscore.TotalPoints = highscore.CurrentPoints;

            _context.Highscore.Update(highscore);
            _context.SaveChanges();

            //_context.Highscore.Update(hi)
            //_context.SaveChanges();

            return highscore;
        }
    }
}
