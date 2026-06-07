using Mapster;
using SurveyBaskt.Contracts.Requests;
using SurveyBaskt.Contracts.Responses;
using SurveyBaskt.Mapping;

namespace SurveyBaskt.Controllers;


[Route("api/[controller]")]
[ApiController]
    
public class PollsController(IPollService _pollService) : ControllerBase    
{
    

    [HttpGet("")]
    public IActionResult GetAll()
    {
       var polls = _pollService.GetAll();

        var data = polls.Adapt<IEnumerable<PollResponse>>();

        return Ok(data);
    }

    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {

        Poll? poll = _pollService.Get(id);

        if (poll is null) return NotFound($"not found poll have id = {id}");

        //var confg = new TypeAdapterConfig();

        //confg.NewConfig<Poll,PollResponse>()
        //     .Map(dist=>dist.Name , src=>src.Title);
       

        var data = poll.Adapt<PollResponse>();
        
        return    Ok(data);
    }

    [HttpPost("")]
    public IActionResult Add([FromBody] CreatePollRequest request)
    {  
      var newpoll =  _pollService.Add(request.Adapt<Poll>());   

      return CreatedAtAction(nameof(Get), new { id = newpoll.Id }, newpoll.Adapt<PollResponse>());
    }

    [HttpPut("{id}")]
    public IActionResult Update( int id ,[FromBody]CreatePollRequest request)
    {
        var updatepoll = _pollService.Update(id, request.Adapt<Poll>());
        
        if(!updatepoll) return NotFound();

        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      var deletepoll =  _pollService.Delete(id);
        if(!deletepoll) return NotFound();
        return NoContent();
    }

    



}

