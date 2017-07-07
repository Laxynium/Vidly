using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using AutoMapper;
using Vidly.Models;
using System.Data.Entity;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;
using Vidly.Infrastructure.Dtos;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    { 
        private readonly IMovieRepository _movieRepository;
        public MoviesController(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        //GET api/movies
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetMovies(string query=null)
        {
            var moviesQuery = _movieRepository.GetMovies().AsQueryable()
                .Where(m => m.AvailableMovies>0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery=moviesQuery.Where(m => m.Name.Contains(query));


            var movieDtos =moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieDtos);
        }

        //GET api/movies/1
        [Authorize]
        [HttpGet]
        public IHttpActionResult GetMovie(int id)
        {
            var movieInDb = _movieRepository.GetMovie(id);

            if (movieInDb == null)
                return NotFound();

            return Ok(Mapper.Map<Movie, MovieDto>(movieInDb));
        }

        //POST api/movies
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movie=Mapper.Map<MovieDto,Movie>(movieDto);

            movie.Genre = null;
            movie.AvailableMovies = movie.NumberInStock;

            _movieRepository.AddMovie(movie);

            movieDto.Id = movie.Id;
            Mapper.Map<Genre, GenreDto>(movie.Genre, movieDto.Genre);

            return Created(new Uri($"{Request.RequestUri}/{movie.Id}"), movieDto);
        }

        //PUT api/movies
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var movieInDb = _movieRepository.GetMovie(id);

            if (movieInDb == null)
                return NotFound();

            Mapper.Map<MovieDto, Movie>(movieDto, movieInDb);
            _movieRepository.UpdateMovie(movieInDb);

            return Ok();


        }
        //DELETE api/movies/1
        [Authorize(Roles = RoleName.CanManageMovies)]
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var movieInDb = _movieRepository.GetMovie(id);

            if (movieInDb == null)
                return NotFound();

            _movieRepository.RemoveMovie(id);

            return Ok();
        }

    }
}
