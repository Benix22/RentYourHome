using Microsoft.EntityFrameworkCore;
using RentYourHomeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentYourHomeAPI.DataAccess
{
    public interface IBookingContext
    {
        DbSet<Owner> Owners { get; set; }
        DbSet<Home> Homes { get; set; }

        void Update(Home home);

        void Remove(Home home);

        Task SaveChangesAsync();

    }
}
