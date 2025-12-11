using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Lessons
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroupLesson Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Lesson = await _context.GroupLessons
                .Include(l => l.Category)
                .Include(l => l.Registrations)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (Lesson == null)
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

            // Capacity check
            if (Lesson.Registrations.Count >= Lesson.MaxCapacity)
            {
                TempData["ErrorMessage"] = "This class is full!";
                return RedirectToPage(new { id });
            }

            // Already registered?
            bool already = await _context.LessonRegistrations
                .AnyAsync(r => r.MemberId == memberId && r.GroupLessonId == id);

            if (already)
            {
                TempData["ErrorMessage"] = "You are already registered!";
                return RedirectToPage(new { id });
            }

            // Register
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
