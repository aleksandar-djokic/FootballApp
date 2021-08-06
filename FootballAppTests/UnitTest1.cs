using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Moq;
using FootballApp.Domain.Abstract;
using FootballApp.Domain.Models;
using System.Collections.Generic;
using FootballApp.WebUI.Controllers;
using FootballApp.WebUI.Models;
using System.Web.Mvc;

namespace FootballAppTests
{
    [TestClass]
    public class UnitTest1
    {
       
        [TestMethod]
        public void Team_Can_Change_Name()
        {
            Mock<ITeamRepository> mock = new Mock<ITeamRepository>();
            var imeTima = "Proba";
            TeamController tc = new TeamController(mock.Object);

            var timZaIzmenu = new TeamViewModel()
            {
                Id = 1,
                TeamName = "ImeTima",
                Description = "OpisTima",
                Picture = null
                

            };
            mock.Setup(x => x.isNameTaken(It.IsAny<string>(),It.IsAny<int>())).Returns(false);
            mock.Setup(x => x.Edit(timZaIzmenu.Id, timZaIzmenu.TeamName, timZaIzmenu.Description, null)).Callback((() => { timZaIzmenu.TeamName = imeTima; })) ;
            tc.Edit(timZaIzmenu);
           
            Assert.AreEqual(imeTima,timZaIzmenu.TeamName);
        }
    }
}
