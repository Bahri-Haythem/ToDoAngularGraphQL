namespace ToDoAngularGraphQL.Server.Models
{
    public class ToDoItem
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Item { get; set; }
        public bool IsDone { get; set; }
    }
}
