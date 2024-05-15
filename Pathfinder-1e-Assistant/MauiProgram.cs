using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using Pathfinder_1e_Assistant.Databases;
using Pathfinder_1e_Assistant.Lib;


namespace Pathfinder_1e_Assistant
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            builder
                .UseMauiCommunityToolkit()
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Add small db holding character names
            string characterDB = FileAccess.GetLocalFilePath(DatabaseConstants.CharacterRepoFilename);
            builder.Services.AddSingleton(s => ActivatorUtilities.CreateInstance<CharactersRepo>(s, characterDB));

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
