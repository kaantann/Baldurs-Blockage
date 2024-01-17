using AutoMapper;
using dotnet_rpg.DTOs.Character;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        private static List<Character> _characters = new List<Character>()
        {
            new Character(),
            new Character(){ Id = 1,Name = "Brian Cluster"}
        };

        private readonly IMapper _mapper;

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddCharacter(AddCharacterRequestDTO character)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            _characters.Add(_mapper.Map<Character>(character));
            serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterByID(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            var character = _characters.FirstOrDefault(x => x.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(character);
            return serviceResponse;
        }
    }
}
