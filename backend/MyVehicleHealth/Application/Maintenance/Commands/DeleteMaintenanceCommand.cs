using MediatR;
using Microsoft.EntityFrameworkCore;
using MyVehicleHealth.Infrastructure.Data;

namespace MyVehicleHealth.Application.Maintenance.Commands;

public record DeleteMaintenanceCommand(int Id) : IRequest<Unit>;

public class DeleteMaintenanceCommandHandler : IRequestHandler<DeleteMaintenanceCommand, Unit>
{
    private readonly AppDbContext _context;

    public DeleteMaintenanceCommandHandler(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteMaintenanceCommand request, CancellationToken cancellationToken)
    {
        var maintenance = await _context.Maintenances
            .FirstOrDefaultAsync(v => v.Id == request.Id, cancellationToken);

        if (maintenance is null) return Unit.Value;
        
        _context.Maintenances.Remove(maintenance);
        await _context.SaveChangesAsync(cancellationToken);
        
        return Unit.Value;
    }
    
}