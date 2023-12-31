using ComfyMusic.Collections;
using ComfyMusic.Models;

namespace ComfyMusic.Services;

public interface ISongService
{
    public Task<List<Song>> GetAll();

    public Task<Song?> Get(string id);

    public Task Add(Song song);

    public Task Delete(string id);

    public Task Update(Song song);
}