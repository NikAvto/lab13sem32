using SQLite;

namespace lab13sem32
{
    [Table("Research")]
    public class Research
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }

}
