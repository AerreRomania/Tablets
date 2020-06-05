namespace SmartB.Core.Models
{
    public class AuthenticationResponse
    {
        public bool IsAuthenticated { get; set; }
        public Angajati User { get; set; }
    }
}
