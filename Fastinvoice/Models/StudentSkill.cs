using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baremiseur.Models
{
    public class StudentSkill
    {
        public int Id { get; set; }
        public Student? Student { get; set; }
        public int StudentId { get; set; }
        public Skill? Skill { get; set; }
        public int SkillId { get; set; }
        public int Level { get; set; }
    }
}
