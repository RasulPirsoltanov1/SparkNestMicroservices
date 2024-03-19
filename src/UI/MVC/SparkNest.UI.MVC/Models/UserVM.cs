namespace SparkNest.UI.MVC.Models
{
    public class UserVM
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Countyr { get; set; }

        public IEnumerable<string> GetUserProperties()
        {
            yield return Id;
            yield return UserName;
            yield return Email;
            yield return Countyr;
        }

        public UserVM(string id, string userName, string email, string countyr)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Countyr = countyr;
        }

        public override bool Equals(object obj)
        {
            return obj is UserVM other &&
                   Id == other.Id &&
                   UserName == other.UserName &&
                   Email == other.Email &&
                   Countyr == other.Countyr;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, UserName, Email, Countyr);
        }
    }
}
