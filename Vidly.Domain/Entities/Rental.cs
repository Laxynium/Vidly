using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Domain.Entities
{
    public class Rental
    {
        public int  Id { get; set; }

        public int CustomerId { get; set; }

        public int MovieId { get; set; }

        public DateTime DateRented { get; set; }

        public DateTime? DateReturned { get; set; }

    }
}