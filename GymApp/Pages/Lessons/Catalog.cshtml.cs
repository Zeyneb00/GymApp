using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GymApp.Pages.Lessons
{
    public class CatalogModel : PageModel
    {
        private readonly AppDbContext _context;

        public CatalogModel(AppDbContext context)
        {
            _context = context;
        }

        public List<GroupLessonCategory> Categories { get; set; } = new();

        public async Task OnGetAsync()
        {
            Categories = await _context.GroupLessonCategories
                .OrderBy(c => c.Name)
                .ToListAsync();
        }
    }
}
