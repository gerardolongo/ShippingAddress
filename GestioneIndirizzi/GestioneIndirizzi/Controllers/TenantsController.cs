using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GestioneIndirizzi.Models;
using Microsoft.AspNetCore.Mvc;

namespace GestioneIndirizzi.Controllers
{
    [Produces("application/json")]
    [Route("admin/Tenants")]
    public class TenantsController : Controller
    {
        private readonly GestioneIndirizziContext _context;

        public TenantsController(GestioneIndirizziContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Tenants()
        {
            List<Tenant> tenants = new List<Tenant>();
            try
            {
                if (_context.Tenant.Count() > 0)
                    tenants = _context.Tenant.Select(x => new Tenant { tenant_id = x.tenant_id }).ToList();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(tenants);
        }


        [HttpPost]
        public IActionResult Tenants([FromBody] Tenant item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (item == null)
                        return BadRequest();

                    if (_context.Tenant.Any(x => x.tenant_id.Equals(item.tenant_id)))
                        return BadRequest("Tenant: " + item.tenant_id + " exists");

                    _context.Tenant.Add(item);
                    _context.SaveChanges();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(new { id = item.id, tenant_id = item.tenant_id });
        }
    }
}
