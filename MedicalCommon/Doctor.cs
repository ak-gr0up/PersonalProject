using System;
using System.Collections.Generic;
using System.Text;

namespace MedicalCommon
{
    public class Doctor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }
        public string Login { get; set; }

        public override string ToString()
        {
            return Name + " " + Surname;
        }
    }
}
