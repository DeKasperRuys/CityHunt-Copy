using Domain;
using System.Collections.Generic;
using System.Linq;
using WebApplication2.Context;
using DataAccessLayer;

namespace WebApplication2.Models
{
    public class DBInitialiser
    {
        public static void Initialize(DBFirstContext context)
        {
            context.Database.EnsureCreated(); //maak db als deze nog niet bestaat

            Vragen v = new Vragen()
            {
                Title = "In welk jaar is het centraal station gebouwd?",
                Antwoord1 = "1800",
                JuisteAntwoord = "1899",
                Lat = "51.2171918",
                Long = "4.4212529",
                TeamId = 1
               // UserId = 33
            };

            Vragen v2 = new Vragen()
            {
                Title = "In welk jaar is het stadhuis gebouwd?",
                Antwoord1 = "1800",
                JuisteAntwoord = "1899",
                Lat = "51.2171918",
                Long = "4.4212529",
                TeamId = 1
                //UserId = 1,
            };

            Users t = new Users()
            {
                //UserId = 1,
                Naam = "Maxim",
                Achternaam = "Wauters",
                Email = "wauters.maxim@hotmail.com",
                Username = "Molimoes",
                Passwoord = "gg",
                isGedetineerde = false,
                TeamId = 1,
                ScoreId = 1
                //Team = g,
                // Vragen = new List<Vragen>() { v, v2 }
            };

            Users t2 = new Users()
            {
                //UserId = 1,
                Naam = "Maximmo",
                Achternaam = "Wauters",
                Email = "wauters.maxim123@gmail.com",
                Username = "binkey450",
                Passwoord = "ggabrams",
                isGedetineerde = false,
                TeamId = 2,
                
                //Team = g,
                //Vragen = new List<Vragen>() { v, v2 }
            };

            Ability a = new Ability()
            {
                AbilityType = 1,
                Latitude = "test",
                Longitude = "test",
                isUsed = false,
                TeamId = 1
            };

            Team g = new Team()
            {
                TeamNaam = "De winners",
                Users = new List<Users>() { t, /*t2*/ },
                Vragen = new List<Vragen>() { v, v2 },
                Ability = new List<Ability>() { a }

            };

            Highscore h = new Highscore()
            {
                TotalPoints = 66,
                CurrentPoints = 66
            };

            

            if (!context.Users.Any())
            {
                context.Vragen.Add(v);
                context.Vragen.Add(v2);
                context.Team.Add(g);
                context.Users.Add(t);
                //context.Users.Add(t2);
                context.Highscore.Add(h);
                context.Ability.Add(a);

                context.SaveChanges();
            }
        }

    }
}