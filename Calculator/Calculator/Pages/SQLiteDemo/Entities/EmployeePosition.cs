using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Pages.SQLiteDemo.Entities
{
    [Table("Positions")]
    public class EmployeePosition
    {
        [PrimaryKey, AutoIncrement, Indexed]
        public int Id { get; set; }
        [Unique]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
