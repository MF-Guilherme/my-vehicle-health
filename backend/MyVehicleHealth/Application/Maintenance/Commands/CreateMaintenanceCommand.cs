using MediatR;
using MyVehicleHealth.Application.Maintenance.Dtos;
using MyVehicleHealth.Domain.Entities;
using MyVehicleHealth.Infrastructure.Data;
using System.Threading;
using System.Threading.Tasks;


namespace MyVehicleHealth.Application.Maintenance.Commands;

public record CreateMaintenanceCommand(MaintenanceCreateDto Dto) : IRequest<Domain.Entities.Maintenance>;

public class CreateMaintenanceCommandHandler : IRequestHandler<CreateMaintenanceCommand, Domain.Entities.Maintenance>
{
    private readonly AppDbContext _context;

    public CreateMaintenanceCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Domain.Entities.Maintenance> Handle(CreateMaintenanceCommand request,
        CancellationToken cancellationToken)
    {
        var maintenance = new Domain.Entities.Maintenance
        {
            VehicleId = request.Dto.VehicleId,
            WorkshopId = request.Dto.WorkshopId,
            MaintenanceDate = request.Dto.MaintenanceDate,
        };
        
        _context.Maintenances.Add(maintenance);
        await _context.SaveChangesAsync(cancellationToken);
        return maintenance;
    }
}