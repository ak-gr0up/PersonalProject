using MedicalClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalWeb3.Models
{
    public class ModelDataPoints
    {
        public ICollection<DataPoint> Data { get; set; }
        public string Id { get; set; }
        public List<List<double>> Table { get; set; }
        public List<string> Headings { get; set; }
        public bool Hidden { get; set; }
    }
}
