using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vidly.Domain.Entities;

namespace Vidly.Domain.Abstract
{
    public interface IRentalRepository
    {
        void AddRental(Rental rental);
    } 
}
