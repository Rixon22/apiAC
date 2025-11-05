using System.Security.Claims;
using API.Controllers;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using NUnit.Framework;

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
        _membersController.ControllerContext = new ControllerContext();
        _membersController.ControllerContext.HttpContext = testHttpContext;

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
            Assert.That(members.Count, Is.EqualTo(1), "Member count does not match.");
        });
    }
}
