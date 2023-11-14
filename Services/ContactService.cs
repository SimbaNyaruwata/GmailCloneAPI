using GmailClone.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;

namespace GmailClone.Services
{
    public class ContactService : BaseService
    {
        public ContactService(GmailCloneDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<Contact> Create (Contact contact)
        {
            try
            {
                var c = new Contact
                {
                    UserId = contact.UserId,
                    FirstName = contact.FirstName,
                    LastName = contact.LastName,
                    Email = contact.Email,
                    Status = contact.Status,
                };
                if (await _dBContext.SaveChangesAsync()>0)
                {
                    var _contact = await
                    _dBContext.Contacts
                    .Where(x => x.ContactId == c.ContactId)
                    .FirstOrDefaultAsync();
                    return new Contact
                    {
                        UserId = _contact.UserId,
                        FirstName = _contact.FirstName,
                        LastName = _contact.LastName,
                        Email = _contact.Email,
                        Status = _contact.Status,
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

        public async Task<Contact> Read(int contactId)
        {
            try
            {
                var contact = await _dBContext.Contacts
                    .Where(x => x.ContactId == contactId)
                    .FirstOrDefaultAsync();

                if (contact != null)
                {
                    return new Contact
                    {
                        UserId = contact.UserId,
                        FirstName = contact.FirstName,
                        LastName = contact.LastName,
                        Email = contact.Email,
                        Status = contact.Status,
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
                throw new Exception("Error occurred while fetching contact from the database", e);
            }
        }


        public async Task<Contact> Update(Contact contact)
        {
            try
            {
                var original = await _dBContext.Contacts.FindAsync(contact.ContactId);

                original.UserId = contact.UserId;
                original.FirstName = contact.FirstName;
                original.LastName = contact.LastName;
                original.Email = contact.Email;
                original.Status = contact.Status;
                if (await _dBContext.SaveChangesAsync()>0)
                {
                    return new Contact
                    {
                        UserId = original.UserId,
                        FirstName = original.FirstName,
                        LastName = original.LastName,
                        Email = original.Email,
                        Status = original.Status,
                    };

                }
                else
                {
                    throw new Exception("Error occured while updating Servicegiver to database");
                }
            }
            catch (Exception e) 
            {
                throw;
            }
        }

        public async Task Delete(int contactId)
        {
            try
            {
                var contactToDelete = await _dBContext.Contacts
                    .Where(x => x.ContactId == contactId)
                    .FirstOrDefaultAsync();

                if (contactToDelete != null)
                {
                    _dBContext.Contacts.Remove(contactToDelete);
                    await _dBContext.SaveChangesAsync();
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
                throw new Exception("Error occurred while deleting contact from the database", e);
            }
        }


    }
}
