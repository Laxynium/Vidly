using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Domain.Entities;
using Vidly.Models;

namespace Vidly.ViewModels
{
    public class CustomerFormViewModel
    {
        public IEnumerable<MembershipType> MembershipTypes { get; set; }
        public Customer Customer { get; set; }

        public string Title => Customer.Id != 0 ? "Edit Customer" : "Add Customer";
    }
}