namespace PBTech.APIv1.ViewModel
{
    public class NewReceive
    {
        public string FullName { get; set; }
        public string Email { get; set; }

        public NewReceive() { }

        public NewReceive(string fullName, string email)
        {
            FullName = fullName;
            Email = email;
        }
    }
}
