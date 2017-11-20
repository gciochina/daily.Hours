using System;

namespace Daily.Hours.Web.Models
{
    public class WorkLog
    {
        public int WorkLogId { get; set; }

        public int Hours { get; set; }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public virtual User User { get; set; }

        public virtual Task Task { get; set; }
    }
}