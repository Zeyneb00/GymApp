using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GymApp.Data;
using GymApp.Models;

namespace GymApp.Pages.Lessons
{
    public class CancelModel : PageModel
    {
        private readonly AppDbContext _context;

        public CancelModel(AppDbContext context)
        {
            _context = context;
        }

        public LessonRegistration Registration { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId is null)
                return RedirectToPage("/Login");

            Registration = await _context.LessonRegistrations
                .Include(r => r.GroupLesson)
                .FirstOrDefaultAsync(r => r.Id == id && r.MemberId == memberId);

            if (Registration is null)
            {
                TempData["ErrorMessage"] = "Registration not found.";
                return RedirectToPage("/Member/Dashboard");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var memberId = HttpContext.Session.GetInt32("MemberId");
            if (memberId is null)
                return RedirectToPage("/Login");

            var reg = await _context.LessonRegistrations
                .FirstOrDefaultAsync(r => r.Id == id && r.MemberId == memberId);

            if (reg is null)
            {
                TempData["ErrorMessage"] = "Registration not found.";
                return RedirectToPage("/Member/Dashboard");
            }

            _context.LessonRegistrations.Remove(reg);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Member/Dashboard");
        }
    }
}
