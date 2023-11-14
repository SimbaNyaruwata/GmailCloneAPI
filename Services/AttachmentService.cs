using GmailClone.Models;
using Microsoft.EntityFrameworkCore;

namespace GmailClone.Services
{
    public class AttachmentService: BaseService
    {
        public AttachmentService(GmailCloneDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<Attachment>Create (Attachment attachment)
        {
            try
            {
                var a = new Attachment
                {
                    EmailId = attachment.EmailId,
                    FileName = attachment.FileName,
                    ContentType = attachment.ContentType,
                    Data = attachment.Data,
                    Status = attachment.Status,
                };
                if (await _dBContext.SaveChangesAsync()>0)
                {
                    var _attachment = await
                    _dBContext.Attachments
                    .Where(x => x.AttachmentId == a.AttachmentId)
                    .FirstOrDefaultAsync();
                    return new Attachment
                    {
                        EmailId = _attachment.EmailId,
                        FileName = _attachment.FileName,
                        ContentType = _attachment.ContentType,
                        Data = _attachment.Data,
                        Status = _attachment.Status,
                    };
                }
                else
                {
                    throw new Exception("Error occurred while saving service user to database");
                }
                
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task<Attachment> Read(int attachmentId)
        {
            try
            {
                var attachment = await _dBContext.Attachments
                    .Where(x => x.AttachmentId == attachmentId)
                    .FirstOrDefaultAsync();

                if (attachment != null)
                {
                    return new Attachment
                    {
                        EmailId = attachment.EmailId,
                        FileName = attachment.FileName,
                        ContentType = attachment.ContentType,
                        Data = attachment.Data,
                        Status = attachment.Status,
                    };
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
                throw new Exception("Error occurred while fetching attachment from the database", e);
            }
        }



        public async Task<Attachment> Update (Attachment attachment)
        {
            try
            {
                var original = await _dBContext.Attachments.FindAsync(attachment.AttachmentId);

                original.EmailId = attachment.EmailId;
                original.FileName = attachment.FileName;
                original.ContentType = attachment.ContentType;
                original.Data = attachment.Data;
                original.Status = attachment.Status;
                if (await _dBContext.SaveChangesAsync()>0)
                {
                    return new Attachment
                    {
                        EmailId = original.EmailId,
                        FileName = original.FileName,
                        ContentType = original.ContentType,
                        Data = original.Data,
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

        public async Task DeleteAttachment(int attachmentId)
        {
            try
            {
                var attachmentToDelete = await _dBContext.Attachments
                    .Where(x => x.AttachmentId == attachmentId)
                    .FirstOrDefaultAsync();

                if (attachmentToDelete != null)
                {
                    _dBContext.Attachments.Remove(attachmentToDelete);
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
                throw new Exception("Error occurred while deleting attachment from the database", e);
            }
        }


    }
}
