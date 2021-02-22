using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MedicalWebService.Model;

namespace MedicalWebService.Data
{
    public class MedicalWebServiceContext : DbContext
    {
        public MedicalWebServiceContext (DbContextOptions<MedicalWebServiceContext> options)
            : base(options)
        {
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Doctor> Doctor { get; set; }
    }
}
