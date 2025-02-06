using System;

namespace Application.Models
{
    public class Otp(string pin)
    {
        private DateTime _createdAt = DateTime.Now;
        public string Pin { get; set; } = pin;

        public bool ResendPermitted => DateTime.Now.Subtract(_createdAt).TotalSeconds > 120;
    }
}
