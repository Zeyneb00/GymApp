using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Admin.Categories
{
    public class CreateModel : PageModel
    {
        private readonly AppDbContext _context;

        public CreateModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public GroupLessonCategory Category { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.GroupLessonCategories.Add(Category);
            await _context.SaveChangesAsync();

            return RedirectToPage("Index");
        }
    }
}
