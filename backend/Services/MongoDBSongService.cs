using ComfyMusic.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ComfyMusic.Services;

public class MongoDBSongService : ISongService
{
    private IMongoCollection<Song> Collection { get; }

    public MongoDBSongService()
    {
        var client = new MongoClient("mongodb://localhost:27017");
        var database = client.GetDatabase("comfy-music");
        Collection = database.GetCollection<Song>("songs");
    }
    
    public async Task<List<Song>> GetAll()
    {
        var documents = await Collection.FindAsync(x => true);
        return documents.ToList();
    }

    public async Task<Song?> Get(ObjectId id)
    {
        var document = await Collection.FindAsync(x => x.Id == id);
        return document.SingleOrDefault();
    }

    public async Task Add(Song song)
    {
        await Collection.InsertOneAsync(song);
    }

    public async Task Delete(ObjectId id)
    {
        await Collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task Update(Song song)
    {
        var document = await Get(song.Id);

        if (document is not null)
        {
            document.Album = song.Album;
            document.Artist = song.Artist;
            document.Name = song.Name;

            await Collection.ReplaceOneAsync(x => x.Id == ObjectId.Empty, document);
        }
    }
}