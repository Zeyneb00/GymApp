using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Pages.Admin.Lessons
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        public GroupLesson Lesson { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Lesson = await _context.GroupLessons
                .Include(l => l.Category)
                .FirstOrDefaultAsync(l => l.Id == id);

            if (Lesson == null)
                return RedirectToPage("./Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var lesson = await _context.GroupLessons.FindAsync(id);
            if (lesson != null)
            {
                _context.GroupLessons.Remove(lesson);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
