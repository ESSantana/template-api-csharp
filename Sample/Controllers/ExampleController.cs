using Microsoft.AspNetCore.Mvc;
using Sample.Core.Entities;
using Sample.Core.Services.Interfaces;
using System.Collections.Generic;

namespace Sample.API.Controllers
{
    [Route("api/example")]
    [ApiController]
    public class ExampleController : ControllerBase
    {
        private readonly IExampleService _service;
        public ExampleController(IExampleService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("get")]
        public ActionResult<List<ExampleEntity>> Get()
        {
            var resultado = _service.Get();

            return resultado.Count > 0 
                ? (ActionResult)Ok(resultado) 
                : NoContent();
        }
    }
}
