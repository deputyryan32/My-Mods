using GTA.Native;

public static class WeatherHelper
{
    public static void SetWeather(string weatherType)
    {
        Function.Call(Hash.SET_WEATHER_TYPE_NOW, weatherType);
    }
}
