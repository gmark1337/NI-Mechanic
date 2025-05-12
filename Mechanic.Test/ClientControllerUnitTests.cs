using Mechanic.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Mechanic.EFcore;
using Mechanic.Shared;

namespace Mechanic.Test
{

    public class ClientControllerUnitTests
    {
        //Creates a virutal database simulating the existing database 
        //It's easier to set up and consistent
        private MechanicDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<MechanicDbContext>()
                .UseInMemoryDatabase(databaseName: "ClientDbTest")
                .Options;

            return new MechanicDbContext(options);  
        }

        [Fact]
        public async Task ClientAddSuccess()
        {
            var dbContext = GetDbContext();
            var controller = new ClientController(dbContext);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var newClient = new Client
            {
                Id = "10",
                Name = "John",
                Address = "Bucket",
                Email = "IloveMiskolc@email.com"
            };

            var result = await controller.Add(newClient);

            Assert.IsType<OkResult>(result);

            var added = await dbContext.Clients.FirstOrDefaultAsync(c => c.Email == "IloveMiskolc@email.com");

            Assert.NotNull(added);
            Assert.Equal("John", added.Name);

        }
          
        [Fact]

        public async Task ClientAddConflict()
        {
            var dbContext = GetDbContext();
            var controller = new ClientController(dbContext);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var existClient = new Client
            {
                Id="1",
                Name = "Jane",
                Address = "Bucket",
                Email = "IloveMiskolc@email.com"
            };
            var duplicateClient = new Client
            {
                Id ="1",
                Name = "Jane",
                Address = "Bucket",
                Email = "IloveMiskolc@email.com"
            };

            await dbContext.Clients.AddAsync(existClient);
            await dbContext.SaveChangesAsync();
            var result = await controller.Add(duplicateClient);

            Assert.IsType<ConflictResult>(result);
        }

        [Fact]

        public async Task ClientDeleteSuccess()
        {
            var dbContext = GetDbContext();
            var controller = new ClientController(dbContext);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            var newClient = new Client
            {
                Id = "1",
                Name = "Mark",
                Address = "Bucket",
                Email = "IloveMiskolc@email.com"
            };
            await dbContext.Clients.AddAsync(newClient);
            await dbContext.SaveChangesAsync();

            var result = await controller.Delete("1");

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ClientDeleteNotFound()
        {
            var contextDb = GetDbContext();
            var controller = new ClientController(contextDb);


            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await contextDb.Database.EnsureDeletedAsync();
            await contextDb.Database.EnsureCreatedAsync();

            var result = await controller.Delete("nothing");

            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task ClientGetAllReturnsAllClients()
        {
            var contextDb = GetDbContext();
            var controller = new ClientController(contextDb);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await contextDb.Database.EnsureDeletedAsync();
            await contextDb.Database.EnsureCreatedAsync();

            var newClient = new Client
            {
                Id = "3",
                Name = "Sip Cola",
                Address = "Vödör",
                Email = "test@email.com"
            };

            await contextDb.Clients.AddAsync(newClient);
            await contextDb.SaveChangesAsync();

            var result = await controller.GetAll();

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var clients = Assert.IsType<List<Client>>(okResult.Value);
            Assert.Single(clients);
        }

        [Fact]
        public async Task ClientReturnWhenExist()
        {
            var contextDb = GetDbContext();
            var controller = new ClientController(contextDb);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await contextDb.Database.EnsureDeletedAsync();
            await contextDb.Database.EnsureCreatedAsync();

            var newClient = new Client
            {
                Id = "4",
                Name = "Coconutnut",
                Address = "Tree",
                Email = "IloveMiskolc@email.com"
            };

            await contextDb.Clients.AddAsync(newClient);
            await contextDb.SaveChangesAsync();

            var result = await controller.Get("4");

            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedClient = Assert.IsType<Client>(okResult.Value);
            Assert.Equal("Coconutnut", returnedClient.Name);

        }
        [Fact]
        public async Task ClientReturnsNothing()
        {
            var contextDb = GetDbContext();
            var controller = new ClientController(contextDb);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await contextDb.Database.EnsureDeletedAsync();
            await contextDb.Database.EnsureCreatedAsync();

            var result = await controller.Get("bomba");

            Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public async Task ClientUpdateSuccess()
        {
            var contextDb = GetDbContext();
            var controller = new ClientController(contextDb);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await contextDb.Database.EnsureDeletedAsync();
            await contextDb.Database.EnsureCreatedAsync();

            var newClient = new Client
            {
                Id = "5",
                Name = "Kis Egér",
                Address = "Bucket",
                Email = "IloveMiskolc@email.com"
            };
            await contextDb.Clients.AddAsync(newClient);
            await contextDb.SaveChangesAsync();

            var updatedClient = new Client
            {
                Id = "5",
                Name = "Kis Egerecske",
                Address = "Bucket",
                Email = "IloveMiskolc@email.com"
            };
            var result = await controller.Update("5", updatedClient);

            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public async Task ClientUpdateNotFound()
        {
            var contextDb = GetDbContext();
            var controller = new ClientController(contextDb);

            //Deletes the content of the virtual database and creates a new one
            //Without this it sabotages other tests
            await contextDb.Database.EnsureDeletedAsync();
            await contextDb.Database.EnsureCreatedAsync();

            var newClient = new Client
            {
                Id = "12",
                Name = "Kis Egér",
                Address = "Vödör",
                Email = "szeretemMiskolc@email.com"
            };
            await contextDb.Clients.AddAsync(newClient);
            await contextDb.SaveChangesAsync();

            var updatedClient = new Client
            {
                Id = "5",
                Name = "Kis Egerecske",
                Address = "Vödör",
                Email = "szeretemMiskolc@email.com"
            };

            var result = await controller.Update("5", updatedClient);

            Assert.IsType<NotFoundResult>(result);

        }
        
    }
}
