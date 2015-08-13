using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Entities
{
    [Table("UserProfile")]
    public partial class UserProfile : BaseSoftDeleteEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UserProfile()
        {
            Candidate = new HashSet<Candidate>();
            Card = new HashSet<Card>();
            Feedback = new HashSet<Feedback>();
        }
        
        [Required]
        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(15)]
        public string Alias { get; set; }

        [StringLength(100)]
        public string UserLogin { get; set; }

        public DateTime Added { get; set; }

        [Column("LViewedActivity")]
        public int LastViewedActivityId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Candidate> Candidate { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Card> Card { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedback { get; set; }
    }
}
