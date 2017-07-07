using System;
using System.Collections.Generic;
using Core.Domain;
using Infrastructure.Dtos;
using Core.Repositories;
namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public MovieDto GetMovie(int Id)
        {
            var movieFromRep=_movieRepository.Get(Id);

            //TODO use automaper
            var movieDto = new MovieDto
            {
                Id = movieFromRep.Id,
                Name = movieFromRep.Name,
                Genre = new GenreDto {Id = movieFromRep.Genre.Id,Name = movieFromRep.Genre.Name},
                DateAdded = movieFromRep.DateAdded,
                ReleaseDate = movieFromRep.ReleaseDate,
                AvailableMovies = movieFromRep.AvailableMovies,
                NumberInStock = movieFromRep.NumberInStock,
                GenreId = movieFromRep.GenreId
            };

            return movieDto;

        }

        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesFromRep=_movieRepository.GetMovies(query);

            var movieDtos = new List<MovieDto>();

            foreach (var movieFromRep in moviesFromRep)
            {
                var movieDto = new MovieDto
                {
                    Id = movieFromRep.Id,
                    Name = movieFromRep.Name,
                    Genre = new GenreDto { Id = movieFromRep.Genre.Id, Name = movieFromRep.Genre.Name },
                    DateAdded = movieFromRep.DateAdded,
                    ReleaseDate = movieFromRep.ReleaseDate,
                    AvailableMovies = movieFromRep.AvailableMovies,
                    NumberInStock = movieFromRep.NumberInStock,
                    GenreId = movieFromRep.GenreId
                };
                movieDtos.Add(movieDto);
            }

            return movieDtos;

        }

        public void AddMovie(MovieDto movieDto)
        {
            var movie=new Movie
            {
                Id = movieDto.Id,
                Name = movieDto.Name,
                Genre = new Genre {Id = movieDto.Genre.Id,Name = movieDto.Genre.Name} ,
                DateAdded =movieDto.DateAdded,
                NumberInStock =movieDto.NumberInStock,
                ReleaseDate = movieDto.ReleaseDate,
                AvailableMovies = movieDto.AvailableMovies,
                GenreId = movieDto.GenreId
            };
            _movieRepository.Add(movie);
        }

        public void UpdateMovie(MovieDto movie)
        {
            //TODO add implementation when automapper will be ready
            throw new NotImplementedException();
        }

        public void Delete(int Id)
        {
            throw new NotImplementedException();
        }
    }
}