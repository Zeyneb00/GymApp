using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Admin.Categories
{
    public class EditModel : PageModel
    {
        private readonly AppDbContext _context;

        public EditModel(AppDbContext context)
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
            if (!ModelState.IsValid)
                return Page();

            _context.Attach(Category).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
