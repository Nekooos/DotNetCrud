using System.Text.Json.Serialization;

namespace UserCrud.Models
{
    public class User
    {
        public User()
        {

        }

        public User(long id, string name, string email, int age, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Age = age;
            Password = password;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
    }
}
