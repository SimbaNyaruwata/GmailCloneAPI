using GmailClone.Models;
using Microsoft.EntityFrameworkCore.Update;

namespace GmailClone.Services.Interfaces
{
    public interface IAttachmentService
    {
        Task<Attachment> Create (Attachment attachment);
        Task<Attachment> Read (Attachment attachment);
        Task<Attachment> Update (Attachment attachment);
        Task<Attachment> Delete (Attachment attachment);
    }
}
