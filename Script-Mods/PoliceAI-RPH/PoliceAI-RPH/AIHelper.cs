using Rage;

namespace PoliceAI_RPH_Edition
{
    public static class AIHelper
    {
        public static void Backup()
        {
            if (ConfigManager.AllowBackup)
            {
                if (ConfigManager.EnableLogging)
                {
                    Game.Console.Print("Backup Allowed.");
                }

                if (ConfigManager.EnableNotifications && ConfigManager.NotifyOnBackup)
                {
                    Game.DisplayNotification("Backup is allowed and ready.");
                }

                // Implement backup logic here, with number of units based on INI setting
                int units = ConfigManager.BackupUnits;
                // Logic to call for backup with the specified number of units
            }
            else
            {
                if (ConfigManager.EnableLogging)
                {
                    Game.Console.Print("Backup not allowed by settings.");
                }

                if (ConfigManager.EnableNotifications && ConfigManager.NotifyOnBackup)
                {
                    Game.DisplayNotification("Backup is not allowed.");
                }
            }
        }
    }
}
