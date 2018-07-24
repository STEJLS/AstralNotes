using System;
using System.Linq;
using System.Threading.Tasks;
using AstralNotes.Database;
using AstralNotes.Domain.Models;
using AstralNotes.Identity.DependencyInjection;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace AstralNotes.Identity
{
    /// <summary>
    /// Провайдер <see cref="SessionContext"/> на основе контекста запроса
    /// </summary>
    public class SessionContextProvider: IProvider<SessionContext>
    {
        private readonly DatabaseContext _databaseContext;
        private readonly HttpContext _httpContext;

        /// <summary/>
        public SessionContextProvider(DatabaseContext databaseContext, IHttpContextAccessor httpContext)
        {
            _databaseContext = databaseContext;
            _httpContext = httpContext.HttpContext;
        }

        /// <inheritdoc />
        public SessionContext Get()
        {
            var name = _httpContext?.User?.Identity.Name;

            return name == null ? new SessionContext() : GetContext(new Guid(name));
        }

        private SessionContext GetContext(Guid userGuid)
        {
            var user = _databaseContext.Users.SingleOrDefault(a => a.UserGuid == userGuid);
            return user == null
                ? new SessionContext()
                : new SessionContext(user);
        }
        
    }
}
