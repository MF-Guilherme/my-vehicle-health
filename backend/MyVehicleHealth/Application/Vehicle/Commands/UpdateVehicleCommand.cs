using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Vehicle.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Vehicle.Commands;

public record UpdateVehicleCommand(int Id, VehicleUpdateDto Dto) : IRequest<int>;

public class UpdateVehicleCommandHandler : IRequestHandler<UpdateVehicleCommand, int>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateVehicleCommandHandler(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> Handle(UpdateVehicleCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new Exception("User ID is invalid");
        }

        var vehicle = await _context.Vehicles
            .FirstOrDefaultAsync(v => v.Id == request.Id && v.UserId == userId, cancellationToken);

        if (vehicle is null)
        {
            throw new Exception("Veículo não encontrado ou não pertence ao usuário");
        }

        vehicle.Name = request.Dto.Name;
        await _context.SaveChangesAsync(cancellationToken);

        return vehicle.Id;
    }
}