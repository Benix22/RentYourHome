using Microsoft.AspNetCore.Mvc;
using RentYourHomeAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentYourHomeAPI.Repositories
{
    public interface IOwnerRepository
    {
        Task<ActionResult<IEnumerable<Owner>>> GetOwners();
        Task<ActionResult<Owner>> GetOwner(int Id);
        void RegisterOwner(Owner owner);
        void UpdateOwner(Owner owner);
        void DeleteOwner(Owner owner);
        Task SaveAsync();
    }
}
