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

        public async Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();

            try
            {
                var character = _characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                if (character is null)
                {
                    throw new Exception($"Character with ID {updatedCharacter.Id} not found");
                }

                //_mapper.Map(updatedCharacter,character);

                character.Name = updatedCharacter.Name;
                character.HitPoint = updatedCharacter.HitPoint;
                character.Strength = updatedCharacter.Strength;
                character.Defence = updatedCharacter.Defence;
                character.Intelligence = updatedCharacter.Intelligence;
                character.ClassType = updatedCharacter.ClassType;

                serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(character);

            }
            catch (Exception e)
            {
                serviceResponse.isSuccessful = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> DeleteCharacter(int id)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();

            try
            {
                var character = _characters.First(c => c.Id == id);
                if (character is null)
                {
                    throw new Exception($"Character with ID {id} not found");
                }

                _characters.Remove(character);

                serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();

            }
            catch (Exception e)
            {
                serviceResponse.isSuccessful = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }
    }
}
