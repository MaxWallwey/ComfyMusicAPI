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
    public ActionResult<List<Song>> GetAll()
    {
        return _songService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Song> Get(int id)
    {
        var song = _songService.Get(id);

        if (song == null)
        {
            return NotFound();
        }
            
        return song;
    }

    [HttpPost]
    public IActionResult Create(Song song)
    {
        _songService.Add(song);
        return CreatedAtAction(nameof(Get), new { id = song.Id }, song);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Song song)
    {
        if (id != song.Id)
        {
            return BadRequest();
        }
           
        var existingPizza = _songService.Get(id);
        if (existingPizza is null)
        {
            return NotFound();
        }
   
        _songService.Update(song);           
   
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var song = _songService.Get(id);

        if (song is null)
        {
            return NotFound();
        }
        
        _songService.Delete(id);

        return NoContent();
    }
}