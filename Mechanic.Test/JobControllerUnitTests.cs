using Mechanic.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mechanic.EFcore;
using Mechanic.Shared;
namespace Mechanic.Test;

public class JobControllerUnitTests
{
    private MechanicDbContext GetDbContext()
    {
        var options = new DbContextOptionsBuilder<MechanicDbContext>()
            .UseInMemoryDatabase(databaseName: "JobDbTest")
            .Options;

        return new MechanicDbContext(options);
    }


    [Fact]
    public async Task JobAddSuccess()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "2",
            customerId = "2",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "2",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };


        var result = await controller.Add(newJob);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task JobAddConflict()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };

        await contextDb.Jobs.AddAsync(newJob);
        await contextDb.SaveChangesAsync();

        var sameJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };


        var result = await controller.Add(sameJob);

        Assert.IsType<ConflictResult>(result);
    }

    [Fact]
    public async Task JobDeleteSuccess()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };

        await contextDb.Jobs.AddAsync(newJob);
        await contextDb.SaveChangesAsync();

        var result = await controller.Delete("1");

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task JobDeleteNotFound()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();


        var result = await controller.Delete("nonExistId");

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task JobGetAllReturnsAllClients()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };
        await contextDb.Jobs.AddAsync(newJob);
        await contextDb.SaveChangesAsync();

        var result = await controller.GetAll();

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var jobResult = Assert.IsType<List<Job>>(okResult.Value);
        Assert.Single(jobResult);

    }

    [Fact]
    public async Task JobReturnsNothing()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var result = await controller.Get("LézerJancsi");
        Assert.IsType<NotFoundResult>(result.Result);
    }

    [Fact]
    public async Task JobReturnWhenExist()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };

        await contextDb.Jobs.AddAsync(newJob);
        await contextDb.SaveChangesAsync();

        var result = await controller.Get("1");

        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var jobResult = Assert.IsType<Job>(okResult.Value);
        Assert.Equal("XMX-265", jobResult.licensePlate);
    }

    [Fact]
    public async Task JobUpdateNotFound()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };
        await contextDb.Jobs.AddAsync(newJob);
        await contextDb.SaveChangesAsync();

        var updateJob = new Job
        {
            jobId = "2",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };

        var result = await controller.Update("2", updateJob);

        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task JobUpdateSuccess()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };
        await contextDb.Jobs.AddAsync(newJob);
        await contextDb.SaveChangesAsync();

        var updateJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };

        var result = await controller.Update("1", updateJob);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task JobGetEstimatedHoursSuccess() 
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var newJob = new Job
        {
            jobId = "1",
            customerId = "1",
            licensePlate = "XMX-265",
            workCategory = workCategory.Motor,
            severity = 7,
            description = "Turbó probléma",
            manufacturingYear = 2016,
            status = workStage.Felvett_Munka,
            Client = new Client
            {
                Id = "1",
                Name = "Re Tard",
                Address = "Mc'donalds",
                Email = "valamiamerika@email.com"
            }
        };
        await contextDb.Jobs.AddAsync(newJob);
        await contextDb.SaveChangesAsync();

        var result = await controller.GetEstimatedHours("1");

        var okResult  = Assert.IsType<OkObjectResult>(result.Result);
        var jobResult = Assert.IsType<double>(okResult.Value);
        Assert.True(jobResult > 0);
    }

    [Fact]
    public async Task JobGetEstimatedHoursNotFound()
    {
        MechanicDbContext contextDb = GetDbContext();
        JobController controller = new JobController(contextDb);

        await contextDb.Database.EnsureDeletedAsync();
        await contextDb.Database.EnsureCreatedAsync();

        var result = await controller.GetEstimatedHours("1");

        Assert.IsType<NotFoundResult>(result.Result);
    }

}
