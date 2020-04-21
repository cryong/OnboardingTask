using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA_1.SpecflowTests.DataObjects
{
    public class Certification : SearchableItem
    {
        public string Name { get; set; }
        public string Organisation { get; set; }
        public string Year { get; set; }

        public Certification()
        {

        }
        public Certification(string name, string organisation, string year)
        {
            Name = name;
            Organisation = organisation;
            Year = year;
        }

        public override bool Equals(object obj)
        {
            return obj is Certification certification &&
                   Name == certification.Name &&
                   Organisation == certification.Organisation &&
                   Year == certification.Year;
        }

        public override int GetHashCode()
        {
            var hashCode = 1118675265;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Organisation);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Year);
            return hashCode;
        }
    }
}
