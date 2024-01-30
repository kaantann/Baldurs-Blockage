using AutoMapper;
using dotnet_rpg.Data;
using dotnet_rpg.DTOs.Character;
using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        public CharacterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }


        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddCharacter(AddCharacterRequestDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var character =_mapper.Map<Character>(newCharacter);

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters(int userID)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var dbCharacters = await _context.Characters.Where(c=> c.User!.Id == userID).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            //serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterByID(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            var dbCharacter = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            //var character = _characters.FirstOrDefault(x => x.Id == id);

            serviceResponse.Data = _mapper.Map<GetCharacterResponseDTO>(dbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updatedCharacter)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();

            try
            {
                //var character = _characters.FirstOrDefault(c => c.Id == updatedCharacter.Id);
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
                if (character is null)
                {
                    throw new ArgumentNullException($"Character with ID {updatedCharacter.Id} not found");
                }

                //_mapper.Map(updatedCharacter,character);

                character.Name = updatedCharacter.Name;
                character.HitPoint = updatedCharacter.HitPoint;
                character.Strength = updatedCharacter.Strength;
                character.Defence = updatedCharacter.Defence;
                character.Intelligence = updatedCharacter.Intelligence;
                character.ClassType = updatedCharacter.ClassType;

                await _context.SaveChangesAsync();
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
                //var character = _characters.First(c => c.Id == id);
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character is null)
                {
                    throw new Exception($"Character with ID {id} not found");
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToListAsync();

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
