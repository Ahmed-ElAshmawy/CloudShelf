using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudShift.Domain.Model
{
    public class GuidEntity : IEntity
    {
        public int Id { get; set; }

        public Guid GuidValue { get; set; } = Guid.NewGuid();
    }
}
