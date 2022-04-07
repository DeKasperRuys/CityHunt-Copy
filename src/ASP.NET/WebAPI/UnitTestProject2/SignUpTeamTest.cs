using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace UnitTestProject2
{
    [TestClass]
    public class SignUpTeamTest
    {
        BusinessLogicLayer.Implementations.TeamsService teamService;
        DataAccessLayer.Interfaces.ITeamsRepository team;
        [TestInitialize]
        public void ArrangeTests()
        {
            team = new Mock<ITeamsRepository>().Object;
            teamService = new BusinessLogicLayer.Implementations.TeamsService(team);
        }

        [DataTestMethod]
        [DataRow("testing_team")]
        public void TestCorrecteTeamToegevoegd(string naam)
        {
            Team teamMock = new Mock<Team>().Object;

            teamMock.TeamNaam = naam;
            teamMock.Users = null;
            teamMock.Vragen = null;
                
            teamService.PostTeam(teamMock);

            Assert.AreEqual(teamMock.TeamNaam, naam);
        }

        [DataTestMethod]
        [DataRow("testing_team_ffffffffffffffffffffffff")]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TestOfTeamNaamNietTeLangIs(string naam)
        {
            Team teamMock = new Mock<Team>().Object;

            teamMock.TeamNaam = naam;

            teamService.PostTeam(teamMock);
        }



    }
}
