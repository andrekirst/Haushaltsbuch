namespace Haushaltsbuch.Library.Infrastructure
{
    public class AppendResult
    {
        public long NextExpectedVersion { get; }

        public AppendResult(long nextExpectedVersion)
        {
            NextExpectedVersion = nextExpectedVersion;
        }
    }
}