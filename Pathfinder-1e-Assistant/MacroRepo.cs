﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Pathfinder_1e_Assistant.Databases;
using Pathfinder_1e_Assistant.Lib;
using SQLite;

namespace Pathfinder_1e_Assistant
{
    public class MacroRepo(string dbPath)
    {
        readonly private string _dbPath = dbPath;

        public string StatusMessage { get; set; } = string.Empty;

        private SQLiteConnection conn = null!;

        private void Init()
        {
            if (conn != null) { return; }

            conn = new SQLiteConnection(_dbPath);
            conn.CreateTable<Macro>();

        }

        public List<Macro> GetAllMacros()
        {
            Init();
            List<Macro> result = [.. conn.Table<Macro>()];
            return result;
        }

        public List<Macro> GetCharMacros(int charId)
        {
            Init();
            List<Macro> result = [.. conn.Table<Macro>().Where(i => i.CharId == charId)];
            return result;
        }

        public Macro GetMacro(int Id)
        {
            Init();

            return conn.Table<Macro>().Where(i => i.Id == Id).FirstOrDefault();
        }


        public void AddNewMacro(int charId, string MacroName, string MacroText)
        {
            try
            {
                Init();
                conn.Insert(new Macro { CharId=charId, Name = MacroName, MacroText = MacroText });
                StatusMessage = $"Macro added: {MacroName}";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to add {MacroName}. Error: {ex.Message}";
                //Debug.WriteLine(StatusMessage);
            }

        }

        public void UpdateMacro(Macro macro)
        {
            Init();
            try
            {
                if (macro.Id != 0)
                {
                    conn.Update(macro);
                    StatusMessage = $"Macro updated: {macro.Name}";
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to update {macro.Name}. Error: {ex.Message}";
            }
        }

        public void DeleteMacro(Macro macro)
        {
            Init();

            if (macro.Id != 0)
            { 
                conn.Delete(macro);
            }
        }
    }
}
