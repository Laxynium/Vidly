using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{  
    public class CustomersController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMembershipTypeRepository _membershipTypeRepository;

        public CustomersController(ICustomerRepository customerRepository
            ,IMembershipTypeRepository membershipTypeRepository)
        {
            _customerRepository = customerRepository;
            _membershipTypeRepository = membershipTypeRepository;
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Index()
        {
            return View();
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Details(int id = 1)
        {
            var customer=_customerRepository.GetCustomer(id);   
            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var viewModel = new CustomerFormViewModel
            {
                Customer = new Customer(),
                MembershipTypes = _membershipTypeRepository.GetMembershipTypes().ToList()
            };
            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Customer customer)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _membershipTypeRepository.GetMembershipTypes().ToList()
                };
                return View("CustomerForm",viewModel);
            }

            if (customer.Id == 0)
                _customerRepository.AddCustomer(customer);
            else
                _customerRepository.UpdateCustomer(customer);

            return RedirectToAction("Index", "Customers");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            
            var customer = _customerRepository.GetCustomer(id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _membershipTypeRepository.GetMembershipTypes().ToList()
            };

            return View("CustomerForm", viewModel);

        }
    }
}