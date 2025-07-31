using ESApplication.Models;
using ESDomain.IRepositories;
using Microsoft.AspNetCore.Authentication;
using System.DirectoryServices.AccountManagement;
using System.Security.Principal;

namespace ESAPI.Providers
{
    public class AdUserProvider : IUserProvider
    {
        public AdUser CurrentUser
        {
            get;
            set;
        }
        public bool Initialized
        {
            get;
            set;
        }
        public async Task Create(HttpContext context, IConfiguration config)
        {
            CurrentUser = await GetAdUser(context.User.Identity);
            Initialized = true;
        }
        public Task<AdUser> GetAdUser(IIdentity identity)
        {
            return Task.Run(() =>
            {
                try
                {
                    PrincipalContext context = new PrincipalContext(ContextType.Domain);
                    UserPrincipal principal = new UserPrincipal(context);
                    if (context != null)
                    {
                        principal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, identity.Name);
                    }
                    return CastToAdUser(principal);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving AD User", ex);
                }
            });
        }
        public Task<AdUser> GetAdUser(string samAccountName)
        {
            return Task.Run(() =>
            {
                try
                {
                    PrincipalContext context = new PrincipalContext(ContextType.Domain);
                    UserPrincipal principal = new UserPrincipal(context);
                    if (context != null)
                    {
                        principal = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, samAccountName);
                    }
                    return CastToAdUser(principal);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving AD User", ex);
                }
            });
        }
        public Task<AdUser> GetAdUser(Guid guid)
        {
            return Task.Run(() =>
            {
                try
                {
                    PrincipalContext context = new PrincipalContext(ContextType.Domain);
                    UserPrincipal principal = new UserPrincipal(context);
                    if (context != null)
                    {
                        principal = UserPrincipal.FindByIdentity(context, IdentityType.Guid, guid.ToString());
                    }
                    return CastToAdUser(principal);
                }
                catch (Exception ex)
                {
                    throw new Exception("Error retrieving AD User", ex);
                }
            });
        }

        public AdUser CastToAdUser(UserPrincipal user)
        {
            return new AdUser
            {
                //AccountExpirationDate = user.AccountExpirationDate,
                //AccountLockoutTime = user.AccountLockoutTime,
                //BadLogonCount = user.BadLogonCount,
                //Description = user.Description,
                //DisplayName = user.DisplayName,
                //DistinguishedName = user.DistinguishedName,
                //EmailAddress = user.EmailAddress,
                //EmployeeId = user.EmployeeId,
                //Enabled = user.Enabled,
                //GivenName = user.GivenName,
                //Guid = user.Guid,
                //HomeDirectory = user.HomeDirectory,
                //HomeDrive = user.HomeDrive,
                //LastBadPasswordAttempt = user.LastBadPasswordAttempt,
                //LastLogon = user.LastLogon,
                //LastPasswordSet = user.LastPasswordSet,
                //MiddleName = user.MiddleName,
                //Name = user.Name,
                //PasswordNeverExpires = user.PasswordNeverExpires,
                //PasswordNotRequired = user.PasswordNotRequired,
                //SamAccountName = user.SamAccountName,
                //ScriptPath = user.ScriptPath,
                //Sid = user.Sid,
                //Surname = user.Surname,
                //UserCannotChangePassword = user.UserCannotChangePassword,
                //UserPrincipalName = user.UserPrincipalName,
                //VoiceTelephoneNumber = user.VoiceTelephoneNumber,
                //  Token = string.Empty,
            };
        }
        //public Task<List<AdUser>> GetDomainUsers()
        //{
        //    return Task.Run(() =>
        //    {
        //        PrincipalContext context = new PrincipalContext(ContextType.Domain);
        //        UserPrincipal principal = new UserPrincipal(context);
        //        principal.UserPrincipalName = "*@*";
        //        principal.Enabled = true;
        //        PrincipalSearcher searcher = new PrincipalSearcher(principal);
        //        var users = searcher.FindAll().Take(50).AsQueryable().Cast<UserPrincipal>().FilterUsers().SelectAdUsers().OrderBy(x => x.Surname).ToList();
        //        return users;
        //    });
        //}
        //public Task<List<AdUser>> FindDomainUser(string search)
        //{
        //    return Task.Run(() =>
        //    {
        //        PrincipalContext context = new PrincipalContext(ContextType.Domain);
        //        UserPrincipal principal = new UserPrincipal(context);
        //        principal.SamAccountName = $ "*{search}*";
        //        principal.Enabled = true;
        //        PrincipalSearcher searcher = new PrincipalSearcher(principal);
        //        var users = searcher.FindAll().AsQueryable().Cast<UserPrincipal>().FilterUsers().SelectAdUsers().OrderBy(x => x.Surname).ToList();
        //        return users;
        //    });
        //}
    }
}
