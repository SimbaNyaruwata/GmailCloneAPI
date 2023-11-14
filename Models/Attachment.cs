using System;
using System.Collections.Generic;

namespace GmailClone.Models;

public partial class Attachment
{
    public int AttachmentId { get; set; }

    public int EmailId { get; set; }

    public string FileName { get; set; } = null!;

    public string ContentType { get; set; } = null!;

    public byte[] Data { get; set; } = null!;

    public int? Status { get; set; }

    public virtual Email Email { get; set; } = null!;
}
