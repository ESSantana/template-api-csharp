using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.API.Models.DTO;
using Sample.Core.Entities;
using Sample.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Sample.API.Controllers
{
    [Route("api/example")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IExampleService _service;
        private readonly IMapper _mapper;

        public ExampleController(IExampleService service, ILogger<ExampleController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("get")]
        [Authorize]
        public ActionResult<List<ExampleDTO>> Get()
        {
            _logger.LogDebug("Get All");
            var result = _service.Get();

            _logger.LogDebug($"Get All result: {result.Count}");

            return result.Count > 0
                ? (ActionResult)Ok(result.Select(r => _mapper.Map<ExampleDTO>(r)).ToList())
                : NoContent();
        }

        [HttpGet]
        [Route("get/{id}")]
        [Authorize]
        public ActionResult<ExampleDTO> Get(long id)
        {
            _logger.LogDebug("Get");
            var result = _service.Get(id);

            _logger.LogDebug($"Get result success? {result != null}");

            return result != null
                ? (ActionResult)Ok(_mapper.Map<ExampleDTO>(result))
                : NoContent();
        }

        [HttpPost]
        [Route("create")]
        [Authorize]
        public ActionResult<object> Create(List<ExampleDTO> exampleDto)
        {
            var exampleEntities = exampleDto.Select(e => _mapper.Map<ExampleEntity>(e)).ToList();

            _logger.LogDebug("Create");
            var result = _service.Create(exampleEntities);

            _logger.LogDebug($"Create: {result} entities created");

            return result > 0
                ? (ActionResult)Ok(new { TotalResult = result })
                : NoContent();
        }

        [HttpPost]
        [Route("modify")]
        [Authorize]
        public ActionResult<ExampleDTO> Modify(ExampleDTO exampleDto)
        {
            var exampleEntity = _mapper.Map<ExampleEntity>(exampleDto);

            _logger.LogDebug("Modify");
            var result = _service.Modify(exampleEntity);

            _logger.LogDebug($"Modify success? {result != null}");

            return result != null
                ? (ActionResult)Ok(_mapper.Map<ExampleDTO>(result))
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
                ? (ActionResult)Ok(new { TotalDeleted = result })
                : NoContent();
        }
    }
}
