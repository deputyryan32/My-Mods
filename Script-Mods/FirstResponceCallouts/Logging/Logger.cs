using Rage;

namespace FirstResponseCallouts.Logging
{
    public static class Logger
    {
        public static void Log(string message)
        {
            Game.LogTrivial("[FirstResponseCallouts] " + message);
        }
    }
}
