using SQLite;

namespace ChildrenObservations.Model
{
    [Table("Child")]
    public class Child
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        [Ignore]
        public string FullName { get { return Firstname + " " + Lastname; } }
    }
}
