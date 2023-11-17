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

    // TODO: Read about dependency injection
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
        var song = await _songService.Get(ObjectId.Parse(id));

        if (song == null)
        {
            return NotFound();
        }
            
        return song;
    }

    [HttpPost]
    public async Task<ActionResult<Song>> Create(CreateSong song)
    {
        var newSong = new Song
        {
            Id = ObjectId.GenerateNewId(),
            Name = song.Name,
            Album = song.Album,
            Artist = song.Artist,
        };
        
        await _songService.Add(newSong);
        return newSong;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, Song song)
    {
        if (ObjectId.Parse(id) != song.Id)
        {
            return BadRequest();
        }
           
        var existingSong = await _songService.Get(ObjectId.Parse(id));
        if (existingSong is null)
        {
            return NotFound();
        }
   
        await _songService.Update(song);           
   
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        var song = await _songService.Get(ObjectId.Parse(id));

        if (song is null)
        {
            return NotFound();
        }
        
        await _songService.Delete(ObjectId.Parse(id));

        return NoContent();
    }
}