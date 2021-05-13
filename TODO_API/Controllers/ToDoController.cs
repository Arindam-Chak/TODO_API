using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using System.Net;
using System.Threading.Tasks;
using TODO_API.Controllers.Resources;
using TODO_API.Core.Services;

namespace ToDoAPI.Controllers
{
    
    [ApiController]
    public class TodoItem_DetailsController : Controller
    {


        private readonly ITodoService _todoService;

        public TodoItem_DetailsController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.PreconditionFailed)]
        [HttpGet]
        [Authorize]
        [Route("/api/fetch_all/todoitem")]
        public async Task<ActionResult> FetchAllTodoItem_Details()
        {
            return Ok(await _todoService.FetchAllTodoItem_Details());

        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.PreconditionFailed)]
        [HttpGet]
        [Authorize]
        [Route("/api/fetch_by_id/todoitem")]
        public async Task<ActionResult> FetchTodoItem_Details(long id)
        {
            var result = await _todoService.FetchTodoItem_Details(id);
            if (result == null)
            {
                return NotFound(); 
            }
            else { return Ok(result); }

           
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.PreconditionFailed)]
        [HttpPut]
        [Authorize]
        [Route("/api/update/todoitem")]
        public async Task<IActionResult> Modify(TodoItem_Details todo_item)
        {
               var result = await _todoService.ModifyTodoItem_Details(todo_item);
             return CreatedAtAction(nameof(Modify), new { id = todo_item.Item_id}, result);
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.PreconditionFailed)]
        [HttpPost]
        [Authorize]
        [Route("/api/add/todoitem")]
        public async Task<ActionResult> AddTodoItem_Details(TodoItem_Details todo_item)
        {
            var result = await _todoService.AddTodoItem_Details(todo_item);
            return CreatedAtAction(nameof(AddTodoItem_Details), new { id = todo_item.Item_id }, result);
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Unauthorized)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.Conflict)]
        [ProducesResponseType(typeof(TodoErrorResponse), (int)HttpStatusCode.PreconditionFailed)]
        [HttpDelete]
        [Authorize]
        [Route("/api/delete/todoitem")]
        public async Task<ActionResult> DeleteTodoItem_Details(long id)
        {
            var result = await _todoService.DeleteTododItem(id);
            return CreatedAtAction(nameof(DeleteTodoItem_Details), new { id = id }, result);
        }

    }
}