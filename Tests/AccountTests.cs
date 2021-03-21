using Castle.Core.Logging;
using MedicalWebService.Data;
using MedicalWebService.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TokenApp.Controllers;

namespace Tests
{
    public class AccountTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestResearcherToken()
        {
            var mockSet = new Mock<DbSet<Researcher>>();

            var data = new List<Researcher>
            {
                new Researcher { Name = "ff", Id = Guid.NewGuid(), Login = "ff", Password = "ff" },
                new Researcher { Name = "gg", Id = Guid.NewGuid(), Login = "gg", Password = "gg" }
            }.AsQueryable();

            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<MedicalWebServiceContext>();
            mockContext.Setup(m => m.Researcher).Returns(mockSet.Object);

            var logger = new Mock<Logger<AccountController>>();
            var controller = new AccountController(mockContext.Object, null);
            
            //test invalid logon
            var tokenFail = controller.Token("aa", "aa");
            Assert.IsTrue(tokenFail.Result is BadRequestObjectResult);

            var tokenFf = controller.Token("ff", "ff");
            OkObjectResult ok = tokenFf.Result as OkObjectResult;
            Assert.IsTrue(ok != null && !string.IsNullOrEmpty(ok.Value as string));

            var tokenGg = controller.Token("gg", "gg");
            OkObjectResult ok2 = tokenGg.Result as OkObjectResult;

            Assert.IsTrue(ok2!= null && !string.IsNullOrEmpty(ok2.Value as string));

            Assert.IsTrue(string.Compare(ok.Value as string, ok2.Value as string) != 0);
        }


        [Test]
        public void TestParticipantToken()
        {
            var mockSet = new Mock<DbSet<Participant>>();

            var data = new List<Participant>
            {
                new Participant { Name = "ff", Id = Guid.NewGuid(), Login = "989" },
                new Participant { Name = "gg", Id = Guid.NewGuid(), Login = "121"}
            }.AsQueryable();

            mockSet.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<MedicalWebServiceContext>();
            mockContext.Setup(m => m.Participant).Returns(mockSet.Object);

            var mockLogger = Mock.Of<ILogger<AccountController>>();
            var controller = new AccountController(mockContext.Object, mockLogger);

            //test invalid logon
            var tokenFail = controller.TokenParticipant("111");
            Assert.IsTrue(tokenFail.Result is BadRequestObjectResult);

            var tokenSucces = controller.TokenParticipant("121");
            OkObjectResult ok = tokenSucces.Result as OkObjectResult;
            Assert.IsTrue(ok != null && !string.IsNullOrEmpty(ok.Value as string));

            var tokenSucces2 = controller.TokenParticipant("989");
            OkObjectResult ok2 = tokenSucces2.Result as OkObjectResult;

            Assert.IsTrue(ok2 != null && !string.IsNullOrEmpty(ok2.Value as string));

            Assert.IsTrue(string.Compare(ok.Value as string, ok2.Value as string) != 0);
        }


    }
}