using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestioneIndirizzi.ExceptionCustom;
using GestioneIndirizzi.Models;
using GestioneIndirizzi.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GestioneIndirizzi.Controllers
{
    [Produces("application/json")]
    [Route("api/billing-addresses")]
    public class BillingAddressesController : BaseController
    {

        public BillingAddressesController(GestioneIndirizziContext context)
           : base(context)
        {

        }


        [Route("{id:int}/history")]
        [HttpGet]
        public IActionResult History(int id)
        {
            IEnumerable<BillingAddressAudit> billingAddressesAudit = null;
            try
            {
                base.GetAuthentication();

                billingAddressesAudit = Context().BillingAddressAudit
                                                 .Where(x => x.id_billing == id).ToList();
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(billingAddressesAudit);

        }

        [HttpGet]
        public IActionResult BillingAddresses(int offset, int limit, string q)
        {
            IEnumerable<BillingAddress> billingAddresses = null;
            try
            {
                base.GetAuthentication();

                billingAddresses = Context().BillingAddress
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

            return Ok(billingAddresses);

        }


        [HttpPost]
        public IActionResult BillingAddresses([FromBody] BillingAddress item)
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

                    checkBillingAddresses(item);

                    Context().BillingAddress.Add(item);
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
                return BadRequest(ex.Message.ToString());
            }

            return Ok(item);
        }

        private void checkBillingAddresses(BillingAddress item)
        {
            var billingAddress = Context().BillingAddress
                  .Where(x => x.email == item.email || x.phone == item.phone);

            if (billingAddress.Count() > 0)
                Context().Loginfo.Add(new LogInfo()
                {
                    date = DateTime.Now,
                    log_type = LogType.warning.ToString(),
                    msg = "Email " + item.email + " and Phone " + item.phone + "exists on db"
                });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult BillingAddresses(int id)
        {
            try
            {
                base.GetAuthentication();

                if (id <= 0)
                {
                    return BadRequest("Not a valid shipping address id");
                }

                var billingAddress = Context().BillingAddress
                    .Where(x => x.id == id).FirstOrDefault();
                base.checkFound<BillingAddress>(billingAddress, id);

                Context().BillingAddress.Remove(billingAddress);
                Context().SaveChanges();
            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok("operation successful");
        }

        [HttpPatch]
        [Route("{id:int}")]
        public IActionResult BillingAddresses(int id, [FromBody] BillingAddress item)
        {
            try
            {

                base.GetAuthentication();

                if (item == null)
                {
                    return BadRequest();
                }

                var billingAddress = Context().BillingAddress.FirstOrDefault(c => c.id == id);
                base.checkFound<BillingAddress>(billingAddress, id);

                writeOnAudit(billingAddress.id, item);

                PatchValidator.patchValidation(billingAddress, item);

                Context().BillingAddress.Update(billingAddress);
                Context().SaveChanges();

            }
            catch (UnauthorizedException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (NotFoundException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.ToString());
            }

            return Ok(item);
        }

        private void writeOnAudit(int id, BillingAddress item)
        {
            Context().BillingAddressAudit.Add(new BillingAddressAudit()
            {
                id_billing = id,
                first_name = item.first_name,
                last_name = item.last_name,
                company = item.company,
                address = item.address,
                city = item.city,
                country = item.country,
                province = item.province,
                postal_code = item.postal_code,
                phone = item.phone,
                vat_code = item.vat_code,
                fiscal_code = item.fiscal_code,
                email = item.email,
                date_upd = DateTime.Now
            });
        }

    }
}