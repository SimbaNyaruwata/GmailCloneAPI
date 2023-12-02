using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GmailClone.Models;
using Microsoft.AspNetCore.Authorization;

namespace GmailClone.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly GmailCloneDbContext _context;

        public UsersController(GmailCloneDbContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
              return _context.Users != null ? 
                          View(await _context.Users.ToListAsync()) :
                          Problem("Entity set 'GmailCloneDbContext.Users'  is null.");
        }

        // GET: Users/Details/5
        [HttpGet]
        [Route("Users/Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Users/Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    user.Status = 1;

                    //Set the hashed password
                    user.SetPassword(user.PasswordHash);

                    _context.Add(user);

                    //Save changes to the database
                    await _context.SaveChangesAsync();

                    // Clear the password hash from the response for security reasons
                    user.PasswordHash = null;
                    return Ok(user);
                    //return directToAction(nameof(Index));
                }
                return Ok(user);
            }
            catch(Exception e)

            {
                throw;
            }
            
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Users/Update")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] User user)
        {

            try
            {
                if (_context.Users.Where(x => x.UserId == user.UserId).Count() < 1)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    // Retrieve the existing user from the database
                    var existingUser = await _context.Users.FindAsync(user.UserId);

                    if (existingUser == null)
                    {
                        return NotFound();
                    }

                    // Update properties that can be modified without hashing
                    existingUser.UserName = user.UserName;
                    existingUser.Email = user.Email;
                    existingUser.SenderId = user.SenderId;
                    existingUser.RecipientId = user.RecipientId;
                    existingUser.Status = user.Status;

                    // Check if the password is being updated
                    if (!string.IsNullOrEmpty(user.PasswordHash))
                    {
                        // Set the new hashed password
                        existingUser.SetPassword(user.PasswordHash);
                    }

                    // Save changes to the database
                    await _context.SaveChangesAsync();

                    return Ok(existingUser);
                }

                return BadRequest("Invalid model state.");
            }
            catch (Exception e)
            {
                // Log the exception or handle it appropriately
                return StatusCode(500, "Internal Server Error");
            }
        }

        // GET: Users/Delete/5
        [HttpDelete]
        [Route("Users/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (user == null)
            {
                return NotFound();
            }
            user.Status = 0;
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'GmailCloneDbContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
          return (_context.Users?.Any(e => e.UserId == id)).GetValueOrDefault();
        }
    }
}
