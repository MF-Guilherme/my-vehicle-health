using System.Security.Claims;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Maintenance.Commands;

public record UpdateMaintenanceCommand(int Id, MaintenanceUpdateDto Dto) : IRequest<int>;

public class UpdateMaintenanceCommandHandler : IRequestHandler<UpdateMaintenanceCommand, int>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UpdateMaintenanceCommandHandler(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> Handle(UpdateMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new Exception("User ID is invalid");
        }

        var maintenance = await _context.Maintenances
            .FirstOrDefaultAsync(m => m.Id == request.Id && m.UserId == userId, cancellationToken);

        if (maintenance is null)
        {
            throw new Exception("Manutenção não encontrada ou não pertence ao usuário");
        }

        maintenance.VehicleId = request.Dto.VehicleId;
        maintenance.WorkshopId = request.Dto.WorkshopId;
        maintenance.MaintenanceDate = request.Dto.MaintenanceDate;
        await _context.SaveChangesAsync(cancellationToken);

        return maintenance.Id;
    }
}