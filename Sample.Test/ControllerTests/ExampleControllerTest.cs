using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using Sample.API.Controllers;
using Sample.API.Models.DTO;
using Sample.Core.Entities;
using Sample.Core.Services.Interfaces;
using Sample.Test.Configuration.Service;
using System.Collections.Generic;
using System.Linq;

namespace Sample.Test.ControllerTests
{
    public class ExampleControllerTest
    {
        private ExampleController controller;
        private Mock<IExampleService> service;

        [SetUp]
        public void Setup()
        {
            var mapper = new Mock<IMapper>();
            var logger = new Mock<ILogger<ExampleController>>();
            service = new Mock<IExampleService>();

            service.Setup(r => r.Get()).Returns(ExampleMockResult.Get());
            service.Setup(r => r.Get(It.IsAny<long>())).Returns(ExampleMockResult.Get().First());
            service.Setup(r => r.Create(It.IsAny<List<ExampleEntity>>())).Returns(1);
            service.Setup(r => r.Modify(It.IsAny<ExampleEntity>())).Returns(ExampleMockResult.Get().First());
            service.Setup(r => r.Delete(It.IsAny<long>())).Returns(1);

            controller = new ExampleController(service.Object, logger.Object, mapper.Object);
        }

        [Test]
        public void Get_ShouldReturn_ListOfExampleDTO()
        {
            var result = controller.Get();

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Get(), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(ActionResult<List<ExampleDTO>>), result);
            });
        }

        [Test]
        public void Get_ShouldReturn_NoContentResult()
        {
            service.Setup(r => r.Get()).Returns(new List<ExampleEntity>());

            var result = controller.Get();

            Assert.Multiple(() =>
            {
                service.Verify(s => s.Get(), Times.Once);
                Assert.NotNull(result);
                Assert.IsInstanceOf(typeof(NoContentResult), result);
            });
        }
    }
}
