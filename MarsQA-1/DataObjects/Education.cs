using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA_1.SpecflowTests.DataObjects
{
    public class Education : SearchableItem
    {
        public string InstituteName { get; set; }
        public string Country { get; set; }
        public string DegreeTitle { get; set; }
        public string DegreeName { get; set; }
        public string YearOfGraduation { get; set; }

        public Education()
        {

        }

        public Education(string country, string instituteName, string degreeTitle, string degreeName, string yearOfGraduation)
        {
            Country = country;
            InstituteName = instituteName;
            DegreeTitle = degreeTitle;
            DegreeName = degreeName;
            YearOfGraduation = yearOfGraduation;
        }

        public override bool Equals(object obj)
        {
            return obj is Education education &&
                   InstituteName == education.InstituteName &&
                   Country == education.Country &&
                   DegreeTitle == education.DegreeTitle &&
                   DegreeName == education.DegreeName &&
                   YearOfGraduation == education.YearOfGraduation;
        }

        public override int GetHashCode()
        {
            var hashCode = 460369524;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(InstituteName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Country);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DegreeTitle);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(DegreeName);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(YearOfGraduation);
            return hashCode;
        }
    }
}
