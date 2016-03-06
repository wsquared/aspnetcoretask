using System;

namespace Task.Common
{
    public class TaskNotFoundException : Exception
    {
        public TaskNotFoundException(string message) : base(message)
        {
        }
        public TaskNotFoundException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}
