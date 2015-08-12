﻿using System;

namespace Hunter.Services
{
    public class CandidateLongListDto
    {
        public int Id { get; set; }
        public byte[] Photo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Company { get; set; }
        public string Location { get; set; }
        public double? Salary { get; set; }
        public double? YearsOfExperience { get; set; }
        public int Stage { get; set; }
        public string Resolution { get; set; }
        public string UserAddsCard { get; set; }
        public DateTime AddDate { get; set; }
    }
}
