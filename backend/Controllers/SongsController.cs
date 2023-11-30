using ComfyMusic.Collections;
using ComfyMusic.Models;
using ComfyMusic.Services;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace ComfyMusic.Controllers;

[ApiController]
[Route("[controller]")]
public class SongsController : ControllerBase
{
    private readonly ISongService _songService;

    public SongsController(ISongService songService)
    {
        _songService = songService;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Song>>> GetAll()
    {
        return await _songService.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Song>> Get(string id)
    {
        var song = await _songService.Get(id);

        if (song == null)
        {
            return NotFound();
        }

        song.IncrementPlayCount();

        await _songService.Update(song);
            
        return song;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateUpdateSong song)
    {
        await _songService.Add(new Song(song.Artist!, song.Album!, song.Name!));
        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, CreateUpdateSong song)
    {
        var existingSong = await _songService.Get(id);
        if (existingSong is null)
        {
            return NotFound();
        }

        existingSong.Artist = song.Artist;
        existingSong.Album = song.Album;
        existingSong.Name = song.Name;
   
        await _songService.Update(existingSong);           
   
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var song = await _songService.Get(id);

        if (song is null)
        {
            return NotFound();
        }
        
        await _songService.Delete(id);

        return NoContent();
    }
}