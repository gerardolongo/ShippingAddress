using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GestioneIndirizzi.ExceptionCustom;
using GestioneIndirizzi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestioneIndirizzi.Controllers
{
    [Produces("application/json")]
    public class BaseController : Controller
    {
        private readonly GestioneIndirizziContext _context;
        private Authentication authentication;

        public BaseController(GestioneIndirizziContext context)
        {
            _context = context;
        }


        protected IActionResult GetAuthentication()
        {
            authentication = new Authentication(_context, Request);
            if (!authentication.checkUserId())
            {
                throw new UnauthorizedException("Unauthorized");                
            }

            return Ok();
        }

        protected int GetUserId() => authentication.getUserId();

        protected GestioneIndirizziContext Context()
        {
            return _context;
        }

        protected void checkFound<E>(E item, int id)
        {
            if (item == null)
                throw new NotFoundException(String.Format("Element with id : {0} not found", id));
        }
    }
}