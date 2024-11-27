using Microsoft.AspNetCore.Mvc;
using SkaftoBageriWMS.Models;
using SkaftoBageriWMS.Data;
using Microsoft.AspNetCore.Http;
using System.Linq;
using BCrypt.Net;

namespace SkaftoBageriWMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;

        // Constructor to inject AppDbContext
        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        // Login Page (GET)
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Login Page (POST)
        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Find the user by username or email
                var user = _context.Users.FirstOrDefault(u => u.Username == model.Username || u.Email == model.Username);

                if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
                {
                    // Store the user's ID in the session to simulate login
                    HttpContext.Session.SetInt32("UserId", user.Id);

                    return RedirectToAction("Dashboard", "Account");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password.");
                }
            }
            return View(model);
        }

        // Register Page (GET)
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        // Register Page (POST)
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the username or email already exists
                var existingUser = _context.Users.FirstOrDefault(u => u.Username == model.Username || u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("", "Username or email already exists.");
                    return View(model);
                }

                // Create a new user and hash the password
                var user = new User
                {
                    Username = model.Username,
                    Password = BCrypt.Net.BCrypt.HashPassword(model.Password),  // Hash the password
                    Email = model.Email
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                // Redirect to the login page after successful registration
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        // Dashboard Page (GET)
        public IActionResult Dashboard()
        {
            // Check if the user is logged in
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Retrieve user information
            var user = _context.Users.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return RedirectToAction("Login", "Account");
            }

            // Pass user ID to the view
            ViewData["UserId"] = userId;

            return View();
        }

        // Logout (Clear Session)
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }
    }
}
