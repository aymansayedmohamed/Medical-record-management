﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Models
{
    public class Medicine :Entity
    {
        [DataMember]
        [BsonRepresentation(BsonType.String)]
        public String ID { get; set; }

        [DataMember]
        [BsonRepresentation(BsonType.String)]
        public String Name { get; set; }
    }
}