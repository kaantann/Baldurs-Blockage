namespace dotnet_rpg.DTOs.User
{
    public class UserRegisterDTO
    {
        public UserRegisterDTO()
        {
            Username = string.Empty;
            Password = string.Empty;

        }

        public string Username { get; set; }
        public string Password { get; set; }
    }
}
