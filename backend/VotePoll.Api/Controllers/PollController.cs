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
}