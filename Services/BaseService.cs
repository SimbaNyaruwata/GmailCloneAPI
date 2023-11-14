using GmailClone.Models;
using System.Data;

namespace GmailClone.Services
{
    public class BaseService
    {
        public GmailCloneDbContext _dBContext;
        public BaseService(GmailCloneDbContext dBContext)
        {
            _dBContext = dBContext;
        }
    }
}
