using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalWebService.Model
{
    public class Form
    {
        public Guid ParticipantId { get; set; }
        public int HeartBeat { get; set; }
        public double Temperature { get; set; }
        public int DistalPressure { get; set; }
        public int SistalPressure { get; set; }
        public int SelfFeeling { get; set; }
    }
}
