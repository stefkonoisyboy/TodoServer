using Microsoft.AspNetCore.Mvc;
using TodoListRestApi.Services.Interfaces;
using TodoListRestApi.ViewModels.Todos;

namespace TodoListRestApi.Controllers
{
    public class TodosController : ApiController
    {
        private readonly ITodosService todosService;

        public TodosController(ITodosService todosService)
        {
            this.todosService = todosService;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<AllTodosListViewModel>> GetAll(int userId)
        {
            AllTodosListViewModel viewModel = new AllTodosListViewModel
            {
                Todos = await this.todosService.GetAllTodosAsync(userId),
            };

            return this.Ok(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTodoInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(input);
            }

            await this.todosService.CreateAsync(input);
            return this.Ok(new { Message = "Todo created successfully!", IsSuccessful = true });
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStatus(int id)
        {
            try
            {
                await this.todosService.UpdateStatusAsync(id);
                return this.Ok(new { Message = "Todo status updated successfully!", IsSuccessful = true });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Message = ex.Message, IsCompleted = false });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await this.todosService.DeleteAsync(id);
                return this.Ok(new { Message = "Todo deleted successfully!", IsSuccessful = true });
            }
            catch (Exception ex)
            {
                return this.BadRequest(new { Message = ex.Message, IsSuccessful = false });
            }
        }
    }
}
