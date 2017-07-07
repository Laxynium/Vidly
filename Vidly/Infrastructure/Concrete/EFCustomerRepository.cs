using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Models;

namespace Vidly.Infrastructure.Concrete
{
    public class EFCustomerRepository:ICustomerRepository,IDisposable
    {
        private readonly ApplicationDbContext _context;

        public EFCustomerRepository()
        {
            _context=new ApplicationDbContext();
        }
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.Include(c => c.MembershipType);
        }

        public Customer GetCustomer(int id)
            => _context.Customers.AsQueryable().Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);


        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            var customerInDb=_context.Customers.AsQueryable().FirstOrDefault(c => c.Id == customer.Id);
            if (customerInDb != null)
                Mapper.Map(customer, customerInDb);

            _context.SaveChanges();
        }

        public void RemoveCustomer(int id)
        {
            var customerInDb = _context.Customers.AsQueryable().FirstOrDefault(c => c.Id == id);
            if (customerInDb != null)
             _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}