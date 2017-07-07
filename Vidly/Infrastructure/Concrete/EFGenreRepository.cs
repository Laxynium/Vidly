using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Models;

namespace Vidly.Infrastructure.Concrete
{
    public class EFGenreRepository:IGenreRepository,IDisposable
    {
        private readonly ApplicationDbContext _context;

        public EFGenreRepository()
        {
            _context=new ApplicationDbContext();
        }

        public IEnumerable<Genre> GetGenres()
        {
            return _context.Genres;
        }

        public void Dispose()
        {
           _context.Dispose();
        }
    }
}