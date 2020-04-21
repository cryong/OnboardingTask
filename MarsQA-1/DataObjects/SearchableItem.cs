using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarsQA_1.SpecflowTests.DataObjects
{
    public abstract class SearchableItem
    {
        public override string ToString()
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(this);
        }

        // ideally, we would want to force subclasses
        // to have unique ids
        // public abstract string GetID();
    }
}
