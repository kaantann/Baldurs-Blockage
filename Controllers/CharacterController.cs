using System.Security.Claims;
using dotnet_rpg.DTOs.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            this._characterService = characterService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> Get(int id)
        {
            return Ok(await _characterService.GetCharacterByID(id));
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> GetAll()
        {
            return Ok(await _characterService.GetAllCharacters());
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> AddCharacter(AddCharacterRequestDTO newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> UpdateCharacter(UpdateCharacterRequestDTO updatedCharacter)
        {
            var response = await _characterService.UpdateCharacter(updatedCharacter);

            return (response.Data is null) ? NotFound(response) : Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDTO>>>> DeleteCharacter(int id)
        {
            var response = await _characterService.DeleteCharacter(id);

            return (response.Data is null) ? NotFound(response) : Ok(response);
        }

        [HttpPost("Skill")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
        {
            return Ok(await _characterService.AddCharacterSkill(newCharacterSkill));
        }
    }
}
