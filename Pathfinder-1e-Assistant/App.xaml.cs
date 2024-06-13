using Pathfinder_1e_Assistant.Lib;

namespace Pathfinder_1e_Assistant
{
    // string file_characters = FileSystem.AppDataDirectory + '';  
    public partial class App : Application
    {

        public static CharactersRepo CharRepo { get; private set; } = null!;

        public App(CharactersRepo repo)
        {
            InitializeComponent();

            MainPage = new AppShell();

            CharRepo = repo; // new(DatabaseConstants.CharactersRepoPath);
            
            // Check for characters made in app, create list if it doesn't exist


        }
    }
}
