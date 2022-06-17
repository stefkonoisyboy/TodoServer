namespace TodoListRestApi.Data.Models
{
    public class ApplicationUser
    {
        public ApplicationUser()
        {
            this.Todos = new HashSet<Todo>();
        }

        public int Id { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public virtual ICollection<Todo> Todos { get; set; }
    }
}
