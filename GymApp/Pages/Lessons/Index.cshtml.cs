using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Admin.Lessons
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public List<GroupLesson> Lessons { get; set; } = new();

        public async Task OnGetAsync()
        {
            Lessons = await _context.GroupLessons
                .Include(l => l.Category)
                .OrderBy(l => l.DayOfWeek)
                .ThenBy(l => l.Time)
                .ToListAsync();
        }
    }
}
