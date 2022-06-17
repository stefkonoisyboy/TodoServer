using Microsoft.EntityFrameworkCore;
using TodoListRestApi.Data;
using TodoListRestApi.Data.Models;
using TodoListRestApi.Services.Interfaces;
using TodoListRestApi.ViewModels.Todos;

namespace TodoListRestApi.Services
{
    public class TodosService : ITodosService
    {
        private readonly ApplicationDbContext dbContext;

        public TodosService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task CreateAsync(CreateTodoInputModel input)
        {
            Todo todo = new Todo
            {
                Text = input.Text,
                IsCompleted = input.IsCompleted,
                CreatedOn = DateTime.UtcNow,
                UserId = input.UserId,
            };

            await this.dbContext.Todos.AddAsync(todo);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Todo todo = await this.dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                throw new ArgumentException("Todo does not exist!");
            }

            this.dbContext.Todos.Remove(todo);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<AllTodosViewModel>> GetAllTodosAsync(int userId)
        {
            return await this.dbContext.Todos
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedOn)
                .Select(t => new AllTodosViewModel
                {
                    Id = t.Id,
                    Text = t.Text,
                    IsCompleted = t.IsCompleted,
                })
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(int id)
        {
            Todo todo = await this.dbContext.Todos.FirstOrDefaultAsync(t => t.Id == id);

            if (todo == null)
            {
                throw new ArgumentException("Todo does not exist!");
            }

            todo.IsCompleted = !todo.IsCompleted;
            await this.dbContext.SaveChangesAsync();
        }
    }
}
