using dotnet_rpg.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace dotnet_rpg.Services.CharacterService
{
    public class CharacterService : ICharacterService
    {

        private static List<Character> _characters = new List<Character>()
        {
            new Character(),
            new Character(){ Id = 1,Name = "Brian Cluster"}
        };

        public async Task<ServiceResponse<List<Character>>> AddCharacter(Character character)
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            _characters.Add(character);
            serviceResponse.Data = _characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<List<Character>>> GetAllCharacters()
        {
            var serviceResponse = new ServiceResponse<List<Character>>();
            serviceResponse.Data = _characters;
            return serviceResponse;
        }

        public async Task<ServiceResponse<Character>> GetCharacterByID(int id)
        {
            var serviceResponse = new ServiceResponse<Character>();
            serviceResponse.Data = _characters.FirstOrDefault(x => x.Id == id);
            return serviceResponse;
        }
    }
}
