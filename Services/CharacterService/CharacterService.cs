using System.Security.Claims;
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

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        private readonly IMapper _mapper;
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;





        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddCharacter(AddCharacterRequestDTO newCharacter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var character = _mapper.Map<Character>(newCharacter);
            character.User = await _context.Users.FirstOrDefaultAsync(user => user.Id == GetUserID());

            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters
                .Where(c => c.User!.Id == GetUserID())
                .Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToListAsync();
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterResponseDTO>>();
            var dbCharacters = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .Where(c => c.User!.Id == GetUserID()).ToListAsync();
            serviceResponse.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            //serviceResponse.Data = _characters.Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterByID(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterResponseDTO>();
            var dbCharacter = await _context.Characters
                .Include(c => c.Weapon)
                .Include(c => c.Skills)
                .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserID());

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
                var character = await _context.Characters
                    .Include(c => c.User)
                    .FirstOrDefaultAsync(c => c.Id == id && c.User!.Id == GetUserID());
                if (character is null || character.User!.Id != GetUserID())
                {
                    throw new Exception($"Character with ID {id} not found");
                }

                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                serviceResponse.Data = await _context.Characters
                    .Where(c => c.User!.Id == GetUserID())
                    .Select(c => _mapper.Map<GetCharacterResponseDTO>(c)).ToListAsync();

            }
            catch (Exception e)
            {
                serviceResponse.isSuccessful = false;
                serviceResponse.Message = e.Message;
            }

            return serviceResponse;
        }



        private int GetUserID()
        {
            return int.Parse(_httpContextAccessor.HttpContext!.User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        }

        public async Task<ServiceResponse<GetCharacterResponseDTO>> AddCharacterSkill(AddCharacterSkillDTO newCharacterSkill)
        {
            var response = new ServiceResponse<GetCharacterResponseDTO>();

            try
            {
                var character = await _context.Characters
                    .Include(c => c.Weapon)
                    .Include(c => c.Skills)
                    .FirstOrDefaultAsync(c => c.Id == newCharacterSkill.CharacterID && c.User!.Id == GetUserID());

                if (character is null)
                {
                    response.isSuccessful = false;
                    response.Message = "Character not found.";

                    return response;
                }


                var skill = await _context.Skills
                    .FirstOrDefaultAsync(s => s.ID == newCharacterSkill.SkillID);

                if (skill is null)
                {
                    response.isSuccessful = false;
                    response.Message = "Skill not found.";

                    return response;
                }

                character.Skills.Add(skill);
                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterResponseDTO>(character);

            }
            catch (Exception ex)
            {
                response.isSuccessful = false;
                response.Message = ex.Message;
            }

            return response;

        }
    }
}
