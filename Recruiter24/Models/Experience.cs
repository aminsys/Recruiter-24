﻿namespace Recruiter24.Models
{
    public class Experience
    {
        public int YearsOfExperience { get; set; }
        public Guid TechnologyId { get; set; }

        public string TechnologyName => Technology.GetName(TechnologyId);
    }
}
