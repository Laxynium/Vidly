using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using AutoMapper;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Models;

namespace Vidly.Infrastructure.Concrete
{
    public class EFMovieRepository:IMovieRepository,IDisposable
    {
        private readonly ApplicationDbContext _context;

        public EFMovieRepository()
        {
            _context=new ApplicationDbContext();         
        }
        public IEnumerable<Movie> GetMovies()
        {
            return _context.Movies.Include(m => m.Genre);
        }

        public Movie GetMovie(int id)
        {
            return _context.Movies.AsQueryable().Include(m=>m.Genre).SingleOrDefault(m => m.Id == id);
        }

        public void AddMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            _context.SaveChanges();
        }

        public void UpdateMovie(Movie movie)
        {
            var movieInDb=_context.Movies.AsQueryable().SingleOrDefault(m => m.Id == movie.Id);

            if (movieInDb != null)
            {
                Mapper.Map(movie, movieInDb);
                _context.SaveChanges();
            }
           
        }

        public void RemoveMovie(int id)
        {
            var movieInDb = GetMovie(id);
            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}