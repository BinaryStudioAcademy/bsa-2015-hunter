using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Entities
{
    [Table("Resume")]
    public partial class Resume : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Resume()
        {
            Candidate = new HashSet<Candidate>();
        }

        public int Id { get; set; }

        [Required]
        public byte[] FileStream { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Candidate> Candidate { get; set; }
    }
}
