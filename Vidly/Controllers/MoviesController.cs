using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity;
using Vidly.Domain.Abstract;
using Vidly.Domain.Entities;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IGenreRepository _genreRepository;

        public MoviesController(IMovieRepository movieRepository,IGenreRepository genreRepository)
        {
            _movieRepository = movieRepository;
            _genreRepository = genreRepository;
        }
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }

        public ActionResult Details(int id)
        {
            var movie = _movieRepository.GetMovie(id);

            if (movie == null)
                return HttpNotFound();

            return View(movie);
        }

        [Authorize(Roles=RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _genreRepository.GetGenres();
            var movie = new Movie();
            var viewModel = new MovieFormViewModel()
            {
                Genres = genres
            };

            return View("MovieForm",viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Save(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new MovieFormViewModel(movie)
                {
                    Genres = _genreRepository.GetGenres()
            };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {
                movie.DateAdded = DateTime.Now;
                movie.AvailableMovies = movie.NumberInStock;
                _movieRepository.AddMovie(movie);
            }
            else
            {
                var movieInDb=_movieRepository.GetMovie(movie.Id);
                var diff = movie.NumberInStock - movieInDb.NumberInStock;
                movie.AvailableMovies = movieInDb.AvailableMovies;
                if (diff > 0)
                    movie.AvailableMovies += diff;
                else if (movie.AvailableMovies > movie.NumberInStock)
                    movie.AvailableMovies = movie.NumberInStock;

                _movieRepository.UpdateMovie(movie);
            }

            return RedirectToAction("Index", "Movies");
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var movie = _movieRepository.GetMovie(id);
            if (movie == null)
                return HttpNotFound();

            var viewModel = new MovieFormViewModel(movie)
            {
                Genres = _genreRepository.GetGenres()
            };
            return View("MovieForm", viewModel);
        }
    }
}