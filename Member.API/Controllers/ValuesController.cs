using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Member.API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
        
    }
}
