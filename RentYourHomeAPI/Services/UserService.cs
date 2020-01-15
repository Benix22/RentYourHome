using RentYourHomeAPI.DataAccess;
using RentYourHomeAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RentYourHomeAPI.Services
{
    public interface IUserService
    {
        Task<Owner> Authenticate(string username, string password);
    }

    public class UserService : IUserService
    {
        private readonly BookingContext _dbContext;

        public UserService(BookingContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Owner> Authenticate(string username, string password)
        {
            var user = await Task.Run(() => _dbContext.Owners.SingleOrDefault(x => x.Name == username && x.Password == password));

            if (user == null)
                return null;

            return user;
        }
    }
}
