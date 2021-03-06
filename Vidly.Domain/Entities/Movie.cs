﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Domain.Entities
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Display(Name = "Release date")]
        public DateTime ReleaseDate { get; set; }

        public DateTime DateAdded { get; set; }

        [Display(Name="Number in stock")]
        [Range(1,20)]
        public int NumberInStock { get; set; }

        public int AvailableMovies { get; set; }

        public Genre Genre { get; set; }

        [Display(Name = "Genre")]
        [Required]
        public byte GenreId { get; set; }
    }
}