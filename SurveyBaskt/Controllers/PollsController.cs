
namespace SurveyBaskt.Controllers;


[Route("api/[controller]")]
[ApiController]
    
public class PollsController(IPollService _pollService) : ControllerBase    
{
    

    [HttpGet("")]
    public IActionResult GetAll()
    {
       var polls = _pollService.GetAll();

       return Ok(polls);
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        Poll? poll = _pollService.Get(id);
        
        return poll is null ? NotFound() : Ok(poll);
    }

    [HttpPost("")]
    public IActionResult Add(Poll poll)
    {  
      var newpoll =  _pollService.Add(poll);   

      return CreatedAtAction(nameof(Get), new { id = newpoll.Id }, newpoll);
    }

    [HttpPut("")]
    public IActionResult Update(Poll requst)
    {
        var updatepoll = _pollService.Update(requst);
        
        if(!updatepoll) return NotFound();

        return NoContent();
    }
    [HttpDelete("{id:int}")]
    public IActionResult Delete(int id)
    {
      var deletepoll =  _pollService.Delete(id);
        if(!deletepoll) return NotFound();
        return NoContent();
    }





}

