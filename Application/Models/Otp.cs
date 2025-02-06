using System;

namespace Application.Models
{
    public class Otp(int userId, string pin)
    {
        private DateTime _createdAt = DateTime.Now;
        public int UserId { get; set; } = userId;
        public string Pin { get; set; } = pin;
        public bool ResendPermitted => DateTime.Now.Subtract(_createdAt).TotalSeconds > 120;
    }
}
