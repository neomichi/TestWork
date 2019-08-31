namespace Motiv.Data
{
    public interface ISyncData
    {
        string Message { get; set; }
        int Progress { get; set; }
    }
}