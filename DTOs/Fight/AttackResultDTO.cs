namespace dotnet_rpg.DTOs.Fight
{
    public class AttackResultDTO
    {
        public AttackResultDTO()
        {
            Attacker = string.Empty;
            Opponent = string.Empty;
        }

        public string Attacker { get; set; }
        public string Opponent { get; set; }
        public int AttackerHP { get; set; }
        public int OpponentHP { get; set; }
        public int Damage { get; set; }
    }
}
