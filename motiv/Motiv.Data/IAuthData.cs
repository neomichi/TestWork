namespace Motiv.Data.Model
{
    public interface IAuthData
    {
        int AuthType { get; set; }
        int DefaultBallance { get; set; }
        string Login { get; set; }
        string Option { get; set; }
        string Password { get; set; }
        string Tarif { get; set; }
    }
}