namespace dotnet_rpg.Models
{
    public class ServiceResponse<T>
    {
        public ServiceResponse()
        {
            isSuccessful = true;
            Message = String.Empty;
        }

        public T? Data { get; set; }
        public bool isSuccessful { get; set; }
        public string Message { get; set; }

    }
}
