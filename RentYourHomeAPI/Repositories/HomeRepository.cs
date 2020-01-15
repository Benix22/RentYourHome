using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentYourHomeAPI.DataAccess;
using RentYourHomeAPI.Helpers;
using RentYourHomeAPI.Models;

namespace RentYourHomeAPI.Repositories
{
    public class HomeRepository : IHomeRepository
    {
        private readonly IBookingContext _dbContext;

        public HomeRepository(IBookingContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<ActionResult<PagedList<Home>>> GetHomesByOwner(int Id, PagingParameters pagingParameters)
        {
            var result = await _dbContext.Homes.Where(h => h.OwnerId == Id).ToListAsync();
            return PagedList<Home>.ToPagedList(result, pagingParameters.PageNumber, pagingParameters.PageSize);
        }
        public async Task<ActionResult<Home>> GetHome(int Id)
        {

            var home = await _dbContext.Homes.Where(x => x.Id == Id).FirstOrDefaultAsync();

            if (home == null)
            {
                return null;
            }

            return home;
        }

        public void RegisterHome(Home home)
        {
            _dbContext.Homes.Add(home);
        }

        public void UpdateHome(Home home)
        {
            _dbContext.Update(home);
        }

        public void DeleteHome(Home home)
        {
            _dbContext.Remove(home);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
