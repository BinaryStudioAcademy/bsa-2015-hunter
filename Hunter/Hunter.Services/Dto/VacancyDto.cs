﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Hunter.Services
{
    public class VacancyDto
    {
        public VacancyDto()
        {
            //Card = new HashSet<Card>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public int Status { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [StringLength(4000)]
        public string Description { get; set; }

        public int PoolId { get; set; }

        //public virtual ICollection<CardDTO> Card { get; set; }

        //public virtual PoolDTO Pool { get; set; }
    }
}