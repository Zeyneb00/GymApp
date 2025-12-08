using System;
using System.Collections.Generic;

namespace GymApp.Models
{
    public class GroupLesson
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        // NEW PROPERTIES
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan Time { get; set; }

        public string InstructorName { get; set; } = string.Empty;

        public string StudioLocation { get; set; } = string.Empty;

        public int MaxCapacity { get; set; }

        // Now enrollment is calculated from registrations
        public int CurrentEnrollment => Registrations.Count;

        public int CategoryId { get; set; }
        public GroupLessonCategory? Category { get; set; }

        // VERY IMPORTANT: added Registrations collection
        public ICollection<LessonRegistration> Registrations { get; set; }
            = new List<LessonRegistration>();
    }
}
