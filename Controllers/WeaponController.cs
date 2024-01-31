﻿using dotnet_rpg.DTOs.Character;
using dotnet_rpg.DTOs.Weapon;
using dotnet_rpg.Models;
using dotnet_rpg.Services.WeaponService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_rpg.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController : ControllerBase
    {
        private readonly IWeaponService _weaponService;

        public WeaponController(IWeaponService weaponService)
        {
            _weaponService = weaponService;
        }

        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDTO>>> AddWeapon(AddWeaponDTO newWeapon)
        {
            return Ok(await _weaponService.AddWeapon(newWeapon));
        }

    }
}
