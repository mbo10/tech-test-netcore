using System.Threading.Tasks;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Todo.Services
{
    public interface IGravatar
    {
        public string GetHash(string emailAddress);
        Task<ProfileInfo> GetInfoAsync(string email);
        Task<string> GenerateUserHtmlAsync(string emailAddress);
    }
}