namespace Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public string DevicePinHash { get; set; }
        public string? DeviceBiometricHash { get; private set; } = null;
        public bool BiometricEnabled { get; private set; } = false;

        public DateTime CreatedAt { get;  set; } = DateTime.UtcNow;

        private Device() { }

        public Device(int userId, string deviceId, string devicePinHash)
        {
            UserId = userId;
            DeviceId = deviceId;
            DevicePinHash = devicePinHash;
        }

        public void EnableBiometrics(string deviceBiometricHash)
        {
            DeviceBiometricHash = deviceBiometricHash;
            BiometricEnabled = true;
        }

        public void DisableBiometrics()
        {
            DeviceBiometricHash = null;
            BiometricEnabled = false;
        }
    }
}
