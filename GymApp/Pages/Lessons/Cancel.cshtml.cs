using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using GymApp.Data;
using GymApp.Models;
using Microsoft.EntityFrameworkCore;

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
            Registration = await _context.LessonRegistrations
                .Include(r => r.GroupLesson)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (Registration == null)
                return RedirectToPage("/Member/Dashboard");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var reg = await _context.LessonRegistrations.FindAsync(id);
            if (reg == null)
                return RedirectToPage("/Member/Dashboard");

            _context.LessonRegistrations.Remove(reg);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Registration cancelled successfully.";
            return RedirectToPage("/Member/Dashboard");
        }
    }
}
