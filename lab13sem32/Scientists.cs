using SQLite;

namespace lab13sem32
{
    [Table("scientists")]
    public class Scientists
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("fieldsofstudy")]
        public string FieldOfStudy { get; set; }
    }
}
