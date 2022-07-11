namespace PBTech.APIv1.ViewModel
{
    public class ErrorMessage
    {
        public string Message { get; set; }

        public ErrorMessage() { }

        public ErrorMessage(string message)
        {
            Message = message;
        }
    }
}
