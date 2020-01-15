using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentYourHomeAPI.DataAccess;
using RentYourHomeAPI.Models;

namespace RentYourHomeAPI.Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly BookingContext _dbContext;

        public OwnerRepository(BookingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ActionResult<Owner>> GetOwner(int Id)
        {
            var owner = await _dbContext.Owners.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if(owner == null)
            {
                return null;
            }

            return owner;
        }

        public async Task<ActionResult<IEnumerable<Owner>>> GetOwners()
        {
            return await _dbContext.Owners.ToListAsync();
        }

        public void RegisterOwner(Owner owner)
        {
            _dbContext.Owners.Add(owner);
        }

        public void UpdateOwner(Owner owner)
        {
            _dbContext.Update(owner);
        }

        public void DeleteOwner(Owner owner)
        {
            _dbContext.Remove(owner);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
