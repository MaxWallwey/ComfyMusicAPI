using ComfyMusic.Models;

namespace ComfyMusic.Services;

public interface ISongService
{
    public Task<List<Song>> GetAll();

    public Task<Song?> Get(int id);

    public Task Add(Song song);

    public Task Delete(int id);

    public Task Update(Song song);
}