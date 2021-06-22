using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.API.DTO;
using Sample.Core.Entities;
using Sample.Service.Services;
using System.Collections.Generic;
using System.Linq;

namespace Sample.API.Controllers
{
    /// <summary>
    /// Example endpoints
    /// </summary>
    [Route("api/example")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly ILogger<ExampleController> _logger;
        private readonly IExampleService _service;
        private readonly IMapper _mapper;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="service">Example Service instance</param>
        /// <param name="logger">Logger instance</param>
        /// <param name="mapper">AutoMapper instance</param>
        public ExampleController(IExampleService service, ILogger<ExampleController> logger, IMapper mapper)
        {
            _service = service;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Get all objects
        /// </summary>
        /// <returns>List of objects</returns>
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

        /// <summary>
        /// Get objetc by id
        /// </summary>
        /// <param name="id">Object id</param>
        /// <returns>Object with id requested</returns>
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

        /// <summary>
        /// Create a new object
        /// </summary>
        /// <param name="exampleDto">object format</param>
        /// <returns>Number of objects created</returns>
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

        /// <summary>
        /// Modify specific object
        /// </summary>
        /// <param name="exampleDto">Object to modify</param>
        /// <returns>Object modified</returns>
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

        /// <summary>
        /// Delete by id
        /// </summary>
        /// <param name="id">Id of the object that should be deleted</param>
        /// <returns>Number of object deleted</returns>
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
