using System.Security.Claims;
using API.Controllers;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;
using NSubstitute.ReturnsExtensions;

namespace API.UnitTests.Controllers;

public class MembersControllerTest
{
    private MembersController _membersController;
    private IMembersRepository _mockMembersRepository;
    [SetUp]
    public void Setup()
    {
        _mockMembersRepository = NSubstitute.Substitute.For<IMembersRepository>();
        _membersController = new MembersController(_mockMembersRepository);
    }

    [Test]
    public async Task GetMembers_Valid_ReturnMembers()
    {
        // Arrange
        var userId = "userId";
        DefaultHttpContext testHttpContext = new()
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([
                new Claim(ClaimTypes.NameIdentifier, userId)
            ]))
        };
        _membersController.ControllerContext = new ControllerContext
        {
            HttpContext = testHttpContext
        };

        IReadOnlyList<Member> expectedMembers = new List<Member>
        {
            new Member {
                Id = "test-id",
                BirthDate = DateOnly.Parse("1990-01-01"),
                ImageUrl = null,
                DisplayName = "Test",
                Created = DateTime.UtcNow,
                Gender = "Gender",
                Description = "Description",
                City = "City",
                Country = "Country",
                User = null!,
                Photos = []
            }
        };

        _mockMembersRepository.GetMembersAsync().Returns(expectedMembers);

        // Act AND Assert
        var membersResult = await _membersController.GetMembers();
        var okResult = membersResult.Result as OkObjectResult;
        Assert.That(okResult, Is.Not.Null, "Expected OkObjectResult but something else was returned.");

        var members = okResult.Value as IReadOnlyList<Member>;
        Assert.That(members, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(members, Has.Count.EqualTo(1));
        });
    }

    [Test]
    public async Task GetMember_Valid_ReturnMembers()
    {
        // Arrange
        var userId = "userId";
        DefaultHttpContext testHttpContext = new()
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([
                new Claim("email", userId)
            ]))
        };
        _membersController.ControllerContext = new ControllerContext
        {
            HttpContext = testHttpContext
        };

        Member expectedMember = new()
        {
            Id = "test-id",
            BirthDate = DateOnly.Parse("2000-01-01"),
            ImageUrl = null,
            DisplayName = "Test",
            Created = DateTime.UtcNow,
            LastActive = DateTime.UtcNow,
            Gender = "Gender",
            Description = "Description",
            City = "City",
            Country = "Country",
            User = null!,
            Photos = []
        };

        _mockMembersRepository.GetMemberAsync(expectedMember.Id).Returns(expectedMember);

        // Act
        var memberResult = await _membersController.GetMember(expectedMember.Id);
        var member = memberResult.Value;

        // Assert
        Assert.That(member, Is.Not.Null);
        Assert.Multiple(() =>
        {
            Assert.That(member.Id, Is.EqualTo(expectedMember.Id));
            Assert.That(member.BirthDate, Is.EqualTo(DateOnly.Parse("2000-01-01")));
            Assert.That(member.City, Is.EqualTo("City"));
        });
    }

    [Test]
    public async Task GetMember_Valid_ReturnNotFound()
    {
        // Arrange
        var userId = "userId";
        DefaultHttpContext testHttpContext = new()
        {
            User = new ClaimsPrincipal(new ClaimsIdentity([
                new Claim("email", userId)
            ]))
        };
        _membersController.ControllerContext = new ControllerContext
        {
            HttpContext = testHttpContext
        };

        Member expectedMember = new()
        {
            Id = "test-id",
            BirthDate = DateOnly.Parse("2000-01-01"),
            ImageUrl = null,
            DisplayName = "Test",
            Created = DateTime.UtcNow,
            LastActive = DateTime.UtcNow,
            Gender = "Gender",
            Description = "Description",
            City = "City",
            Country = "Country",
            User = null!,
            Photos = []
        };

        _mockMembersRepository.GetMemberAsync(expectedMember.Id).ReturnsNull();

        // Act & Assert
        var memberResult = await _membersController.GetMember(expectedMember.Id);
        var notFoundResult = memberResult.Result as NotFoundObjectResult;
        Assert.That(notFoundResult, Is.Not.Null, "Expected NotFoundObjectResult but got something else");

        var member = memberResult.Value;
        Assert.That(member, Is.Null);
    }
}
