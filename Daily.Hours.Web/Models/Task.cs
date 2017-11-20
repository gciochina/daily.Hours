namespace Daily.Hours.Web.Models
{
    public class Task
    {
        public int TaskId { get; set; }

        public string Name { get; set; }

        public virtual Project Project { get; set; }
    }
}