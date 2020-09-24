using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT1_CodeFirst.Models
{
    public class Specialty : Base.Item
    {
        public virtual Group Group { get; set; }
        public virtual Trainer Trainer { get; set; }
        public virtual Audience Audience { get; set; }
        public virtual Schedule Schedule { get; set; }
    }
}
