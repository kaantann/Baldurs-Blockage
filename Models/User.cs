namespace dotnet_rpg.Models
{
    public class User
    {

        public User()
        {
            Username = string.Empty;
            PasswordHash = new byte[0];
            PasswordSalt = new byte[0];
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public List<Character>? Characters { get; set; }
    }
}
