using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult OnGet()
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            if (!memberId.HasValue)
            {
                // not logged in
                return RedirectToPage("/Login");
            }

            Member = _context.Members.Find(memberId.Value);
            if (Member == null)
            {
                // session contained invalid id — clear session and ask to login
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