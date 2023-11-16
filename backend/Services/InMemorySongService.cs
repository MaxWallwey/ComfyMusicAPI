using ComfyMusic.Models;

namespace ComfyMusic.Services;

public class InMemorySongService : ISongService
{
    static List<Song> Songs { get; }
    static int nextId = 3;
    static InMemorySongService()
    {
        Songs = new List<Song>
        {
            new Song { Id = 1, Name = "Rebel Rebel", Album = "Diamond Dogs", Artist = "David Bowie" },
            new Song { Id = 2, Name = "Growing Sideways", Album = "Stick Season", Artist = "Noah Kahan" }
        };
    }

    public Task<List<Song>> GetAll()
    {
        return Task.FromResult(Songs);
    }

    public Task<Song?> Get(int id)
    {
        return Task.FromResult(Songs.FirstOrDefault(p => p.Id == id));
    }
    public Task Add(Song song)
    {
        song.Id = nextId++;
        Songs.Add(song);
        return Task.CompletedTask;
    }

    public async Task Delete(int id)
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