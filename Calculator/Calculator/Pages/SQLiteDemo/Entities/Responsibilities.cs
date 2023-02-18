using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Pages.SQLiteDemo.Entities
{
    [Table("Responsibilities")]
    public class Responsibilities
    {
        [PrimaryKey, AutoIncrement, Indexed]
        [Column("Id")]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public string Description { get; set; }

        [Indexed]
        public int EmployeePositionId { get; set; }
    }
}
