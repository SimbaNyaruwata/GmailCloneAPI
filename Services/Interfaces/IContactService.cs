using GmailClone.Models;

namespace GmailClone.Services.Interfaces
{
    public interface IContactService
    {
        Task<Contact> Create (Contact contact);
        Task<Contact> Read (Contact contact);
        Task<Contact> Update (Contact contact); 
        Task<Contact> Delete (Contact contact);

    }
}
