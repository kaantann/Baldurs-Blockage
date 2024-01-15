using dotnet_rpg.Models;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private static List<Character> _characters = new List<Character>()
        {
            new Character(),
            new Character(){ Id = 1,Name = "Brian Cluster"}
        };


        [HttpGet("{id}")]
        public ActionResult<Character> Get(int id)
        {
            return Ok(_characters.FirstOrDefault(c => c.Id == id));
        }


        [HttpGet]
        [Route("GetAll")]
        public ActionResult<List<Character>> GetAll()
        {
            return Ok(_characters);
        }
    }
}
