using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ToDoListWebApi.Models;
using ToDoListWebApi.Services;

namespace ToDoListWebApi.Controllers.Version1
{
    [ApiController]
    [Route("api/v1/Task/")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService) 
        {
            _taskService = taskService;        
        }

        [HttpGet("getitems")]
        public async Task<IEnumerable<TaskEntity>> GetItems()
        {
            return await _taskService.GetItemsAsync();
        }

        [HttpGet("getitem/{id:int}")]
        public async Task<TaskEntity> GetItemById(int id)
        {
            return await _taskService.GetItemByIdAsync(id);
        }

        [HttpPost("add")]
        public async Task<HttpResponseMessage> Add(TaskEntity item)
        {
            if (item == null || item.Created < DateTime.Today || item.Title == string.Empty || item.Description == string.Empty)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            await _taskService.AddTaskAsync(item);

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        [HttpPut("edit")]
        [HttpPatch("edit")]
        public async Task<HttpResponseMessage> Edit(TaskEntity item)
        {
            if (item == null || item.Created < DateTime.Today || item.Title == string.Empty || item.Description == string.Empty)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            await _taskService.EditTaskAsync(item);

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        [HttpDelete("remove/{id:int}")]
        public async Task<HttpResponseMessage> Remove(int id)
        {
            if (id <= 0)
            {
                return new HttpResponseMessage()
                {
                    StatusCode = System.Net.HttpStatusCode.BadRequest
                };
            }

            await _taskService.RemoveTaskAsync(id);

            return new HttpResponseMessage()
            {
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
