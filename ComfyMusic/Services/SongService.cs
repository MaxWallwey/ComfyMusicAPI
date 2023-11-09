using ComfyMusic.Models;

namespace ComfyMusic.Servies;

public static class SongService
{
    static List<Song> Songs { get; }
    static int nextId = 3;
    static SongService()
    {
        Songs = new List<Song>
        {
            new Song { Id = 1, Name = "Rebel Rebel", Album = "Diamond Dogs", Artist = "David Bowie" },
            new Song { Id = 2, Name = "Growing Sideways", Album = "Stick Season", Artist = "Noah Kahan" }
        };
    }
    public static List<Song> GetAll() => Songs;

    public static Song? Get(int id) => Songs.FirstOrDefault(p => p.Id == id);

    public static void Add(Song song)
    {
        song.Id = nextId++;
        Songs.Add(song);
    }

    public static void Delete(int id)
    {
        var song = Get(id);
        if(song is null)
            return;

        Songs.Remove(song);
    }

    public static void Update(Song song)
    {
        var index = Songs.FindIndex(p => p.Id == song.Id);
        if(index == -1)
            return;
        
        Songs[index] = song;
    }
}