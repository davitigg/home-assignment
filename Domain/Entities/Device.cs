using System;

namespace Domain.Entities
{
    public class Device
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string DeviceId { get; set; }
        public string DeviceAuthHash { get; set; }

        private Device() { }

        public Device(int userId, string deviceId, string deviceAuthHash)
        {
            UserId = userId;
            DeviceId = deviceId;
            DeviceAuthHash = deviceAuthHash;
        }
    }
}
