using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PatientMedicalHistory : Entity
    {
        [DataMember]
        [BsonRepresentation(BsonType.String)]
        public string PatientName { get; set; }

        [DataMember]
        [BsonRepresentation(BsonType.String)]
        public string SocialNumber { get; set; }

        [DataMember]
        [BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [DataMember]
        public Hospital Hospital { get; set; }

        [DataMember]
        public Doctor Doctor { get; set; }

        [DataMember]
        public List<Medicine> Medicines { get; set; }

        [DataMember]
        public List<Surgery> Surgeries { get; set; }
    }
}





    
    
    
