using System;
using System.Net;
using Rage;

namespace PoliceAI_RPH_Edition.VersionCheck
{
    public static class VersionChecker
    {
        private static readonly string versionCheckURL = "https://www.lcpdfr.com/applications/downloadsng/interface/api.php?do=checkForUpdates&fileId=48971&textOnly=1";
        private static readonly string currentVersion = "1.0.0";

        public static void CheckForUpdates()
        {
            try
            {
                using (WebClient client = new WebClient())
                {
                    string latestVersion = client.DownloadString(versionCheckURL).Trim();

                    if (latestVersion == currentVersion)
                    {
                        Game.Console.Print("PoliceAI is up to date.");
                        Game.DisplayNotification("PoliceAI is up to date.");
                    }
                    else
                    {
                        Game.Console.Print($"PoliceAI Update Available! Current: {currentVersion}, Latest: {latestVersion}");
                        Game.DisplayNotification($"PoliceAI Update Available! Please update to version {latestVersion}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Game.Console.Print($"Version check failed: {ex.Message}");
                Game.DisplayNotification("Error checking for PoliceAI updates.");
            }
        }
    }
}
