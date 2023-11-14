using System;
using System.Collections.Generic;

namespace GmailClone.Models;

public partial class Email
{
    public int EmailId { get; set; }

    public int SenderId { get; set; }

    public int RecipientId { get; set; }

    public string Subject { get; set; } = null!;

    public string Body { get; set; } = null!;

    public DateTime SentDate { get; set; }

    public bool IsRead { get; set; }

    public int? Status { get; set; }

    public virtual ICollection<Attachment> Attachments { get; set; } = new List<Attachment>();

    public virtual User Recipient { get; set; } = null!;
}
