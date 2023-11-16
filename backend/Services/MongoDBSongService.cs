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
        var database = client.GetDatabase("foo");
        var collection = database.GetCollection<BsonDocument>("bar");

        await collection.InsertOneAsync(new BsonDocument("Name", "Jack"));

        var list = await collection.Find(new BsonDocument("Name", "Jack"))
            .ToListAsync();

        foreach(var document in list)
        {
            Console.WriteLine(document["Name"]);
        }

        throw new NotImplementedException();
    }

    public Task<Song?> Get(int id)
    {
        throw new NotImplementedException();
    }

    public Task Add(Song song)
    {
        throw new NotImplementedException();
    }

    public Task Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Update(Song song)
    {
        throw new NotImplementedException();
    }
}