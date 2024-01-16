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

        public async Task<List<Character>> AddCharacter(Character character)
        {
            _characters.Add(character);
            return _characters;
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            return _characters;
        }

        public async Task<Character> GetCharacterByID(int id)
        {
            return _characters.FirstOrDefault(c => c.Id == id)!;
        }
    }
}
