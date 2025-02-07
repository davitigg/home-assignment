namespace Application.DTOs.Requests
{
    public class AddDeviceRequest
    {
        public string ICNumber { get; set; }
        public string DeviceId { get; set; }
        public string Pin { get; set; }
    }
}