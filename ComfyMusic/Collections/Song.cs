using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComfyMusic.Collections;
public class Song
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Album { get; set; }
    public string? Artist { get; set; }
}