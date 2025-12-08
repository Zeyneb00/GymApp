using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Lessons
{
    public class RegisterModel : PageModel
    {
        private readonly AppDbContext _context;

        public RegisterModel(AppDbContext context)
        {
            _context = context;
        }

        public GroupLesson Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Lesson = await _context.GroupLessons
                .Include(l => l.Registrations)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (Lesson is null)
                return RedirectToPage("/Lessons/Catalog");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId is null)
                return RedirectToPage("/Login");

            Lesson = await _context.GroupLessons
                .Include(l => l.Registrations)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (Lesson is null)
                return RedirectToPage("/Lessons/Catalog");

            // Check if capacity full
            if (Lesson.Registrations.Count >= Lesson.MaxCapacity)
                return Content("Class is full!");

            // Check duplicate registration
            bool already = await _context.LessonRegistrations
                .AnyAsync(r => r.MemberId == memberId && r.GroupLessonId == id);

            if (already)
                return Content("You are already registered!");

            // Create registration
            var reg = new LessonRegistration
            {
                MemberId = memberId.Value,
                GroupLessonId = id
            };

            _context.LessonRegistrations.Add(reg);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Member/Dashboard");
        }
    }
}
