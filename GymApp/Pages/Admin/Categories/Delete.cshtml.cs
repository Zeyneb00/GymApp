using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Admin.Categories
{
    public class DeleteModel : PageModel
    {
        private readonly AppDbContext _context;

        public DeleteModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroupLessonCategory Category { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Category = await _context.GroupLessonCategories.FindAsync(id);

            if (Category == null)
                return RedirectToPage("Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Category == null)
                return RedirectToPage("Index");

            var category = await _context.GroupLessonCategories.FindAsync(Category.Id);

            if (category != null)
            {
                _context.GroupLessonCategories.Remove(category);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("Index");
        }
    }
}
