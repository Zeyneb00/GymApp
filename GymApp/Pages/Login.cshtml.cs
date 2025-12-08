using GymApp.Data;
using GymApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly AppDbContext _context;

        public LoginModel(AppDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public string TcNumber { get; set; }

        [BindProperty]
        public string Password { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet()
        {
            // If already logged in, redirect to dashboard
            var existing = HttpContext.Session.GetInt32("MemberId");
            if (existing.HasValue)
            {
                Response.Redirect("/Member/Dashboard");
            }
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrWhiteSpace(TcNumber) || string.IsNullOrWhiteSpace(Password))
            {
                ErrorMessage = "Please enter TC number and password.";
                return Page();
            }

            // Find member (plain text password check for learning)
            var member = _context.Members
                .FirstOrDefault(m => m.TcNumber == TcNumber && m.Password == Password);

            if (member == null)
            {
                ErrorMessage = "Invalid TC number or password.";
                return Page();
            }

            // Save logged-in user id in session
            HttpContext.Session.SetInt32("MemberId", member.Id);

            // redirect to member dashboard
            return RedirectToPage("/Member/Dashboard");
        }
    }
}