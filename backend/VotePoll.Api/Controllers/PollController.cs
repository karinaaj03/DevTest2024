using System.ComponentModel.DataAnnotations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using VotePoll.Application.Commands.Command;
using VotePoll.Application.Dtos;
using VotePoll.Application.Queries.Query;
using VotePoll.Domain.Entities.Concretes;

namespace VotePoll.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PollController : ControllerBase
{
    private readonly IMediator _mediator;

    public PollController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<IActionResult> CreatePoll([FromBody] CreatePollCommand request)
    {
        var command = new CreatePollCommand(request.Name, request.Options);
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllPolls()
    {
        var query = new GetPollsQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }
    
    [HttpPost("{id}/votes")]
    public async Task<IActionResult> Vote(Guid id, [FromBody] VoteRequest request)
    {
        try 
        {
            var command = new VoteCommand 
            { 
                PollId = id,
                OptionId = request.OptionId,
                EmailVoter = request.EmailVoter
            };
        
            var result = await _mediator.Send(command);
        
            return Ok(new
            {
                id = result,
                pollId = id,
                optionId = request.OptionId,
                voterEmail = request.EmailVoter
            });
        }
        catch (ValidationException ex)
        {
            return BadRequest(new 
            { 
                message = "Unable to submit the vote.",
                details = ex.Message
            });
        }
        catch (Exception)
        {
            return StatusCode(500, new 
            { 
                message = "Unable to submit the vote.",
                details = "An unexpected error occurred while processing your vote."
            });
        }
    }

}