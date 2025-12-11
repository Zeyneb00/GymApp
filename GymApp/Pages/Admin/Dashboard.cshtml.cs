using GymApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GymApp.Pages.Admin
{
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _context;

        public DashboardModel(AppDbContext context)
        {
            _context = context;
        }

        public int TotalMembers { get; set; }
        public int TotalCategories { get; set; }
        public int TotalLessons { get; set; }
        public int TotalRegistrations { get; set; }

        public void OnGet()
        {
            TotalMembers = _context.Members.Count();
            TotalCategories = _context.GroupLessonCategories.Count();
            TotalLessons = _context.GroupLessons.Count();
            TotalRegistrations = _context.LessonRegistrations.Count();
        }
    }
}
