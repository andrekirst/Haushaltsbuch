using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Haushaltsbuch.WebApi.Benutzerkonto.Models;
using Microsoft.AspNetCore.Identity;

namespace Haushaltsbuch.UI.Web.Services.Benutzerkonto
{
    public class RoleStore :
        IRoleStore<Benutzerrolle>,
        IUserRoleStore<WebApi.Benutzerkonto.Models.Benutzerkonto>
    {
        public void Dispose()
        {
        }

        public Task<IdentityResult> CreateAsync(Benutzerrolle role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(Benutzerrolle role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> CreateAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> DeleteAsync(WebApi.Benutzerkonto.Models.Benutzerkonto user, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        Task<WebApi.Benutzerkonto.Models.Benutzerkonto> IUserStore<WebApi.Benutzerkonto.Models.Benutzerkonto>.FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        Task<WebApi.Benutzerkonto.Models.Benutzerkonto> IUserStore<WebApi.Benutzerkonto.Models.Benutzerkonto>.FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
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

        Task<Benutzerrolle> IRoleStore<Benutzerrolle>.FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        Task<Benutzerrolle> IRoleStore<Benutzerrolle>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetNormalizedRoleNameAsync(Benutzerrolle role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetRoleIdAsync(Benutzerrolle role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<string> GetRoleNameAsync(Benutzerrolle role, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetNormalizedRoleNameAsync(Benutzerrolle role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task SetRoleNameAsync(Benutzerrolle role, string roleName, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public Task<IdentityResult> UpdateAsync(Benutzerrolle role, CancellationToken cancellationToken)
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
    }
}