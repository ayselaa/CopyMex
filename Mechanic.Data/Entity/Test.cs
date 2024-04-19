using Mechanic.Data.Entity.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mechanic.Data.Entity
{
    public class Test: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
