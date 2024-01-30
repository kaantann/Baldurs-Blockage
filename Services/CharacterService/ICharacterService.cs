using dotnet_rpg.DTOs.Character;
using dotnet_rpg.Models;

namespace dotnet_rpg.Services.CharacterService
{
    public interface ICharacterService
    {
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> GetAllCharacters(int userID);
        Task<ServiceResponse<GetCharacterResponseDTO>> GetCharacterByID(int id);
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> AddCharacter(AddCharacterRequestDTO newCharacter);

        Task<ServiceResponse<GetCharacterResponseDTO>> UpdateCharacter(UpdateCharacterRequestDTO updatedCharacter);
        Task<ServiceResponse<List<GetCharacterResponseDTO>>> DeleteCharacter(int id);

    }
}
