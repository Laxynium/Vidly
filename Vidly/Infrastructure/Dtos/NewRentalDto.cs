using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Vidly.Infrastructure.Dtos
{
    public class NewRentalDto
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public List<int> MovieIds { get; set; }

    }
}