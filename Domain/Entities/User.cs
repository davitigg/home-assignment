namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ICNumber { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }

        private User() { }

        public User(string name, string icNumber, string mobile, string email)
        {
            Name = name;
            ICNumber = icNumber;
            Mobile = mobile;
            Email = email;
        }
    }
}
