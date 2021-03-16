using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Index = Microsoft.EntityFrameworkCore.Metadata.Internal.Index;

namespace MedicalWebService.Model
{
    [Index(nameof(Login), IsUnique = true)]
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
