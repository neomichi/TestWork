using Motiv.Data.Model;

namespace Motiv.Data
{
    public interface IBallance
    {
        event Ballance.ProgressHandler Change;

        Subscription GetBallanse(IAuthData auth, int oneTime, int twoTime);
    }
}