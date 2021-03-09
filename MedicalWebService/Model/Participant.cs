using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalWebService.Model
{
    public class Participant
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Researcher")]
        public Guid ResearcherId { get; set; }

        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Login { get; set; }
        public Gender Gender { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }

        public Researcher Researcher { get; set; }
        public ICollection<DataPoint> DataPoints { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }

}
