using GmailClone.Models;
using Microsoft.AspNetCore.Mvc;

namespace GmailClone.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Create(User user);

        Task<User> Read(User user);

        Task<User> Update(User user);
        Task<User> Delete(User user);
        
        
    }
}
