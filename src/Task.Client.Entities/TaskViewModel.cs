using System;

namespace Task.Client.Entities
{
    public class TaskViewModel
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }
    }
}