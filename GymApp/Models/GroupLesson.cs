using System;

namespace GymApp.Models
{
    public class GroupLesson
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime LessonTime { get; set; }

        public string InstructorName { get; set; } = string.Empty;

        public string StudioLocation { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }

        public int CurrentEnrollment { get; set; }

        public int CategoryId { get; set; }
        public GroupLessonCategory? Category { get; set; }
    }
}
