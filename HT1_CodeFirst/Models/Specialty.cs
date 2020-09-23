using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HT1_CodeFirst.Models
{
    public class Specialty : Base.Item
    {
        public Group Group { get; set; }
        public Trainer Trainer { get; set; }
        public Audience Audience { get; set; }
        public Schedule Schedule { get; set; }
    }
}
