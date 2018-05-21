using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Member.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Member.API.Data.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;

namespace Member.API.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private readonly MemberDbContext _dbContext;
        private readonly ILogger<UserController> _logger;

        public UserController(MemberDbContext dbContext, ILogger<UserController> logger)
        {
            _dbContext = dbContext;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get(int id = 1)
        {
            return Json(await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == id));
        }
        
        [HttpPatch]
        public async Task<IActionResult> Patch([FromBody]JsonPatchDocument<User> patch)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == 1);
            patch.ApplyTo(user);
            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();
            return Json(user);
        }

        [Route("get-or-create")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> GetOrCreate(string phone)
        {
            // 参数检查

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Phone.Equals(phone));
            if(user == null)
            {
                user = new User
                {
                    Name = DateTime.UtcNow.ToString("yyyyMMddHHmmssfff"),
                    Phone = phone
                };
                _dbContext.Users.Add(user);
                await _dbContext.SaveChangesAsync();
            }

            return Json(user);
        }
    }
}
