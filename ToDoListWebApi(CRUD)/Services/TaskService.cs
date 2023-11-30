using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using ToDoListWebApi.Models;

namespace ToDoListWebApi.Services
{
    public class TaskService : ITaskService
    {
        private readonly MyDbContext _myDbContext;

        public TaskService(MyDbContext myDbContext) 
        {
            _myDbContext = myDbContext;
        }

        public async Task AddTaskAsync(TaskEntity item)
        {
            await _myDbContext.Tasks.AddAsync(item);

            await _myDbContext.SaveChangesAsync();
        }

        public async Task EditTaskAsync(TaskEntity item)
        {
            var data = await _myDbContext.Tasks.Where(x => x.Id == item.Id).FirstOrDefaultAsync();

            if (data != null)
            {
                data.Title = item.Title;
                data.Description = item.Description;
                data.Created = item.Created;
                await _myDbContext.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TaskEntity>> GetItemsAsync()
        {
            return await _myDbContext.Tasks.ToListAsync();
        }

        public async Task<TaskEntity> GetItemByIdAsync(int id)
        {
            return await _myDbContext.Tasks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task RemoveTaskAsync(int id)
        {
            var result = await FindById(id);

            if (result != null)
            {
                _myDbContext.Tasks.Remove(result);

                await _myDbContext.SaveChangesAsync();
            }
        }

        private async Task<TaskEntity> FindById(int? id)
        {
            if (id == null)
            {
                return null;
            }

            return await _myDbContext.Tasks.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
