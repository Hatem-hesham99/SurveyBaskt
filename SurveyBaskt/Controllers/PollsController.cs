using Microsoft.AspNetCore.Authorization;

namespace SurveyBaskt.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]

public class PollsController(IPollService _pollService) : ControllerBase
{
    [HttpGet("")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var polls = await _pollService.GetAllAsync(cancellationToken);
        return Ok(polls);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] int id, CancellationToken cancellationToken)
    {
        var poll = await _pollService.GetAsync(id, cancellationToken);
        return poll == null ? NotFound() : Ok(poll);
    }

    [HttpPost("")]
    public async Task<IActionResult> Add([FromBody] PollRequest request, CancellationToken cancellationToken)
    {
        var newPoll = await _pollService.AddAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = newPoll.Id }, newPoll);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id , PollRequest createPollRequest , CancellationToken cancellationToken )
    {
        var result = await _pollService.UpdateAsync(id, createPollRequest , cancellationToken);

        return result  ? NoContent() : NotFound();

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id , CancellationToken cancellationToken)
    {
        var result = await _pollService.DeleteAsync(id, cancellationToken);
        return result ? NoContent() : NotFound();
    }
    [HttpPut("TogelPublish/{id}")]
    public async Task<IActionResult> TogelPublishStatus([FromRoute] int id, CancellationToken cancellationToken)
    {
        var result = await _pollService.TogelPublish(id, cancellationToken);
        return result ? NoContent() : NotFound();
    }
}



















#region In-Memory Data Store (Commented Out)
//[HttpGet("")]
//public IActionResult GetAll()
//{
//   var polls = _pollService.GetAll();

//    var data = polls.Adapt<IEnumerable<PollResponse>>();

//    return Ok(data);
//}

//[HttpGet("{id}")]
//public IActionResult Get(int id)
//{

//    Poll? poll = _pollService.Get(id);

//    if (poll is null) return NotFound($"not found poll have id = {id}");

//    //var confg = new TypeAdapterConfig();

//    //confg.NewConfig<Poll,PollResponse>()
//    //     .Map(dist=>dist.Name , src=>src.Title);


//    var data = poll.Adapt<PollResponse>();

//    return    Ok(data);
//}

//[HttpPost("")]
//public IActionResult Add([FromBody] CreatePollRequest request)
//{  
//  var newpoll =  _pollService.Add(request.Adapt<Poll>());   

//  return CreatedAtAction(nameof(Get), new { id = newpoll.Id }, newpoll.Adapt<PollResponse>());
//}

//[HttpPut("{id}")]
//public IActionResult Update( int id ,[FromBody]CreatePollRequest request)
//{
//    var updatepoll = _pollService.Update(id, request.Adapt<Poll>());

//    if(!updatepoll) return NotFound();

//    return NoContent();
//}
//[HttpDelete("{id}")]
//public IActionResult Delete(int id)
//{
//  var deletepoll =  _pollService.Delete(id);
//    if(!deletepoll) return NotFound();
//    return NoContent();
//}
#endregion






