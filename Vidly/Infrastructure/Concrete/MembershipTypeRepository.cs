using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Models;

namespace Vidly.Infrastructure.Concrete
{
    public class MembershipTypeRepository: IMembershipTypeRepository,IDisposable
    {
        private ApplicationDbContext _context;

        public MembershipTypeRepository()
        {
            _context = new ApplicationDbContext();
        }
        public IEnumerable<MembershipType> GetMembershipTypes()
        {
            return _context.MembershipTypes.AsEnumerable();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}