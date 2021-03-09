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
        public MedicalWebServiceContext()
        {

        }

        public MedicalWebServiceContext (DbContextOptions<MedicalWebServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Participant> Participant { get; set; }
        public virtual DbSet<Researcher> Researcher { get; set; }
        public virtual DbSet<DataPoint> DataPoint { get; set; }

    }
}
