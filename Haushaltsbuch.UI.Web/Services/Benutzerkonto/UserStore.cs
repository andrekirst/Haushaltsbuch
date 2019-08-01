using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Haushaltsbuch.UI.Web.Services.Benutzerkonto
{
    public class UserStore :
        IUserStore<WebApi.Benutzerkonto.Models.Benutzerkonto>,
        IUserEmailStore<WebApi.Benutzerkonto.Models.Benutzerkonto>,
        IUserClaimStore<WebApi.Benutzerkonto.Models.Benutzerkonto>,
        IUserLoginStore<WebApi.Benutzerkonto.Models.Benutzerkonto>,
        IUserRoleStore<WebApi.Benutzerkonto.Models.Benutzerkonto>,
        IUserPasswordStore<WebApi.Benutzerkonto.Models.Benutzerkonto>,
        IUserSecurityStampStore<WebApi.Benutzerkonto.Models.Benutzerkonto>
    {
        private IBenutzerkontoService BenutzerkontoService { get; }

        public UserStore(IBenutzerkontoService benutzerkontoService)
        {
            BenutzerkontoService = benutzerkontoService;
        }

        public void Dispose()
        {
        }

        public async Task<IdentityResult> CreateAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            bool successfully = await BenutzerkontoService.Registrieren(
                anmeldenummer: user.UserName,
                email: user.Email,
                passwortHash: user.PasswordHash,
                securityStamp: user.SecurityStamp);

            return successfully ? IdentityResult.Success : IdentityResult.Failed();
        }

        public Task<IdentityResult> DeleteAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<WebApi.Benutzerkonto.Models.Benutzerkonto> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<WebApi.Benutzerkonto.Models.Benutzerkonto> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            return BenutzerkontoService.SucheAnhandAnmeldenummer(
                anmeldenummer: normalizedUserName,
                cancellationToken: cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            return BenutzerkontoService.LiefereUserId(anmeldenummer: user.UserName, cancellationToken: cancellationToken);
        }

        public async Task<string> GetUserNameAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            //return BenutzerkontoService.LiefereUserName(anmeldenummer: user.UserName, cancellationToken: cancellationToken);
            return user.UserName;
        }

        public async Task SetNormalizedUserNameAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
        }

        public Task SetUserNameAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string userName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task AddClaimsAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<Claim>> GetClaimsAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            return new List<Claim>();
        }

        public Task<IList<WebApi.Benutzerkonto.Models.Benutzerkonto>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveClaimsAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, IEnumerable<Claim> claims, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task ReplaceClaimAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, Claim claim, Claim newClaim, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task AddLoginAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, UserLoginInfo login, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<WebApi.Benutzerkonto.Models.Benutzerkonto> FindByLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IList<UserLoginInfo>> GetLoginsAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveLoginAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string loginProvider, string providerKey,
            CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task AddToRoleAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<IList<string>> GetRolesAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            return new List<string>();
        }

        public Task<IList<WebApi.Benutzerkonto.Models.Benutzerkonto>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> IsInRoleAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task RemoveFromRoleAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetPasswordHashAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken) =>
            BenutzerkontoService.LieferePasswordHash(anmeldenummer: user.UserName, cancellationToken: cancellationToken);

        public Task<bool> HasPasswordAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetPasswordHashAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
        }

        public async Task<string> GetSecurityStampAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            return user.SecurityStamp ?? await BenutzerkontoService.LiefereSecurityStamp(
                       anmeldenummer: user.UserName,
                       cancellationToken: cancellationToken);
        }

        public async Task SetSecurityStampAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string stamp, CancellationToken cancellationToken)
        {
            user.SecurityStamp = stamp;
            //return BenutzerkontoService.SetzeSecurityStamp(anmeldenummer: user.UserName, securityStamp: stamp,
            //    cancellationToken: cancellationToken);
        }

        public Task<WebApi.Benutzerkonto.Models.Benutzerkonto> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
        {
            return BenutzerkontoService.SucheAnhandEMail(email: normalizedEmail, cancellationToken: cancellationToken);
        }

        public async Task<string> GetEmailAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            return user.Email;
            //return BenutzerkontoService.LiefereEMail(
            //    anmeldenummer: user.UserName,
            //    cancellationToken: cancellationToken);
        }

        public Task<bool> GetEmailConfirmedAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedEmailAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string email, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetEmailConfirmedAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, bool confirmed, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task SetNormalizedEmailAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string normalizedEmail, CancellationToken cancellationToken)
        {
            user.NormalizedEmail = normalizedEmail;
            //return BenutzerkontoService.SetzeNormalisierteEMailAdresse(
            //    anmeldenummer: user.UserName,
            //    normalisierteEMail: normalizedEmail,
            //    cancellationToken: cancellationToken);
        }
    }
}