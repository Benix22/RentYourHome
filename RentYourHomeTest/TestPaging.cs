using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using RentYourHomeAPI.DataAccess;
using RentYourHomeAPI.Helpers;
using RentYourHomeAPI.Models;
using RentYourHomeAPI.Repositories;
using RentYourHomeTest.Helper;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class TestPaging
    {
        private Mock<IBookingContext> _context;
        private IHomeRepository _repo;


        [Test]
        public void Test1() 
        {
            var data = DataList();
            var mockSet = new Mock<DbSet<Home>>();

            mockSet.As<IAsyncEnumerable<Home>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new TestAsyncEnumerator<Home>(data.GetEnumerator()));

            mockSet.As<IQueryable<Home>>()
               .Setup(m => m.Provider)
               .Returns(new TestAsyncQueryProvider<Home>(data.Provider));

            mockSet.As<IQueryable<Home>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Home>>().Setup(m => m.ElementType).Returns(data.ElementType);

            _context = new Mock<IBookingContext>();
            _context.Setup(c => c.Homes).Returns(mockSet.Object);

            _repo = new HomeRepository(_context.Object);

            var homes = _repo.GetHomesByOwner(1, new PagingParameters());
            Assert.AreEqual(5, homes.Result.Value.Count); 

        }

        private IQueryable<Home> DataList()
        {
            var data = new List<Home> {
                 new Home
                    {
                        Id = 1,
                        OwnerId = 1,
                        Name = "La Campana",
                        Stars = 5,
                        City = "Motril",
                        Description = "El mejor hotel en la Costa Tropical",
                        Imagen = "http://localhost:62122/imgs/campana1.jpg"
                    },
                    new Home
                    {
                        Id = 2,
                        OwnerId = 1,
                        Name = "Bahia Málaga",
                        Stars = 4,
                        City = "Málaga",
                        Description = "El mejor hotel en la Costa del Sol",
                        Imagen = "http://localhost:62122/imgs/bahia1.jpg"
                    },
                    new Home
                    {
                        Id = 3,
                        OwnerId = 1,
                        Name = "Teatinos Alto",
                        Stars = 2,
                        City = "Málaga",
                        Description = "El mejor hotel de Teatinos",
                        Imagen = "http://localhost:62122/imgs/Teatinos1.jpg"
                    },
                    new Home
                    {
                        Id = 4,
                        OwnerId = 1,
                        Name = "Teatinos Bajo",
                        Stars = 2,
                        City = "Málaga",
                        Description = "El mejor hotel de Teatinos Bajo",
                        Imagen = "http://localhost:62122/imgs/Teatinos1.jpg"
                    },
                    new Home
                    {
                        Id = 5,
                        OwnerId = 1,
                        Name = "Teatinos Medio",
                        Stars = 2,
                        City = "Málaga",
                        Description = "El mejor hotel de Teatinos Medio",
                        Imagen = "http://localhost:62122/imgs/Teatinos1.jpg"
                    },
                    new Home
                    {
                        Id = 6,
                        OwnerId = 1,
                        Name = "Teatinos Left",
                        Stars = 2,
                        City = "Málaga",
                        Description = "El mejor hotel de Teatinos Left",
                        Imagen = "http://localhost:62122/imgs/Teatinos1.jpg"
                    },
                    new Home
                    {
                        Id = 7,
                        OwnerId = 1,
                        Name = "Teatinos Right",
                        Stars = 2,
                        City = "Málaga",
                        Description = "El mejor hotel de Teatinos Right",
                        Imagen = "http://localhost:62122/imgs/Teatinos1.jpg"
                    }
            }.AsQueryable();

            return data;
        }
    }
}