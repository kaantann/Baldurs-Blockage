using dotnet_rpg.Models;

namespace dotnet_rpg.DTOs.Character
{
    public class AddCharacterRequestDTO
    {
        public string? Name { get; set; }
        public int HitPoint { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int Intelligence { get; set; }
        public RpgClass ClassType { get; set; }
    }
}
