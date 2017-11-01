using System;

namespace Daily.Hours.Web.Models
{
    public class ActivityModel : BaseModel
    {
        public UserModel User { get; set; }
        public TaskModel Task { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
    }
}