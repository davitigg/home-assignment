namespace Application.DTOs.Requests
{
    public class SetDevicePinRequest
    {
        public string ICNumber { get; set; }
        public string DeviceId { get; set; }
        public string Pin { get; set; }
    }
}