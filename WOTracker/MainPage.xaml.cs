using WOTracker.DataAccess;
using System.Reflection;
using WOTracker.Utilities;

namespace WOTracker;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        SQLitePCL.Batteries.Init();
        InitializeDatabase();
    }

    private void InitializeDatabase()
    {
        if (!File.Exists(PathDB.GetPath("WOTracker.db")))
        {
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(App)).Assembly;
            using (Stream stream = assembly.GetManifestResourceStream("WOTracker.DataAccess.WOTracker.db")!)
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    stream.CopyTo(memoryStream);

                    File.WriteAllBytes(PathDB.GetPath("WOTracker.db"), memoryStream.ToArray());
                }
            }
        }
       
    }
}

