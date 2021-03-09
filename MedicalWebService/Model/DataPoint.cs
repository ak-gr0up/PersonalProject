using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MedicalWebService.Model
{
    public class DataPoint
    {
        [Key]
        public Guid Id { get; set; }

        [ForeignKey("Participant")]
        public Guid ParticipantId { get; set; }

        [ForeignKey("Researcher")]
        public Guid ResearcherId { get; set; }

        public Participant Participant { get; set; }
        public Researcher Researcher { get; set; }

        public int HeartBeat { get; set; }
        public double Temperature { get; set; }
        public int DistalPressure { get; set; }
        public int SistalPressure { get; set; }
        public int SelfFeeling { get; set; }
    }
}
