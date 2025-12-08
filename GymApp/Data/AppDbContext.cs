using Microsoft.EntityFrameworkCore;
using GymApp.Models;


namespace GymApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        { 
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<GroupLessonCategory> GroupLessonCategories { get; set; }
        public DbSet<LessonRegistration> LessonRegistrations { get; set; }
        public DbSet<GroupLesson> GroupLessons { get; set; }

    }
}
