using SQLite;


namespace Pathfinder_1e_Assistant.Databases
{
    [Table("macros")]
    public class Macro
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }


        public int CharId { get; set; }

        [MaxLength(200)]
        public string Name { get; set; } = string.Empty;

        public string MacroText {  get; set; } = string.Empty;

    }
}
