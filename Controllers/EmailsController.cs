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
    //[Authorize]
    public class EmailsController : Controller
    {
        private readonly GmailCloneDbContext _context;

        public EmailsController(GmailCloneDbContext context)
        {
            _context = context;
        }

        // GET: Emails
        public async Task<IActionResult> Index()
        {
            var gmailCloneDbContext = _context.Emails.Include(e => e.Recipient);
            return View(await gmailCloneDbContext.ToListAsync());
        }

        // GET: Emails/Details/5
        [HttpGet]
        [Route("Emails/Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .Include(e => e.Recipient)
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }

            return Ok(email);
        }

        // GET: Emails/Create
        public IActionResult Create()
        {
            ViewData["RecipientId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: Emails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Emails/Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmailId,SenderId,RecipientId,Subject,Body,SentDate,IsRead")] Email email)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    email.Status = 1;
                    _context.Add(email);
                    await _context.SaveChangesAsync();
                    return Ok(email);
                    //return RedirectToAction(nameof(Index));
                }
                ViewData["RecipientId"] = new SelectList(_context.Users, "UserId", "UserId", email.RecipientId);
                return Ok(email);
            }
            catch(Exception e)
            {
                throw;
            }
            
        }

        // GET: Emails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var email = await _context.Emails.FindAsync(id);
            if (email == null)
            {
                return NotFound();
            }
            ViewData["RecipientId"] = new SelectList(_context.Users, "UserId", "UserId", email.RecipientId);
            return Ok(email);
        }

        // POST: Emails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Emails/Update")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmailId,SenderId,RecipientId,Subject,Body,SentDate,IsRead")] Email email)
        {
            if (id != email.EmailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(email);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailExists(email.EmailId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(email);
                //return RedirectToAction(nameof(Index));
            }
            ViewData["RecipientId"] = new SelectList(_context.Users, "UserId", "UserId", email.RecipientId);
            return Ok(email);
        }

        // GET: Emails/Delete/5
        [HttpDelete]
        [Route("Emails/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Emails == null)
            {
                return NotFound();
            }

            var email = await _context.Emails
                .Include(e => e.Recipient)
                .FirstOrDefaultAsync(m => m.EmailId == id);
            if (email == null)
            {
                return NotFound();
            }
            email.Status = 0;
            await _context.SaveChangesAsync();
            return Ok(email);
        }

        // POST: Emails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Emails == null)
            {
                return Problem("Entity set 'GmailCloneDbContext.Emails'  is null.");
            }
            var email = await _context.Emails.FindAsync(id);
            if (email != null)
            {
                _context.Emails.Remove(email);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmailExists(int id)
        {
          return (_context.Emails?.Any(e => e.EmailId == id)).GetValueOrDefault();
        }
    }
}
