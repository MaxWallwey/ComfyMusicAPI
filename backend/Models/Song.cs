using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComfyMusic.Models;
public class Song
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int Id { get; set; }

    [BsonElement("Name")]
    public string? Name { get; set; }
    public string? Album { get; set; }
    public string? Artist { get; set; }
}