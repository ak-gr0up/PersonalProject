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
    public class ReseacrcherTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetResearcher()
        {
            var mockSet = new Mock<DbSet<Researcher>>();

            var data = new List<Researcher>
            {
                new Researcher { Name = "ff", Id = Guid.NewGuid(), Login = "ff", Password = "ff"},
            }.AsQueryable();

            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Researcher>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockSet.Setup(p => p.FindAsync(It.IsAny<object[]>())).
                Returns((Func<object[], ValueTask<Researcher>>)(
                p => new ValueTask<Researcher>(
                    Task.FromResult(data.FirstOrDefault(q => q.Id == (Guid)p[0]))
                    )));

            var mockContext = new Mock<MedicalWebServiceContext>();
            mockContext.Setup(m => m.Researcher).Returns(mockSet.Object);

            var mockLogger = Mock.Of<ILogger<ResearcherController>>();
            var controller = new ResearcherController(mockContext.Object, mockLogger);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", data.ToList()[0].Id.ToString()),
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //test invalid get
            var getSucces = controller.GetResearcher().Result;
            Assert.IsTrue(getSucces.Value.Id == data.ToList()[0].Id);
        }


    }
}