using BCrypt.Net;
using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.Security.Cryptography.X509Certificates;

namespace DataAccessLayer.Implementations
{
    class UsersRepository : IUsersRepository
    {
        private static AppDBContext _context;
        public UsersRepository(AppDBContext context)
        {
            _context = context;
        }

        
        public bool EmailOrUsernameExists(string email, string username)
        {
            var userEmailToCheck = _context.Users.Any(x => x.Email == email);
            var usernameToCheck = _context.Users.Any(x => x.Username == username);

            if (userEmailToCheck) return true;
            if (usernameToCheck) return true;
            else return false;
        }

        public Users GetUserByEmail(string email)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);

            if (user == null) return null;
            else return user;
        }

        public Users AuthorizeUserLogin(string email, string password)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            // Decrypt bij aanmelden het gehashte paswoord van de user en check of dit overeenkomt.
            bool isValidPasswd = BCrypt.Net.BCrypt.Verify(password, user.Passwoord);

            if (isValidPasswd) return user;

            return null;
        }

        public void DeleteUserHighscore(int id)
        {
            var userHighscoreToDelete = _context.Highscore.SingleOrDefault(x => x.ScoreId == id);

            _context.Highscore.Remove(userHighscoreToDelete);
            _context.SaveChanges();
        }

        public Users DeleteUser(Users user)
        {
            var userExists = _context.Users.SingleOrDefault(x => x.Email == user.Email);

            if (userExists == null) return null;

            int scoreId = userExists.ScoreId;   // verwijder ook de highscore van de user als een user verwijderd wordt.

            DeleteUserHighscore(scoreId);       

            _context.Users.Remove(userExists);
            _context.SaveChanges();

            return userExists;
        }

        public List<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public IQueryable<Users> GetUserByEmailAddress(string email)
        {
            IQueryable<Users> theUser = _context.Users;

            try
            {
                theUser = theUser.Where(u => u.Email == email);

                return theUser;
            }
            catch (Exception)
            {
                throw;
            }
            
        }

        public Users AddUser(Users user, Highscore highscore)
        {
            _context.Highscore.Add(highscore);      // maak eerst highscores aan voor nieuwe gebruiker
            _context.SaveChanges();

            user.ScoreId = highscore.ScoreId;       // link de scoreID van de gebruiker met de highscoreID
            
            _context.Users.Add(user);               // voeg tot slot de gebruiker met highscores toe
            _context.SaveChanges();

            return user;
        }

        /*
        // Deze methode is naar de BLL gedelegeerd omdat er daar validatie op de user velden gebeurd.
        public Users SignUpEncryptedUser(Users user)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);  // controlleer de velden op de juiste inhoud, zie models

            //if (_context.Users.Any(u => u.Email == user.Email)) return null; //Controlleer of de user(name) al bestaat
            
            //_context.Users.Add(user);
            //user.Passwoord = BCrypt.Net.BCrypt.HashPassword(user.Passwoord);
            
            //_context.SaveChanges();


            // alle functionaliteit van deze methode in de laag is weg-gedelegeerd?

            return user;
        }
        */

        public Users UpdateGPSLocForUser(Users user)
        {
            var userExists = _context.Users.SingleOrDefault(x => x.Email == user.Email);

            //UserExists(user.Email); // kan ook zo...

            if (userExists == null) return null;

            userExists.Lat = user.Lat;        //Update enkel lat/long velden
            userExists.Long = user.Long;

            _context.Users.Update(userExists);
            _context.SaveChanges();

            return userExists;
        }

        public Users UpdateUserByEmail(Users user)
        {
            var userExists = _context.Users.SingleOrDefault(x => x.Email == user.Email);

            if (userExists == null) return null;

            userExists.Naam = user.Naam;
            userExists.Achternaam = user.Achternaam;
            userExists.Email = user.Email;
            userExists.Passwoord = BCrypt.Net.BCrypt.HashPassword(user.Passwoord);
            userExists.Username = user.Username;
            userExists.TeamId = user.TeamId;              // Team id moet worden meegegeven ( maak groepid 99999 en teamnaam 'geen team' )

            _context.Users.Update(userExists);
            _context.SaveChanges();

            return userExists;
        }

       
    }
}
