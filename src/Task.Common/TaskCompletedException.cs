using System;

namespace Task.Common
{
    public class TaskCompletedException : Exception
    {
        public TaskCompletedException(string message) : base(message)
        {
        }
        public TaskCompletedException(string message, Exception ex) : base(message, ex)
        {
        }
    }
}