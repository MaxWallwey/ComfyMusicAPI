using ComfyMusic.Models;
using ComfyMusic.Services;
using Microsoft.AspNetCore.Mvc;

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
    public async Task<ActionResult<Song>> Get(int id)
    {
        var song = await _songService.Get(id);

        if (song == null)
        {
            return NotFound();
        }
            
        return song;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Song song)
    {
        await _songService.Add(song);
        return CreatedAtAction(nameof(Get), new { id = song.Id }, song);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Song song)
    {
        if (id != song.Id)
        {
            return BadRequest();
        }
           
        var existingPizza = await _songService.Get(id);
        if (existingPizza is null)
        {
            return NotFound();
        }
   
        await _songService.Update(song);           
   
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
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