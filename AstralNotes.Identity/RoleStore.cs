using System;
using System.Threading;
using System.Threading.Tasks;
using AstralNotes.Database;
using Microsoft.AspNetCore.Identity;

namespace AstralNotes.Identity
{
    /// <summary>
    /// Заглушка сервиса ролей
    /// </summary>
    public class RoleStore: IRoleStore<AccessPolicy>
    {
        private readonly DatabaseContext _context;

        /// <summary/>
        public RoleStore(DatabaseContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public Task<IdentityResult> CreateAsync(AccessPolicy role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IdentityResult> UpdateAsync(AccessPolicy role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<IdentityResult> DeleteAsync(AccessPolicy role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetRoleIdAsync(AccessPolicy role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetRoleNameAsync(AccessPolicy role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task SetRoleNameAsync(AccessPolicy role, string roleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<string> GetNormalizedRoleNameAsync(AccessPolicy role, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task SetNormalizedRoleNameAsync(AccessPolicy role, string normalizedName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<AccessPolicy> FindByIdAsync(string roleId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public Task<AccessPolicy> FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
