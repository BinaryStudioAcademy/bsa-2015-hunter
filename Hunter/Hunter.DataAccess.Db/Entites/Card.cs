using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Hunter.DataAccess.Db
{
    [Table("Card")]
    public partial class Card : IEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Card()
        {
            Feedback = new HashSet<Feedback>();
            Interview = new HashSet<Interview>();
            SpecialNote = new HashSet<SpecialNote>();
            Test = new HashSet<Test>();
        }

        public int Id { get; set; }

        public int CandidateId { get; set; }

        public int VacancyId { get; set; }

        public DateTime Added { get; set; }

        public int Stage { get; set; }

        public int? AddedByProfileId { get; set; }

        public virtual Candidate Candidate { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual Vacancy Vacancy { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Feedback> Feedback { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Interview> Interview { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SpecialNote> SpecialNote { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Test> Test { get; set; }
    }
}
