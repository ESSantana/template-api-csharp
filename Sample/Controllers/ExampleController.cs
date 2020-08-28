using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public ActionResult<List<ExampleEntity>> Get()
        {
            _logger.LogDebug("Get All");
            var result = _service.Get();

            _logger.LogDebug($"Get All result: {result.Count}");

            return result.Count > 0
                ? (ActionResult)Ok(result)
                : NoContent();
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public ActionResult<ExampleEntity> Get(long id)
        {
            _logger.LogDebug("Get");
            var result = _service.Get(id);

            _logger.LogDebug($"Get result success? {result != null}");

            return result != null
                ? (ActionResult)Ok(result)
                : NoContent();
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult<object> Create(List<ExampleEntity> entities)
        {
            _logger.LogDebug("Create");
            var result = _service.Create(entities);

            _logger.LogDebug($"Create: {result} entities created");

            return result > 0
                ? (ActionResult)Ok(new { TotalResult = result })
                : NoContent();
        }

        [HttpPost]
        [Route("modify")]
        [Authorize]
        public ActionResult<ExampleEntity> Modify(ExampleEntity entity)
        {
            _logger.LogDebug("Modify");
            var result = _service.Modify(entity);

            _logger.LogDebug($"Modify success? {string.IsNullOrEmpty(result.Name)}");

            return result.Id > 0
                ? (ActionResult)Ok(result)
                : NoContent();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        [Authorize]
        public ActionResult<object> Delete(long id)
        {
            _logger.LogDebug("Delete");
            var result = _service.Delete(id);

            _logger.LogDebug($"Delete: {result} entities deleted");

            return result > 0
                ? (ActionResult)Ok(new { TotalDeleted = result})
                : NoContent();
        }
    }
}
