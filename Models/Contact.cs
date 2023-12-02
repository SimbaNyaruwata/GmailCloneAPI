using System;
using System.Collections.Generic;

namespace GmailClone.Models;

public partial class Contact
{
    public int ContactId { get; set; }

    public int UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public int? Status { get; set; }

   // public virtual User User { get; set; } = null!;
}
