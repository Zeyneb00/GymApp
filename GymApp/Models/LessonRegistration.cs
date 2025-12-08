namespace GymApp.Models
{
    public class LessonRegistration
    {
        public int Id { get; set; }

        public int MemberId { get; set; }
        public Member? Member { get; set; }

        public int GroupLessonId { get; set; }
        public GroupLesson? GroupLesson { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.Now;
    }
}
