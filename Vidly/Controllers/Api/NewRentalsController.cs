using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web;
using System.Web.Http;
using AutoMapper;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Infrastructure.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class NewRentalsController : ApiController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IRentalRepository _rentalRepository;
        public NewRentalsController(ICustomerRepository customerRepository, IMovieRepository movieRepository,IRentalRepository rentalRepository)
        {
            _customerRepository = customerRepository;
            _movieRepository = movieRepository;
            _rentalRepository = rentalRepository;

        }
        [HttpPost]
        public IHttpActionResult CreateNewRentals(NewRentalDto newRental)
        {
            var customer = _customerRepository.GetCustomer(newRental.CustomerId);

            var movies=_movieRepository.GetMovies().Where(m => newRental.MovieIds.Contains(m.Id)).ToList();
            foreach (var movie in movies)
            {
                if(movie.AvailableMovies==0)
                    return BadRequest($"Movie{movie.Name}is not available.");
                movie.AvailableMovies--;
                _movieRepository.UpdateMovie(movie);

                var rental = new Rental
                {
                    DateRented = DateTime.Now,
                    CustomerId = customer.Id,
                    MovieId = movie.Id
                };
                _rentalRepository.AddRental(rental);
            }

            return Ok();
        }

    }
}
