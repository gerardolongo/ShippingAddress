using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using GestioneIndirizzi.ExceptionCustom;
using GestioneIndirizzi.Models;
using GestioneIndirizzi.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace GestioneIndirizzi.Controllers
{
    [Produces("application/json")]
    [Route("api/shipping-addresses")]
    public class ShippingAddressesController : BaseController
    {
        public ShippingAddressesController(GestioneIndirizziContext context)
            : base(context)
        {

        }

        [Route("{id:int}/history")]
        [HttpGet]
        public IActionResult History(int id)
        {
            IEnumerable<ShippingAddressAudit> shippingAddressesAudit = null;
            try
            {
                GetAuthentication();

                shippingAddressesAudit = Context().ShippingAddressAudit
                    .Where(x => x.id_shipping == id).ToList();

            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(shippingAddressesAudit);

        }

        [HttpGet]
        public IActionResult ShippingAddresses(int offset, int limit, string q)
        {
            IEnumerable<ShippingAddress> shippingAddresses = null;
            try
            {
                base.GetAuthentication();

                shippingAddresses = Context().ShippingAddress
                   .Where(x => x.tenant_id == GetUserId())
                   .OrderBy(c => c.id)
                   .Skip(offset)
                   .Take(limit)
                   .Where(x => x.label.Contains(q) || x.last_name.Contains(q) || x.first_name.Contains(q))
                   .ToList();
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(shippingAddresses);

        }

        [HttpPost]
        public IActionResult ShippingAddresses([FromBody] ShippingAddress item)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    base.GetAuthentication();

                    if (item == null)
                    {
                        return BadRequest();
                    }

                    item.label = item.first_name + "_" + item.last_name + "_" + Guid.NewGuid();
                    item.tenant_id = GetUserId();

                    Context().ShippingAddress.Add(item);
                    Context().SaveChanges();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Ok(item);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult ShippingAddresses(int id)
        {
            try
            {
                base.GetAuthentication();

                if (id <= 0)
                    return BadRequest("Not a valid shipping address id");

                var shippingAddress = Context().ShippingAddress
                    .Where(x => x.id == id).FirstOrDefault();

                base.checkFound<ShippingAddress>(shippingAddress, id);

                Context().ShippingAddress.Remove(shippingAddress);
                Context().SaveChanges();
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Ok("operation successful");
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IActionResult ShippingAddresses(int id, [FromBody] ShippingAddress item)
        {
            try
            {
                base.GetAuthentication();

                if (item == null)
                {
                    return BadRequest();
                }

                var shippingAddress = Context().ShippingAddress.FirstOrDefault(c => c.id == id);
                base.checkFound<ShippingAddress>(shippingAddress, id);

                writeOnAudit(shippingAddress.id, item);

                PatchValidator.patchValidation(shippingAddress, item);

                Context().ShippingAddress.Update(shippingAddress);
                Context().SaveChanges();

            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500);
            }

            return Ok(item);
        }


        private void writeOnAudit(int id, ShippingAddress item)
        {
            Context().ShippingAddressAudit.Add(new ShippingAddressAudit()
            {
                id_shipping = id,
                first_name = item.first_name,
                last_name = item.last_name,
                company = item.company,
                address = item.address,
                city = item.city,
                country = item.country,
                province = item.province,
                postal_code = item.postal_code,
                phone = item.phone,
                date_upd = DateTime.Now
            });
        }
    }
}