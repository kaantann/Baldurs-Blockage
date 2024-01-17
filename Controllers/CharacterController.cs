﻿using dotnet_rpg.DTOs.Character;
using dotnet_rpg.Models;
using dotnet_rpg.Services.CharacterService;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{

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
    }
}
