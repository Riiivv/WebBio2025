using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebBio2025.Domain.entities;
using WebBio2025.Infrastucture;
using WebBio2025.Infrastucture.Repositories;

namespace WebBio2025.Test.Repositories
{
    public class PersonRepositoryTest
    {
        private DbContextOptions<DatabaseContext> options;
        private DatabaseContext context;

        public PersonRepositoryTest()
        {
            options = new DbContextOptionsBuilder<DatabaseContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            context = new DatabaseContext(options);

            context.Persons.Add(new Person
            {
                Id = 1,
                Name = "John",
                Lastname = "Doe",
                Mail = "testmail.com"
            });
            context.Persons.Add(new Person
            {
                Id = 2,
                Name = "John",
                Lastname = "Doe",
                Mail = "testmail.com"
            });
            context.SaveChanges();
        }
        [Fact]
        public async Task GetPersonById_Null()
        {
            // Arrange
            PersonRepository repo = new PersonRepository(context);

            // Act
            var result = await repo.GetPersonById(500);

            // Assert
            // assert.equal(500, result.Id); 
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPersonById()
        {
            // Arrange
            PersonRepository repo = new PersonRepository(context);

            // Act
            var result = await repo.GetPersonById(1);
            // Assert
            Assert.Equal(1, result.Id);
        }


        [Fact]
        public async Task GetPersons()
        {
            // Arrange
            PersonRepository repo = new PersonRepository(context);
            // act
            var result = await repo.GetAll();
            // assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task CreatePerson()
        {
            // Arrange
            PersonRepository repo = new PersonRepository(context);
            Person newPerson = new Person
            {
                Name = "Mathias",
                Lastname = "Altenburg",
                Mail = "Testmail@yay.com"
            };
            // act
            var result = await repo.CreatePerson(newPerson);
            // assert
            Assert.NotNull(result);
            Assert.True(result.Id > 0);
            Assert.Equal("Mathias", result.Name);
        }
        [Fact]
        public async Task UpdatePerson()
        {
            // Arrange
            PersonRepository repo = new PersonRepository(context);
            Person updatePerson = new Person
            {
                Id = 1,
                Name = "UpdatedName",
                Lastname = "UpdatedLastname",
                Mail = "testmail@woop.com"
            };
            // act
            var result = await repo.UpdatePerson(updatePerson);
            // assert
            Assert.NotNull(result);
        }
        [Fact]
        public async Task DeletePerson()
        {
            // Arrange
            PersonRepository repo = new PersonRepository(context);
            // act
            var result = await repo.DeleteUserAsync(2);
            // assert
            Assert.True(result);

            var deletedPerson = await repo.GetPersonById(2);
            Assert.Null(deletedPerson);
        }
    }
}


//public string Name { get; set; }
//public string Lastname { get; set; }
//public string Mail { get; set; }
//public int Id { get; set; } // kan staves på alle måder + className
//                            //    public int PersonId { get; set; }
//public Movies Movies { get; set; } // navigation property
//public Hall Hall { get; set; } // navigation property