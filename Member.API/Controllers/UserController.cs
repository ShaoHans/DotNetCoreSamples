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

        [HttpPost]
        [Route("search/{phone}")]
        public async Task<IActionResult> Seach(string phone)
        {
            return Json(await _dbContext.Users.Where(u => u.Phone.Equals(phone)).ToListAsync());
        }

        [HttpGet]
        [Route("tags")]
        public async Task<IActionResult> GetUserTags()
        {
            return Json(await _dbContext.UserTags.Where(t => t.UserId == 1).ToListAsync());
        }

        [HttpPut]
        [Route("update/tags")]
        public async Task<IActionResult> UpdateUserTags([FromBody]List<string> tags)
        {
            var originTags = await _dbContext.UserTags.Where(u => u.UserId == 1).ToListAsync();
            var newTags = tags.Except(originTags.Select(t => t.Tag));
            _dbContext.UserTags.AddRange(newTags.Select(t => new UserTag
            {
                UserId = 1,
                Tag = t,
                CreateTime = DateTime.Now
            }));
            await _dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
