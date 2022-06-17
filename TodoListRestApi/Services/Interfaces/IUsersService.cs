using TodoListRestApi.Data.Models;

namespace TodoListRestApi.Services.Interfaces
{
    public interface IUsersService
    {
        ApplicationUser Create(ApplicationUser user);

        ApplicationUser GetByEmail(string email);

        ApplicationUser GetById(int id);

        bool Exists(string email);
    }
}
