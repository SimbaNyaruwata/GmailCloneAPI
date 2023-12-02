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
    public class AttachmentsController : Controller
    {
        private readonly GmailCloneDbContext _context;

        public AttachmentsController(GmailCloneDbContext context)
        {
            _context = context;
        }

        // GET: Attachments
        public async Task<IActionResult> Index()
        {
            var gmailCloneDbContext = _context.Attachments;
            return View(await gmailCloneDbContext.ToListAsync());
        }

        // GET: Attachments/Details/5
        [HttpGet]
        [Route("Attachments/Read")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attachments == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachments
                //.Include(a => a.Email)
                .FirstOrDefaultAsync(m => m.AttachmentId == id);
            if (attachment == null)
            {
                return NotFound();
            }


            return Ok(attachment);
        }

        // GET: Attachments/Create
        public IActionResult Create()
        {
            ViewData["EmailId"] = new SelectList(_context.Emails, "EmailId", "EmailId");
            return View();
        }

        // POST: Attachments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Attachments/Create")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromBody] Attachment attachment)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    attachment.Status = 1;
                    _context.Add(attachment);
                    await _context.SaveChangesAsync();
                    return Ok(attachment);
                    //return RedirectToAction(nameof(Index));
                }
               // ViewData["EmailId"] = new SelectList(_context.Emails, "EmailId", "EmailId", attachment.EmailId);
                return Ok(attachment);
            }
            catch(Exception e)
            {
                throw;
            }
            

        }

        // GET: Attachments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attachments == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment == null)
            {
                return NotFound();
            }
            ViewData["EmailId"] = new SelectList(_context.Emails, "EmailId", "EmailId", attachment.EmailId);
            return View(attachment);
        }

        // POST: Attachments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Route("Attachments/Update")]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromBody] Attachment attachment)
        {
            if (_context.Attachments.Where(x => x.AttachmentId == attachment.AttachmentId).Count() < 1)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attachment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttachmentExists(attachment.AttachmentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Ok(attachment);
            }
            ViewData["EmailId"] = new SelectList(_context.Emails, "EmailId", "EmailId", attachment.EmailId);
            return Ok(attachment);
        }

        // GET: Attachments/Delete/5
        [HttpDelete]
        [Route("Attachments/Delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attachments == null)
            {
                return NotFound();
            }

            var attachment = await _context.Attachments
                //.Include(a => a.Email)
                .FirstOrDefaultAsync(m => m.AttachmentId == id);
            if (attachment == null)
            {
                return NotFound();
            }
            attachment.Status = 0;


            return Ok(attachment);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attachments == null)
            {
                return Problem("Entity set 'GmailCloneDbContext.Attachments'  is null.");
            }
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment != null)
            {
                _context.Attachments.Remove(attachment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttachmentExists(int id)
        {
          return (_context.Attachments?.Any(e => e.AttachmentId == id)).GetValueOrDefault();
        }
    }
}
