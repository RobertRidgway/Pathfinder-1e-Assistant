using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



// AppDataDirectory is C:\Users\robbi\AppData\Local\Packages\com.companyname.pathfinder1eassistant_9zz4h110yvjzm\LocalState
namespace Pathfinder_1e_Assistant.Lib
{
    public static class DatabaseConstants
    {
        public const string CharacterRepoFilename = "characters.db3";

        public static string CharactersRepoPath = 
            Path.Combine(FileSystem.AppDataDirectory, CharacterRepoFilename);

        public const SQLite.SQLiteOpenFlags Flags = SQLite.SQLiteOpenFlags.ReadWrite | 
                                              SQLite.SQLiteOpenFlags.Create | 
                                              SQLite.SQLiteOpenFlags.SharedCache;

        public const string MacrosRepoFilename = "macros.db3";
        
        public static string MacrosRepoPath =
            Path.Combine(FileSystem.AppDataDirectory, MacrosRepoFilename);

    }
}
