using System.Security.Claims;
using MediatR;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Maintenance.Commands;

public record CreateMaintenanceCommand(MaintenanceCreateDto Dto) : IRequest<int>;

public class CreateMaintenanceCommandHandler : IRequestHandler<CreateMaintenanceCommand, int>
{
    private readonly AppDbContext _context;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CreateMaintenanceCommandHandler(AppDbContext context, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<int> Handle(CreateMaintenanceCommand request,
        CancellationToken cancellationToken)
    {
        var userIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userIdClaim is null || !Guid.TryParse(userIdClaim.Value, out var userId))
        {
            throw new Exception("User ID is invalid");
        }
        var maintenance = new Domain.Entities.Maintenance
        {
            VehicleId = request.Dto.VehicleId,
            WorkshopId = request.Dto.WorkshopId,
            MaintenanceDate = request.Dto.MaintenanceDate,
            UserId = userId,
        };
        
        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync(cancellationToken);
        return maintenance.Id;
    }
}