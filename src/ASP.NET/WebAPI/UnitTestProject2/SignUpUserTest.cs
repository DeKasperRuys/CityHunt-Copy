using BusinessLogicLayer.Implementations;
using DataAccessLayer.Implementations;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace UnitTestProject2
{
    [TestClass]
    public class SignUpUserTest
    {
        BusinessLogicLayer.Implementations.UsersService userService;
        DataAccessLayer.Interfaces.IUsersRepository users;
        [TestInitialize]
        public void ArrangeTests()
        {
            users = new Mock<IUsersRepository>().Object;
            userService = new UsersService(users);
        }

        [DataTestMethod]
        [DataRow( "van de vijver", "laura", "username_test", "thisisa.test@gmail.be", "54321", "41.291821", "05.28382", true)]
        public void TestNewUserSignUp( string naam, string achternaam, string username, string email, string passwoord, string lat, string lon, bool isgedet)
        {
            // arrange
            Users userMock = new Mock<Users>().Object;
 
            userMock.Achternaam = achternaam;
            userMock.Naam = naam;
            userMock.Email = email;
            userMock.Username = username;
            userMock.Passwoord = passwoord;
            userMock.Lat = lat;
            userMock.Long = lon;
            userMock.isGedetineerde = isgedet;
            // act
            Users result = userService.SignUpEncryptedUser(userMock);
            // assert
            Assert.IsNotNull(result);
        }

        [DataTestMethod]
        [DataRow("van de vijverrrrrrrrrrrrrrrrrrrrr", "laura", "username_test", "thisisa.test@gmail.be", "54321", "41.291821", "05.28382", true)]
        [DataRow("van de vijver", "lauraaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa", "username_test", "thisisa.test@gmail.be", "54321", "41.291821", "05.28382", true)]
        [DataRow("van de vijver", "laura", "username_die_obv_veel_te_lang_is", "thisisa.test@gmail.be", "54321", "41.291821", "05.28382", true)]
        [DataRow("van de vijverr", "laura", "username_test", "thisisa.wrong@adress", "54321", "41.291821", "05.28382", true)]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestCorrectFieldBySignUp(string naam, string achternaam, string username, string email, string passwoord, string lat, string lon, bool isgedet)
        {
            // arrange
            Users userMock = new Mock<Users>().Object;
            // act
            userMock.Achternaam = achternaam;
            userMock.Naam = naam;
            userMock.Email = email;
            userMock.Username = username;
            userMock.Passwoord = passwoord;
            userMock.Lat = lat;
            userMock.Long = lon;
            userMock.isGedetineerde = isgedet;
            // assert
            userService.SignUpEncryptedUser(userMock);
        }
    }

}

