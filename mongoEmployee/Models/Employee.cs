using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Models
{
    public class Employee
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int RoleId { get; set; }
        public string Password { get; set; }

        public List<string> EmployeeTraining { get; set; } = new List<string>();

        [BsonIgnore]
        public List<Training> Trainings { get; set; } = new List<Training>();

        [BsonIgnore]
        public List<string> TrainingNames { get; set; } = new List<string>();
    }
}
