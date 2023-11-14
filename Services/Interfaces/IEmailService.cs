using GmailClone.Models;

namespace GmailClone.Services.Interfaces
{
    public interface IEmailService
    {
        Task<Email> Create(Email email);
        Task<Email> Read(Email email);
        Task<Email> Update(Email email);
        Task<Email> Delete(Email email);

    }
}
