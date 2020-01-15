using Microsoft.AspNetCore.Mvc;
using RentYourHomeAPI.Helpers;
using RentYourHomeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentYourHomeAPI.Repositories
{
    public interface IHomeRepository
    {
        Task<ActionResult<PagedList<Home>>> GetHomesByOwner(int Id, PagingParameters pagingParameters);
        Task<ActionResult<Home>> GetHome(int Id);
        void RegisterHome(Home owner);
        void UpdateHome(Home owner);
        void DeleteHome(Home owner);
        Task SaveAsync();
    }
}
