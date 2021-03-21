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
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using TokenApp.Controllers;

namespace Tests
{
    public class DataPointTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        public static async IAsyncEnumerator<DataPoint> GetAsyncEnumerator(List<DataPoint> list)
        {
            foreach(var v in list)
                yield return v;
            await Task.CompletedTask; // to make the compiler warning go away
        }

        [Test]
        public void TestGetDataPoint()
        {
            var mockSet = new Mock<DbSet<DataPoint>>();
            var list = new List<DataPoint>
            {
                new DataPoint { Id = Guid.NewGuid(), ParticipantId = Guid.NewGuid(), ResearcherId = Guid.NewGuid()},
            };

            IQueryable<DataPoint> data = InitDataPointsMock(mockSet, list);

            var mockContext = new Mock<MedicalWebServiceContext>();
            mockContext.Setup(m => m.DataPoint).Returns(mockSet.Object);

            var mockLogger = Mock.Of<ILogger<DataPointController>>();
            var controller = new DataPointController(mockContext.Object, mockLogger);

            //test valid get
            var getSucces = controller.GetDataPoint(data.ToList()[0].ParticipantId).Result;
            Assert.IsFalse(getSucces.Result is BadRequestObjectResult);
        }

        private static IQueryable<DataPoint> InitDataPointsMock(Mock<DbSet<DataPoint>> mockSet, List<DataPoint> list)
        {
            var data = new TestAsyncEnumerable<DataPoint>(list).AsQueryable();

            mockSet.As<IAsyncEnumerable<DataPoint>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>())).
                Returns(new TestAsyncEnumerator<DataPoint>(data.GetEnumerator()));

            mockSet.As<IQueryable<DataPoint>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<DataPoint>(data.Provider));
            mockSet.As<IQueryable<DataPoint>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<DataPoint>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<DataPoint>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            mockSet.Setup(p => p.Add(It.IsAny<DataPoint>())).Callback((Action<DataPoint>)(p => list.Add(p)));
            return data;
        }

        [Test]
        public void TestPostDataPoint()
        {
            var mockDataPoints = new Mock<DbSet<DataPoint>>();
            var mockParticipants = new Mock<DbSet<Participant>>();

            var dataParticpant = new TestAsyncEnumerable<Participant>(new List<Participant>
            {
                new Participant { Id = Guid.NewGuid(),  ResearcherId = Guid.NewGuid()},
            }).AsQueryable();

            InitParticipantsMock(mockParticipants, dataParticpant);

            var list = new List<DataPoint>();
            IQueryable<DataPoint> data = InitDataPointsMock(mockDataPoints, list);


            var mockContext = new Mock<MedicalWebServiceContext>();
            mockContext.Setup(m => m.DataPoint).Returns(mockDataPoints.Object);
            mockContext.Setup(m => m.Participant).Returns(mockParticipants.Object);

            var mockLogger = Mock.Of<ILogger<DataPointController>>();
            var controller = new DataPointController(mockContext.Object, mockLogger);

            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim("id", dataParticpant.First().Id.ToString()),
            }));

            controller.ControllerContext = new ControllerContext()
            {
                HttpContext = new DefaultHttpContext() { User = user }
            };

            //test valid get
            var d = new DataPoint() { Id = Guid.NewGuid() };
            var getSucces = controller.PostDataPoint(d).Result;
            //Assert.IsTrue(getSucces.Result is OkObjectResult);
            Assert.IsTrue(list.Count > 0);
            Assert.IsTrue(list[0].Id == d.Id);
        }

        private static void InitParticipantsMock(Mock<DbSet<Participant>> mockParticipants, IQueryable<Participant> dataParticpant)
        {
            mockParticipants.As<IAsyncEnumerable<Participant>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>())).
                Returns(new TestAsyncEnumerator<Participant>(dataParticpant.GetEnumerator()));

            mockParticipants.As<IQueryable<Participant>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Participant>(dataParticpant.Provider));
            mockParticipants.As<IQueryable<Participant>>().Setup(m => m.Expression).Returns(dataParticpant.Expression);
            mockParticipants.As<IQueryable<Participant>>().Setup(m => m.ElementType).Returns(dataParticpant.ElementType);
            mockParticipants.As<IQueryable<Participant>>().Setup(m => m.GetEnumerator()).Returns(dataParticpant.GetEnumerator());
        }
    }
}