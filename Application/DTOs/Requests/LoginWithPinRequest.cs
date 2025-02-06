namespace Application.DTOs.Requests
{
    public class LoginWithPinRequest
    {
        public string ICNumber { get; set; }
        public string DeviceId { get; set; }
        public string Pin { get; set; }
    }
}