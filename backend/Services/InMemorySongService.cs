using ComfyMusic.Collections;
using ComfyMusic.Models;
using MongoDB.Bson;

namespace ComfyMusic.Services;

public class InMemorySongService : ISongService
{
    public List<Song> Songs { get; }
    
    public InMemorySongService()
    {
        Songs = new List<Song>
        {
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Rebel Rebel", Album = "Diamond Dogs", Artist = "David Bowie" },
            new() { Id = ObjectId.GenerateNewId().ToString(), Name = "Growing Sideways", Album = "Stick Season", Artist = "Noah Kahan" }
        };
    }

    public Task<List<Song>> GetAll()
    {
        return Task.FromResult(Songs);
    }

    public Task<Song?> Get(string id)
    {
        return Task.FromResult(Songs.FirstOrDefault(p => p.Id == id));
    }
    public Task Add(CreateUpdateSong song)
    {
        var newSong = new Song
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = song.Name,
            Artist = song.Artist,
            Album = song.Album,
        };
        
        Songs.Add(newSong);
            
        return Task.CompletedTask;
    }

    public async Task Delete(string id)
    {
        var song = await Get(id);
        if (song is null)
        {
            return;
        }

        Songs.Remove(song);
    }

    public Task Update(string id, CreateUpdateSong song)
    {
        var index = Songs.FindIndex(p => p.Id == id);
        if (index == -1)
        {
            return Task.CompletedTask;
        }
        
        Songs[index].Name = song.Name;
        Songs[index].Artist = song.Artist;
        Songs[index].Album = song.Album;

        return Task.CompletedTask;
    }
}