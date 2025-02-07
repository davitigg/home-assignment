namespace Application.DTOs.Requests
{
    public class EnableBiometricsRequest
    {
        public string ICNumber { get; set; }
        public string DeviceId { get; set; }
        public byte[] BiometricData { get; set; }
    }
}