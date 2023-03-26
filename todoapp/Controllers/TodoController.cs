using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace todoapp.Controllers
{
    [ApiController]
    [Route("todo")]
    public class TodoController : ControllerBase
    {
        
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;

        public TodoController(
            ILogger<TodoController> logger,
            ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get(Guid id)
        {
            var result = await _todoService.Fetch(id);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post(TodoEntity todoEntity)
        {
            await _todoService.Create(todoEntity);
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> Put(TodoEntity todoEntity)
        {
            await _todoService.Update(todoEntity);
            return NoContent();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _todoService.Delete(id);
            return NoContent();
        }
    }
}

