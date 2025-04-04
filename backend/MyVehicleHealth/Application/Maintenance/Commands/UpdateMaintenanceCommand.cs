using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Maintenance.Commands;

public record UpdateMaintenanceCommand(int Id, MaintenanceUpdateDto Dto) : IRequest<int>;

public class UpdateMaintenanceCommandHandler : IRequestHandler<UpdateMaintenanceCommand, int>
{
    private readonly AppDbContext _context;

    public UpdateMaintenanceCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(UpdateMaintenanceCommand request,
        CancellationToken cancellationToken)
    {
        var maintenance = await _context.Maintenances
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        if (maintenance is null)
        {
            throw new Exception("Manutenção não encontrada");
        }
        maintenance.VehicleId = request.Dto.VehicleId;
        maintenance.WorkshopId = request.Dto.WorkshopId;
        maintenance.MaintenanceDate = request.Dto.MaintenanceDate;
        await _context.SaveChangesAsync(cancellationToken);
        return maintenance.Id;
    }
    
}