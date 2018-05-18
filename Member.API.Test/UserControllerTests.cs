using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Moq;
using Member.API.Data;
using Member.API.Data.Entities;
using Microsoft.Extensions.Logging;
using Member.API.Controllers;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.JsonPatch;


namespace Member.API.Test
{
    public class UserControllerTests
    {
        private (MemberDbContext dbContext,ILogger<UserController> logger) GetMemberDbContext()
        {
            DbContextOptions<MemberDbContext> options = new DbContextOptionsBuilder<MemberDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            MemberDbContext dbContext = new MemberDbContext(options);

            dbContext.Users.Add(new User { Id = 1, Name = "shz" });
            dbContext.SaveChanges();

            var loggerMoq = new Mock<ILogger<UserController>>();
            return (dbContext, loggerMoq.Object);
        }

        [Fact]
        public async Task Get_ReturnUser_WhenUserIdExist()
        {
            (MemberDbContext dbContext, ILogger<UserController> logger) = GetMemberDbContext();
            var controller = new UserController(dbContext, logger);
            var response = await controller.Get(1);

            var jsonResult = response.Should().BeOfType<JsonResult>().Subject;
            var user = jsonResult.Value.Should().BeAssignableTo<User>().Subject;
            user.Id.Should().Be(1);
            user.Name.Should().Be("shz");
        }

        [Fact]
        public async Task Patch_ReturnUser_ReplaceFieldValue()
        {
            (MemberDbContext dbContext, ILogger<UserController> logger) = GetMemberDbContext();
            var controller = new UserController(dbContext, logger);

            JsonPatchDocument<User> replacePatch = new JsonPatchDocument<User>();
            replacePatch.Replace(u => u.Company, "∞¢¿Ô");
            var response = await controller.Patch(replacePatch);

            var jsonResult = response.Should().BeOfType<JsonResult>().Subject;
            var user = jsonResult.Value.Should().BeAssignableTo<User>().Subject;
            var dbUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            dbUser.Company.Should().Be("∞¢¿Ô");
        }
    }
}
