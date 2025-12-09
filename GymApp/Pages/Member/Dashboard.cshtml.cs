using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MemberModel = GymApp.Models.Member;

namespace GymApp.Pages.Member
{
    public class DashboardModel : PageModel
    {
        private readonly AppDbContext _context;

        public DashboardModel(AppDbContext context)
        {
            _context = context;
        }

        public MemberModel Member { get; set; }
        public List<GroupLesson> MyLessons { get; set; } = new();

        public IActionResult OnGet()
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            if (!memberId.HasValue)
            {
                return RedirectToPage("/Login");
            }

            Member = _context.Members
            .Include(m => m.Registrations)
                .ThenInclude(r => r.GroupLesson)
                    .ThenInclude(gl => gl.Registrations)
            .FirstOrDefault(m => m.Id == memberId.Value);


            if (Member == null)
            {
                HttpContext.Session.Remove("MemberId");
                return RedirectToPage("/Login");
            }

            return Page();
        }


        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Remove("MemberId");
            return RedirectToPage("/Index");
        }
    }
}
