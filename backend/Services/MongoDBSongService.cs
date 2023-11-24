using ComfyMusic.Collections;
using ComfyMusic.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace ComfyMusic.Services;

public class MongoDBSongService : ISongService
{
    private IMongoCollection<Song> Collection { get; }

    public MongoDBSongService(IConfiguration configuration)
    {
        var client = new MongoClient(configuration["Mongo:ConnectionString"]);
        var database = client.GetDatabase("comfy-music");
        Collection = database.GetCollection<Song>("songs");
    }
    
    public async Task<List<Song>> GetAll()
    {
        var documents = await Collection.FindAsync(x => true);
        return documents.ToList();
    }

    public async Task<Song?> Get(string id)
    {
        var document = await Collection.FindAsync(x => x.Id == id);
        return document.SingleOrDefault();
    }

    public async Task Add(CreateUpdateSong song)
    {
        var newSong = new Song
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = song.Name,
            Artist = song.Artist,
            Album = song.Album,
            PlayCount = song.PlayCount,
        };
        
        await Collection.InsertOneAsync(newSong);
    }

    public async Task Delete(string id)
    {
        await Collection.DeleteOneAsync(x => x.Id == id);
    }

    public async Task Update(string id, CreateUpdateSong song)
    {
        var document = await Get(id);

        if (document is not null)
        {
            document.Album = song.Album;
            document.Artist = song.Artist;
            document.Name = song.Name;
            document.PlayCount = song.PlayCount;

            await Collection.ReplaceOneAsync(x => x.Id == id, document);
        }
    }
}