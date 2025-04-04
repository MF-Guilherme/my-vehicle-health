using Microsoft.AspNetCore.Mvc;
using MediatR;
using MyVehicleHealth.Application.Shared.Commands;
using MyVehicleHealth.Application.Shared.Dtos;

namespace MyVehicleHealth.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await _mediator.Send(new RegisterCommand(registerDto));
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto loginDto)
    {
        var result = await _mediator.Send(new LoginCommand(loginDto));
        return Ok(result);
    }
}