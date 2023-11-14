using GmailClone.Models;
using Microsoft.EntityFrameworkCore;

namespace GmailClone.Services
{
    public class EmailService : BaseService
    {
        public EmailService(GmailCloneDbContext dBContext) : base(dBContext)
        { 
        }

        public async Task<Email> Create (Email email)
        {
            try
            {
                var e = new Email
                {
                    SenderId = email.SenderId,
                    RecipientId = email.RecipientId,
                    Subject = email.Subject,
                    Body = email.Body,
                    SentDate = DateTime.Now,
                    IsRead = email.IsRead,
                    Status = email.Status,
                };
                if (await _dBContext.SaveChangesAsync()>0)
                {
                    var _email = await
                    _dBContext.Emails
                    .Where(x => x.EmailId == e.EmailId)
                    .FirstOrDefaultAsync();
                    return new Email
                    {
                        SenderId = _email.SenderId,
                        RecipientId = _email.RecipientId,
                        Subject = _email.Subject,
                        Body = _email.Body,
                        SentDate = DateTime.Now,
                        IsRead = _email.IsRead,
                        Status = _email.Status,
                    };

                }
                else
                {
                    throw new Exception("Error occured while saving service user to database");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Email> Read(int emailId)
        {
            try
            {
                var email = await _dBContext.Emails
                    .Where(x => x.EmailId == emailId)
                    .FirstOrDefaultAsync();

                if (email != null)
                {
                    return new Email
                    {
                        SenderId = email.SenderId,
                        RecipientId = email.RecipientId,
                        Subject = email.Subject,
                        Body = email.Body,
                        SentDate = email.SentDate,
                        IsRead = email.IsRead,
                        Status = email.Status,
                    };
                }
                else
                {
                    throw new Exception("Error occured while saving service user to database");
                    // Handle the exception appropriately based on your application needs
                }
            }
            catch (Exception e)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while fetching email from the database", e);
            }
        }


        public async Task<Email> Update (Email email)
        {
            try
            {
                var original = await _dBContext.Emails.FindAsync(email.EmailId);

                original.SenderId = email.SenderId;
                original.RecipientId = email.RecipientId;
                original.Subject = email.Subject;
                original.Body = email.Body;
                original.SentDate = DateTime.Now;
                original.IsRead = email.IsRead;
                original.Status = email.Status;
                if (await _dBContext.SaveChangesAsync() > 0)
                {
                    return new Email
                    {
                        SenderId = original.SenderId,
                        RecipientId = original.RecipientId,
                        Subject = original.Subject,
                        Body = original.Body,
                        SentDate = DateTime.Now,
                        IsRead = original.IsRead,
                        Status = original.Status,
                    };
                }
                else
                {
                    throw new Exception("Error occured while updating Servicegiver to database");
                }

            }
            catch(Exception e)
            {
                throw;
            }
        }


        public async Task DeleteEmail(int emailId)
        {
            try
            {
                var emailToDelete = await _dBContext.Emails
                    .Where(x => x.EmailId == emailId)
                    .FirstOrDefaultAsync();

                if (emailToDelete != null)
                {
                    _dBContext.Emails.Remove(emailToDelete);
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Error occured while updating Servicegiver to database");
                    // Handle the exception appropriately based on your application needs
                }
            }
            catch (Exception e)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while deleting email from the database", e);
            }
        }

    }
}
