using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace GmailClone.Models;

public partial class GmailCloneDbContext : DbContext
{
    public GmailCloneDbContext()
    {
    }

    public GmailCloneDbContext(DbContextOptions<GmailCloneDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Attachment> Attachments { get; set; }

    public virtual DbSet<Contact> Contacts { get; set; }

    public virtual DbSet<Email> Emails { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=email-server911.database.windows.net;User Id =simbakeith;password=632138431p50#A;Database=GmailCloneDB;Trusted_Connection=False;Encrypt=True");
        //.LogTo(Console.WriteLine, LogLevel.Information);

    


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attachment>(entity =>
        {
            entity.Property(e => e.ContentType).HasMaxLength(50);
            entity.Property(e => e.FileName).HasMaxLength(255);

           /* entity.HasOne(d => d.Email).WithMany(p => p.Attachments)
                .HasForeignKey(d => d.EmailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Attachments_Emails1");*/
        });

        modelBuilder.Entity<Contact>(entity =>
        {
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);

            /*entity.HasOne(d => d.User).WithMany(p => p.Contacts)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Contacts_Users1");*/
        });

        modelBuilder.Entity<Email>(entity =>
        {
            entity.HasIndex(e => e.EmailId, "IX_Emails").IsUnique();

            entity.Property(e => e.SentDate).HasColumnType("datetime");
            entity.Property(e => e.Subject).HasMaxLength(255);

           /* entity.HasOne(d => d.Recipient).WithMany(p => p.Emails)
                .HasForeignKey(d => d.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Emails_Users1");*/
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_Users").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }



       partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
