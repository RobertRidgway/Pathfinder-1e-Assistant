using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;
using Pathfinder_1e_Assistant.Databases;
using System.Diagnostics;
using System.Xml.Linq;

namespace Pathfinder_1e_Assistant
{
    public class CharactersRepo(string dbPath)
    {
        readonly string _dbPath = dbPath;

        public string StatusMessage { get; set; } = string.Empty;

        private SQLiteConnection conn;

        private void Init()
        {
            if (conn != null) { return; }

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Character>();

        }

        public Character GetCharacter(int Id)
        {
            Init();

            return conn.Table<Character>().Where(i => i.Id == Id).FirstOrDefault();
        }

        public List<Character> GetAllCharacters()
        {
            Init();
            List<Character> result = [.. conn.Table<Character>()];
            
            return result; 
        }

        public void AddNewCharacter(string name)
        {

            try 
            {
                Init();
                if (string.IsNullOrEmpty(name)) { throw new Exception("Valid name required!"); }

                conn.Insert(new Character { CharacterName = name });

                StatusMessage = $"Character added: {name}";
            }
            catch (Exception ex) 
            {
                StatusMessage = $"Failed to add {name}. Error: {ex.Message}";
            }
        }

        public void UpdateCharacter(Character character)
        {
            Init();
            try
            {
                if (string.IsNullOrEmpty(character.CharacterName)) { throw new Exception("Valid name required!"); }
                if (character.Id != 0)
                { 
                    conn.Update(character); 
                    StatusMessage = $"Character updated: {character.CharacterName}"; 
                }
            }
            catch (Exception ex) 
            {
                StatusMessage = $"Failed to update {character.CharacterName}. Error: {ex.Message}";
            }
        }

        public void DeleteCharacter(Character character)
        {
            Init();

            if (character.Id != 0)
            {
                conn.Delete(character);
            }
        }
    }

}
