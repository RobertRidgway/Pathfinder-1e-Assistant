using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SQLite;

namespace Pathfinder_1e_Assistant.Databases
{

    [Table("characters")]
    public class Character
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(250), Unique]
        public string CharacterName { get; set; } = string.Empty;
    }
}
