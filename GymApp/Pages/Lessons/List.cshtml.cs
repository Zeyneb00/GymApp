using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Lessons
{
    public class ListModel : PageModel
    {
        private readonly AppDbContext _context;

        public ListModel(AppDbContext context)
        {
            _context = context;
        }

        public GroupLessonCategory Category { get; set; }
        public List<GroupLesson> Lessons { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Load the selected category
            Category = await _context.GroupLessonCategories
                .FirstOrDefaultAsync(c => c.Id == id);

            if (Category == null)
            {
                return RedirectToPage("/Lessons/Catalog");
            }

            // Load lessons in this category
            Lessons = await _context.GroupLessons
            .Where(l => l.CategoryId == id)
            .OrderBy(l => l.DayOfWeek)
            .ThenBy(l => l.Time)
            .ToListAsync();



            return Page();
        }
    }
}
