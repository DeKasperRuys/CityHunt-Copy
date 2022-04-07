using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BusinessLogicLayer.Implementations
{
    public class UsersService : IUsersService
    {
        private IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Users AddUser(Users user, Highscore highscore)
        {
            return _usersRepository.AddUser(user, highscore);
        }

        public Users AuthorizeUserLogin(string email, string password)
        {
            try
            {
                if (_usersRepository.GetUserByEmailAddress(email) == null) throw new ArgumentNullException("Email adres is niet gekend");
                
                return _usersRepository.AuthorizeUserLogin(email, password);  // check ook pass nog
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Users DeleteUser(Users user)
        {
            return _usersRepository.DeleteUser(user);
        }

        public List<Users> GetAllUsers()
        {
            return  _usersRepository.GetAllUsers();
        }

        public IQueryable<Users>GetUserByEmailAddress(string email)
        {
            return _usersRepository.GetUserByEmailAddress(email);
        }

        public Users SignUpEncryptedUser(Users user)
        {
            if (user.Naam.Length > 15) throw new ArgumentOutOfRangeException("Naam is te lang.");
            if (user.Achternaam.Length > 15) throw new ArgumentOutOfRangeException("Achternaam is te lang.");
            if (user.Username.Length > 15) throw new ArgumentOutOfRangeException("Username is te lang.");

            Regex isValidEmail = new Regex(@"\b[\w\.-]+@[\w\.-]+\.\w{2,4}\b");

            if (isValidEmail.IsMatch(user.Email) == false) throw new ArgumentOutOfRangeException("Foutive email.");

            //check of er niet reeds een user bestaat met zelfde email adress
            //repo.GetUserByEmail (user.email)
            //indien user bestaat dan exceptie
            if (_usersRepository.EmailOrUsernameExists(user.Email, user.Username) != true)
            {
                //Wachtwoord encrypteren
                user.Passwoord = BCrypt.Net.BCrypt.HashPassword(user.Passwoord);
                // highscore aanmaken voor user
                Highscore h = new Highscore();

                h.CurrentPoints = 0;
                h.TotalPoints = 0;

                //User bewaren in db
                _usersRepository.AddUser(user, h);
                //result = repo.AddUser(user)
                return user;
                //return result
            }
            else throw new ArgumentException("Gebruiker bestaat al!");
        }

        public Users UpdateGPSLocForUser(Users user)
        {
            return _usersRepository.UpdateGPSLocForUser(user);
        }

        public Users UpdateUserByEmail(Users user)
        {
            return _usersRepository.UpdateUserByEmail(user);
        }
    }
}
