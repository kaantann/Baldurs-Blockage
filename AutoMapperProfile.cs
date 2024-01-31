using AutoMapper;
using dotnet_rpg.DTOs.Character;
using dotnet_rpg.DTOs.Weapon;
using dotnet_rpg.Models;

namespace dotnet_rpg
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterResponseDTO>();
            CreateMap<AddCharacterRequestDTO, Character>();
            CreateMap<UpdateCharacterRequestDTO, Character>();
            CreateMap<Weapon, GetWeaponDTO>();
        }
    }
}
