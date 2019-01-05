using GestioneIndirizzi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestioneIndirizzi
{
    public class Authentication
    {
        private readonly GestioneIndirizziContext _context;
        private readonly HttpRequest _request;

        public Authentication(GestioneIndirizziContext context, HttpRequest request)
        {
            _context = context;
            _request = request;
        }

        public int getUserId() => checkUserId()? _context.Tenant.Where(x => x.tenant_id.Equals(userId())).FirstOrDefault().id : 0;

        public bool checkUserId() => _context.Tenant.Any(x => x.tenant_id.Equals(userId()));

        private string userId()
        {
            StringValues values;

            if (_request == null) //only for test
            {
                return "Test1";
            }

            var found = _request.Headers.TryGetValue("X-Tenant-ID", out values);
            return values.FirstOrDefault();
        }

    }
}
