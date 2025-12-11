using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GymApp.Data;

namespace GymApp.Pages.Member
{
    public class ProfileModel : PageModel
    {
        private readonly AppDbContext _context;

        public ProfileModel(AppDbContext context)
        {
            _context = context;
        }

        public Models.Member Member { get; set; }

        public IActionResult OnGet()
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId == null)
                return RedirectToPage("/Login");

            Member = _context.Members.Find(memberId.Value);
            if (Member == null)
                return RedirectToPage("/Login");

            return Page();
        }
    }
}
