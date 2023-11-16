using ComfyMusic.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ComfyMusic.Services;

public class MongoDBSongService : ISongService
{
    // TODO: Look at async 
    public async Task<List<Song>> GetAll()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("comfy-music");
        var collection = database.GetCollection<Song>("songs");

        var list = await collection.Find(x => true).ToListAsync();

        return list;
    }

    public Task<Song?> Get(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Song song)
    {
        throw new NotImplementedException();
    }

    public Task Delete(ObjectId id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Song song)
    {
        throw new NotImplementedException();
    }
}