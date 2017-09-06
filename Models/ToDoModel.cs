using System;

namespace ToDoWithAuth.Models
{
    public class ToDoModel
    {
        public int ID { get; set; }
        public string TaskName { get; set; }
        public bool? Complete { get; set; } = false; 
        public DateTime Time {get; set;} = DateTime.Now;

        public void CompleteTask()
        {
            Complete = true;
            Time = DateTime.Now;
        }

        public string UserID {get; set;}
        public ApplicationUser User {get; set;}
    }
}
