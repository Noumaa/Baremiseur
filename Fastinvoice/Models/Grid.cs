using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baremiseur.Models
{
    public class Grid
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Skill>? Skills { get; set; }
    }
}
