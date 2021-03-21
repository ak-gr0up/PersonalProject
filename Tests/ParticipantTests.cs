using Castle.Core.Logging;
using MedicalWebService.Controllers;
using MedicalWebService.Data;
using MedicalWebService.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TokenApp.Controllers;

namespace Tests
{
    public class ParticipantTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetParticipant()
        {
            var mockSet = new Mock<DbSet<Participant>>();

            var data = new List<Participant>
            {
                new Participant { Name = "ff", Id = Guid.NewGuid(), Login = "ff"},
                new Participant { Name = "gg", Id = Guid.NewGuid(), Login = "gg"}
            }.AsQueryable();

            mockSet.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockSet.Setup(p => p.FindAsync(It.IsAny<object[]>())).
                Returns((Func<object[], ValueTask<Participant>>)(
                p => new ValueTask<Participant>(
                    Task.FromResult(data.FirstOrDefault(q => q.Id == (Guid)p[0]))
                    )));

            var mockContext = new Mock<MedicalWebServiceContext>();
            mockContext.Setup(m => m.Participant).Returns(mockSet.Object);

            var mockLogger = Mock.Of<ILogger<ParticipantController>>();
            var controller = new ParticipantController(mockContext.Object, mockLogger);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", data.ToList()[0].Id.ToString()),
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //test invalid get
            var getFail = controller.GetParticipantMe().Result;
            Assert.IsTrue(getFail.Result is OkObjectResult);   
        }


        [Test]
        public void TestPostParticipant()
        {
            var mockSet = new Mock<DbSet<Participant>>();

            var data = new List<Researcher>
            {
                new Researcher { Name = "ff", Id = Guid.NewGuid(), Login = "ff", Password = "ff"},
                new Researcher { Name = "gg", Id = Guid.NewGuid(), Login = "gg", Password = "gg"}
            }.AsQueryable();

            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<MedicalWebServiceContext>();
            mockContext.Setup(m => m.Participant).Returns(mockSet.Object);

            var mockLogger = Mock.Of<ILogger<ParticipantController>>();
            var controller = new ParticipantController(mockContext.Object, mockLogger);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", data.ToList()[0].Id.ToString()),
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //test valid post

            Participant p = new Participant();
            p.Name = "q";
            p.Surname = "q";
            var postSucces = controller.PostParticipant(p).Result;
            Assert.IsTrue(postSucces.Value.Id == p.Id);
        }

    }
}