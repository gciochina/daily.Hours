using System;

namespace Daily.Hours.Web.ViewModels
{
    public class ActivityViewModel : BaseViewModel
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public DateTime Date { get; set; }
        public string Hours { get; set; }
    }
}