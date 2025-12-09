using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Admin.Categories
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<GroupLessonCategory> Categories { get; set; }

        public async Task OnGetAsync()
        {
            Categories = await _context.GroupLessonCategories.ToListAsync();
        }
    }
}
