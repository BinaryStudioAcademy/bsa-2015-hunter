using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Entities.Entites
{
    public class RoleMapping : IEntity
    {
        public int Id { get; set; }

        public string Position { get; set; }

        public int RoleId { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
}
