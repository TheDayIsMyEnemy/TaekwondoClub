using Data;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;

        public StudentsController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<IEnumerable<Student>> Get()
        {
            return await _appDbContext.Students.ToListAsync();
        }
    }
}
