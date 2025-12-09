using GymApp.Data;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace GymApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }

        public int LessonCount { get; set; }
        public int CategoryCount { get; set; }
        public int MemberCount { get; set; }

        public void OnGet()
        {
            LessonCount = _context.GroupLessons.Count();
            CategoryCount = _context.GroupLessonCategories.Count();
            MemberCount = _context.Members.Count();
        }
    }
}
