using ComfyMusic.Models;
using ComfyMusic.Services;
using Microsoft.AspNetCore.Mvc;

namespace ComfyMusic.Controllers;

[ApiController]
[Route("[controller]")]
public class SongsController : ControllerBase
{
    [HttpGet]
    public ActionResult<List<Song>> GetAll()
    {
        return SongService.GetAll();
    }

    [HttpGet("{id}")]
    public ActionResult<Song> Get(int id)
    {
        var song = SongService.Get(id);

        if (song == null)
        {
            return NotFound();
        }
            
        return song;
    }

    [HttpPost]
    public IActionResult Create(Song song)
    {
        SongService.Add(song);
        return CreatedAtAction(nameof(Get), new { id = song.Id }, song);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Song song)
    {
        if (id != song.Id)
        {
            return BadRequest();
        }
           
        var existingPizza = SongService.Get(id);
        if (existingPizza is null)
        {
            return NotFound();
        }
   
        SongService.Update(song);           
   
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        var song = SongService.Get(id);

        if (song is null)
        {
            return NotFound();
        }
        
        SongService.Delete(id);

        return NoContent();
    }
}