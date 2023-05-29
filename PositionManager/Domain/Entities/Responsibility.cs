using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PositionManager.Domain.Entities
{
    public class Responsibility : Entity
    {
        public Responsibility() {}
        public Responsibility(string name, string description, int importance, int positionId) : base(name)
        {
            Description = description;
            Importance = importance;
            EmployeePositionId = positionId;
        }

        public string Description { get; set; } = string.Empty;

        public int Importance { get; init; }

        public int EmployeePositionId { get; set; }
    }
}
