using CoreDriven.Data.Entities;
using CoreDriven.Data.Repositories;
using CoreDriven.UseCases.Users.Queries;
using CoreDriven.Utils.Spec;
using Microsoft.EntityFrameworkCore;

namespace CoreDriven.UnitTests.UseCases.Users.Queries;

public class GetUsersTest
{
    private Data.Entities.AppContext _context;
    private IUserRepository _repository;
    private GetUsers _getUsers;
    [SetUp]
    public async Task Setup()
    {
        var options = new DbContextOptionsBuilder<Data.Entities.AppContext>()
            .UseInMemoryDatabase("TestDatabase")
            .Options;
        _context = new Data.Entities.AppContext(options);
        await _context.Database.EnsureDeletedAsync();
        await _context.Database.EnsureCreatedAsync();

        // Add test data to the context
       await _context.Roles.AddRangeAsync(
            new Role { Id = 1, Value = "admin" },
            new Role { Id = 2, Value = "user" }
        );
        await _context.Users.AddRangeAsync(
            new User
            {
                Id = new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                Name = "Juan Pérez",
                Email = "",
                ImageUrl = "",
                ImageName = "",
                Password = "password123",
                RoleId = 1,
                Active = true,
                CreationDate = Convert.ToDateTime("2023-10-01T00:00:00Z")
            },
            new User
            {
                Id = new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                Name = "María García",
                Email = "",
                ImageUrl = "",
                ImageName = "",
                Password = "password123",
                RoleId = 1,
                Active = true,
                CreationDate = Convert.ToDateTime("2023-10-01T00:00:00Z")
            },
            new User
            {
                Id = new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                Name = "Carlos López",
                Email = "",
                ImageUrl = "",
                ImageName = "",
                Password = "password123",
                RoleId = 1,
                Active = true,
                CreationDate = Convert.ToDateTime("2023-10-01T00:00:00Z")
            });
        await _context.SaveChangesAsync();
        _repository = new UserRepository(_context);
        _getUsers = new GetUsers(_repository);
    }

    [TearDown]
    public async Task Cleanup()
    {
        await _context.DisposeAsync();
    }
    [Test]
    public async Task Execute_WithValidParams_ReturnsPagedUsers()
    {
        var bqp = new BaseQueryParams
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Id",
            SortDesc = false
        };
        var result = await _getUsers.Execute(bqp);
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Name, Is.EqualTo("Juan Pérez"));
            Assert.That(result[1].Name, Is.EqualTo("María García"));
            Assert.That(result[2].Name, Is.EqualTo("Carlos López"));
        });
    }
    [Test]
    public async Task Execute_WithSearchValue_FiltersCorrectly()
    {
        var bqp = new BaseQueryParams
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Id",
            SortDesc = false,
            Value = "Juan"
        };

        var result = await _getUsers.Execute(bqp);
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Name, Is.EqualTo("Juan Pérez"));
    }
    [Test]
    public async Task Execute_WithSortDesc_OrdersDescending()
    {
        var bqp = new BaseQueryParams
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Name",
            SortDesc = true
        };

        var result = await _getUsers.Execute(bqp);

        Assert.That(result, Has.Count.EqualTo(3));
        Assert.Multiple(() =>
        {
            Assert.That(result[0].Name, Is.EqualTo("María García"));
            Assert.That(result[1].Name, Is.EqualTo("Juan Pérez"));
            Assert.That(result[2].Name, Is.EqualTo("Carlos López"));
        });
    }
    [Test]
    public async Task Execute_WithPagination_ReturnsCorrectPage()
    {
        var bqp = new BaseQueryParams
        {
            PageNumber = 2,
            PageSize = 2,
            SortBy = "Name",
            SortDesc = false
        };

        var result = await _getUsers.Execute(bqp);
        Assert.That(result, Has.Count.EqualTo(1)); // 3 usuarios, página 2 con tamaño 2 => solo queda 1
        Assert.That(result[0].Name, Is.EqualTo("María García"));
    }
    [Test]
    public async Task Execute_WithInvalidSearch_ReturnsEmpty()
    {
        var bqp = new BaseQueryParams
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Name",
            SortDesc = false,
            Value = "not-exist"
        };

        var result = await _getUsers.Execute(bqp);

        Assert.That(result, Is.Empty);
    }
    [Test]
    public async Task Execute_SortByEmail_WorksCorrectly()
    {
        var bqp = new BaseQueryParams
        {
            PageNumber = 1,
            PageSize = 10,
            SortBy = "Email",
            SortDesc = false
        };

        var result = await _getUsers.Execute(bqp);

        var expectedOrder = new[]
        {
            "carlos@test.com",
            "juan@test.com",
            "maria@test.com"
        };

        Assert.That(result.Select(u => u.Email), Is.EqualTo(expectedOrder));
    }
    
}