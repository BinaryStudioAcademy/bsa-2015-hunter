﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hunter.Services
{
    public class AppResultCardDto
    {
        public int VacancyId { get; set; }
        public DateTime Date { get; set; }
        public int Stage { get; set; }
        public string Pool{ get; set; }
    }
}
