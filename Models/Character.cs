namespace dotnet_rpg.Models
{
    public class Character
    {

        public Character()
        {
            InitWithDefaults();
        }

        private void InitWithDefaults()
        {
            Name = "John Doe";
            HitPoint = 100;
            Strength = 10;
            Defence = 10;
            Intelligence = 10;
            ClassType = RpgClass.Knight;
        }


        public int Id { get; set; }
        public string? Name { get; set; }
        public int HitPoint { get; set; }
        public int Strength { get; set; }
        public int Defence { get; set; }
        public int Intelligence { get; set; }
        public RpgClass ClassType { get; set; }
        public User? User { get; set; }
        public Weapon? Weapon { get; set; }

    }
}
