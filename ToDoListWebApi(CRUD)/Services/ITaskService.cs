using ToDoListWebApi.Models;

namespace ToDoListWebApi.Services
{
    public interface ITaskService
    {
        Task AddTaskAsync(TaskEntity item);
        Task EditTaskAsync(TaskEntity item);
        Task RemoveTaskAsync(int index);
        Task<IEnumerable<TaskEntity>> GetItemsAsync();
        Task<TaskEntity> GetItemByIdAsync(int id);
    }
}
