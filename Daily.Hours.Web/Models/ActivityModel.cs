using System;

namespace Daily.Hours.Web.Models
{
    public class ActivityModel : BaseModel
    {
        public int Hours { get; set; }

        public DateTime Date { get; set; }

        public virtual UserModel User { get; set; }

        public virtual TaskModel Task { get; set; }
    }
}