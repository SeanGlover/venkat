using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.RegularExpressions;
namespace venkat.Models
{
    [BsonIgnoreExtraElements]
    public class Student
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        private string _id = string.Empty;
        public string Id
        {
            get { return _id; }
            set
            {
                string newId = value ?? string.Empty;
                if (newId.Length == 24) { _id = newId; }
                else
                {
                    _id = Guid.NewGuid().ToString();
                    _id = Regex.Replace(_id, "[^0-9]", string.Empty, RegexOptions.None);
                    _id = Regex.Match(_id, "[0-9]{24}").Value;
                }
            }
        }

        [BsonElement("name")]
        public string Name { get; set; } = string.Empty;

        [BsonElement("graduated")]
        public bool IsGraduated { get; set; }

        [BsonElement("courses")]
        public string[]? Courses { get; set; }

        [BsonElement("gender")]
        public string Gender { get; set; } = string.Empty;

        [BsonElement("age")]
        public int Age { get; set; }
    }
}
