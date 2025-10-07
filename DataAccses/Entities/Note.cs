namespace DataAccess.Entities
{
    public class Note
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? ReminderDate { get; set; }   
        public bool IsReminderSet { get; set; }  // Чи ввімкнене нагадування
        public bool IsCompleted { get; set; }   
   
    }
}
