using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;
using MemberModel = GymApp.Models.Member;

namespace GymApp.Pages.Admin.Members
{
    public class DetailsModel : PageModel
    {
        private readonly AppDbContext _context;

        public DetailsModel(AppDbContext context)
        {
            _context = context;
        }

        public MemberModel Member { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Member = await _context.Members
                .Include(m => m.Registrations)
                .ThenInclude(r => r.GroupLesson)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Member == null)
                return RedirectToPage("/Admin/Members/Index");

            return Page();
        }
    }
}
