using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CRUDWebApi.ViewModels;
using CRUDWebApi.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using Microsoft.AspNetCore.Routing;

namespace CRUDWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly ApplicationDbContext database;
        public PersonController(ApplicationDbContext _database)
        {
            database = _database;
        }
        [Produces("application/json")]
        [HttpGet("FindAll")]
        public IActionResult FindAll()
        {
            try
            {
                var query = database.Person.Select(c => new PersonViewModels
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    SetGender = c.Gender,
                    Age = c.Age

                }).ToList();

                return Ok(query);
            }
            catch
            {
                return BadRequest();
            }
        }
        [Produces("application/json")]
        [HttpGet("FindById/{id}")]
        public IActionResult FindById(int id)
        {
            try
            {
                var query = database.Person.Where(c => c.Id == id).Select(c => new PersonViewModels
                {
                    Id = c.Id,
                    FullName = c.FullName,
                    SetGender = c.Gender,
                    Age = c.Age

                }).SingleOrDefault();

                IList<string> lst = new List<string>
                {
                  query.FullName,
                  query.GenderByName,
                  query.Age.ToString()
                };


                return Ok(lst);
            }
            catch
            {
                return BadRequest();
            }
        }

        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody]PersonViewModels model)
        {
            try
            {
                var query = new Person
                {
                    FullName = model.FullName,
                    Gender = model.Gender,
                    Age = model.Age

                };
                database.Person.Add(query);
                await database.SaveChangesAsync();
                return RedirectToAction("FindAll");
            }
            catch
            {
                return BadRequest();
            }
        }
        [Consumes("application/json")]
        [Produces("application/json")]
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update([FromBody]PersonViewModels model)
        {
            try
            {
                var query = database.Person.Find(model.Id);

                query.FullName = model.FullName;
                query.Gender = model.Gender;
                query.Age = model.Age;

                await database.SaveChangesAsync();

                return RedirectToAction("FindById", "Person", new { @id = model.Id });

            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var query = database.Person.Find(id);
                database.Person.Remove(query);
                await database.SaveChangesAsync();
                return Ok(query);
            }
            catch
            {
                return BadRequest();
            }
        }
        [HttpPost("SendImage/{id}")]
        public async Task<IActionResult> SendImage(IFormFile file, int id)
        {
            try
            {
                var query = database.Person.Find(id);
                query.ImageProfile = file.FileName;
                await database.SaveChangesAsync();

                return Ok(query);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}