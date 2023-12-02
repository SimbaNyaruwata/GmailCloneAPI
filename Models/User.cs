using System;
using System.Collections.Generic;

namespace GmailClone.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int SenderId { get; set; }

    public int RecipientId { get; set; }

    public string PasswordHash { get; set; } = null!;

    public int? Status { get; set; }

    public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    public virtual ICollection<Email> Emails { get; set; } = new List<Email>();
}
