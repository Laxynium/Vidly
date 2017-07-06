using System;
using System.Linq;
using System.Web.Http;
using AutoMapper;
using Vidly.Models;
using System.Data.Entity;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Infrastructure.Dtos;

namespace Vidly.Controllers.Api
{
    public class CustomersController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        // GET /api/customers
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult GetCustomers(string query=null)
        {
            var customersQuery = _customerRepository.GetCustomers();

            if (!String.IsNullOrWhiteSpace(query))
                customersQuery = customersQuery.Where(c => c.Name.Contains(query));

             var customerDtos=customersQuery
                .ToList()
                .Select(Mapper.Map<Customer, CustomerDto>);

            return Ok(customerDtos);
        }

        //GET /api/customers/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _customerRepository.GetCustomer(id);
            if (customer == null)
                return NotFound();

            return Ok(Mapper.Map<Customer,CustomerDto>(customer));
        }

        //POST /api/customers
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);

            //To be sure that client didnt assign value to them
            customer.Id = 0;
            customer.MembershipType = null;

            _customerRepository.AddCustomer(customer);

            customerDto.Id = customer.Id;

            return Created(new Uri($"{Request.RequestUri}/{customer.Id}"),customerDto);

        }

        // PUT /api/customers
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        public IHttpActionResult UpdateCustomers(int id,CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var customerInDb = _customerRepository.GetCustomer(id);

            if (customerInDb == null)
                return NotFound();

            Mapper.Map(customerDto, customerInDb);
            
            _customerRepository.UpdateCustomer(customerInDb);

            return Ok();
        }

        //DELETE /api/customers/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete]
        public IHttpActionResult DeleteCustomer(int id)
        {
            var customerInDb = _customerRepository.GetCustomer(id);
            if (customerInDb == null)
                return NotFound();
            _customerRepository.RemoveCustomer(id);
            return Ok();
        }

}
}
