using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Baremiseur.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string VeryBad { get; set; }
        public string Bad { get; set; }
        public string Good { get; set; }
        public string VeryGood { get; set; }
        public int GridId { get; set; }
        public Grid Grid { get; set; } = null!;
    }
}
