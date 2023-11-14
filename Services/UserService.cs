using GmailClone.Models;
using GmailClone.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace GmailClone.Services
{
    public class UserService : BaseService
    {
        public UserService(GmailCloneDbContext dBContext) : base(dBContext)
        {
        }

        public async Task<User> Create(User user)
        {
            try
            {
                var u = new User
                {
                    UserName = user.UserName,
                    Email = user.Email,
                    SenderId = user.SenderId,
                    RecipientId = user.RecipientId,
                    PasswordHash = user.PasswordHash,
                    Status = user.Status,
                };
                if(await _dBContext.SaveChangesAsync() > 0)
                {
                    var _user = await
                    _dBContext.Users
                    .Where(x => x.UserId == u.UserId)
                    .FirstOrDefaultAsync();
                    return new User
                    {
                        UserName = _user.UserName,
                        Email = _user.Email,
                        SenderId = _user.SenderId,
                        RecipientId = _user.RecipientId,
                        PasswordHash = _user.PasswordHash,
                        Status = _user.Status,
                    };
                }
                else
                {
                    throw new Exception("Error occured while saving service user to database");
                }
            }
            catch (Exception e) 
            {
                throw;
            }
        }


        public async Task<User> Read(int UserId)
        {
            try
            {
                var user = await _dBContext.Users
                    .Where(x => x.UserId == UserId)
                    .FirstOrDefaultAsync();

                if (user != null)
                {
                    return new User
                    {
                        UserId = user.UserId,
                        UserName = user.UserName,
                        Email = user.Email,
                        SenderId = user.SenderId,
                        RecipientId = user.RecipientId,
                        PasswordHash = user.PasswordHash,
                        Status = user.Status,
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
                throw new Exception("Error occurred while fetching user from the database", e);
            }
        }



        public async Task<User> Update(User user)
        {
            try
            {
                var original = await _dBContext.Users.FindAsync(user.UserId);

                original.UserName = user.UserName;
                original.Email = user.Email;
                original.SenderId = user.SenderId;
                original.RecipientId = user.RecipientId;
                original.PasswordHash = user.PasswordHash;
                original.Status = user.Status;
                if (await _dBContext.SaveChangesAsync() > 0)
                {
                    return new User
                    {
                        UserName = original.UserName,
                        Email = original.Email,
                        SenderId = original.SenderId,
                        RecipientId = original.RecipientId,
                        PasswordHash = original.PasswordHash,
                        Status = original.Status,

                    };
                }
                else
                {
                    throw new Exception("Error occured while updating Servicegiver to database");
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public async Task Delete(int UserId)
        {
            try
            {
                var userToDelete = await _dBContext.Users
                    .Where(x => x.UserId == UserId)
                    .FirstOrDefaultAsync();

                if (userToDelete != null)
                {
                    _dBContext.Users.Remove(userToDelete);
                    await _dBContext.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Error occured, user not found in database");
                    // Handle the exception appropriately based on your application needs
                }
            }
            catch (Exception e)
            {
                // Log the exception or handle it as needed
                throw new Exception("Error occurred while deleting user from the database", e);
            }
        }



    }
}

