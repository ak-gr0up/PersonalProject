using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MedicalWebService.Model
{
    public class Researcher
    {
        [Key]
        public Guid Id { get; set; } //Guid
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
