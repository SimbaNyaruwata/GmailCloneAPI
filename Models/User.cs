using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

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

    //  public virtual ICollection<Contact> Contacts { get; set; } = new List<Contact>();

    //public virtual ICollection<Email> Emails { get; set; } = new List<Email>();

    // Additional properties and methods as needed

    public void SetPassword(string password)
    {
        // Generate a random salt
        byte[] salt = GenerateSalt();

        // Hash the password along with the salt
        string hashedPassword = HashPassword(password, salt);

        // Combine the hashed password and salt, and store it in the PasswordHash property
        PasswordHash = $"{Convert.ToBase64String(salt)}:{hashedPassword}";
    }

    public bool VerifyPassword(string password)
    {
        // Split the stored PasswordHash into salt and hashed password
        string[] parts = PasswordHash.Split(':');
        if (parts.Length != 2)
        {
            return false; // Invalid format
        }

        byte[] salt = Convert.FromBase64String(parts[0]);
        string hashedPassword = parts[1];

        // Hash the input password with the stored salt
        string hashedInputPassword = HashPassword(password, salt);

        // Compare the computed hash with the stored hash
        return hashedPassword == hashedInputPassword;
    }

    private byte[] GenerateSalt()
    {
        // Generate a random salt using a cryptographic random number generator
        byte[] salt = new byte[32];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(salt);
        }
        return salt;
    }

    private string HashPassword(string password, byte[] salt)
    {
        // Combine the password and salt, and hash using SHA-256
        using (var sha256 = SHA256.Create())
        {
            byte[] combinedBytes = Encoding.UTF8.GetBytes(password).Concat(salt).ToArray();
            byte[] hashedBytes = sha256.ComputeHash(combinedBytes);
            return Convert.ToBase64String(hashedBytes);
        }
    }
}
