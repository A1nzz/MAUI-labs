using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManager.Domain.Entities
{
    public class EmployeePosition : Entity
    {
        public int Salary { get; set; }

        public List<Responsibility> PositionResponsibilities = new();

        public EmployeePosition()
        {
        }

        public EmployeePosition(string name, int salary) : base(name)
        {
            Salary = salary;
        }


    }
}
