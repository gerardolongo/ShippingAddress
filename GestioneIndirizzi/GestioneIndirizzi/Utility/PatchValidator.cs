using GestioneIndirizzi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestioneIndirizzi.Utility
{
    public class PatchValidator 
    {
        public static void patchValidation(ShippingAddress shippingAddresses, ShippingAddress toUpdate)
        {
            shippingAddresses.first_name = toUpdate.first_name != null ? toUpdate.first_name : shippingAddresses.first_name;
            shippingAddresses.last_name = toUpdate.last_name != null ? toUpdate.last_name : shippingAddresses.last_name;
            shippingAddresses.company = toUpdate.company != null ? toUpdate.company : shippingAddresses.company;
            shippingAddresses.address = toUpdate.address != null ? toUpdate.address : shippingAddresses.address;
            shippingAddresses.country = toUpdate.country != null ? toUpdate.country : shippingAddresses.country;
            shippingAddresses.province = toUpdate.province != null ? toUpdate.province : shippingAddresses.province;
            shippingAddresses.postal_code = toUpdate.postal_code != null ? toUpdate.postal_code : shippingAddresses.postal_code;
            shippingAddresses.phone = toUpdate.phone != null ? toUpdate.phone : shippingAddresses.phone;

        }

        public static void patchValidation(BillingAddress billingAddresses, BillingAddress toUpdate)
        {
            billingAddresses.first_name = toUpdate.first_name != null ? toUpdate.first_name : billingAddresses.first_name;
            billingAddresses.last_name = toUpdate.last_name != null ? toUpdate.last_name : billingAddresses.last_name;
            billingAddresses.company = toUpdate.company != null ? toUpdate.company : billingAddresses.company;
            billingAddresses.address = toUpdate.address != null ? toUpdate.address : billingAddresses.address;
            billingAddresses.country = toUpdate.country != null ? toUpdate.country : billingAddresses.country;
            billingAddresses.province = toUpdate.province != null ? toUpdate.province : billingAddresses.province;
            billingAddresses.postal_code = toUpdate.postal_code != null ? toUpdate.postal_code : billingAddresses.postal_code;
            billingAddresses.phone = toUpdate.phone != null ? toUpdate.phone : billingAddresses.phone;
            billingAddresses.fiscal_code = toUpdate.fiscal_code != null ? toUpdate.fiscal_code : billingAddresses.fiscal_code;
            billingAddresses.vat_code = toUpdate.vat_code != null ? toUpdate.vat_code : billingAddresses.vat_code;
            billingAddresses.email = toUpdate.email != null ? toUpdate.email : billingAddresses.email;

        }

    }
}
