using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sithec.Models;
using Sithec.Models.Entity;

namespace Sithec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HumanController : ControllerBase
    {
        private SithecContext db;

        public HumanController(SithecContext _db)
        {
            db = _db;
        }

        [HttpGet("GetAllHumans")]
        public async Task<IActionResult> GetAllHumans()
        {
            try
            {
                List<Human> humansList = await db.Humans.ToListAsync();
                return Ok(humansList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetHumanByID/{id}")]
        public async Task<IActionResult> GetHumanByID(int id)
        {
            try
            {
                Human? human = await db.Humans.Where(human => human.Id == id).FirstOrDefaultAsync();
                if (human == null) return NotFound("There is no human with that ID");
                return Ok(human);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("InsertHuman")]
        public async Task<IActionResult> InsertHuman([FromBody] Human human)
        {
            try
            {
                db.Humans.Add(human);
                await db.SaveChangesAsync();
                return Ok(human);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("UpdateHuman")]
        public async Task<IActionResult> UpdateHuman([FromBody] Human human)
        {
            try
            {
                Human? humanToUpdate = await db.Humans.Where(humanInDB => humanInDB.Id == human.Id).FirstOrDefaultAsync();
                if (humanToUpdate == null) return NotFound("There is no human with that ID");

                humanToUpdate.Nombre = human.Nombre;
                human.Sexo = human.Sexo;
                human.Edad = human.Edad;
                human.Altura = human.Altura;
                human.Peso = human.Peso;
                await db.SaveChangesAsync();
                return Ok(human);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
