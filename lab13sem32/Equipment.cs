using SQLite;

namespace lab13sem32
{
    [Table("equipment")]
    public class Equipment
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("type")]
        public string Type { get; set; }
    }
}
