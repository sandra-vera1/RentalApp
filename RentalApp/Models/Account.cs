namespace RentalApp.Models
{
    public class Account
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public Account(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
    }
    ///CheckCredentials(username,password)
    ///GetMaskedPasword()
}
