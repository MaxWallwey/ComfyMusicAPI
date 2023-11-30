using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ComfyMusic.Collections;
public class Song
{
    public Song(string artist, string album, string name) {
        Id = ObjectId.GenerateNewId().ToString();
        Artist = artist;
        Album = album;
        Name = name;
        PlayCount = 0;
    }

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string? Name { get; set; }
    public string? Album { get; set; }
    public string? Artist { get; set; }
    public int? PlayCount { get; private set; }

    public void IncrementPlayCount() {
        PlayCount++;
    }
}