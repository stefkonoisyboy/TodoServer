using TodoListRestApi.ViewModels.Todos;

namespace TodoListRestApi.Services.Interfaces
{
    public interface ITodosService
    {
        Task CreateAsync(CreateTodoInputModel input);

        Task UpdateStatusAsync(int id);

        Task DeleteAsync(int id);

        Task<IEnumerable<AllTodosViewModel>> GetAllTodosAsync(int userId);
    }
}
