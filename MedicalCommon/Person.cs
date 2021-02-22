using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalCommon
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public string Login { get; set; }
        public Gender Gender { get; set; }
        public Guid DoctorId { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }

}
