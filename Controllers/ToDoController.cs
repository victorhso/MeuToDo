using MeuTodo.Data;
using MeuTodo.Model;
using MeuTodo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MeuTodo.Controllers
{
    [ApiController]
    [Route(template: "v1")]
    public class ToDoController : ControllerBase
    {
        [HttpGet]
        [Route(template: "todos")]
        public async Task<IActionResult> GetAsync([FromServices] AppDbContext context)
        {
            var todos = await context.ToDos.AsNoTracking().ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context.ToDos.AsNoTracking().FirstOrDefaultAsync(x => x.Id.Equals(id));

            return todo == null ? NotFound() : Ok(todo);
        }

        [HttpPost(template: "todos")]
        public async Task<IActionResult> PostAsync([FromServices] AppDbContext context, [FromBody] CreateToDoViewModel todo)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var task = new ToDo()
            {
                Date = DateTime.Now,
                Done = false,
                Title = todo.Title
            };

            try
            {
                await context.ToDos.AddAsync(task);
                await context.SaveChangesAsync();

                return Created(uri: $"v1/todos/{task.Id}", task);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPut(template: "todos/{id}")]
        public async Task<IActionResult> PutAsync([FromServices] AppDbContext context, [FromBody] CreateToDoViewModel todo, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var task = await context.ToDos.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (task is null)
                return NotFound();

            try
            {
                task.Title = todo.Title;

                context.ToDos.Update(task);
                await context.SaveChangesAsync();

                return Ok(task);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete(template: "todos/{id}")]
        public async Task<IActionResult> DeleteAsync([FromServices] AppDbContext context, [FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var task = await context.ToDos.FirstOrDefaultAsync(x => x.Id.Equals(id));

            if (task is null)
                return NotFound();

            try
            {
                context.ToDos.Remove(task);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch
            {
                return BadRequest();
            }

        }
    }
}
