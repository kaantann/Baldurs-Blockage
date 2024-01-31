namespace dotnet_rpg.Models
{
    public class Weapon
    {
        public Weapon()
        {
            _name = string.Empty;
        }

        private int _id;
        private string _name;
        private int _damage;
        private Character _character;
        private int _characterID;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public int Damage { get => _damage; set => _damage = value; }
        public Character Character { get => _character; set => _character = value; }
        public int CharacterID { get => _characterID; set => _characterID = value; }
    }
}
