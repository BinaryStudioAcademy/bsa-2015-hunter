using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Entities
{
    [Table("Candidate")]
    public partial class Candidate : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Candidate()
        {
            Card = new HashSet<Card>();
            Pool = new HashSet<Pool>();
            Origin = (int)OriginEnum.Sourced;
            Resolution = (int)ResoultionEnum.None;
            Shortlisted = false;
        }

        public int Id { get; set; }

        [Column(TypeName = "image")]
        public byte[] Photo { get; set; }

        [Required]
        [StringLength(300)]
        public string FirstName { get; set; }

        [StringLength(300)]
        public string LastName { get; set; }

        [StringLength(300)]
        public string Email { get; set; }

        [StringLength(300)]
        public string CurrentPosition { get; set; }

        [StringLength(300)]
        public string Company { get; set; }

        [StringLength(300)]
        public string Location { get; set; }

        [StringLength(300)]
        public string Skype { get; set; }

        [StringLength(300)]
        public string Phone { get; set; }

        [StringLength(300)]
        public string Salary { get; set; }

        public double? YearsOfExperience { get; set; }

        public int ResumeId { get; set; }

        public int? AddedByProfileId { get; set; }

        public virtual Resume Resume { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public int Origin { get; set; }

        public int Resolution { get; set; }

        public bool Shortlisted { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> Card { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Pool> Pool { get; set; }
    }
}
