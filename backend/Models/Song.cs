using MongoDB.Bson;

namespace ComfyMusic.Models;
public class Song
{
    public ObjectId Id { get; set; }
    public string? Name { get; set; }
    public string? Album { get; set; }
    public string? Artist { get; set; }
}