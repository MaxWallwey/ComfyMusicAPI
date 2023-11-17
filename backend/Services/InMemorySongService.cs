using ComfyMusic.Collections;
using MongoDB.Bson;

namespace ComfyMusic.Services;

public class InMemorySongService : ISongService
{
    public List<Song> Songs { get; }
    
    public InMemorySongService()
    {
        Songs = new List<Song>
        {
            new() { Id = ObjectId.GenerateNewId(), Name = "Rebel Rebel", Album = "Diamond Dogs", Artist = "David Bowie" },
            new() { Id = ObjectId.GenerateNewId(), Name = "Growing Sideways", Album = "Stick Season", Artist = "Noah Kahan" }
        };
    }

    public Task<List<Song>> GetAll()
    {
        return Task.FromResult(Songs);
    }

    public Task<Song?> Get(ObjectId id)
    {
        return Task.FromResult(Songs.FirstOrDefault(p => p.Id == id));
    }
    public Task Add(Song song)
    {
        song.Id = ObjectId.GenerateNewId();
        Songs.Add(song);
        return Task.CompletedTask;
    }

    public async Task Delete(ObjectId id)
    {
        var song = await Get(id);
        if (song is null)
        {
            return;
        }

        Songs.Remove(song);
    }

    public Task Update(Song song)
    {
        var index = Songs.FindIndex(p => p.Id == song.Id);
        if (index == -1)
        {
            return Task.CompletedTask;
        }
        
        Songs[index] = song;

        return Task.CompletedTask;
    }
}