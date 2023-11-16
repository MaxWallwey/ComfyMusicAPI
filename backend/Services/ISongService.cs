using ComfyMusic.Models;
using MongoDB.Bson;

namespace ComfyMusic.Services;

public interface ISongService
{
    public Task<List<Song>> GetAll();

    public Task<Song?> Get(ObjectId id);

    public Task Add(Song song);

    public Task Delete(ObjectId id);

    public Task Update(Song song);
}