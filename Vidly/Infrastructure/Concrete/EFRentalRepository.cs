using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Models;

namespace Vidly.Infrastructure.Concrete
{
    public class EFRentalRepository:IRentalRepository,IDisposable
    {
        private readonly ApplicationDbContext _context;
        public EFRentalRepository()
        {
            _context=new ApplicationDbContext();
        }
        public void AddRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}