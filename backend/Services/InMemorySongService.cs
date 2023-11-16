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
    public List<Song> GetAll() => Songs;

    public Song? Get(int id) => Songs.FirstOrDefault(p => p.Id == id);

    public void Add(Song song)
    {
        song.Id = nextId++;
        Songs.Add(song);
    }

    public void Delete(int id)
    {
        var song = Get(id);
        if(song is null)
            return;

        Songs.Remove(song);
    }

    public void Update(Song song)
    {
        var index = Songs.FindIndex(p => p.Id == song.Id);
        if(index == -1)
            return;
        
        Songs[index] = song;
    }
}