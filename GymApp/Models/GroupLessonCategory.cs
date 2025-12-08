using System.Collections.Generic;

namespace GymApp.Models
{
    public class GroupLessonCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public ICollection<GroupLesson> GroupLessons { get; set; } = new List<GroupLesson>();
    }
}
