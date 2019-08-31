namespace Motiv.Data.Model
{
    public class AuthData : IAuthData
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public int AuthType { get; set; }

        public string Tarif { get; set; }

        public int DefaultBallance { get; set; }            
        public string Option { get; set; }
    }
}
