using ComfyMusic.Models;

namespace ComfyMusic.Services;

public interface ISongService
{
    public List<Song> GetAll();

    public Song? Get(int id);

    public void Add(Song song);

    public void Delete(int id);

    public void Update(Song song);
}