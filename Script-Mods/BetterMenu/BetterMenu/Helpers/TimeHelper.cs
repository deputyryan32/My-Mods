using GTA;
using GTA.Native;

public static class TimeHelper
{
    public static void SetTime(int hours, int minutes, int seconds)
    {
        // SET_CLOCK_TIME takes hour and minute parameters; seconds are not typically required in this context
        Function.Call(Hash.SET_CLOCK_TIME, hours, minutes, seconds);
    }
}
