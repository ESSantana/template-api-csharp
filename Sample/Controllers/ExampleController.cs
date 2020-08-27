using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.Core.Entities;
using Sample.Core.Services.Interfaces;
using System.Collections.Generic;

namespace Sample.API.Controllers
{
    [Route("api/example")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IExampleService _service;
        public ExampleController(IExampleService service, ILogger<ExampleController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<List<ExampleEntity>> Get()
        {
            _logger.LogTrace("Get All");
            var result = _service.Get();

            _logger.LogTrace($"Get All result: {result.Count}");

            return result.Count > 0
                ? (ActionResult)Ok(result)
                : NoContent();
        }

        [HttpGet]
        [Route("get/{id}")]
        public ActionResult<ExampleEntity> Get(long id)
        {
            var result = _service.Get(id);

            return result != null
                ? (ActionResult)Ok(result)
                : NoContent();
        }

        [HttpPost]
        [Route("create")]
        public ActionResult<int> Create(List<ExampleEntity> entities)
        {
            var result = _service.Create(entities);

            return result > 0
                ? (ActionResult)Ok(new { TotalResult = result })
                : NoContent();
        }

        [HttpPost]
        [Route("modify")]
        public ActionResult<ExampleEntity> Modify(ExampleEntity entity)
        {
            var result = _service.Modify(entity);

            return result.Id > 0
                ? (ActionResult)Ok(result)
                : NoContent();
        }

        [HttpDelete]
        [Route("delete")]
        public ActionResult<int> Delete(long Id)
        {
            var result = _service.Delete(Id);

            return result > 0
                ? (ActionResult)Ok(result)
                : NoContent();
        }
    }
}
