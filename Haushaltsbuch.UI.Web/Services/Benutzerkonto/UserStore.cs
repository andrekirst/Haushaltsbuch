using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Haushaltsbuch.UI.Web.Services.Benutzerkonto
{
    public class UserStore :
        IUserStore<WebApi.Benutzerkonto.Models.Benutzerkonto>,
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

        public Task<IdentityResult> CreateAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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
            return BenutzerkontoService.FindByNameAsync(
                anmeldenummer: normalizedUserName,
                cancellationToken: cancellationToken);
        }

        public Task<string> GetNormalizedUserNameAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserIdAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetUserNameAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedUserNameAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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

        public Task<IList<Claim>> GetClaimsAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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

        public Task<IList<string>> GetRolesAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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

        public Task<string> GetPasswordHashAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> HasPasswordAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetPasswordHashAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string passwordHash, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetSecurityStampAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetSecurityStampAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, string stamp, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}