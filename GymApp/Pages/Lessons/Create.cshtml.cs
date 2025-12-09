using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Admin.Lessons
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroupLesson Lesson { get; set; } = new GroupLesson();

        public List<SelectListItem> CategoryOptions { get; set; }

        public async Task OnGetAsync()
        {
            CategoryOptions = await _context.GroupLessonCategories
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                })
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(); // reload categories
                return Page();
            }

            _context.GroupLessons.Add(Lesson);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
