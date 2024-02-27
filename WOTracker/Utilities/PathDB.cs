
namespace WOTracker.Utilities
{
    internal class PathDB
    {

        public static string GetPath(string nameDB)
        {
            string pathDbSqlite = string.Empty;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {
                pathDbSqlite = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), nameDB);    
            }
            else if (DeviceInfo.Platform == DevicePlatform.iOS)
            {
                pathDbSqlite = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..", "Library", nameDB); 
            }
            else if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                pathDbSqlite = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), nameDB);
            }
            else
            {  
                pathDbSqlite = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), nameDB);
            }

            return pathDbSqlite;
        }
    }
}
