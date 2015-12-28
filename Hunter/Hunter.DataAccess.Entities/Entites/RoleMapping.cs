using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.DataAccess.Entities.Entites
{
    public class RoleMapping : IEntity
    {
        public int Id { get; set; }

        [StringLength(30)]
        [Index("PositionIndex", IsUnique = true)]
        public string Position { get; set; }

        public int RoleId { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
}
