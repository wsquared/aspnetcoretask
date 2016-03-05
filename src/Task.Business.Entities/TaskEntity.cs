using System;

namespace Task.Business.Entities
{
    public class TaskEntity
    {
        public Guid TaskId { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTimeOffset? CompletedDate { get; set; }
    }
}
