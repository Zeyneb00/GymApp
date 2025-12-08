using System.Linq;
using GymApp.Data;
using GymApp.Models;


namespace GymApp
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();
            if (!context.GroupLessonCategories.Any())
            {
                context.GroupLessonCategories.AddRange(
                    new GroupLessonCategory { Name = "Cardio", Description = "High energy, heart rate boosting classes." },
                    new GroupLessonCategory { Name = "Strength & Conditioning", Description = "Build muscle and endurance." },
                    new GroupLessonCategory { Name = "Flexibility & Mind", Description = "Focus on stretching, mobility, and mindfulness." }
                );
                context.SaveChanges();
            }

            // Seed Lessons
            // Seed Lessons
            if (!context.GroupLessons.Any())
            {
                var cardioCat = context.GroupLessonCategories.First(c => c.Name == "Cardio");
                var strengthCat = context.GroupLessonCategories.First(c => c.Name == "Strength & Conditioning");
                var flexCat = context.GroupLessonCategories.First(c => c.Name == "Flexibility & Mind");

                context.GroupLessons.AddRange(
                    new GroupLesson
                    {
                        Name = "Zumba",
                        InstructorName = "Ayşe Demir",
                        StudioLocation = "Studio A",
                        MaxCapacity = 20,
                        DayOfWeek = DayOfWeek.Monday,
                        Time = new TimeSpan(18, 0, 0),
                        CategoryId = cardioCat.Id
                    },
                    new GroupLesson
                    {
                        Name = "Crossfit Beginner",
                        InstructorName = "Ali Yılmaz",
                        StudioLocation = "Studio C",
                        MaxCapacity = 15,
                        DayOfWeek = DayOfWeek.Tuesday,
                        Time = new TimeSpan(17, 0, 0),
                        CategoryId = strengthCat.Id
                    },
                    new GroupLesson
                    {
                        Name = "Yoga",
                        InstructorName = "Esra Kaya",
                        StudioLocation = "Studio B",
                        MaxCapacity = 20,
                        DayOfWeek = DayOfWeek.Wednesday,
                        Time = new TimeSpan(16, 0, 0),
                        CategoryId = flexCat.Id
                    }
                );

                context.SaveChanges();
            }

            // --- add this inside DataSeeder.Seed(AppDbContext context) ---

            // Seed a test Member (for login)
            if (!context.Members.Any())
            {
                context.Members.Add(new Member
                {
                    TcNumber = "12345678901",    // 11-digit T.C. style example
                    Password = "password123",    // plain text for learning
                    FirstName = "Test",
                    LastName = "User",
                    MembershipPackage = "Gold Annual",
                    MembershipEndDate = DateTime.Today.AddMonths(6)
                });
                context.SaveChanges();
            }

        }
    }
}
