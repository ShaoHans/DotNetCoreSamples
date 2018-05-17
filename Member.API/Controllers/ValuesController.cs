using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Member.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.JsonPatch;
using Member.API.Data.Entities;

namespace Member.API.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly MemberDbContext _dbContext;

        public ValuesController(MemberDbContext dbContext)
        {
            _dbContext = dbContext;
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
    }
}
